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

    public static List<DeserializedItem> Load(string path, Inventory inventory)
    {
        List<DeserializedItem> items = new();
        var lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            int start, length;

            if (line.Contains("ITEM"))
            {
                DeserializedItem item = new();

                // Read ITEM
                start = line.IndexOf("ITEM:") + 6;
                length = line.IndexOf('{') - start;

                item.type = System.Type.GetType(line.Substring(start, length).Trim());

                // Read SlotID
                if (line.Contains("SlotID"))
                {
                    start = line.IndexOf("SlotID:") + 8;
                    length = 1;

                    item.slotID = int.Parse(line.Substring(start, length).Trim());
                }

                // Read RemainingUses
                if (line.Contains("RemainingUses"))
                {
                    start = line.IndexOf("RemainingUses:") + 15;
                    length = 1;

                    item.RemainingUses = int.Parse(line.Substring(start, length).Trim());
                }

                // Add the deserialized item to the list
                items.Add(item);
            }
        }

        return items;
    }
}

[Serializable]
public class DeserializedItem
{
    public System.Type type;
    public int slotID;
    public int RemainingUses;
}
