using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleManager;

public class choixcombatmanager : MonoBehaviour
{
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
            }
        }
    }
    int timer = 0;
    int option_selectionee = 0;
    float angle = 2; // commence sur attaque
    const float MAGNITUDE = 6.0f;
    const float VITESSE_ROTATION = 10.0f; // plus petit = plus vite, doit être int
    const float SELECTION_TILT = -0.3f;
    float angle_entre_enfants; // const, ne pas assigner


    // Start is called before the first frame update
    void Start()
    {
        Active = false;
        transform.position = origin.position;
        angle_entre_enfants = Mathf.PI * 2 / enfants.Count;
        Rotate();
        ChangerTailleEnfants();
    }

    // Update is called once per frame
    public int CheckRotate()
    {
        if (timer != 0)
        {
            Rotate();
            timer += Math.Sign(timer) * -1;
            return -1;
        }

        if (left_held) // todo: remplacer avec vrai input
        {
            timer = -(int)VITESSE_ROTATION;
            option_selectionee++;
        }
        else if (right_held)// todo: remplacer avec vrai input
        {
            timer = (int)VITESSE_ROTATION;
            option_selectionee--;
        }
        else if (select_pressed)//todo: remplacer avec vrai input
        {
            select_pressed = false;
            return MathMod(option_selectionee, enfants.Count); // le modulo évite un overflow
        }

        if (timer != 0)
        {
            angle = MathF.Round(angle);
            option_selectionee = MathMod(option_selectionee, enfants.Count);
            ChangerTailleEnfants();
        }

        return -1;
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
