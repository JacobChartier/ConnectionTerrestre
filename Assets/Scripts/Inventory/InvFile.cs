using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class InvFile
{
    public static void Save(string path, Inventory inventory)
    {
        FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        StreamWriter sw = new(fs);

        fs.SetLength(0);
        sw.WriteLine($"INVENTORY: {inventory.id}");

        foreach (var item in inventory.items)
        {
            if (item.GetSlot().name == null)
                continue;

            sw.WriteLine();
            sw.WriteLine($"\tITEM_TYPE: {item.GetType()}");

            sw.WriteLine($"\t\tSLOT: {(item.GetSlot() == null ? $"UNKNOW_SLOT" : item.GetSlot().name)}");
        }

        sw.Close();
    }

    public static List<RetrievedItems> Load(string path, Inventory inventory)
    {
        List<RetrievedItems> items = new List<RetrievedItems>();
        var lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            string s_type = default, slot = "";
            System.Type type = default;

            RetrievedItems retrievedItems = new RetrievedItems();

            if (line.ToString().Contains("ITEM_TYPE"))
            {
                var start = line.ToString().IndexOf(":") + 1;

                s_type = line.ToString().Substring(start, (line.ToString().Length - start));

                type = System.Type.GetType(s_type);

                retrievedItems.type = type;
            }

            if (line.ToString().Contains("SLOT"))
            {
                var start = line.ToString().IndexOf(":") + 1;

                slot = line.ToString().Substring(start, (line.ToString().Length - start));

                if (slot == "UNKNOW_SLOT")
                    slot = "";
                
                retrievedItems.slot = slot;
            }

            if (type != null)
            {
                var item = ItemManager.Instance.CreateItem(type);

                inventory.Add(item.GetComponent<Item>());
            }

            items.Add(retrievedItems);
        }

        return items;
    }
}

[Serializable]
public class RetrievedItems
{
    public System.Type type;
    public string slot;
}
