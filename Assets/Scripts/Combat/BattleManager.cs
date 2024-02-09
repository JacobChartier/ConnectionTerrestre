using Cinemachine;
using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    int timer = 0;
    bool fuir_fail = false;
    private bool DEBUG_PLAYER_ALWAYS_GOES_FIRST = true; // debug!!

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
        if (timer <= 1)
        {
            camera_joueur_fuit.Priority = 0;
            camera_generale.Priority = 1;
            camera_option_joueur.Priority = 0;
            choix_combat.Active = false;
        }

        if (timer > 100) //todo: monstre choisit attaque + anim attaque + bloc/parry
        {
            evenement_actuel = Evenement.TOUR_JOUEUR;
        }
    }

    private void CodeTourJoueur()
    {
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
                Debug.Log("atk");
                break;
            case 1:
                Debug.Log("mag");
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

    private void CodeIntro()
    {
        if (timer >= 200)
        {
            if (stats_joueur.AttackSpeed.Current > stats_ennemi.AttackSpeed.Current ||
                DEBUG_PLAYER_ALWAYS_GOES_FIRST ||
                stats_joueur.AttackSpeed.Current == stats_ennemi.AttackSpeed.Current && UnityEngine.Random.Range(0, 2) == 0) // si vitesses égales, premier tour = hasard
            {
                evenement_actuel = Evenement.TOUR_JOUEUR;
            }
            else
            {
                evenement_actuel = Evenement.TOUR_ENNEMI;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
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
            SceneManager.LoadScene("World"); // à connecter plus tard
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
                fuir_fail = UnityEngine.Random.Range(0, 100 / (100 - POURCENTAGE_CHANCE_FUITE)) == 0; // le joueur a 80% de chance de pouvoir fuire

                if (fuir_fail)
                    timer = 2;
            }
        }
    }
}
