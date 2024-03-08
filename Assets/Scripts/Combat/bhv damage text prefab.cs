using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class bhvdamagetextprefab : MonoBehaviour
{
    const float UPWARD_SPEED = 2.0f;
    const float LIFETIME_IN_SECS = 3.0f;

    TMP_Text text;
    Transform ou_aller;

    private int local_timer = 0;

    public void Init(float dommages, Transform pos)
    {
        text = GetComponent<TMP_Text>();
        ou_aller = pos;

        text.text = $"{(dommages < 0 ? '+' : '-')} {MathF.Round(Mathf.Abs(dommages), MidpointRounding.AwayFromZero)} HP";

        if (dommages < 0)
            text.color = Color.green;
        else
            text.color = Color.red;

        transform.position = Camera.main.WorldToScreenPoint(ou_aller.position, Camera.MonoOrStereoscopicEye.Mono);

        Destroy(gameObject, LIFETIME_IN_SECS);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        local_timer++;
        transform.position = Camera.main.WorldToScreenPoint(ou_aller.position, Camera.MonoOrStereoscopicEye.Mono);
        transform.position += new Vector3(0, UPWARD_SPEED * local_timer, 0);
        text.color -= new Color(0, 0, 0, Time.deltaTime / LIFETIME_IN_SECS);
    }
}
