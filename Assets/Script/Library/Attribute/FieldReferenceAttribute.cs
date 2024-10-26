using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>フィールドを参照する属性</summary>
public abstract class FieldReferenceAttribute : PropertyAttribute
{
    /// <summary>フィールド名</summary>
    public readonly string Name;

    /// <param name="name">フィールド名</param>
    public FieldReferenceAttribute(string name) => Name = name;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(FieldReferenceAttribute))]
public class GetFieldDrawer : PropertyDrawer
{
    /// <summary>フィールド名</summary>
    protected string Name => ((FieldReferenceAttribute)attribute).Name;

    /// <summary>プロパティを探す</summary>
    /// <param name="obj">探すプロパティが含まれているSerializedObject</param>
    /// <param name="p">取得してきたProperty</param>
    /// <returns>Propertyを取得できたか</returns>
    protected bool TryFindProperty(SerializedObject obj, out SerializedProperty p) => (p = obj.FindProperty(Name)) != null;
}
#endif
