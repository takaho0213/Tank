using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>•¶š—ñ‚ğ‰B‚·‘®«</summary>
[AttributeUsage(AttributeTargets.Field)]
public class PasswordAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(PasswordAttribute))]
public class PasswordDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.String)
        {
            p.stringValue = EditorGUI.PasswordField(r, l, p.stringValue);
        }
    }
}
#endif
