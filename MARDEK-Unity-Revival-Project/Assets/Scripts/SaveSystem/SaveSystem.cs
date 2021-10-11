using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FullSerializer;
using System.Text.RegularExpressions;

public class SaveSystem: MonoBehaviour
{
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
    static bool formatSaveFiles = true;
    const string formatterDataFieldName = "\"jsonData\": ";

    class AddresableSaveWrapper {
        public string name = default;
        public string jsonData = default;
    }
    static Dictionary<Guid, AddresableSaveWrapper> loadedAddressables = new Dictionary<Guid, AddresableSaveWrapper>();
    static fsSerializer serializer = new fsSerializer();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        loadedAddressables.Clear();
    }

    public static void SaveToFile(string fileName = "quicksave.json")
    {
        serializer.TrySerialize(loadedAddressables, out fsData data);
        string json = fsJsonPrinter.PrettyJson(data);
        if(formatSaveFiles)
            json = FormatSaveFile(json);
        string filePath = System.IO.Path.Combine(persistentPath, fileName);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log($"Game file saved to {filePath}");
    }
    public static void LoadFromFile(string fileName = "quicksave.json")
    {
        string filePath = System.IO.Path.Combine(persistentPath, fileName); // TODO: check if path exists
        string json = System.IO.File.ReadAllText(filePath);
        if (formatSaveFiles)
            json = FormatLoadFile(json);
        fsJsonParser.Parse(json, out fsData data);
        serializer.TryDeserialize(data, ref loadedAddressables);
        Debug.Log($"Game file loaded from {filePath}");
    }
    
    static string FormatSaveFile(string content)
    {
        string result = default;
        while (true)
        {
            var Separatorindex = content.IndexOf(formatterDataFieldName);
            if (Separatorindex == -1)
            {
                result += content;
                break;
            }
            string beforeSeparator = content.Substring(0, Separatorindex + formatterDataFieldName.Length);
            string AfterSeparator = content.Substring(Separatorindex + formatterDataFieldName.Length);
            int jsonStringIndex = AfterSeparator.IndexOf(Environment.NewLine);
            string jsonString = AfterSeparator.Substring(0, jsonStringIndex);
            content = AfterSeparator.Substring(jsonStringIndex);

            result += beforeSeparator;
            jsonString = jsonString.Substring(1, jsonString.Length - 2); // remove first and last '"'
            result += Regex.Unescape(jsonString); //remove escape characters
        }
        return result;
    }
    static string FormatLoadFile(string content)
    {
        string result = default;
        while (true)
        {
            var Separatorindex = content.IndexOf(formatterDataFieldName);
            if (Separatorindex == -1)
            {
                result += content;
                break;
            }

            string beforeSeparator = content.Substring(0, Separatorindex + formatterDataFieldName.Length);
            string AfterSeparator = content.Substring(Separatorindex + formatterDataFieldName.Length);
            int jsonStringIndex = AfterSeparator.IndexOf(Environment.NewLine);
            string jsonString = AfterSeparator.Substring(0, jsonStringIndex);
            content = AfterSeparator.Substring(jsonStringIndex);

            result += beforeSeparator;
            jsonString = "\"" + jsonString.Replace("\"", "\\\"") + "\""; // undo Regex.Unescape() made in FormatSaveFile
            result += jsonString;
        }
        return result;
    }
    
    public static void SaveObject(IAddressableGuid addressable)
    {        
        if (Application.isPlaying == false)
            throw new Exception("Don't Save while outside playmode");

        Guid guid = addressable.GetGuid();
        if (loadedAddressables.ContainsKey(guid) == false)
            loadedAddressables.Add(guid, null);

        serializer.TrySerialize(addressable.GetType(), addressable, out fsData data);
        var json = fsJsonPrinter.CompressedJson(data);
        var newWrapper = new AddresableSaveWrapper() { jsonData = json };
        loadedAddressables[guid] = newWrapper;
    }
    public static void LoadObject(IAddressableGuid addressable)
    {
        if (Application.isPlaying == false)
            throw new Exception("Don't Load while outside playmode");

        Guid guid = addressable.GetGuid();
        if (loadedAddressables.ContainsKey(guid))
        {
            // Addressable found, override object from json
            loadedAddressables.TryGetValue(guid, out AddresableSaveWrapper wrappedAddressable);
            JsonUtility.FromJsonOverwrite(wrappedAddressable.jsonData, addressable);
        }
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
}