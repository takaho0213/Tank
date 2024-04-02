using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>îÕàÕÇ0Å`1ì‡Ç…êßå¿Ç∑ÇÈëÆê´</summary>
[AttributeUsage(AttributeTargets.Field)]
public class Range01Attribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Range01Attribute))]
public class Range01Drawer : PropertyDrawer
{
    private const int IMax = 1;

    private const float FMax = 1f;

    protected virtual int IntMax(Rect r, SerializedProperty p, GUIContent l) => IMax;

    protected virtual float FloatMax(Rect r, SerializedProperty p, GUIContent l) => FMax;

    protected void Float(Rect r, SerializedProperty p, GUIContent l)
    {
        EditorGUI.Slider(r, p, default, FloatMax(r, p, l), l);
    }

    protected void Int(Rect r, SerializedProperty p, GUIContent l)
    {
        EditorGUI.IntSlider(r, p, default, IntMax(r, p, l), l);
    }

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        switch (p.propertyType)
        {
            case SerializedPropertyType.Float: Float(r, p, l); return;

            case SerializedPropertyType.Integer: Int(r, p, l); return;
        }

        EditorGUI.PropertyField(r, p, l);
    }
}
#endif

