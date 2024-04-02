using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>”ÍˆÍ‚ğ0`ˆø”‚Ì’l“à‚É§ŒÀ‚·‚é‘®«</summary>
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
