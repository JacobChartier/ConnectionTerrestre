using Assets.Scripts.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public InventorySlot[] slots;
    public static Item[] items;
    public Item[] unauthorizedItems;

    private void Awake()
    {
        prefab = Resources.Load<GameObject>("Prefabs/inventory_item");
    }

    public bool Add(Item item)
    {
        // Stacking
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < item.stackSize)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Check for an empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

            if (itemInSlot == null)
            {
                Spawn(item, slot);
                return true;
            }
        }

        return false;
    }

    public void Spawn(Item item, InventorySlot slot)
    {
        GameObject itemGO = Instantiate(prefab, slot.transform);
        itemGO.AddComponent(ItemManager.Instance.items.ElementAt(0).GetType());

        DraggableItem inventoryItem = itemGO.GetComponent<DraggableItem>();

        inventoryItem.InitialiseItem(item);
    }

    public Item GetItem(InventorySlot slot)
    {
        return slot.GetComponentInChildren<DraggableItem>().item;
    }

    public Item[] GetItems(Inventory inventory)
    {
        Item[] items = new Item[slots.Length];

        for(int i = 0; i < inventory.slots.Length; i++)
        {
            items[i] = inventory.GetItem(inventory.slots[i]);
        }

        Debug.Log(items);
        return items;
    }

    public static System.Type GenerateRandomItem()
    {
        System.Random rand = new System.Random();
        //List<System.Type> itemTypes = new List<System.Type> { typeof(HealthPotion) };

        int randomIndex = rand.Next(ItemManager.Instance.types.Count);
        System.Type randomType = ItemManager.Instance.types[randomIndex];

        return randomType;
    }

    public static void RemoveAllItem(Inventory inventory)
    {
        foreach (var slot in inventory.slots)
        {
            Destroy(slot.gameObject.transform.GetChild(0).gameObject);
        }
    }
}
