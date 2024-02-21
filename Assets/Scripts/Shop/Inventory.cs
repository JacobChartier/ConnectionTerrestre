using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots;
    public Item[] items;

    [SerializeField] private GameObject prefab;

    public void Add(Item item)
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

                return;
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
                return;
            }
        }
    }

    private void Spawn(Item item, InventorySlot slot)
    {
        GameObject itemGO = Instantiate(prefab, slot.transform);
        DraggableItem inventoryItem = itemGO.GetComponent<DraggableItem>();

        inventoryItem.InitialiseItem(item);
    }

    public Item GenerateRandomItem()
    {
        var num = UnityEngine.Random.Range(0, items.Length);
        return items[num];
    }
}
