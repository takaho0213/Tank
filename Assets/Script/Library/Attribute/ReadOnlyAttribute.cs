using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>読み取り専用にする属性(Ctrlを押している間は編集可能)</summary>
[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        l.tooltip = "Ctrlを押している間は編集可能";

        EditorGUI.BeginDisabledGroup(!Drawer.IsDefault(p) && !Drawer.IsControlKey);
        EditorGUI.PropertyField(r, p, l);
        EditorGUI.EndDisabledGroup();
    }
}
#endif
