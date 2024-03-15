using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bhvxptext : MonoBehaviour
{
    const float UPWARD_SPEED = 4.0f;
    const float LIFETIME_IN_SECS = 1.5f;

    TMP_Text text;
    Transform ou_aller;
    System.Random rng;

    private int local_timer = 0;

    public void Init(Transform pos)
    {
        text = GetComponent<TMP_Text>();
        ou_aller = pos;

        text.text = $"+1 EXP";

        transform.position = Camera.main.WorldToScreenPoint(ou_aller.position, Camera.MonoOrStereoscopicEye.Mono);

        Destroy(gameObject, LIFETIME_IN_SECS);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        local_timer++;
        transform.position = Camera.main.WorldToScreenPoint(ou_aller.position, Camera.MonoOrStereoscopicEye.Mono);
        transform.position += new Vector3(0, UPWARD_SPEED * local_timer, 0);
        text.color = new Color((float)rng.NextDouble(), (float)rng.NextDouble(), (float)rng.NextDouble(), Time.deltaTime / LIFETIME_IN_SECS);
    }
}
