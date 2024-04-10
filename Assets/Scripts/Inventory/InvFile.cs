using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public static class InvFile
{
    public static void Save(string path, Inventory inventory)
    {
        //Debug.Log($"Saving <b>inventory ({inventory.id})</b> to <b>{path}</b>");

        //FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        //StreamWriter stw = new(fs);

        //fs.SetLength(0);
        //stw.WriteLine($"INVENTORY: {inventory.id}");

        //foreach (var item in inventory.items)
        //{
        //    if (item.GetSlot().name == null)
        //        continue;

        //    string line = "\t";

        //    line += $"ITEM: {item.GetType()}";
        //    CreateInvString('|', "eiwfnoe", "efubie");

        //    stw.WriteLine();

        //    stw.WriteLine($"\t\tSLOT_ID: {(item.GetSlot() == null ? $"UNKNOW_SLOT" : item.GetSlotID())}");

        //    Debug.Log($"<b>[SAVED DATA]</b> <color=#FF00FF>{item.Name}</color>");
        //}

        //stw.Close();
        //fs.Close();

        using (StreamWriter sw = new StreamWriter(path))
        {
            // File header
            //sw.WriteLine($"# This file has been automatically generated on {DateTime.Today:yyyy-MM-dd} at {DateTime.Now:HH:mm:ss}.\n# It contains inventory data for entity \"{inventory.id}\".\n");

            sw.WriteLine($"INVENTORY_ID: \"{inventory.id}\"");
        }
    }

    //private static string CreateInvString(char devider, params string[] args)
    //{
    //    //string output = "";
    //    //foreach (var item in args)
    //    //    if (item == devider)

    //    //    output += item.ToString();
        
    //    //return output;
    //}

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
