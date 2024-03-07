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
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateItem<DebugItem>();
        CreateItem<HealthPotion>();
    }

    public Item CreateItem(System.Type type)
    {
        if (!GameObject.Find("Items"))
            new GameObject("Items");

        GameObject itemObject = new GameObject();
        Item item = (Item)itemObject.AddComponent(type);

        if (!types.Contains(type))
            types.Add(type);

        items.Add(item);

        itemObject.transform.parent = GameObject.Find("Items").transform;
        itemObject.name = item.Name;

        return item;
    }

    public Item CreateItem<T>() where T : Item, new()
    {
        return CreateItem(typeof(T));
    }

    #region Debug Stuff (to be remove later)
    public Inventory playerInventory;
    public void AddRandomItemToPlayerInventory()
    {
        System.Type type = Inventory.GenerateRandomItem();
        playerInventory.Add(CreateItem(type));
    }

    public void ClearAllItemFromPlayerInventory()
    {
        Inventory.RemoveAllItem(playerInventory);
    }
    #endregion
}
