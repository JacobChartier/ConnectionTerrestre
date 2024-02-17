using Assets.Scripts.Combat;
using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] CinemachineVirtualCamera camera_option_joueur;
    [SerializeField] CinemachineVirtualCamera camera_generale;
    [SerializeField] CinemachineVirtualCamera camera_joueur_fuit;
    [SerializeField] choixcombatmanager choix_combat;
    [SerializeField] EntityStats stats_joueur;
    [SerializeField] Transform joueur_transform;
    [SerializeField] EntityStats stats_ennemi;
    [SerializeField] Image image_liste_atk;

    public static PlayersControls controls;
    public static bool left_pressed = false;
    public static bool right_pressed = false;
    public static bool left_held = false;
    public static bool right_held = false;
    public static bool select_pressed = false;
    public static bool up_pressed = false;
    public static bool down_pressed = false;

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
    private InfoAttaqueMonstre attaque_monstre;
    private InfosMonstre monstre;

    int timer = 0;
    bool fuir_fail = false;
    bool stunlock = false;
    bool selection_atk = false;
    bool selection_magie = false;
    public bool essence_guerrier = false;
    public bool essence_magicien = false;
    public bool bouclier_temporaire = false;
    private bool DEBUG_PLAYER_ALWAYS_GOES_FIRST = true; // debug!!

    private void Awake()
    {
        controls = new PlayersControls();
        controls.Player.Enable();
    }

    private void OnEnable()
    {
        controls.Player.selectiongauche.performed += ctx => left_pressed = left_held = ctx.ReadValueAsButton();
        controls.Player.selectiongauche.canceled += ctx => left_pressed = left_held = ctx.ReadValueAsButton();

        controls.Player.selectiondroite.performed += ctx => right_pressed = right_held = ctx.ReadValueAsButton();
        controls.Player.selectiondroite.canceled += ctx => right_pressed = right_held = ctx.ReadValueAsButton();

        controls.Player.selectionenter.performed += ctx => select_pressed = ctx.ReadValueAsButton();
        controls.Player.selectionenter.canceled += ctx => select_pressed = ctx.ReadValueAsButton();

        controls.Player.selectionhaut.performed += ctx => up_pressed = ctx.ReadValueAsButton();
        controls.Player.selectionhaut.canceled += ctx => up_pressed = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        evenement_actuel = Evenement.INTRO;
        camera_generale.Priority = 1;
        camera_option_joueur.Priority = 0;
        choix_combat.Active = false;
        monstre = GameObject.Find("Ennemi").GetComponent<InfosMonstre>();
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

            case Evenement.FUIR:
                CodeFuir();
                break;

            case Evenement.TOUR_ENNEMI:
                CodeTourEnnemi();
                break;
        }

        timer++;
    }

    private void CodeTourEnnemi()
    {
        const int FRAMES_AVANT_MONSTRE_ATTAQUE = 80;
        if (timer <= 1)
        {
            camera_joueur_fuit.Priority = 0;
            camera_generale.Priority = 1;
            camera_option_joueur.Priority = 0;
            choix_combat.Active = false;
        }

        if (timer == FRAMES_AVANT_MONSTRE_ATTAQUE)
        {
            attaque_monstre = monstre.attaques[UnityEngine.Random.Range(0, monstre.attaques.Length)];
            //donner anim � objet monstre
        }

        if (timer > attaque_monstre.anim_len + FRAMES_AVANT_MONSTRE_ATTAQUE) //todo: monstre choisit attaque + anim attaque + bloc/parry
        {
            stunlock = false;
            evenement_actuel = Evenement.TOUR_JOUEUR;
        }

        if (select_pressed && !stunlock)
        {
            if (timer > attaque_monstre.parry_frame + FRAMES_AVANT_MONSTRE_ATTAQUE)
            {
                stunlock = true;
                // todo (peut etre) animation stumble joueur
            }
            else if (timer == attaque_monstre.parry_frame + FRAMES_AVANT_MONSTRE_ATTAQUE)
            {
                // mettre l'anim joueur � attaque
                float dommages = (attaque_monstre.damage + stats_ennemi.Strength.Current - stats_ennemi.Defense.Current) / 2.0f;
                stats_ennemi.Health.Remove(MathF.Round(dommages, MidpointRounding.AwayFromZero));
            }
            else if (timer > attaque_monstre.parry_frame + FRAMES_AVANT_MONSTRE_ATTAQUE - attaque_monstre.block_window)
            {
                // mettre l'anim monstre � knockback
                // mettre l'anim joueur � block
                float dommages = (attaque_monstre.damage + stats_ennemi.Strength.Current - stats_joueur.Defense.Current) / 2.0f;
                stats_joueur.Health.Remove(dommages);
            }
        }
        else if (timer == attaque_monstre.parry_frame + FRAMES_AVANT_MONSTRE_ATTAQUE)
        {
            float dommages = attaque_monstre.damage + stats_ennemi.Strength.Current - stats_joueur.Defense.Current;

            if (stats_joueur.Health.Remove(dommages))
            {
                // utiliser feuille vitale si en a une
            }
        }
    }

    private void CodeTourJoueur()
    {
        if (selection_atk)
        {
            CodeChoixAtk();
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

        switch (choix_combat.CheckRotate())
        {
            case 0:
                selection_atk = true;
                image_liste_atk.color = Color.red;
                break;
            case 1:
                selection_magie = true;
                image_liste_atk.color = Color.yellow;
                break;
            case 2:
                Debug.Log("itm");
                break;
            case 3:
                fuir_fail = false;
                evenement_actuel = Evenement.FUIR;
                break;

            default:
                break;
        }
    }

    private void CodeChoixAtk()
    {

    }

    private void CodeIntro()
    {
        if (timer >= 200)
        {
            if (stats_joueur.AttackSpeed.Current > stats_ennemi.AttackSpeed.Current ||
                DEBUG_PLAYER_ALWAYS_GOES_FIRST ||
                stats_joueur.AttackSpeed.Current == stats_ennemi.AttackSpeed.Current && UnityEngine.Random.Range(0, 2) == 0) // si vitesses �gales, premier tour = hasard
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
        const int POURCENTAGE_CHANCE_FUITE = 80; // !!! ne pas mettre � 100, sinon erreure division par z�ro

        if (timer <= 1)
        {
            choix_combat.Active = false;
            camera_generale.Priority = 0;
            camera_option_joueur.Priority = 0;
            camera_joueur_fuit.Priority = 1;
        }

        if (fuir_fail)
        {
            // todo: ajouter animation fail au mod�le joueur et le faire

            if (timer < 60) // mettre animation ici. tomber � terre peut-�tre??
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
                j_r = new Vector3(0, 0, 0); // BUG POTENTIEL: ch�pa c'est quoi la direction du joueur en ce momment car il est
                                            // un cylindre, si le mod�le du joueur pointe dans la mauvaise direction quand ajout�, ceci
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
}
