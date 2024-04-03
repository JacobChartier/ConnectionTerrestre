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
        InvFile.Save($"{persistent_data_path}/{inventory.id}.inv", inventory);
    }

    public static void Load(Inventory inventory)
    {
        var items = InvFile.Load($"{persistent_data_path}/{inventory.id}.inv", inventory);

        if (SceneManager.GetSceneByName("World").isLoaded)
        {
            foreach (var item in items)
            {
                Player.Instance.inventory.Add(ItemManager.Instance.CreateItem(item.type).GetComponent<Item>());
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