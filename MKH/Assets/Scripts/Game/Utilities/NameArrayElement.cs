using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor;

#if UNITY_EDITOR

public class NamedArrayElementAttribute : PropertyAttribute
{
    public string name;
    public NamedArrayElementAttribute(string name)
    {
        this.name = name;
    }
}

[CustomPropertyDrawer(typeof(NamedArrayElementAttribute))]
public class NamedArrayElementTitleDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    protected virtual NamedArrayElementAttribute Attribute => attribute as NamedArrayElementAttribute;
    SerializedProperty TitleNameProp;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string FullPathName = property.propertyPath + "." + Attribute.name;
        TitleNameProp = property.serializedObject.FindProperty(FullPathName);
        string newLabel = GetName();
        if (string.IsNullOrEmpty(newLabel))
            newLabel = label.text;
        EditorGUI.PropertyField(position, property, new GUIContent(newLabel, label.tooltip), true);
    }

    string GetName()
    {
        string name = "";
        switch (TitleNameProp.propertyType)
        {
            case SerializedPropertyType.Enum:
                name = TitleNameProp.enumNames[TitleNameProp.enumValueIndex];
                break;
            default:
                break;
        }

        return name;
    }
}

#endif