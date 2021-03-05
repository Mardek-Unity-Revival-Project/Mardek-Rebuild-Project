using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(ScriptableObject),true)]
public class ScriptableObjectDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight;
        if (property.objectReferenceValue == null)
        {
            //height += EditorGUIUtility.singleLineHeight;
            return height;
        }

        SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);
        SerializedProperty field = targetObject.GetIterator();
        field.NextVisible(true);

        while (field.NextVisible(false))
        {
            height += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // rect will be the work rect for the various elements in the drawers, it starts as a copy of position.
        Rect rect = new Rect(position);
        rect.height = EditorGUIUtility.singleLineHeight;
        //EditorGUI.DrawRect(rect, new Color(1, 0, 0, 0.2f));
        EditorGUI.BeginProperty(rect, label, property);

        // Draw SO label
        rect.width = EditorGUIUtility.labelWidth;
        EditorGUI.LabelField(rect, label);
        rect.x += rect.width;

        // Get referenced SO's type
        System.Type type = GetSOType(property);

        // Style for new and save buttons
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        //rect = EditorGUI.IndentedRect(rect); //indent the buttons and object holder
        rect.width = 45; // width for new/save buttons

        if (property.objectReferenceValue == null)
        {
            // draw "New" button
            if (GUI.Button(rect, new GUIContent("New"), buttonStyle))
            {
                NewSOWindow.Open(type, ref property);
            }
            rect.x += rect.width;
        }
        else
        {
            // check if the referenced SO is an asset
            string path = AssetDatabase.GetAssetPath(property.objectReferenceValue);
            if (path == "" || string.IsNullOrEmpty(path))
            {
                // draw "Save" button
                if (GUI.Button(rect, new GUIContent("Save"), buttonStyle))
                {
                    NewSOWindow.SaveSOToAsset((ScriptableObject)property.objectReferenceValue);
                }
                rect.x += rect.width;
            }
        }
        
        // Draw SO object holder/picker
        rect.xMax = position.xMax;
        int ignoredIndentLevel = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        property.objectReferenceValue = EditorGUI.ObjectField(rect, property.objectReferenceValue, type, false);
        EditorGUI.indentLevel = ignoredIndentLevel;

        // draw SO's children properties
        if (property.objectReferenceValue != null)
        {
            
            SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);
            SerializedProperty field = targetObject.GetIterator();
            field.NextVisible(true);

            rect.x = position.x;
            rect.width = position.width;
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.indentLevel++;

            //Color randColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), .2f);
            while (field.NextVisible(false))
            {
                //EditorGUI.DrawRect(rect, randColor);
                EditorGUI.PropertyField(rect, field, true);
                rect.y += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
            }
            EditorGUI.indentLevel--;
            targetObject.ApplyModifiedProperties();
        }

        EditorGUI.EndProperty();
    }


    // modified from https://answers.unity.com/questions/1347203/a-smarter-way-to-get-the-type-of-serializedpropert.html
    private System.Type GetSOType(SerializedProperty property)
    {
        var parent = property.serializedObject.targetObject.GetType();
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        var path = property.propertyPath;
        var fi = parent.GetField(path, flags);
        var paths = property.propertyPath.Split('.');

        for (int i = 0; i < paths.Length; i++)
        {
            fi = parent.GetField(paths[i], flags);

            if(fi == null) 
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
