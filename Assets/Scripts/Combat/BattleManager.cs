using Assets.Scripts.Combat;
using Cinemachine;
using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
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

    const int FRAMES_AVANT_ATTAQUE = 80;
    const float LEVEL_UP_SPEED = 1.5f;

    [SerializeField] GameObject prefab_text_dommages;
    [SerializeField] GameObject prefab_text_xp;
    [Space]
    [SerializeField] CinemachineVirtualCamera camera_option_joueur;
    [SerializeField] CinemachineVirtualCamera camera_generale;
    [SerializeField] CinemachineVirtualCamera camera_joueur_fuit;
    [SerializeField] CinemachineVirtualCamera camera_joueur_mort;
    [Space]
    [SerializeField] choixcombatmanager choix_combat;
    [Space]
    [SerializeField] Transform joueur_transform;
    [SerializeField] Transform ennemi_transform;
    [Space]
    [SerializeField] selectormanager selector;
    [SerializeField] bhvFlecheSelection curseur;
    [SerializeField] Image image_defaite;

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
    private Inventory inventory;
    private bool DEBUG_PLAYER_ALWAYS_GOES_FIRST = false;
    private bool DEBUG_ENEMY_1HP = false;

    private void Awake()
    {
        if (BattleInfo.player == null)
        {
            Debug.LogError("battleinfo player null");
            BattleInfo.player = new EntityStats();
            BattleInfo.player.Health.Add(100);
        }

        if (BattleInfo.enemy == null)
        {
            //Debug.LogError("battleinfo enemy null");
            BattleInfo.enemy = new EntityStats();
        }

        controls = new PlayersControls();
        controls.Player.Enable();

        if (BattleInfo.player.Attaques.Count == 0)
            BattleInfo.player.Attaques.Add(new("Coup de poing", 15, 30, 100));
        if (BattleInfo.enemy.Attaques.Count == 0)
            BattleInfo.enemy.Attaques.Add(new(1, 1, 100, 10));

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
        if (BattleInfo.enemy.Attaques.Count == 0)
            BattleInfo.enemy.Attaques.Add(new(30, 15, 100, 1));
        evenement_actuel = Evenement.INTRO;
        camera_generale.Priority = 1;
        camera_option_joueur.Priority = 0;
        choix_combat.Active = false;
        if (BattleInfo.enemy.Experience < 10)
            BattleInfo.enemy.Experience = 100;

        if (DEBUG_ENEMY_1HP)
        {
            BattleInfo.enemy.enemyType = EnemyType.BOSS4;
            BattleInfo.enemy.Health.Current = 1;
        }
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
                CodeVictoireJoueur();
                break;

            case Evenement.DEFAITE_JOUEUR:
                CodeDefaiteJoueur();
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

        if (timer < FRAMES_AVANT_ATTAQUE)
            return;

        if (timer == FRAMES_AVANT_ATTAQUE)
        {
            attaque_monstre = BattleInfo.enemy.Attaques[UnityEngine.Random.Range(0, BattleInfo.enemy.Attaques.Count - 1)];
            //donner anim à objet monstre
        }

        if (timer > attaque_monstre.anim_len + FRAMES_AVANT_ATTAQUE) //todo: anim attaque
        {
            stunlock = false;
            evenement_actuel = Evenement.TOUR_JOUEUR;
        }

        if (select_pressed && !stunlock)
        {
            if (timer < attaque_monstre.contact_frame + FRAMES_AVANT_ATTAQUE)// TROP TÔT: joueur est stunned
            {
                Debug.Log("avant");
                stunlock = true;
                // todo (peut etre) animation stumble joueur
            }
            else if (timer == attaque_monstre.contact_frame + FRAMES_AVANT_ATTAQUE)// PARRY: monstre recoit moitié de dommages de son attaque
            {
                Debug.Log("pendant");
                // mettre l'anim joueur à attaque
                float dommages = (attaque_monstre.damage + BattleInfo.enemy.Strength.Current - BattleInfo.enemy.Defense.Current) / 2.0f;

                FaireDegats(dommages, false);
                stunlock = true;
            }
            else if (timer > attaque_monstre.contact_frame + FRAMES_AVANT_ATTAQUE - attaque_monstre.block_window)// BLOCK: joueur recoit moitié dommages
            {
                Debug.Log("après");
                // mettre l'anim monstre à knockback
                // mettre l'anim joueur à block
                float dommages = (attaque_monstre.damage + BattleInfo.enemy.Strength.Current - BattleInfo.player.Defense.Current) / 2.0f;

                FaireDegats(dommages, true);
                stunlock = true;
            }
        }
        else if (timer == attaque_monstre.contact_frame + FRAMES_AVANT_ATTAQUE)
        {
            float dommages = attaque_monstre.damage + BattleInfo.enemy.Strength.Current - BattleInfo.player.Defense.Current;

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

        if (timer == attaque_joueur.contact_frame + FRAMES_AVANT_ATTAQUE)
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
        Transform position_defendant;

        if (monstre_est_attaqueur)
        {
            attaqueur = BattleInfo.enemy;
            defendant = BattleInfo.player;
            position_defendant = joueur_transform;
        }
        else
        {
            attaqueur = BattleInfo.player;
            defendant = BattleInfo.enemy;
            position_defendant = ennemi_transform;
        }

        dommages = MathF.Round(dommages, MidpointRounding.AwayFromZero);

        // safety check
        if (dommages <= 0)
            dommages = 1;

        bhvdamagetextprefab damage_text = Instantiate(prefab_text_dommages, GameObject.Find("Canvas").transform).GetComponent<bhvdamagetextprefab>();
        damage_text.Init(dommages, position_defendant);

        if (!defendant.Health.Remove(dommages))
        {
            if (monstre_est_attaqueur)
            {
                // todo: feuille vitale
                if (BattleInfo.inventory.items.Find(x => x.Name == "Feuille vitale") != null)
                {
                    BattleInfo.player.Health.Current = (BattleInfo.player.Health.Max / 4);
                    BattleInfo.inventory.items.Remove(BattleInfo.inventory.items.Find(x => x.Name == "Feuille vitale"));
                    return;
                }
                evenement_actuel = Evenement.DEFAITE_JOUEUR;
                return;
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
                if (!selector.Startup(selectormanager.StartupType.PHYSIQUE))
                    break;

                selection = true;
                curseur.SetActive(true);
                break;

            case 1:
                if (!selector.Startup(selectormanager.StartupType.MAGIQUE))
                    break;

                selection = true;
                curseur.SetActive(true);
                break;

            case 2:
                if (!selector.Startup(selectormanager.StartupType.ITEM))
                    break;

                selection = true;
                curseur.SetActive(true);
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
            attaque_joueur = BattleInfo.player.Attaques[curseur.GetChoixSelection()];
            selection = false;
            selector.Close();
            curseur.SetActive(false);
            evenement_actuel = Evenement.ANIMATION_JOUEUR;
        }
    }

    //private InfoAttaque TrouverAttaque(int index, bool magique)
    //{
    //    int counter = 0;

    //    foreach (InfoAttaque i in BattleInfo.player.Attaques)
    //    {
    //        if (i.magique == magique)
    //        {
    //            if (counter == index)
    //                return i;

    //            counter++;
    //        }
    //    }

    //    return null;
    //}

    private void CodeIntro()
    {
        if (timer >= 200)
        {
            if (BattleInfo.player.AttackSpeed.Current > BattleInfo.enemy.AttackSpeed.Current ||
                DEBUG_PLAYER_ALWAYS_GOES_FIRST ||
                BattleInfo.player.AttackSpeed.Current == BattleInfo.enemy.AttackSpeed.Current && UnityEngine.Random.Range(0, 2) == 0) // si vitesses égales, premier tour = hasard
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
            timer = 200; // le joueur peut skip le cutscene avec enter
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
        const int FRAMES_DEATH_ANIM = 600;

        if (timer <= 1)
        {
            camera_generale.Priority = 0;
            camera_option_joueur.Priority = 0;
            camera_joueur_fuit.Priority = 0;
            camera_joueur_mort.Priority = 1;

            joueur_transform.Rotate(new Vector3(90, 0, 0));

            GameObject canvas = GameObject.Find("Canvas");
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                if (canvas.transform.GetChild(i).gameObject.name == "damage text(Clone)")
                    continue;

                canvas.transform.GetChild(i).gameObject.SetActive(false);
            }

            image_defaite.gameObject.SetActive(true);
            image_defaite.GetComponent<Image>().color -= new Color(0, 0, 0, 1);

        }

        if (timer > FRAMES_DEATH_ANIM / 2 && timer < FRAMES_DEATH_ANIM / 2 + 100)
        {
            image_defaite.color += new Color(0, 0, 0, 0.01f);
        }

        if (timer < FRAMES_DEATH_ANIM)
        {
            camera_joueur_mort.GetComponent<CameraJoueurMort>().Move();
        }
        else
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
    }

    private void CodeVictoireJoueur()
    {
        const int LONGUEURE_ANIM_VICTOIRE = 300;

        if (timer <= 1)
        {
            camera_generale.Priority = 0;
            camera_option_joueur.Priority = 0;
            camera_joueur_fuit.Priority = 1;

            if (BattleInfo.enemy.enemyType >= EnemyType.BOSS1)
            {
                switch (BattleInfo.enemy.enemyType)
                {
                    case EnemyType.BOSS1:
                        BattleInfo.player.Attaques.Add(new InfoAttaque("Hache solide", 20, 50, 20));
                        BattleInfo.player.Attaques.Add(new InfoAttaque("Magie Terreste", 50, 55, 25 + UnityEngine.Random.Range(-5, 5), 10));
                        break;
                    case EnemyType.BOSS2:
                        BattleInfo.player.Attaques.Add(new InfoAttaque("Épée standard", 40, 50, 25));
                        BattleInfo.player.Attaques.Add(new InfoAttaque("Boule de feu", 50, 51, 30, 30));
                        break;
                    case EnemyType.BOSS3:
                        BattleInfo.player.Attaques.Add(new InfoAttaque("Épée du magicien", 40, 42, 35));
                        BattleInfo.player.Attaques.Add(new InfoAttaque("Retour de la connection terreste", 30, 35, (int)(50 + BattleInfo.player.MagicPoint.Current / 50), 50));
                        break;
                    case EnemyType.BOSS4:
                        BattleInfo.player.Attaques.Add(new InfoAttaque("L'épée du vainqueur", 50, 70, 9999));
                        BattleInfo.player.Attaques.Add(new InfoAttaque("Le dernier recours", 100, 100, int.MaxValue, (int)(BattleInfo.player.MagicPoint.Max + 1)));
                        break;
                }

                Tour.ToursCompletes[Tour.current_tour] = true;
            }
        }

        if (timer == 100 && BattleInfo.enemy.Experience > 0)
        {
            timer--;
            BattleInfo.player.Experience++;
            BattleInfo.enemy.Experience--;
            if (BattleInfo.enemy.Coins > 0)
            {
                BattleInfo.player.Coins++;
                BattleInfo.enemy.Coins--;
            }

            bhvxptextprefab xp = Instantiate(prefab_text_xp, GameObject.Find("Canvas").transform).GetComponent<bhvxptextprefab>();
            xp.Init(false, joueur_transform);

            if (BattleInfo.player.Experience >= Mathf.Pow(LEVEL_UP_SPEED, BattleInfo.player.Niveau + 4))
            {
                Debug.Log(BattleInfo.player.Experience);
                bhvxptextprefab lvl = Instantiate(prefab_text_xp, GameObject.Find("Canvas").transform).GetComponent<bhvxptextprefab>();
                lvl.Init(true, joueur_transform);
                LevelUpStats();
            }
        }

        if (timer > LONGUEURE_ANIM_VICTOIRE)
        {
            if (BattleInfo.enemy.enemyType == EnemyType.BOSS4)
            {
                SceneManager.LoadScene("Fin du jeu");
                return;
            }

            BattleInfo.inventory = inventory;
            SceneManager.LoadScene("World");
        }
    }

    private void LevelUpStats()
    {
        const float UPGRADE_SPEED = 1.1f; // no testing whoops

        BattleInfo.player.Niveau++;
        BattleInfo.player.Health.Max = (int)(BattleInfo.player.Health.Max * UPGRADE_SPEED);
        BattleInfo.player.MagicPoint.Max = (int)(BattleInfo.player.MagicPoint.Max * UPGRADE_SPEED);
        BattleInfo.player.AttackSpeed.Current *= UPGRADE_SPEED;
        BattleInfo.player.Defense.Current *= UPGRADE_SPEED;
        BattleInfo.player.Strength.Current *= UPGRADE_SPEED;

        BattleInfo.player.MagicPoint.Current = BattleInfo.player.MagicPoint.Max;
        BattleInfo.player.Health.Current = BattleInfo.player.Health.Max;
    }
}
