using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadUpDisplay : MonoBehaviour, IMenuHandler
{
    public static HeadUpDisplay Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
