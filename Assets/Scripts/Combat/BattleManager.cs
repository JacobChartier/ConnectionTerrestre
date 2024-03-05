using Assets.Scripts.Combat;
using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class BattleManager : MonoBehaviour
{
    public enum Evenement
    {
        INTRO,
        TOUR_JOUEUR,
        TOUR_ENNEMI,
        ITEMS,
        FUIR,
        ANIMATION_JOUEUR,
        VICTOIRE_JOUEUR,
        DEFAITE_JOUEUR
    }

    const int FRAMES_AVANT_ATTAQUE = 80;

    [SerializeField] GameObject prefab_text_dommages;
    [Space]
    [SerializeField] CinemachineVirtualCamera camera_option_joueur;
    [SerializeField] CinemachineVirtualCamera camera_generale;
    [SerializeField] CinemachineVirtualCamera camera_joueur_fuit;
    [SerializeField] CinemachineVirtualCamera camera_joueur_mort;
    [Space]
    [SerializeField] choixcombatmanager choix_combat;
    [Space]
    [SerializeField] EntityStats stats_joueur;
    [SerializeField] EntityStats stats_monstre;
    [Space]
    [SerializeField] Transform joueur_transform;
    [SerializeField] Transform ennemi_transform;
    [Space]
    [SerializeField] selectormanager selector;
    [SerializeField] bhvFlecheSelection curseur;

    public static PlayersControls controls;
    public static bool left_pressed = false;
    public static bool right_pressed = false;
    public static bool left_held = false;
    public static bool right_held = false;
    public static bool select_pressed = false;
    public static bool up_pressed = false;
    public static bool down_pressed = false;
    public static bool down_held = false;
    public static bool up_held = false;

    Evenement evenement_actuel
    {
        set
        {
            timer = 0;
            evenement = value;
        }
        get
        {
            return evenement;
        }
    }
    private Evenement evenement;
    private InfoAttaque attaque_monstre;
    private InfoAttaque attaque_joueur;

    int timer = 0;
    bool fuir_fail = false;
    bool stunlock = false;
    bool selection = false;
    public bool essence_guerrier = false;
    public bool essence_magicien = false;
    public bool bouclier_temporaire = false;
    private bool DEBUG_PLAYER_ALWAYS_GOES_FIRST = true; // debug!!

    private void Awake()
    {
        controls = new PlayersControls();
        controls.Player.Enable();

        stats_monstre.Attaques.Add(new InfoAttaque("test", 30, 15, 100, 10)); // debug
    }

    private void OnEnable()
    {
        controls.Player.selectiongauche.performed += ctx => left_pressed = left_held = ctx.ReadValueAsButton();
        controls.Player.selectiongauche.canceled += ctx => left_pressed = left_held = ctx.ReadValueAsButton();

        controls.Player.selectiondroite.performed += ctx => right_pressed = right_held = ctx.ReadValueAsButton();
        controls.Player.selectiondroite.canceled += ctx => right_pressed = right_held = ctx.ReadValueAsButton();

        controls.Player.selectionenter.performed += ctx => select_pressed = ctx.ReadValueAsButton();
        controls.Player.selectionenter.canceled += ctx => select_pressed = ctx.ReadValueAsButton();

        controls.Player.selectionhaut.performed += ctx => up_pressed = up_held = ctx.ReadValueAsButton();
        controls.Player.selectionhaut.canceled += ctx => up_pressed = up_held = ctx.ReadValueAsButton();

        controls.Player.selectionbas.performed += ctx => down_pressed = down_held = ctx.ReadValueAsButton();
        controls.Player.selectionbas.canceled += ctx => down_pressed = down_held = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        evenement_actuel = Evenement.INTRO;
        camera_generale.Priority = 1;
        camera_option_joueur.Priority = 0;
        choix_combat.Active = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (evenement_actuel)
        {
            case Evenement.INTRO:
                CodeIntro();
                break;

            case Evenement.TOUR_JOUEUR:
                CodeTourJoueur();
                break;

            case Evenement.ANIMATION_JOUEUR:
                CodeAnimJoueur();
                break;

            case Evenement.FUIR:
                CodeFuir();
                break;

            case Evenement.TOUR_ENNEMI:
                CodeTourEnnemi();
                break;

            case Evenement.VICTOIRE_JOUEUR:
                break;

            case Evenement.DEFAITE_JOUEUR:
                break;
        }

        timer++;
    }

    private void CodeTourEnnemi()
    {
        if (timer <= 1)
        {
            camera_joueur_fuit.Priority = 0;
            camera_generale.Priority = 1;
            camera_option_joueur.Priority = 0;
            choix_combat.Active = false;
        }

        if (timer == FRAMES_AVANT_ATTAQUE)
        {
            attaque_monstre = stats_monstre.Attaques[UnityEngine.Random.Range(0, stats_monstre.Attaques.Count - 1)];
            //donner anim à objet monstre
        }

        if (timer > attaque_monstre.anim_len + FRAMES_AVANT_ATTAQUE) //todo: monstre choisit attaque + anim attaque + bloc/parry
        {
            stunlock = false;
            evenement_actuel = Evenement.TOUR_JOUEUR;
        }

        if (select_pressed && !stunlock)
        {
            if (timer > attaque_monstre.parry_frame + FRAMES_AVANT_ATTAQUE)
            {
                stunlock = true;
                // todo (peut etre) animation stumble joueur
            }
            else if (timer == attaque_monstre.parry_frame + FRAMES_AVANT_ATTAQUE)
            {
                // mettre l'anim joueur à attaque
                float dommages = (attaque_monstre.damage + stats_monstre.Strength.Current - stats_monstre.Defense.Current) / 2.0f;

                FaireDegats(dommages, false);
            }
            else if (timer > attaque_monstre.parry_frame + FRAMES_AVANT_ATTAQUE - attaque_monstre.block_window)
            {
                // mettre l'anim monstre à knockback
                // mettre l'anim joueur à block
                float dommages = (attaque_monstre.damage + stats_monstre.Strength.Current - stats_joueur.Defense.Current) / 2.0f;

                FaireDegats(dommages, true);
            }
        }
        else if (timer == attaque_monstre.parry_frame + FRAMES_AVANT_ATTAQUE)
        {
            float dommages = attaque_monstre.damage + stats_monstre.Strength.Current - stats_joueur.Defense.Current;

            FaireDegats(dommages, true);
        }
    }

    private void CodeAnimJoueur()
    {
        if (timer <= 1)
        {
            camera_joueur_fuit.Priority = 0;
            camera_generale.Priority = 1;
            camera_option_joueur.Priority = 0;
            choix_combat.Active = false;
        }

        if (timer == attaque_joueur.parry_frame)
        {
            float dommages = attaque_joueur.damage;
            if (select_pressed)
                dommages *= 1.5f;

            FaireDegats(dommages, false);
        }

        if (timer > attaque_joueur.anim_len + FRAMES_AVANT_ATTAQUE)
        {
            stunlock = false;
            evenement_actuel = Evenement.TOUR_ENNEMI;
        }
    }

    private void FaireDegats(float dommages, bool monstre_est_attaqueur)
    {
        EntityStats attaqueur;
        EntityStats defendant;
        Vector3 position_defendant;

        if (monstre_est_attaqueur)
        {
            attaqueur = stats_monstre;
            defendant = stats_joueur;
            position_defendant = joueur_transform.position;
        }
        else
        {
            attaqueur = stats_joueur;
            defendant = stats_monstre;
            position_defendant = ennemi_transform.position;
        }

        dommages = MathF.Round(dommages, MidpointRounding.AwayFromZero);

        bhvdamagetextprefab damage_text = Instantiate(prefab_text_dommages, GameObject.Find("Canvas").GetComponent<Transform>()).GetComponent<bhvdamagetextprefab>();
        damage_text.Init(dommages, position_defendant);

        if (defendant.Health.Remove(dommages))
        {
            if (monstre_est_attaqueur)
            {
                // todo: feuille vitale
                if (false)
                {
                    return;
                }

                evenement_actuel = Evenement.DEFAITE_JOUEUR;
            }

            evenement_actuel = Evenement.VICTOIRE_JOUEUR;
        }
    }

    private void CodeTourJoueur()
    {
        if (selection)
        {
            CodeChoixAtkItm();
            return;
        }

        if (timer <= 1)
        {
            camera_generale.Priority = 0;
            camera_joueur_fuit.Priority = 0;
            camera_option_joueur.Priority = 1;
            choix_combat.Active = true;
        }

        // assure que les choix du joueur sont visibles avant qu'il ne peux en choisir
        if (timer < 30)
            return;
        // todo: qqc de meilleur comme code
        switch (choix_combat.CheckRotate())
        {
            case 0:
                selection = true;
                curseur.SetActive(true);
                selector.Startup(selectormanager.StartupType.PHYSIQUE);
                break;
            case 1:
                selection = true;
                curseur.SetActive(true);
                selector.Startup(selectormanager.StartupType.MAGIQUE);
                break;
            case 2:
                selection = true;
                curseur.SetActive(true);
                selector.Startup(selectormanager.StartupType.ITEM);
                break;
            case 3:
                fuir_fail = false;
                evenement_actuel = Evenement.FUIR;
                break;

            default:
                break;
        }
    }

    private void CodeChoixAtkItm()
    {
        if (left_pressed)
        {
            selection = false;
            selector.Close();
            timer = 15;
            curseur.SetActive(false);
        }

        if (up_pressed)
        {
            curseur.BougerHaut();
        }

        if (down_pressed)
        {
            curseur.BougerBas();
        }

        if (select_pressed)
        {
            attaque_joueur = stats_joueur.Attaques[curseur.GetChoixSelection()];
            selection = false;
            selector.Close();
            curseur.SetActive(false);
            evenement_actuel = Evenement.ANIMATION_JOUEUR;
        }
    }

    private void CodeIntro()
    {
        if (timer >= 200)
        {
            if (stats_joueur.AttackSpeed.Current > stats_monstre.AttackSpeed.Current ||
                DEBUG_PLAYER_ALWAYS_GOES_FIRST ||
                stats_joueur.AttackSpeed.Current == stats_monstre.AttackSpeed.Current && UnityEngine.Random.Range(0, 2) == 0) // si vitesses égales, premier tour = hasard
            {
                evenement_actuel = Evenement.TOUR_JOUEUR;
            }
            else
            {
                evenement_actuel = Evenement.TOUR_ENNEMI;
            }
        }

        if (select_pressed)
        {
            select_pressed = false;
            timer = 200; // le joueur peut skip le cutscene avec J
        }
    }

    private void CodeFuir()
    {
        const int NB_FRAMES_JOUEUR_TOURNE = 30;
        const int NB_FRAMES_AVANT_JOUEUR_TOURNE = 60;
        const int NB_FRAMES_JOUEUR_COURT = 180;
        const float VITESSE_JOUEUR_FUITE = 0.2f;
        const int POURCENTAGE_CHANCE_FUITE = 80; // !!! ne pas mettre à 100, sinon erreure division par zéro

        if (timer <= 1)
        {
            choix_combat.Active = false;
            camera_generale.Priority = 0;
            camera_option_joueur.Priority = 0;
            camera_joueur_fuit.Priority = 1;
        }

        if (fuir_fail)
        {
            // todo: ajouter animation fail au modèle joueur et le faire

            if (timer < 60) // mettre animation ici. tomber à terre peut-être??
                return;
            else
            {
                Vector3 j_r = joueur_transform.rotation.eulerAngles;
                j_r += new Vector3(0, 180 / (NB_FRAMES_JOUEUR_TOURNE / 2), 0);
                joueur_transform.rotation = Quaternion.Euler(j_r);
            }

            if (timer >= NB_FRAMES_JOUEUR_TOURNE / 2)
            {
                Vector3 j_r = joueur_transform.rotation.eulerAngles;
                j_r = new Vector3(0, 0, 0); // BUG POTENTIEL: chépa c'est quoi la direction du joueur en ce momment car il est
                                            // un cylindre, si le modèle du joueur pointe dans la mauvaise direction quand ajouté, ceci
                                            // est pourquoi.
                joueur_transform.rotation = Quaternion.Euler(j_r);
                evenement_actuel = Evenement.TOUR_ENNEMI;
                return;
            }

            return;
        }

        if (timer == NB_FRAMES_JOUEUR_COURT + NB_FRAMES_AVANT_JOUEUR_TOURNE + NB_FRAMES_JOUEUR_TOURNE)
        {
            SceneManager.LoadScene("World");
        }
        else if (timer > NB_FRAMES_AVANT_JOUEUR_TOURNE + NB_FRAMES_JOUEUR_TOURNE)
        {
            joueur_transform.position += new Vector3(VITESSE_JOUEUR_FUITE, 0, 0);
        }
        else if (timer > NB_FRAMES_AVANT_JOUEUR_TOURNE)
        {
            Vector3 j_r = joueur_transform.rotation.eulerAngles;
            j_r += new Vector3(0, 180 / NB_FRAMES_JOUEUR_TOURNE, 0);
            joueur_transform.rotation = Quaternion.Euler(j_r);

            if (timer == NB_FRAMES_AVANT_JOUEUR_TOURNE + NB_FRAMES_JOUEUR_TOURNE)
            {
                // le joueur a 80% de chance de pouvoir fuire
                fuir_fail = UnityEngine.Random.Range(0, 100 / (100 - POURCENTAGE_CHANCE_FUITE)) == 0;

                if (fuir_fail)
                    timer = 2;
            }
        }
    }

    private void CodeDefaiteJoueur()
    {
        if (timer <= 1)
        {
            choix_combat.Active = false;
            camera_generale.Priority = 0;
            camera_option_joueur.Priority = 0;
            camera_joueur_fuit.Priority = 0;
            camera_joueur_mort.Priority = 1;
        }
    }
}
