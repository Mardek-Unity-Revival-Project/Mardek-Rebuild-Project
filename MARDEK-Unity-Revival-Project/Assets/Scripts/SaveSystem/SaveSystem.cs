using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FullSerializer;
using System.Text.RegularExpressions;

public class SaveSystem: MonoBehaviour
{
    class SaveWrapper {
        public string name = default;
        public string data = default;
    }

    static Dictionary<Guid, SaveWrapper> guidObjectMap = new Dictionary<Guid, SaveWrapper>();
    static fsSerializer serializer = new fsSerializer();
    static string jsonSeparator = "\"data\": ";

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
        //Debug.LogWarning("Clearing guid map");
        guidObjectMap.Clear();
        //LoadFromFile();
    }

    public static void SaveObject(IAddressableGuid addressable)
    {        
        if (saveOrLoadOutsidePlaymode == false && Application.isPlaying == false)
            return;
        Guid guid = addressable.GetGuid();
        if (guidObjectMap.ContainsKey(guid) == false)
            guidObjectMap.Add(guid, new SaveWrapper());

        serializer.TrySerialize(addressable.GetType(), addressable, out fsData data);
        var contents = fsJsonPrinter.CompressedJson(data);
        var newWrapper = new SaveWrapper() {
            data = contents,
            //fsData = data.AsDictionary
        };
        guidObjectMap[guid] = newWrapper;
    }
    public static void LoadObject(IAddressableGuid addressable)
    {
        if (saveOrLoadOutsidePlaymode == false && Application.isPlaying == false)
            return;
        Guid guid = addressable.GetGuid();
        if (guidObjectMap.ContainsKey(guid) == false)
            return;
        guidObjectMap.TryGetValue(guid, out SaveWrapper wrappedObj);
        JsonUtility.FromJsonOverwrite(wrappedObj.data, addressable);
    }

    [ContextMenu("SaveToFile")]
    public void Save()
    {
        SaveToFile();
    }
    [ContextMenu("LoadFromFile")]
    public void Load()
    {
        LoadFromFile();
    }

    public void SaveToFile(string fileName = "quicksave.json")
    {
        fsData data;
        serializer.TrySerialize(guidObjectMap, out data);
        string contents = fsJsonPrinter.PrettyJson(data);
        contents = FormatSaveFile(contents);

        string filePath = System.IO.Path.Combine(persistentPath, fileName);
        System.IO.File.WriteAllText(filePath, contents);
        Debug.Log($"saved file to {filePath}");
    }
    public static void LoadFromFile(string fileName = "quicksave.json")
    {
        string filePath = System.IO.Path.Combine(persistentPath, fileName); // TODO: check if path exists
        string content = System.IO.File.ReadAllText(filePath);
        content = FormatLoadFile(content);
        //Debug.Log(content);
        fsJsonParser.Parse(content, out fsData data);
        serializer.TryDeserialize(data, ref guidObjectMap);
        //foreach (var pair in guidObjectMap)
        //    Debug.Log($"{pair.Key} :: {pair.Value.data}");
        Debug.Log($"loaded file from {filePath}");
    }
    
    static string FormatSaveFile(string content)
    {
        string result = default;
        while (true)
        {
            var Separatorindex = content.IndexOf(jsonSeparator);
            if (Separatorindex == -1)
            {
                result += content;
                break;
            }

            string beforeSeparator = content.Substring(0, Separatorindex + jsonSeparator.Length);
            string AfterSeparator = content.Substring(Separatorindex + jsonSeparator.Length);
            int jsonStringIndex = AfterSeparator.IndexOf(Environment.NewLine);
            string jsonString = AfterSeparator.Substring(0, jsonStringIndex);
            content = AfterSeparator.Substring(jsonStringIndex);

            result += beforeSeparator;
            jsonString = jsonString.Substring(1, jsonString.Length - 2); // remove first and last '"'
            result += Regex.Unescape(jsonString);
        }
        return result;
    }
    static string FormatLoadFile(string content)
    {
        string result = default;
        while (true)
        {
            var Separatorindex = content.IndexOf(jsonSeparator);
            if (Separatorindex == -1)
            {
                result += content;
                break;
            }

            string beforeSeparator = content.Substring(0, Separatorindex + jsonSeparator.Length);
            string AfterSeparator = content.Substring(Separatorindex + jsonSeparator.Length);
            int jsonStringIndex = AfterSeparator.IndexOf(Environment.NewLine);
            string jsonString = AfterSeparator.Substring(0, jsonStringIndex);
            content = AfterSeparator.Substring(jsonStringIndex);

            result += beforeSeparator;
            jsonString = "\"" + jsonString.Replace("\"", "\\\"") + "\""; // undo Regex.Unescape() made in FormatSaveFile
            result += jsonString;
        }
        return result;
    }
}