using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public T GetMenu<T>() where T : MenuHandler
    {
        return FindObjectOfType<T>(true);
    }
}

