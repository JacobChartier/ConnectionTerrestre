using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bhvdamagetextprefab : MonoBehaviour
{
    const float UPWARD_SPEED = 2.0f;
    const float LIFETIME_IN_SECS = 3.0f;

    TMP_Text text;

    public void Init(float dommages, Vector3 pos)
    {
        text = GetComponent<TMP_Text>();

        text.text = $"{(dommages < 0 ? '+' : '-')} {MathF.Round(Mathf.Abs(dommages), MidpointRounding.AwayFromZero)} HP";

        if (dommages < 0)
            text.color = Color.green;
        else
            text.color = Color.red;

        transform.position = Camera.main.WorldToScreenPoint(pos, Camera.MonoOrStereoscopicEye.Mono);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, UPWARD_SPEED, 0);
        text.color -= new Color(0, 0, 0, Time.deltaTime / LIFETIME_IN_SECS);
    }
}
