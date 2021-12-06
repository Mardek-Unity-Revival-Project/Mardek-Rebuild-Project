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
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);
            return path;
        }
    }

    static bool formatSaveFiles = true;
    const string formatterDataFieldName = "\"jsonData\": ";
    [SerializeField] List<AddressableMonoBehaviour> objsToSaveBeforeSavingToFile = new List<AddressableMonoBehaviour>();

    class AddresableSaveWrapper {
        public string jsonData = default;
    }
    static Dictionary<Guid, AddresableSaveWrapper> loadedAddressables = new Dictionary<Guid, AddresableSaveWrapper>();
    static fsSerializer serializer = new fsSerializer();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        loadedAddressables.Clear();
    }

    public void SaveToFile(string fileName = "quicksave.json")
    {
        foreach (var o in objsToSaveBeforeSavingToFile)
            o.Save();

        serializer.TrySerialize(loadedAddressables, out fsData data);
        string json = fsJsonPrinter.PrettyJson(data);
        if(formatSaveFiles)
            json = FormatSaveFile(json, true);
        string filePath = System.IO.Path.Combine(persistentPath, fileName);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log($"Game file saved to {filePath}");
    }
    public static void LoadFromFile(string fileName = "quicksave.json")
    {
        string filePath = System.IO.Path.Combine(persistentPath, fileName);
        string json = System.IO.File.ReadAllText(filePath);
        if (formatSaveFiles)
            json = FormatSaveFile(json, false);
        fsJsonParser.Parse(json, out fsData data);
        serializer.TryDeserialize(data, ref loadedAddressables);
        Debug.Log($"Game file loaded from {filePath}");
    }
    
    static string FormatSaveFile(string content, bool isSaving)
    {
        string result = default;
        while (true)
        {
            var Separatorindex = content.IndexOf(formatterDataFieldName);
            if (Separatorindex == -1)
            {
                result += content; //append the rest of the string
                break;
            }
            string beforeSeparator = content.Substring(0, Separatorindex + formatterDataFieldName.Length);
            string AfterSeparator = content.Substring(Separatorindex + formatterDataFieldName.Length);
            int endOfJsonStringIndex = AfterSeparator.IndexOf(Environment.NewLine); //TODO: find end of json string in another way, maybe count "{" and "}"
            string jsonString = AfterSeparator.Substring(0, endOfJsonStringIndex);
            content = AfterSeparator.Substring(endOfJsonStringIndex);

            result += beforeSeparator;
            if (isSaving)
            {
                jsonString = jsonString.Substring(1, jsonString.Length - 2); // remove first and last '"'
                result += Regex.Unescape(jsonString); //remove escape characters
            }
            else //is Loading
            {
                jsonString = "\"" + jsonString.Replace("\"", "\\\"") + "\""; // undo Regex.Unescape() made in FormatSaveFile
                result += jsonString;
            }
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
    public static bool LoadObject(IAddressableGuid addressable)
    {
        if (Application.isPlaying == false)
            throw new Exception("Don't Load while outside playmode");

        Guid guid = (addressable).GetGuid();
        if (loadedAddressables.ContainsKey(guid))
        {
            // Addressable found, override object from json
            loadedAddressables.TryGetValue(guid, out AddresableSaveWrapper wrappedAddressable);
            fsJsonParser.Parse(wrappedAddressable.jsonData, out fsData data);
            var type = addressable.GetType();
            var obj = addressable as object;
            serializer.TryDeserialize(data, storageType: type, ref obj);
            //JsonUtility.FromJsonOverwrite(wrappedAddressable.jsonData, addressable);
            return true;
        }
        return false;
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