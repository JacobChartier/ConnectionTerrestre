using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryLoader
{
    private const string FILE_FORMAT = ".inv";

    private static readonly string persistent_data_path = Application.persistentDataPath;

    public static void Save(Inventory inventory)
    {
        inventory.items.Clear();

        foreach (var slot in inventory.slots)
            inventory.items.Add(slot.GetItem());

        InvFile.Save($"{persistent_data_path}/{inventory.id}{FILE_FORMAT}", inventory);
        Debug.Log($"Saving inventory data to: <b>{persistent_data_path}/{inventory.id}{FILE_FORMAT}</b>");
    }

    public static void Update(Inventory inventory, Item item)
    {
        InvFile.Update($"{persistent_data_path}/{inventory.id}{FILE_FORMAT}", inventory, item);
    }

    public static void Delete(Inventory inventory, Item item)
    {
        InvFile.Delete($"{persistent_data_path}/{inventory.id}{FILE_FORMAT}", item, inventory);
    }

    public static void Load(Inventory inventory)
    {
        inventory.items.Clear();

        var items = InvFile.Load($"{persistent_data_path}/{inventory.id}{FILE_FORMAT}", inventory);
        Debug.Log($"Loading inventory data from: <b>{persistent_data_path}/{inventory.id}{FILE_FORMAT}</b>");

        foreach (var item in items)
        {
            Debug.Log($"<color=#FF00FF>{item.type}</color> {{SlotID: {item.slotID}}}");

            foreach (var slot in inventory.slots)
            {
                if (inventory.slots[item.slotID] == null)
                {
                    var itemCreated = ItemManager.Instance.CreateItem(item.type, remainingUses: item.RemainingUses);
                    inventory.items.Add(itemCreated.GetComponent<Item>());

                    break;
                }
                else if (slot.IsIDTheSame(item.slotID))
                {
                    var itemCreated = ItemManager.Instance.CreateItem(item.type, slot, item.RemainingUses);
                    inventory.items.Add(itemCreated.GetComponent<Item>());

                    if (slot.transform.childCount > 1)
                        UnityEngine.Object.Destroy(itemCreated);

                    continue;
                }

                continue;
            }
        }
    }

    public static void Load(LoadMode mode, Inventory inventory)
    {
        switch (mode)
        {
            case LoadMode.WORLD_FULL:
                Load(inventory);
                break;

            case LoadMode.WORLD_HOTBAR:

                break;

            case LoadMode.COMBAT_FULL:
                LoadCombatInventory(inventory);
                break;

            case LoadMode.COMBAT_PARTIAL:

                break;
        }
    }

    private static void LoadCombatInventory(Inventory inventory)
    {
        inventory.items.Clear();

        var items = InvFile.Load($"{persistent_data_path}/{inventory.id}{FILE_FORMAT}", inventory);
        Debug.Log($"Loading inventory data from: <b>{persistent_data_path}/{inventory.id}{FILE_FORMAT}</b>");

        foreach (var item in items)
        {
            Debug.Log($"<color=#FF00FF>{item.type}</color> {{SlotID: {item.slotID}}}");

            var itemCreated = ItemManager.Instance.CreateItem(item.type, remainingUses: item.RemainingUses);
            inventory.items.Add(itemCreated.GetComponent<Item>());
        }
    }
}

[Serializable]
public class InventoryList
{
    public List<InventoryData> data = new();
}

[Serializable]
public struct InventoryData
{
    public string Name;
    public int Id;
}

public enum SaveMode
{
    COMBAT_FULL,            // Save all items
    COMBAT_PARTIAL,         // Save only modified items

    WORLD_FULL,             // Save all items
    WORLD_PARTIAL,          // Save only modified items
    WORLD_HOTBAR,           // Save only hotbar items
    WORLD_HOTBAR_PARTIAL    // Save only modified items from hotbar
}

public enum LoadMode
{
    COMBAT_FULL,    // Load all items NO EXCEPTION
    COMBAT_PARTIAL, // Load all authorized items

    WORLD_FULL,     // Load all items found in "player.inv"
    WORLD_HOTBAR    // Load all items with SlotID < 10 (Only load the hotbar)
}