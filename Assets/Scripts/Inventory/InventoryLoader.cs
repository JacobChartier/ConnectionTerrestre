using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryLoader
{
    private const string FILE_FORMAT = ".inv";

    private static string persistent_data_path = Application.persistentDataPath;

    public static void Save(Inventory inventory)
    {
        InvFile.Save($"{persistent_data_path}/{inventory.id}{FILE_FORMAT}", inventory);
        Debug.Log($"Saving inventory data to: <b>{persistent_data_path}/{inventory.id}{FILE_FORMAT}</b>");
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
                if (slot.IsIDTheSame(item.slotID))
                {
                    var itemCreated = ItemManager.Instance.CreateItem(item.type, slot, item.RemainingUses);
                    inventory.items.Add(itemCreated.GetComponent<Item>());

                    if (slot.transform.childCount > 2)
                        UnityEngine.Object.Destroy(itemCreated);

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