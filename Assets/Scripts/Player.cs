using Assets.Scripts.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private List<InfoAttaque> Attaques_Depart = new List<InfoAttaque>()
    {
        new InfoAttaque("Coup de poing", 30, 100, 10),
        new InfoAttaque("Coup de pied", 10, 50, 10),

        // le joueur commence avec 2 attaques physiques et 0 attaques magiques.
    };

    public static Player Instance;

    public Inventory inventory;
    private EntityStats stats;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(Instance);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        // donner les attaques de départ au joueur
        stats = GetComponent<EntityStats>();

        if (stats.Attaques.Count == 0)
        {
            foreach (InfoAttaque a in Attaques_Depart)
            {
                stats.Attaques.Add(a);
            }
        }
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
