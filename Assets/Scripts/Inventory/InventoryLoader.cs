using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryLoader
{
    private const string FILE_FORMAT = ".inv";

    private static string persistent_data_path = Application.persistentDataPath;

    public static void Save(Inventory inventory)
    {
        InvFile.Save($"{persistent_data_path}/{inventory.id}{FILE_FORMAT}", inventory);
    }

    public static void Load(Inventory inventory)
    {
        inventory.items.Clear();

        var items = InvFile.Load($"{persistent_data_path}/{inventory.id}{FILE_FORMAT}", inventory);

        foreach (var item in items)
        {
            Debug.Log($"<color=#FF00FF>{item.type}</color> {{SlotID: {item.slotID}}}");

            foreach (var slot in inventory.slots)
                if (slot.IsIDTheSame(item.slotID))
                {
                    Debug.Log($"<color=#FF0000>{item.type}</color> {{SlotID: {item.slotID}}}");
                    inventory.Add(ItemManager.Instance.CreateItem(item.type, slot).GetComponent<Item>(), slot: slot);
                    continue;
                }
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