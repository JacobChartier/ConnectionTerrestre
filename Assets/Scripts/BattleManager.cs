using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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
    [SerializeField] choixcombatmanager choix_combat;
    Evenement evenement_actuel;
    int timer = 0;

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
                break;
        }

        timer++;
    }

    void CodeIntro()
    {
        if (timer == 200)
        {
            if (PlayerInfo.Vitesse >= 0) // debug: remplacer avec vitesse monstre
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
    }
}
