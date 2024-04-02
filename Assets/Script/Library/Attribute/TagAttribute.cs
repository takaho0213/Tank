using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�^�O��pUI��\�������鑮��</summary>
[AttributeUsage(AttributeTargets.Field)]
public class TagAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.String)
        {
            p.stringValue = EditorGUI.TagField(r, l, p.stringValue);
        }
    }
}
#endif
