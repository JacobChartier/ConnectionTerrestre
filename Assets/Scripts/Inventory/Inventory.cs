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
        items.Add(item);

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
        if (slot != null)
            ItemManager.Instance.CreateItem(item.GetType(), slot);
        else
            ItemManager.Instance.CreateItem(item.GetType());
    }

    public void ChangeSlot(GameObject item, Inventory inventory)
    {
        if (item.GetComponent<Item>().GetSlotID() <= 9)
        {
            for (int i = 10; i < inventory.slots.Length; i++)
            {
                if (!inventory.slots[i].isOccupied)
                {
                    item.transform.SetParent(inventory.slots[i].transform, false);
                    break;
                }
                else
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (!inventory.slots[j].isOccupied && j < item.GetComponent<Item>().GetSlotID())
                        {
                            item.transform.SetParent(inventory.slots[j].transform, false);
                            break;
                        }
                    }
                }
            }
        }

        else if (item.GetComponent<Item>().GetSlotID() > 9)
        {
            for (int i = 0; i < 10; i++)
            {
                if (!inventory.slots[i].isOccupied)
                {
                    item.transform.SetParent(inventory.slots[i].transform, false);
                    break;
                }
                else
                {
                    for (int j = 10; j < inventory.slots.Length; j++)
                    {
                        if (!inventory.slots[j].isOccupied && j < item.GetComponent<Item>().GetSlotID())
                        {
                            item.transform.SetParent(inventory.slots[j].transform, false);
                            break;
                        }
                    }
                }
            }
        }
    }

    #region Debug Stuff
    public static System.Type GenerateRandomItem()
    {
        System.Random rand = new System.Random();

        int randomIndex = rand.Next(ItemManager.types.Count);
        System.Type randomType = ItemManager.types[randomIndex];

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