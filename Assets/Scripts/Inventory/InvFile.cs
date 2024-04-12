using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;

public static class InvFile
{
    public static void Save(string path, Inventory inventory)
    {
        using StreamWriter sw = new(path);

        // Header
        sw.WriteLine($"# This file has been generated on {DateTime.Today:yyyy-MM-dd} at {DateTime.Now:HH:mm:ss}.\n# It contains inventory data for entity \"{inventory.id}\".\n");

        // Data
        sw.WriteLine($"INVENTORY_ID: \"{inventory.id}\"");

        foreach (var item in inventory.items)
            sw.WriteLine($"{SerializeItem(item, inventory)}");

        // Footer
        sw.WriteLine($"\n# {inventory.items.Count.ToString()} items saved for entity \"{inventory.id}\".");
    }

    private static string SerializeItem(Item item, Inventory inventory)
    {
        string output = "\t";

        output += $"ITEM: {item.GetType().ToString()} ";

        output += "{ ";

        if (item.GetSlotID() > -1)
            output += $"SlotID: {item.GetSlotID().ToString()}, ";

        if (item.RemainingUses != 5)
            output += $"RemainingUses: {item.RemainingUses.ToString()}, ";

        output = output.TrimEnd(',', ' ');

        output += " }";

        return output;
    }

    private static Item DeserializeItem(string line)
    {
        return default;
    }

    public static List<DeserializedItem> Load(string path, Inventory inventory)
    {
        List<DeserializedItem> items = new();
        var lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i++)
        {
            string s_type = default;
            int slot = -2;
            System.Type type = default;

            DeserializedItem retrievedItems = new DeserializedItem();

            if (lines[i].ToString().Contains("TYPE"))
            {
                s_type = ReadData<string>("TYPE", lines[i]);
                retrievedItems.type = System.Type.GetType(s_type);


                retrievedItems.slotID = ReadData<int>("SLOT_ID", lines[i + 1]);
                UnityEngine.Debug.Log($"<color=#00FF00>{retrievedItems.type}</color> {{{retrievedItems.slotID}}}");
            }

            if (type != null)
            {
                items.Add(retrievedItems);
            }
        }

        return items;
    }

    private static T ReadData<T>(string data, string line)
    {
        int start = (line.IndexOf(':') + 1);
        int length = (line.Length - start);

        T output;
        string s_value = line.Substring(start, length);

        output = (T)Convert.ChangeType(s_value, typeof(T));
        return output;
    }
}

[Serializable]
public struct DeserializedItem
{
    public System.Type type;
    public int slotID;
    public int RemainingUses;
}
