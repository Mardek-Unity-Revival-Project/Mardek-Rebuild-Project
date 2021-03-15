
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using System.Reflection;

public class CreateReferenceAttribute : PropertyAttribute {

    public Type type = null;

    public CreateReferenceAttribute(Type fieldType)
    {
        type = fieldType;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(CreateReferenceAttribute), true)]
public class CreateReference : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = 0;
        height += EditorGUI.GetPropertyHeight(property);
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(property.propertyType != SerializedPropertyType.ManagedReference)
        {
            Debug.LogWarning("trying to use create CreateReferenceAttribute in a non-managedReference property");
            base.OnGUI(position, property, label);
            return;
        }
        // Get referenced object type
        Type baseType = (attribute as CreateReferenceAttribute).type;
        
        // rect will be the work rect for the various elements in the drawers, it starts as a copy of position.
        Rect rect = new Rect(position);
        rect.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.BeginProperty(rect, label, property);

        // Draw object label
        rect.width = EditorGUIUtility.labelWidth;
        EditorGUI.LabelField(rect, label);
        rect.x += rect.width;

        // Style for new and save buttons
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        rect.width = 45; // width for new/save buttons

        // draw "New" button
        if (true)
        {
            if (GUI.Button(rect, new GUIContent("New"), buttonStyle))
            {
                CreateObjectWindow.Open(baseType, ref property);
                property.isExpanded = true;
            }
            rect.x += rect.width;
        }

        rect.xMax = position.xMax;

        var typeStr = "None";
        SerializedProperty prop = property.serializedObject.GetIterator();
        do
        {
            if (prop.propertyPath == property.propertyPath)
            {
                string[] fullPath = prop.managedReferenceFullTypename.Split(' ');
                string path = fullPath[fullPath.Length - 1];
                if (string.IsNullOrEmpty(path) == false)
                {
                    typeStr = path;
                    break;
                }
            }
        }
        while (prop.NextVisible(true));

        rect.xMin += 10;
        EditorGUI.LabelField(rect, typeStr);
        rect.xMin = position.xMin;

        try 
        { 
            EditorGUI.PropertyField(rect, property, true); 
        }
        catch (Exception e)
        {
            if(e.Message == "Queue empty.")
            {
                // ignore unity inspector bug
            }
            else
            {
                throw e;
            }
        }

        if (GUI.changed)
            property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();
    }

    // modified from https://answers.unity.com/questions/1347203/a-smarter-way-to-get-the-type-of-serializedpropert.html
    private Type GetReferenceType(SerializedProperty property)
    {
        var parent = property.serializedObject.targetObject.GetType();
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        var path = property.propertyPath;
        var fi = parent.GetField(path, flags);
        var paths = property.propertyPath.Split('.');

        for (int i = 0; i < paths.Length; i++)
        {
            fi = parent.GetField(paths[i], flags);

            if (fi == null)
                return null;

            if (fi.FieldType.IsArray)
            {
                parent = fi.FieldType.GetElementType();
                i += 2;
                continue;
            }

            if (fi.FieldType.IsGenericType)
            {
                parent = fi.FieldType.GetGenericArguments()[0];
                i += 2;
                continue;
            }

            parent = fi.FieldType;
        }

        if (fi.FieldType.IsGenericType) return fi.FieldType.GetGenericArguments()[0];
        if (fi.FieldType.IsArray) return fi.FieldType.GetElementType();
        return fi.FieldType;
    }
}
#endif