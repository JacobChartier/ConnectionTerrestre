using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class bhvFlecheSelection : MonoBehaviour
{
    const float VITESSE = 8.0f;
    const float MAGNITUDE = 3.0f;
    const float DIST_BOUGER = 11.0f;

    [SerializeField] TMP_Text text;
    int timer = 0;
    int choix = 0;
    public int max_choix = 0;
    float og_x;

    // Start is called before the first frame update
    void Start()
    {
        og_x = transform.position.x;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        transform.position = new Vector3(
            og_x + Mathf.Sin(timer / VITESSE) * MAGNITUDE,
            transform.position.y,
            transform.position.z
        );
        //Debug.Log(transform.position);
    }

    public void BougerHaut()
    {
        if (choix <= 0)
            return;

        transform.position += new Vector3(0, DIST_BOUGER, 0);
        choix--;
    }

    public void BougerBas()
    {
        if (choix >= max_choix)
            return;

        transform.position -= new Vector3(0, DIST_BOUGER, 0);
        choix++;
    }

    public int GetChoixSelection()
    {
        return choix;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
