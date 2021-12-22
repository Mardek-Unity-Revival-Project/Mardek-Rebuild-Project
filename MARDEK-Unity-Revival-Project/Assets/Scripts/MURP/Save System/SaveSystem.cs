using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using FullSerializer;
using MURP.Core;

namespace MURP.SaveSystem
{
    public class SaveSystem : MonoBehaviour
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
        static fsSerializer serializer = new fsSerializer();
        public delegate void SaveCallback();
        public static event SaveCallback OnBeforeSave = delegate { };

        static SaveState currentSaveState;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialization()
        {
            currentSaveState = new SaveState();
        }
        
        public static void SaveObject(IAddressableGuid addressable)
        {
            if (Application.isPlaying == false)
                throw new Exception("Don't Save while outside playmode");
            currentSaveState.SaveObject(addressable, serializer);
            
        }
        public static bool LoadObject(IAddressableGuid addressable)
        {
            if (Application.isPlaying == false)
                throw new Exception("Don't Load while outside playmode");
            return currentSaveState.LoadObjects(addressable, serializer);
        }

        [ContextMenu("QuickSave")] void QuickSave() => SaveToFile();
        [ContextMenu("QuickLoad")] void QuickLoad() => LoadFromFile();

        public void SaveToFile(string fileName = "quicksave")
        {
            OnBeforeSave.Invoke();
            serializer.TrySerialize(currentSaveState.addressableState, out fsData data);
            string json = fsJsonPrinter.PrettyJson(data);
            if (formatSaveFiles)
                json = FormatSaveFile(json, true);
            string filePath = System.IO.Path.Combine(persistentPath, $"{fileName}.json");
            System.IO.File.WriteAllText(filePath, json);
            Debug.Log($"Game file saved to {filePath}");
        }
        public void LoadFromFile(string fileName = "quicksave")
        {
            currentSaveState = new SaveState();
            string filePath = System.IO.Path.Combine(persistentPath, $"{fileName}.json");
            string json = System.IO.File.ReadAllText(filePath);
            if (formatSaveFiles)
                json = FormatSaveFile(json, false);
            fsJsonParser.Parse(json, out fsData data);
            serializer.TryDeserialize(data, ref currentSaveState.addressableState);
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
                    result += content; // append the rest of the string
                    break;
                }

                string beforeSeparator = content.Substring(0, Separatorindex + formatterDataFieldName.Length);
                result += beforeSeparator;

                // get json object by outmost pair of balanced curly braces
                if (isSaving) Separatorindex++; // skip first '\"'
                string AfterSeparator = content.Substring(Separatorindex + formatterDataFieldName.Length);
                ParseCurlyBraces(AfterSeparator, out int startIndex, out int endIndex);
                string json = AfterSeparator.Substring(startIndex, endIndex - startIndex + 1);
                if (isSaving)
                    result += Regex.Unescape(json); // remove escape characters
                else
                    result += "\"" + json.Replace("\"", "\\\"") + "\""; // undo Regex.Unescape()

                // update content for next iteration
                if (isSaving) endIndex++; // skip last '\"'
                content = AfterSeparator.Substring(endIndex+1); 
            }
            return result;
        }
        static void ParseCurlyBraces(string content, out int startIndex, out int endIndex)
        {
            startIndex = 0;
            endIndex = content.Length - 1;
            bool isInsideQuotes = false;
            int curlyBracesDepth = 0;
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == '\"')
                {
                    isInsideQuotes = !isInsideQuotes;
                    continue;
                }
                if (isInsideQuotes)
                    continue;

                if (content[i] == '{')
                {
                    if (curlyBracesDepth == 0)
                        startIndex = i;
                    curlyBracesDepth++;
                }
                else if (content[i] == '}')
                {
                    curlyBracesDepth--;
                    if (curlyBracesDepth == 0)
                    {
                        endIndex = i;
                        break;
                    }
                }
            }
        }
    }
}