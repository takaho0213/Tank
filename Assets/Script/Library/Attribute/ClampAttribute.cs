using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�l�𐧌����鑮��(int, float)</summary>
[AttributeUsage(AttributeTargets.Field)]
public class ClampAttribute : PropertyAttribute
{
    /// <summary>Min�l</summary>
    public readonly float Min;
    /// <summary>Max�l</summary>
    public readonly float Max;

    /// <param name="min">Min�l</param>
    /// <param name="max">Max�l</param>
    public ClampAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}

/// <summary>�l��Max�𐧌����鑮��(int, float)</summary>
[AttributeUsage(AttributeTargets.Field)]
public class ClampMaxAttribute : PropertyAttribute
{
    /// <summary>Max�l</summary>
    public readonly float Max;

    /// <param name="max">Max�l</param>
    public ClampMaxAttribute(float max) => Max = max;
}

/// <summary>�l��Min�𐧌����鑮��(int, float)</summary>
[AttributeUsage(AttributeTargets.Field)]
public class ClampMinAttribute : PropertyAttribute
{
    /// <summary>Min�l</summary>
    public readonly float Min;

    /// <param name="min">Min�l</param>
    public ClampMinAttribute(float min) => Min = min;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ClampAttribute))]
public class ClampDrawer : PropertyDrawer
{
    private ClampAttribute A => attribute as ClampAttribute;

    protected virtual int IntClamp(int value) => Mathf.RoundToInt(MathEx.Clamp(value, A.Min, A.Max));

    protected virtual float FloatClamp(float value) => MathEx.Clamp(value, A.Min, A.Max);

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        switch (p.propertyType)
        {
            case SerializedPropertyType.Integer:

                p.intValue = IntClamp(p.intValue);

                break;

            case SerializedPropertyType.Float:

                p.floatValue = FloatClamp(p.floatValue);

                break;
        }

        EditorGUI.PropertyField(r, p, l);
    }
}

[CustomPropertyDrawer(typeof(ClampMaxAttribute))]
public class ClampMaxDrawer : ClampDrawer
{
    protected virtual float Max => (attribute as ClampMaxAttribute).Max;

    protected override float FloatClamp(float value) => MathEx.ClampMax(value, Max);

    protected override int IntClamp(int value) => MathEx.ClampMax(value, Mathf.RoundToInt(Max));
}

[CustomPropertyDrawer(typeof(ClampMinAttribute))]
public class ClampMinDrawer : ClampDrawer
{
    protected virtual float Min => (attribute as ClampMaxAttribute).Max;

    protected override float FloatClamp(float value) => MathEx.ClampMin(value, Min);

    protected override int IntClamp(int value) => MathEx.ClampMin(value, Mathf.RoundToInt(Min));
}
#endif
