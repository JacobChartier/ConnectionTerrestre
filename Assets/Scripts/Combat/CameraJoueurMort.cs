using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoueurMort : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    [SerializeField] GameObject lumiere;
    [SerializeField] const float VITESSE = 0.5f;
    [SerializeField] const float VITESSE_Y = 0.05f;
    [SerializeField] const float LIMITE_Y = 100f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    public void Move()
    {
        if (cam.Priority == 0)
            return;

        lumiere.transform.position = new Vector3(10, lumiere.transform.position.y, 0);

        Vector3 nouvel_angle = transform.rotation.eulerAngles;
        nouvel_angle.y += VITESSE;
        transform.rotation = Quaternion.Euler(nouvel_angle);

        if (transform.position.y < LIMITE_Y)
            transform.position += new Vector3(0, VITESSE_Y, 0);
    }
}
