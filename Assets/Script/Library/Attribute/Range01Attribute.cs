using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>”ÍˆÍ‚ğ0`1“à‚É§ŒÀ‚·‚é‘®«</summary>
[AttributeUsage(AttributeTargets.Field)]
public class Range01Attribute : PropertyAttribute { }

#if UNITY_EDITOR
public abstract class BaseRangeDrawer : PropertyDrawer
{
    protected abstract int MaxInt(SerializedProperty p);
    protected abstract int MinInt(SerializedProperty p);

    protected abstract float MaxFloat(SerializedProperty p);
    protected abstract float MinFloat(SerializedProperty p);

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        switch (p.propertyType)
        {
            case SerializedPropertyType.Float:

                EditorGUI.Slider(r, p, MinFloat(p), MaxFloat(p), l);

                return;

            case SerializedPropertyType.Integer:

                EditorGUI.IntSlider(r, p, MinInt(p), MaxInt(p), l);

                return;

            default:

                EditorGUI.PropertyField(r, p, l);

                return;
        }
    }
}

[CustomPropertyDrawer(typeof(Range01Attribute))]
public class Range01Drawer : BaseRangeDrawer
{
    protected override int MaxInt(SerializedProperty p) => (int)MathEx.OneF;
    protected override int MinInt(SerializedProperty p) => default;

    protected override float MaxFloat(SerializedProperty p) => MathEx.OneF;
    protected override float MinFloat(SerializedProperty p) => default;
}
#endif

