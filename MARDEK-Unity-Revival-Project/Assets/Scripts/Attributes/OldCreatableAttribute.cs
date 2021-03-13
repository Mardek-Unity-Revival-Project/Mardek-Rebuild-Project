using UnityEngine;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class OldOldCreatableAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(OldOldCreatableAttribute), true)]
public class CreatableObjectDrawer : PropertyDrawer
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

    // Cached scriptable object editor
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // rect will be the work rect for the various elements in the drawers, it starts as a copy of position.
        Rect rect = new Rect(position);
        rect.height = EditorGUIUtility.singleLineHeight;
        //EditorGUI.DrawRect(rect, new Color(1, 0, 0, 0.2f));
        EditorGUI.BeginProperty(rect, label, property);

        // Draw object label
        rect.width = EditorGUIUtility.labelWidth;
        EditorGUI.LabelField(rect, label);
        rect.x += rect.width;

        // Get referenced object type
        System.Type type = GetSOType(property);

        // Style for new and save buttons
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        //rect = EditorGUI.IndentedRect(rect); //indent the buttons and object holder
        rect.width = 45; // width for new/save buttons

        if (property.objectReferenceValue as Object == null)
        {
            // draw "New" button
            if (GUI.Button(rect, new GUIContent("New"), buttonStyle))
            {
                CreateObjectWindow.Open(type, ref property);
            }
            rect.x += rect.width;
        }
        else
        {
            if(property.objectReferenceValue is ScriptableObject)
            {
                // check if the referenced SO is an asset
                string path = AssetDatabase.GetAssetPath(property.objectReferenceValue);
                if (string.IsNullOrEmpty(path))
                {
                    // draw "Save" button
                    if (GUI.Button(rect, new GUIContent("Save"), buttonStyle))
                    {
                        CreateObjectWindow.SaveSOToAsset((ScriptableObject)property.objectReferenceValue);
                    }
                    rect.x += rect.width;
                }
            }
        }

        //Draw object holder/ picker
        rect.xMax = position.xMax;
        int ignoredIndentLevel = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        property.objectReferenceValue = EditorGUI.ObjectField(rect, property.objectReferenceValue, type, false);
        EditorGUI.indentLevel = ignoredIndentLevel;

        // draw children properties

        if (property.objectReferenceValue != null)
        {
            var data = (ScriptableObject)property.objectReferenceValue;
            var propertyObject = new SerializedObject(data);

            //var propertyObject = new SerializedObject(property.objectReferenceValue);

            var field = propertyObject.GetIterator();
            field.Next(true);
            field.NextVisible(false);

            rect.x = position.x;
            rect.width = position.width;
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.indentLevel++;

            while (field.NextVisible(false))
            {
                EditorGUI.PropertyField(rect, field);
                rect.y += EditorGUI.GetPropertyHeight(field) + EditorGUIUtility.standardVerticalSpacing;
            }
            propertyObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;
        }

        if (GUI.changed)
            property.serializedObject.ApplyModifiedProperties();
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
