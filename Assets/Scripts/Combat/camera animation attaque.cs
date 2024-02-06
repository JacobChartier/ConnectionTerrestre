using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraanimationattaque : MonoBehaviour
{
    [SerializeField]
    float VITESSE_ROTATION_YAW = 1.0f;
    [SerializeField]
    float MAGNITUDE_ROTATION_PITCH = 50.0f;
    [SerializeField]
    float VITESSE_ROTATION_PITCH = 1.0f;

    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        Vector3 r = transform.rotation.eulerAngles;
        Vector3 p = transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(
            r.x,
            timer * VITESSE_ROTATION_YAW,
            r.z
        ));

        transform.position = new Vector3(p.x, p.y + Mathf.Sin(timer * VITESSE_ROTATION_PITCH) * MAGNITUDE_ROTATION_PITCH);
    }
}