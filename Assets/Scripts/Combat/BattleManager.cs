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

    Evenement evenement_actuel;
    int timer = 0;
    bool fuir_fail = false;

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
                        timer = 0;
                        evenement_actuel = Evenement.FUIR;
                        break;

                    default:
                        break;
                }
                break;
            case Evenement.FUIR:
                CodeFuir();
                break;
        }

        timer++;
    }

    void CodeIntro()
    {
        if (timer >= 200)
        {
            if (stats_joueur.currentATKSpeed > stats_ennemi.currentATKSpeed ||
                true || //DEBUG!!!!!!!!!!
                stats_joueur.currentATKSpeed == stats_ennemi.currentATKSpeed && UnityEngine.Random.Range(0, 2) == 0) // si vitesses égales, premier tour = hasard
            {
                evenement_actuel = Evenement.TOUR_JOUEUR;
                camera_generale.Priority = 0;
                camera_option_joueur.Priority = 1;
                choix_combat.Active = true;
            }
            else
            {
                evenement_actuel = Evenement.TOUR_ENNEMI;
                camera_generale.Priority = 1;
                camera_option_joueur.Priority = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            timer = 200; // le joueur peut skip le cutscene avec J
        }
    }

    void CodeFuir()
    {
        const int NB_FRAMES_JOUEUR_TOURNE = 30;
        const int NB_FRAMES_AVANT_JOUEUR_TOURNE = 60;
        const int NB_FRAMES_JOUEUR_COURT = 180;
        const float VITESSE_JOUEUR_FUITE = 0.2f;

        if (timer <= 1)
        {
            choix_combat.Active = false;
            camera_generale.Priority = 0;
            camera_option_joueur.Priority = 0;
            camera_joueur_fuit.Priority = 1;
        }

        if (timer > NB_FRAMES_JOUEUR_COURT + NB_FRAMES_AVANT_JOUEUR_TOURNE + NB_FRAMES_JOUEUR_TOURNE)
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
                fuir_fail = UnityEngine.Random.Range(0, 2) == 0;
            }
        }
    }
}
