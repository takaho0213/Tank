using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>範囲を配列のサイズに制限する属性</summary>
[AttributeUsage(AttributeTargets.Field)]
public class RangeArraySizeAttribute : FieldReferenceAttribute
{
    public RangeArraySizeAttribute(string name) : base(name) { }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RangeArraySizeAttribute))]
public class RangeArraySizeDrawer : Range01Drawer
{
    private float Max(SerializedProperty p)
    {
        var att = attribute as RangeArraySizeAttribute;

        if (att == null) return default;

        var array = p.serializedObject.FindProperty(att.Name);

        if (array == null || array.isArray)
        {
            Debug.LogError($"{att.Name} is {array.propertyType}");

            return default;
        }

        return p.arraySize;
    }

    protected override int IntMax(Rect r, SerializedProperty p, GUIContent l) => Mathf.RoundToInt(Max(p));

    protected override float FloatMax(Rect r, SerializedProperty p, GUIContent l) => Max(p);
}
#endif
