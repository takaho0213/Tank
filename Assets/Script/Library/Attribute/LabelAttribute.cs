using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�\������u������Attribute</summary>
[AttributeUsage(AttributeTargets.Field)]
public class LabelAttribute : PropertyAttribute
{
    public readonly string Text;

    public LabelAttribute(string text) => Text = text;
}

/// <summary>�\������"�ϐ��� : �e�L�X�g"�ɒu������Attribute</summary>
[AttributeUsage(AttributeTargets.Field)]
public class AddLabelAttribute : LabelAttribute
{
    public AddLabelAttribute(string text) : base($" : {text}") { }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LabelAttribute))]
[CustomPropertyDrawer(typeof(AddLabelAttribute))]
public class LabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var a = (LabelAttribute)attribute;

        var add = a as AddLabelAttribute;

        l.text = add == null ? a.Text : l.text + add.Text;

        EditorGUI.PropertyField(r, p, l);
    }
}
#endif
