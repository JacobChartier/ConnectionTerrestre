using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Inventory inventory;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        DontDestroyOnLoad(Instance);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    public void LoadInventory()
    {
        inventory = gameObject.GetComponentInChildren<Inventory>();
        inventory.slots = new Slot[30];

        for (int i = 0; i < 30; i++)
        {
            if (i < 10)
                inventory.slots[i] = GameObject.Find($"Hotbar Slot ({i})").GetComponent<Slot>();

            if (i >= 10 && i < 30)
                inventory.slots[i] = GameObject.Find($"Inventory Slot ({i})").GetComponent<Slot>();
        }

        InventoryLoader.Load(inventory);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        inventory = gameObject.GetComponentInChildren<Inventory>();
        inventory.slots = new Slot[30];

        for (int i = 0; i < 30; i++)
        {
            if (i < 10)
                inventory.slots[i] = GameObject.Find($"Hotbar Slot ({i})").GetComponent<Slot>();

            if (i >= 10 && i < 30)
                inventory.slots[i] = GameObject.Find($"Inventory Slot ({i})").GetComponent<Slot>();
        }

        InventoryLoader.Load(inventory);
    }
}
