using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public List<Item> items = new List<Item>();
    public List<System.Type> types = new List<System.Type>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Potions
        CreateItem<ExperiencePotion>();

        CreateItem<WeakHealthPotion>();
        CreateItem<NormalHealthPotion>();
        CreateItem<StrongHealthPotion>();
        CreateItem<UltimateHealthPotion>();

        // Essences
        CreateItem<MagicianEssence>();
        CreateItem<WarriorEssence>();

        CreateItem<WeakMagicEssence>();
        CreateItem<NormalMagicEssence>();
        CreateItem<StrongMagicEssence>();
        CreateItem<UltimateMagicEssence>();

        // Leaves
        CreateItem<VitalLeaf>();
        CreateItem<FourLeafClover>();

        // Shields
        CreateItem<Shield>();

        DontDestroyOnLoad(GameObject.Find("Items"));
    }

    public static Item CreateItem<T>() where T : Item, new()
    {
        return Instance.CreateItem(typeof(T));
    }

    public Item CreateItem(System.Type type)
    {
        if (!GameObject.Find("Items"))
            new GameObject("Items");

        GameObject itemObject = new GameObject();

        // Add the components
        Item item = (Item)itemObject.AddComponent(type);

        // Generate the two prefabs as a child
        var inventoryObject = Instantiate(Resources.Load("Prefabs/Items/inventory_item"), itemObject.transform);
        inventoryObject.name = "Inventory Item";

        var droppedObject = Instantiate(Resources.Load("Prefabs/Items/dropped_item"), itemObject.transform);
        droppedObject.name = "Dropped Item";

        if (!types.Contains(type))
            types.Add(type);

        items.Add(item);

        itemObject.transform.parent = GameObject.Find("Items").transform;
        itemObject.name = item.Name;

        return item;
    }

    #region Debug Stuff (to be remove later)
    public Inventory playerInventory;
    public void AddRandomItemToPlayerInventory()
    {
        System.Type type = Inventory.GenerateRandomItem();
        playerInventory.Add(CreateItem(type));

        items.ElementAt(0).Use();
    }

    public void ClearAllItemFromPlayerInventory()
    {
        Inventory.RemoveAllItem(playerInventory);
    }
    #endregion
}
