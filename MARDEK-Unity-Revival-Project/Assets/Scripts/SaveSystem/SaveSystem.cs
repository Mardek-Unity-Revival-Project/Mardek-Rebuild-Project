using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FullSerializer;

public static class SaveSystem
{
    static Dictionary<Guid, string> guidObjectMap = new Dictionary<Guid, string>();
    static fsSerializer serializer = new fsSerializer();
    static bool saveOrLoadOutsidePlaymode = false;
    static string persistentPath
    {
        get
        {
            string path;
#if UNITY_WEBGL
            path = System.IO.Path.Combine("/idbfs", Application.productName);
#else
            path = Application.persistentDataPath;
#endif
            return path;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        guidObjectMap.Clear();
    }

    public static string PrintObjectMap()
    {
        serializer.TrySerialize(guidObjectMap, out fsData data);
        return fsJsonPrinter.PrettyJson(data);
    }
    public static void SaveObject(IAddressableGuid addressable)
    {
        if (saveOrLoadOutsidePlaymode == false && Application.isPlaying == false)
            return;
        Guid guid = addressable.GetGuid();
        if (guidObjectMap.ContainsKey(guid) == false)
        {
            guidObjectMap.Add(guid, string.Empty);
        }
        serializer.TrySerialize(addressable.GetType(), addressable, out fsData data);
        guidObjectMap[guid] = fsJsonPrinter.CompressedJson(data);
    }
    public static void LoadObject(IAddressableGuid addressable)
    {
        if (saveOrLoadOutsidePlaymode == false && Application.isPlaying == false)
            return;
        Guid guid = addressable.GetGuid();
        if (guidObjectMap.ContainsKey(guid) == false)
            return;
        guidObjectMap.TryGetValue(guid, out string json);
        JsonUtility.FromJsonOverwrite(json, addressable);
    }

    public static void SaveToFile(string fileName = "quicksave.json")
    {
        string filePath = System.IO.Path.Combine(persistentPath, fileName);
        string contents = PrintObjectMap();
        System.IO.File.WriteAllText(filePath, contents);
        Debug.Log($"saved file to {filePath}");
    }
    public static void LoadFromFile(string fileName = "quicksave.json")
    {
        string filePath = System.IO.Path.Combine(persistentPath, fileName); // TODO: check if path exists
        string contents = System.IO.File.ReadAllText(filePath);
        fsJsonParser.Parse(contents, out fsData data);
        serializer.TryDeserialize(data, ref guidObjectMap);
        Debug.Log($"loaded file from {filePath}");
    }
}