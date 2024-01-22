using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RandomManager
{
    public List<string> Export()
    {
        List<string> toExport = new List<string>();

        foreach(KeyValuePair<string, RandomItem> kvp in Instance.items)
            toExport.Add($"{kvp.Key}={kvp.Value}");

        return toExport;
    }

    public void Import(List<string> toImport, bool clear = true)
    {
        if (clear)
            Instance.items.Clear();

        foreach(string item in toImport)
        {
            string[] vals = item.Split(';');
            Instance.items.Add(vals[0], new RandomItem(int.Parse(vals[1]), int.Parse(vals[2])));
        }
    }
}