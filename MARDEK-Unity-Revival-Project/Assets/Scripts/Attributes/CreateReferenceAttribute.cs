
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

    public CreateReferenceAttribute(Type fieldType =  null)
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
        // Get referenced object type
        Type type;

        type = (attribute as CreateReferenceAttribute).type;

        if (type == null)
            type = GetReferenceType(property);

        if(type == null)
        {
            Debug.LogError("type is null");
            return;
        }
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

        if (true)   //property.objectReferenceValue == null)
        {
            // draw "New" button
            if (GUI.Button(rect, new GUIContent("New"), buttonStyle))
            {
                CreateObjectWindow.Open(type, ref property);
            }
            rect.x += rect.width;
        }

        if (type.IsSubclassOf(typeof(UnityEngine.Object)))
        {
            //Draw object holder/ picker
            rect.xMax = position.xMax;
            int ignoredIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            property.objectReferenceValue = EditorGUI.ObjectField(rect, property.objectReferenceValue, type, false);
            EditorGUI.indentLevel = ignoredIndentLevel;
        }
        else
        {
            rect.xMax = position.xMax;
            rect.xMin = position.xMin;
            EditorGUI.PropertyField(rect, property, true);
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