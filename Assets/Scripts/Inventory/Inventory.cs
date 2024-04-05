using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public string id;
    public Slot[] slots;
    public List<Item> items;
    public Item[] unauthorizedItems;

    public static Dictionary<Item, int> itemsDictionary = new();
    public static event Action<Item, int> OnChange;

    private void Awake()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Items/inventory_item");
    }

    public void EmptyInventory()
    {
        itemsDictionary.Clear();
        items = new List<Item>();
    }

    public bool Add(Item item, int amount = 1, Slot slot = null)
    {
        if (itemsDictionary.TryAdd(item, amount))
            itemsDictionary[item] += amount;

        OnChange?.Invoke(item, amount);

        //// Stacking
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    Slot slot = slots[i];
        //    Draggable itemInSlot = slot?.GetComponentInChildren<Draggable>();

        //    if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < item.StackSize)
        //    {
        //        itemInSlot.count++;
        //        //itemInSlot.RefreshCount();

        //        return true;
        //    }
        //}

        if (slot == null)
        {
            // Check for an empty slot
            for (int i = 0; i < slots.Length; i++)
            {
                Slot slotInArray = slots[i];
                Draggable itemInSlot = slotInArray.GetComponentInChildren<Draggable>();

                if (itemInSlot == null)
                {
                    Spawn(item, slotInArray);
                    items[i] = item;

                    return true;
                }
            }
        }
        else
        {
            Spawn(item, slot);
            return true;
        }

        return false;
    }

    public void Remove(Item item, int amount = 1)
    {
        if (!itemsDictionary.ContainsKey(item))
            return;

        itemsDictionary[item] -= amount;

        if (itemsDictionary[item] < 1)
            itemsDictionary.Remove(item);

        OnChange?.Invoke(item, amount);
    }

    public void Spawn(Item item, Slot slot)
    {
        GameObject itemGameObject = ItemManager.Instance.CreateItem(item.GetType(), slot);
    }

    #region Debug Stuff
    public static System.Type GenerateRandomItem()
    {
        System.Random rand = new System.Random();

        int randomIndex = rand.Next(ItemManager.Instance.types.Count);
        System.Type randomType = ItemManager.Instance.types[randomIndex];

        return randomType;
    }

    public static void RemoveAllItem(Inventory inventory)
    {
        foreach (var slot in inventory.slots)
        {
            Destroy(slot.gameObject.transform.GetChild(0)?.gameObject);
        }
    }
    #endregion
}