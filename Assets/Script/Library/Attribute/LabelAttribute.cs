using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>表示名を置換するAttribute</summary>
[AttributeUsage(AttributeTargets.Field)]
public class LabelAttribute : PropertyAttribute
{
    /// <summary>置換テキスト</summary>
    public readonly string Text;

    /// <param name="text">置換テキスト</param>
    public LabelAttribute(string text) => Text = text;
}

/// <summary>表示名を"変数名 : テキスト"に置換するAttribute</summary>
[AttributeUsage(AttributeTargets.Field)]
public class AddLabelAttribute : LabelAttribute
{
    /// <param name="text">追加テキスト</param>
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
