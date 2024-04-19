using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIScaler : MonoBehaviour
{
    Canvas canvas;
    RectTransform rectTransform;
    void Start()
    {
        canvas = GameObject.Find("Menus").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
