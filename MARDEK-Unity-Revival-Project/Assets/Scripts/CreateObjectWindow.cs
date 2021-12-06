#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CreateObjectWindow : EditorWindow
{
    static List<Type> types;
    static SerializedProperty property;
    static CreateObjectWindow window;
    UnityEngine.Object[] objects;

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        if (types != null)
        {
            foreach (Type type in types)
            {
                if (GUILayout.Button(new GUIContent(type.Name)))
                {
                    AssignNewInstance(property, type);
                    Close();
                    // TODO: remove this quickfix when a list field stop not updating itself
                    // DeselectThenReselectToRefreshInspector();
                }
            }
        }
        EditorGUILayout.EndVertical();
    }

    void DeselectThenReselectToRefreshInspector()
    {
        objects = Selection.objects;
        Selection.objects = null;
        EditorApplication.delayCall += ReselectObjects;
    }

    void ReselectObjects()
    {
        Selection.objects = objects;
        EditorApplication.delayCall -= ReselectObjects;
    }

    static void AssignNewInstance(SerializedProperty property, Type type)
    {
        if(type.IsSubclassOf(typeof(ScriptableObject)))
        {
            ScriptableObject newSO = CreateInstance(type); 
            newSO.name = "(Instance)" + newSO.name;
            property.objectReferenceValue = newSO;
            property.serializedObject.ApplyModifiedProperties(); 
            if (property.objectReferenceValue == null)
            {
                // couldn't hold SO instance, save it to a .asset
                Debug.LogWarning("An asset can't hold a reference to a non-asset instance (Type Mismatch), saving the created object as asset first");
                SaveSOToAsset(newSO);
                property.objectReferenceValue = newSO;
            }
        }
        else
        {
            if(property.propertyType == SerializedPropertyType.ManagedReference)
                property.managedReferenceValue = Activator.CreateInstance(type);
            else
                Debug.LogError($"can't assing managed value of {property.propertyType} type property");
        }
        property.serializedObject.ApplyModifiedProperties();
        property.serializedObject.Update();
    }

    public static void SaveSOToAsset(ScriptableObject so)
    {
        //string prefabPath = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage()?.assetPath;
        //if (string.IsNullOrEmpty(prefabPath) == false)
        //{
        //    Debug.Log($"saving SO to prefab: {prefabPath}");
        //    //so.hideFlags = HideFlags.HideInHierarchy;
        //    AssetDatabase.AddObjectToAsset(so, prefabPath);
        //    return;
        //}
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
                window = CreateInstance<CreateObjectWindow>();
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
#endif