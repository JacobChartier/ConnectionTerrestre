using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public List<Item> items = new List<Item>();
    public List<System.Type> types = new List<System.Type>();

    public Inventory playerInventory;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        SceneManager.sceneLoaded += CreateItems;

        DontDestroyOnLoad(this);
    }

    public void CreateItems(Scene scene, LoadSceneMode mode)
    {
        playerInventory = GameObject.Find("Player").GetComponentInChildren<Inventory>();

        items.Clear();
        playerInventory.EmptyInventory();

        if (scene == SceneManager.GetSceneByName("World"))
            InventoryLoader.Load(Player.Instance.inventory);

        if (scene == SceneManager.GetSceneByName("Combat"))
            InventoryLoader.Load(LoadMode.COMBAT_FULL, Player.Instance.inventory);

    }

    public static GameObject CreateItem<T>(Slot slot = null) where T : Item, new()
    {
        return Instance.CreateItem(typeof(T), slot);
    }

    public GameObject CreateItem(System.Type type, Slot slot = null, int remainingUses = 5)
    {
        // Create the Items GameObject is it doesn't already exist.
        if (!GameObject.Find("Items"))
            new GameObject("Items");

        // Register the item type if it doesn't already exist.
        if (!types.Contains(type))
            types.Add(type);

        GameObject itemObject = new();

        // Add the components
        Item itemComponent = (Item)itemObject.AddComponent(type);
        itemComponent.RemainingUses = remainingUses;

        Draggable draggableCompoment = itemObject.AddComponent<Draggable>();
        draggableCompoment.item = itemComponent;
        //Droppable droppableComponent = itemObject.AddComponent<Droppable>();
        //droppableComponent.item = itemComponent;

        // Instantiate the prefabs
        GameObject inventoryObject = Instantiate((GameObject)Resources.Load("Prefabs/Items/inventory_item"), itemObject.transform);
        inventoryObject.name = "Inventory Item";

        draggableCompoment.countText = inventoryObject.GetComponentInChildren<TMP_Text>(true);
        //var droppedObject = Instantiate(Resources.Load("Prefabs/Items/dropped_item"), itemObject.transform);
        //droppedObject.name = "Dropped Item";

        items.Add(itemComponent);

        itemObject.transform.parent = GameObject.Find("Items").transform;
        itemObject.name = itemComponent.Name;

        if (slot != null)
            itemObject.transform.SetParent(slot.gameObject.transform);

        draggableCompoment.InitialiseItem();

        return itemObject;
    }

    #region Debug Stuff (to be remove later)
    public void AddRandomItemToPlayerInventory()
    {
        System.Type type = Inventory.GenerateRandomItem();
        //playerInventory.Add(type);

        items.ElementAt(0).Use(Scenes.WORLD);
    }

    public void ClearAllItemFromPlayerInventory()
    {
        Inventory.RemoveAllItem(playerInventory);
    }

    public void AddAllItemsToInventory()
    {
        // Potions
        CreateItem<ExperiencePotion>(GameObject.Find("Inventory Slot (14)").GetComponent<Slot>());

        CreateItem<WeakHealthPotion>(GameObject.Find("Inventory Slot (10)").GetComponent<Slot>());
        CreateItem<NormalHealthPotion>(GameObject.Find("Inventory Slot (11)").GetComponent<Slot>());
        CreateItem<StrongHealthPotion>(GameObject.Find("Inventory Slot (12)").GetComponent<Slot>());
        CreateItem<UltimateHealthPotion>(GameObject.Find("Inventory Slot (13)").GetComponent<Slot>());

        // Essences
        CreateItem<MagicianEssence>(GameObject.Find("Inventory Slot (22)").GetComponent<Slot>());
        CreateItem<WarriorEssence>(GameObject.Find("Inventory Slot (23)").GetComponent<Slot>());

        CreateItem<WeakMagicEssence>(GameObject.Find("Inventory Slot (18)").GetComponent<Slot>());
        CreateItem<NormalMagicEssence>(GameObject.Find("Inventory Slot (19)").GetComponent<Slot>());
        CreateItem<StrongMagicEssence>(GameObject.Find("Inventory Slot (20)").GetComponent<Slot>());
        CreateItem<UltimateMagicEssence>(GameObject.Find("Inventory Slot (21)").GetComponent<Slot>());

        // Leaves
        CreateItem<VitalLeaf>(GameObject.Find("Inventory Slot (26)").GetComponent<Slot>());
        CreateItem<FourLeafClover>(GameObject.Find("Inventory Slot (27)").GetComponent<Slot>());

        // Shields
        CreateItem<Shield>(GameObject.Find("Inventory Slot (28)").GetComponent<Slot>());


        // SHOP 
        CreateItem<Shield>(GameObject.Find("Shop Slot (0)").GetComponent<Slot>());
        CreateItem<ExperiencePotion>(GameObject.Find("Shop Slot (1)").GetComponent<Slot>());
    }
    #endregion
}
