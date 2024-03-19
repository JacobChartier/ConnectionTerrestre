using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bhvxptextprefab : MonoBehaviour
{
    const float UPWARD_SPEED = 4.0f;
    const float LIFETIME_IN_SECS = 1.5f;

    TMP_Text text;
    Transform ou_aller;
    System.Random rng;

    public void Init(bool level_up, Transform pos)
    {
        text = GetComponent<TMP_Text>();
        rng = new System.Random();
        ou_aller = pos;

        text.text = $"+1 XP";

        text.color = Color.red;

        transform.position = Camera.main.WorldToScreenPoint(ou_aller.position, Camera.MonoOrStereoscopicEye.Mono);
        transform.position += new Vector3(rng.Next(-100, 100), rng.Next(-100, 100));

        if (level_up)
        {
            text.text = $"LEVEL UP!!";
            text.fontSize *= 4;
            //transform.position += new Vector3(0, 100);
        }

        Destroy(gameObject, LIFETIME_IN_SECS);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, UPWARD_SPEED);
        text.color = new Color((float)rng.NextDouble(), (float)rng.NextDouble(), (float)rng.NextDouble(), 1);
    }
}
