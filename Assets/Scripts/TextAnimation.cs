using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    float speed = 2.5f;
    float timer = 10f;

    [SerializeField] private TMP_Text text;

    public void StartAnimation(float lifetime, string text = "")
    {
        this.text.text = text;
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        timer++;
        transform.position += new Vector3(0, speed);
    }
}
