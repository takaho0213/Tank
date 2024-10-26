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
public class RangeArraySizeDrawer : BaseRangeDrawer
{
    private int Exception(string text)
    {
        Debug.LogError(text);

        return default;
    }

    private int Max(SerializedProperty p)
    {
        var target = attribute as RangeArraySizeAttribute;

        if (target == null) return Exception($"{typeof(RangeArraySizeAttribute)}が見つかりません");

        var property = p.serializedObject.FindProperty(target.Name);

        if (property == null) return Exception($"プロパティ名\"{target.Name}\"は見つかりませんでした");

        if (!property.isArray) return Exception($"プロパティ名\"{target.Name}\"は配列ではありません");

        var size = property.arraySize;

        return size == default ? default : size - 1;
    }

    protected override int MaxInt(SerializedProperty p) => Max(p);
    protected override int MinInt(SerializedProperty p) => default;

    protected override float MaxFloat(SerializedProperty p) => Max(p);
    protected override float MinFloat(SerializedProperty p) => default;
}
#endif
