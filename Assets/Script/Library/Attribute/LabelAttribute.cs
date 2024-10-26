using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�\������u������Attribute</summary>
[AttributeUsage(AttributeTargets.Field)]
public class LabelAttribute : PropertyAttribute
{
    /// <summary>�u���e�L�X�g</summary>
    public readonly string Text;

    /// <param name="text">�u���e�L�X�g</param>
    public LabelAttribute(string text) => Text = text;
}

/// <summary>�\������"�ϐ��� : �e�L�X�g"�ɒu������Attribute</summary>
[AttributeUsage(AttributeTargets.Field)]
public class AddLabelAttribute : LabelAttribute
{
    /// <param name="text">�ǉ��e�L�X�g</param>
    public AddLabelAttribute(string text) : base($" : {text}") { }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LabelAttribute))]
[CustomPropertyDrawer(typeof(AddLabelAttribute))]
public class LabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var label = attribute as LabelAttribute;

        var addlabel = label as AddLabelAttribute;

        l.text = addlabel == null ? label.Text : l.text + addlabel.Text;

        EditorGUI.PropertyField(r, p, l);
    }
}
#endif
