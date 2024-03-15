using Assets.Scripts.Items;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public InventorySlot[] slots;
    public Item[] items;
    public Item[] unauthorizedItems;

    private void Awake()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Items/Item");
    }

    public bool Add(Item item)
    {
        // Stacking
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            Draggable itemInSlot = slot.GetComponentInChildren<Draggable>();

            if (itemInSlot != null && itemInSlot.item.Name == item.Name && itemInSlot.count < item.StackSize)
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

    public void Spawn(Item item, InventorySlot slot)
    {
        GameObject itemGO = Instantiate(prefab, slot.transform);
        itemGO.AddComponent(ItemManager.Instance.items.ElementAt(0).GetType());

        Draggable inventoryItem = itemGO.GetComponent<Draggable>();

        inventoryItem.InitialiseItem(item);
    }

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
            Destroy(slot.gameObject.transform.GetChild(0).gameObject);
        }
    }
}
