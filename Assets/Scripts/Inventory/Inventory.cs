using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public InventorySlot[] slots;
    public Item[] items;

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

    public Item GenerateRandomItem()
    {
        var num = UnityEngine.Random.Range(0, items.Length);
        return items[num];
    }
}
