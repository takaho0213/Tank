using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�ǂݎ���p�ɂ��鑮��(Ctrl�������Ă���Ԃ͕ҏW�\)</summary>
[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        l.tooltip = "Ctrl�������Ă���Ԃ͕ҏW�\";

        EditorGUI.BeginDisabledGroup(!Drawer.IsDefault(p) && !Drawer.IsControlKey);
        EditorGUI.PropertyField(r, p, l);
        EditorGUI.EndDisabledGroup();
    }
}
#endif
