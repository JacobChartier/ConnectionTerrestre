using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public Slot[] slots;
    public List<Item> items;
    public Item[] unauthorizedItems;

    private void Awake()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Items/inventory_item");
    }

    private void OnEnable()
    {
        //foreach (Slot slot in slots)
        //{
        //    slot.GetComponentInChildren<Draggable>().InitialiseItem();
        //}
    }

    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //items[i].Slot = slots[i];
            //items[i].Item = slots[i].GetItemInSlot();
        }
    }

    public bool Add(Item item)
    {
        // Stacking
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];
            Draggable itemInSlot = slot.GetComponentInChildren<Draggable>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < item.StackSize)
            {
                itemInSlot.count++;
                //itemInSlot.RefreshCount();

                return true;
            }
        }

        // Check for an empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];
            Draggable itemInSlot = slot.GetComponentInChildren<Draggable>();

            if (itemInSlot == null)
            {
                Spawn(item, slot);
                items[i] = item;

                return true;
            }
        }

        return false;
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