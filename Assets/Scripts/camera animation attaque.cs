using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraanimationattaque : MonoBehaviour
{
    const float VITESSE_ROTATION_YAW = 1.0f;
    const float MAGNITUDE_ROTATION_PITCH = 50.0f;
    const float VITESSE_ROTATION_PITCH = 1.0f;

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
        transform.rotation = Quaternion.Euler(new Vector3(
            r.x + Mathf.Sin(timer * VITESSE_ROTATION_PITCH) * MAGNITUDE_ROTATION_PITCH,
            timer * VITESSE_ROTATION_YAW,
            r.z
        ));
    }
}