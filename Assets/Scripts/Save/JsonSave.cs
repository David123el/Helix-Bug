using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

public static class JsonSave
{
    public static void Save(object o, string path)
    {
        string json = JsonUtility.ToJson(o);

        //StreamWriter sw = File.CreateText(path);
        //sw.Close();

        File.WriteAllText(path, json);
    }

    public static void Save(IEnumerable collection, string path)
    {
        string json = "";

        foreach (object item in collection)
        {
            json += JsonUtility.ToJson(item) + "\n";
        }

        //StreamWriter sw = File.CreateText(path);
        //sw.Close();

        File.WriteAllText(path, json);
    }

    public static void Load(string path, ref object o)
    {
        string json = File.ReadAllText(path);

        o = JsonUtility.FromJson<object>(json);
    }

    public static void Load<T>(string path, ref T t)
    {
        string json = File.ReadAllText(path);

        t = JsonUtility.FromJson<T>(json);
    }

    public static void Load<T>(string path, List<T> list)
    {
        list.Clear();
        string json = File.ReadAllText(path);

        var c = json.Split('\n');
        foreach (var item in c)
        {
            if (item != string.Empty)
            {
                var temp = JsonUtility.FromJson<T>(item);
                list.Add(temp);
            }
        }
    }

    public static void Unload<T>(string path, List<T> list)
    {
        list.Clear();
        File.WriteAllText(path, string.Empty);
    }
}
