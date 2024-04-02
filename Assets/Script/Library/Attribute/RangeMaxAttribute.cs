using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>範囲を0〜引数の値内に制限する属性</summary>
[AttributeUsage(AttributeTargets.Field)]
public class RangeMaxAttribute : PropertyAttribute
{
    public readonly float Max;

    public RangeMaxAttribute(float max) => Max = max;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RangeMaxAttribute))]
public class RangeMaxDrawer : Range01Drawer
{
    private float Max => (attribute as RangeMaxAttribute).Max;

    protected override float FloatMax(Rect r, SerializedProperty p, GUIContent l) => Max;

    protected override int IntMax(Rect r, SerializedProperty p, GUIContent l) => Mathf.RoundToInt(Max);
}
#endif
