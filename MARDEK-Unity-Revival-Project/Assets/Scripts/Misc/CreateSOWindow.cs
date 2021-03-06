using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CreateSOWindow : EditorWindow
{
    static List<Type> types;
    static SerializedProperty property;
    static CreateSOWindow window;

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        if (types != null)
        {
            foreach (Type type in types)
            {
                if (GUILayout.Button(new GUIContent(type.Name)))
                {
                    ScriptableObject newSO = AssignNewInstance(property, type);
                    if (property.objectReferenceValue == null)
                    {
                        // couldn't hold SO instance, save it to a .asset
                        Debug.LogWarning("An asset can't hold a reference to a non-asset instance (Type Mismatch), saving the created object as asset first");
                        SaveSOToAsset(newSO);
                        property.objectReferenceValue = newSO;
                    }
                    property.serializedObject.ApplyModifiedProperties();
                    Close();
                }
            }
        }
        EditorGUILayout.EndVertical();
    }

    static ScriptableObject AssignNewInstance(SerializedProperty property, Type type)
    {
        ScriptableObject newSO = CreateInstance(type);
        newSO.name = "(Instance)" + newSO.name;
        property.objectReferenceValue = newSO;
        property.serializedObject.ApplyModifiedProperties();
        return newSO;
    }

    public static void SaveSOToAsset(ScriptableObject so)
    {
        string path = EditorUtility.SaveFilePanelInProject(so.name, $"New {so.GetType().Name}", "asset", "save scriptable object as asset");
        if (path.Length != 0)
        {
            AssetDatabase.CreateAsset(so, path);
        }
    }

    public static void Open(Type type, ref SerializedProperty serializedProperty)
    {
        if (type == typeof(ScriptableObject))
        {
            Debug.LogError("Creation of type 'ScriptableObject' not implemented");
        }
        else
        {
            property = serializedProperty;
            types = AppDomain.CurrentDomain.GetAllDerivedTypes(type);
            if(types.Count == 0)
            {
                Debug.LogError("No concrete types found for this field");
            }
            else if(types.Count == 1)
            {
                AssignNewInstance(property, types[0]);
            }
            else
            {
                if (window)
                    window.Close();
                window = CreateInstance<CreateSOWindow>();
                window.ShowUtility();
            }
        }
    }
}

public static class ReflectionHelpers
{
    public static List<Type> GetAllDerivedTypes(this AppDomain aAppDomain, Type aType)
    {
        var result = new List<Type>();
        var assemblies = aAppDomain.GetAssemblies();

        if(!aType.IsAbstract)
            result.Add(aType);
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(aType) && !type.IsAbstract)
                    result.Add(type);                
            }
        }
        return result;
    }
}
