using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class choixcombatmanager : MonoBehaviour
{
    [SerializeField] Transform lookat;
    [SerializeField] Transform origin;
    [SerializeField] List<GameObject> enfants;
    public bool Active
    {
        get
        {
            return Active;
        }
        set
        {
            foreach (GameObject o in enfants)
            {
                o.SetActive(value);
                Debug.Log(value);
            }
        }
    }
    int timer = 0;
    int option_selectionee = 2;
    float angle = 0;
    const float MAGNITUDE = 6.0f;
    const float VITESSE_ROTATION = 10.0f; // plus petit = plus vite, doit être int
    const float SELECTION_TILT = -0.3f;
    float angle_entre_enfants; // const, ne pas assigner
    //float timer_rotation_une; // const, ne pas assigner

    // Start is called before the first frame update
    void Start()
    {
        Active = false;
        transform.position = origin.position;
        angle_entre_enfants = Mathf.PI * 2 / enfants.Count;
        //timer_rotation_une = VITESSE_ROTATION * 2 * Mathf.PI / enfants.Count;
        Rotate();
        ChangerTailleEnfants();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer != 0)
        {
            Rotate();
            timer += Math.Sign(timer) * -1;
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            timer = -(int)VITESSE_ROTATION;
            option_selectionee++;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            timer = (int)VITESSE_ROTATION;
            option_selectionee--;
        }

        if (timer != 0)
        {
            option_selectionee = MathMod(option_selectionee, enfants.Count);
            ChangerTailleEnfants();
        }
    }

    void ChangerTailleEnfants()
    {
        int index = 0;
        foreach (GameObject o in enfants)
        {
            o.GetComponent<optionjoueurmouvement>().ChangeSize(index == option_selectionee);
            index++;
        }
    }

    void Rotate()
    {
        angle += 1 / VITESSE_ROTATION * MathF.Sign(timer);

        int index = 0;
        foreach (GameObject o in enfants)
        {
            float angle_rotation = angle_entre_enfants * index + (angle / 1.0f * angle_entre_enfants) + SELECTION_TILT;
            o.transform.position = new Vector3(
                Mathf.Cos(angle_rotation) * MAGNITUDE,
                Mathf.Sin(angle_rotation) * MAGNITUDE,
                0
            ) + transform.position;
            index++;
        }
    }
    public static int MathMod(int a, int b) // l'opération modulo de c# FUCK UP les nombres négatifs... fonction pour le faire correctement
    {
        return (Math.Abs(a * b) + a) % b;
    }
}
