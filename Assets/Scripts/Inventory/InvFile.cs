using System;
using System.Collections.Generic;
using System.IO;

public static class InvFile
{ 
    public static void Save(string path, Inventory inventory)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            // Header
            sw.WriteLine($"# This file has been automatically generated on {DateTime.Today:yyyy-MM-dd} at {DateTime.Now:HH:mm:ss}.\n# It contains inventory data for entity \"{inventory.id}\".\n");

            // Saved Data
            sw.WriteLine($"INVENTORY_ID: \"{inventory.id}\"");
            foreach (var item in inventory.items)
            {
                sw.WriteLine($"\t{item.ToString()}");
            }

            // Footer
            sw.WriteLine($"");
        }
    }

    public static List<RetrievedItems> Load(string path, Inventory inventory)
    {
        List<RetrievedItems> items = new List<RetrievedItems>();
        var lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i++)
        {
            string s_type = default;
            int slot = -2;
            System.Type type = default;

            RetrievedItems retrievedItems = new RetrievedItems();

            //if (lines[i].ToString().Contains("TYPE"))
            //{
            //    s_type = ReadData<string>("TYPE", lines[i]);
            //    retrievedItems.type = System.Type.GetType(s_type);


            //    retrievedItems.slotID = ReadData<int>("SLOT_ID", lines[i + 1]);
            //    Debug.Log($"<color=#00FF00>{retrievedItems.type}</color> {{{retrievedItems.slotID}}}");
            //}

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
public class RetrievedItems
{
    public System.Type type;
    public int slotID;
    public int quantity;
}
