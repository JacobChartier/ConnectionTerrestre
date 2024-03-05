using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public Inventory playerInventory;
}
