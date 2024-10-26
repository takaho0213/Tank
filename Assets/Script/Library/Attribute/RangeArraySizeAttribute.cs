using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�͈͂�z��̃T�C�Y�ɐ������鑮��</summary>
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

        if (target == null) return Exception($"{typeof(RangeArraySizeAttribute)}��������܂���");

        var property = p.serializedObject.FindProperty(target.Name);

        if (property == null) return Exception($"�v���p�e�B��\"{target.Name}\"�͌�����܂���ł���");

        if (!property.isArray) return Exception($"�v���p�e�B��\"{target.Name}\"�͔z��ł͂���܂���");

        var size = property.arraySize;

        return size == default ? default : size - 1;
    }

    protected override int MaxInt(SerializedProperty p) => Max(p);
    protected override int MinInt(SerializedProperty p) => default;

    protected override float MaxFloat(SerializedProperty p) => Max(p);
    protected override float MinFloat(SerializedProperty p) => default;
}
#endif
