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

            sw.WriteLine($"\t\tSLOT_ID: {(item.GetSlot() == null ? $"UNKNOW_SLOT" : item.GetSlotID())}");
        }

        sw.Close();
    }

    public static List<RetrievedItems> Load(string path, Inventory inventory)
    {
        List<RetrievedItems> items = new List<RetrievedItems>();
        var lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i++)
        {
            string s_type = default, s_slotID = default;
            int slot = -2;
            System.Type type = default;

            RetrievedItems retrievedItems = new RetrievedItems();

            if (lines[i].ToString().Contains("ITEM_TYPE"))
            {
                var start = lines[i].ToString().IndexOf(":") + 1;

                s_type = lines[i].ToString().Substring(start, (lines[i].ToString().Length - start));

                type = System.Type.GetType(s_type);

                retrievedItems.type = type;

                if (lines[i + 1].ToString().Contains("SLOT_ID"))
                {
                    var start2 = lines[i + 1].ToString().IndexOf(":") + 1;

                    s_slotID = lines[i + 1].ToString().Substring(start2, (lines[i + 1].ToString().Length - start2));

                    retrievedItems.slotID = int.Parse(s_slotID);
                    Debug.Log($"<color=#FF0000>{retrievedItems.slotID}</color>");

                    if (retrievedItems.slotID < 0)
                        continue;

                    retrievedItems.slotID = slot;
                }
            }

            if (type != null)
            {
                var item = ItemManager.Instance.CreateItem(type);
                var slotToUse = default(Slot);

                foreach (var s in inventory.slots)
                {
                    if (retrievedItems.slotID == s.ID)
                    {
                        slotToUse = s.GetComponent<Slot>();
                        Debug.Log(slotToUse.name);
                    }
                }

                inventory.Add(item.GetComponent<Item>(), 1, slotToUse);
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
    public int slotID;
}
