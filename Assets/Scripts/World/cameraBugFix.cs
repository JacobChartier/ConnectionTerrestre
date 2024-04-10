using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBugFix : MonoBehaviour
{
    private CinemachineVirtualCamera Vcam;

    // Start is called before the first frame update
    void Start()
    { // après chaque scène de combat, la caméra ne suit plus le joueur. ceci répare ce problème manuellement (il reste 2 semaines pour ce projet)
        Vcam = GetComponent<CinemachineVirtualCamera>();
        Vcam.Follow = GameObject.Find("Player").transform;
        Vcam.Priority++;
        //Vcam.m_Lens.NearClipPlane = 0.6f; // pour ne pas voir le modèle du joueur
    }
}
