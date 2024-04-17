using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;

public static class InvFile
{
    public static void Save(string path, Inventory inventory)
    {
        var savedItemsCounter = 0;
        using StreamWriter sw = new(path);

        // Header
        sw.WriteLine($"# This file has been generated on {DateTime.Today:yyyy-MM-dd} at {DateTime.Now:HH:mm:ss}.\n# It contains inventory data for entity \"{inventory.id}\".\n");

        // Data
        sw.WriteLine($"INVENTORY_ID: \"{inventory.id}\"");

        foreach (var item in inventory.items)
            if (item != null)
            {
                sw.WriteLine($"{SerializeItem(item, inventory)}");
                savedItemsCounter++;
            }

        // Footer
        sw.WriteLine($"\n# {savedItemsCounter} items saved for entity \"{inventory.id}\".");
    }

    private static int GetNumberOfItemsSaved(string[] invFileData)
    {
        int count = 0;

        foreach (var lines in invFileData)
            if (lines.Contains("ITEM"))
                count++;

        return count;
    }

    private static string SerializeItem(Item item, Inventory inventory)
    {
        string output = "\t";

        output += $"ITEM: {item.GetType().ToString()} ";

        output += "{ ";

        if (item.Id != null)
            output += $"UUID: {item.Id}, ";

        if (item.GetSlotID() > -1)
            output += $"SlotID: {item.GetSlotID().ToString()}, ";

        if (item.IsBreakable)
            output += $"RemainingUses: {item.RemainingUses.ToString()}, ";

        output = output.TrimEnd(',', ' ');

        output += " }";

        return output;
    }

    public static void Update(string path, Inventory inventory, Item item)
    {
        string[] lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains($"ITEM: {item?.GetType()}") && lines[i].Contains($"{item?.Id}"))
            {
                lines[i] = SerializeItem(item, inventory);
                break;
            }
        }

        File.WriteAllLines(path, lines);
    }

    public static void Delete(string path, Item item, Inventory inventory)
    {
        string[] lines = File.ReadAllLines(path);
        int index = Array.FindIndex(lines, line => line.Contains($"UUID: {item.Id}"));

        if (index != -1)
        {
            List<string> updatedLines = new List<string>(lines);
            updatedLines.RemoveAt(index);

            updatedLines[^1] = $"# {GetNumberOfItemsSaved(updatedLines.ToArray()).ToString()} items saved for entity \"{inventory.id}\".";

            File.WriteAllLines(path, updatedLines);
        }
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

                // Read UUID
                if (line.Contains("UUID"))
                {
                    start = line.IndexOf("UUID:") + "UUID:".Length + 1;
                    length = line.IndexOf(',') - start;

                    item.UUID = line.Substring(start, length).Trim(',', ' ');
                }

                // Read SlotID
                if (line.Contains("SlotID"))
                {
                    start = line.IndexOf("SlotID:") + "SlotID:".Length + 1;
                    length = 2;

                    item.slotID = int.Parse(line.Substring(start, length).Trim(',', ' '));
                }

                // Read RemainingUses
                if (line.Contains("RemainingUses"))
                {
                    start = line.IndexOf("RemainingUses:") + "RemainingUses:".Length + 1;
                    length = 1;

                    item.RemainingUses = int.Parse(line.Substring(start, length).Trim(',', ' '));
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
    public string UUID;
    public int slotID;
    public int RemainingUses;
}
