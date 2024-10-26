using UnityEditor;
using UnityEngine;

public abstract class FieldNameAttribute : PropertyAttribute
{
    public readonly string Name;

    public FieldNameAttribute(string name) => Name = name;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(FieldNameAttribute))]
public class FieldNameDrawer : PropertyDrawer
{
    /// <summary>フィールド名</summary>
    protected string Name => (attribute as FieldNameAttribute).Name;

    /// <summary>プロパティを探す</summary>
    /// <param name="obj">探すプロパティが含まれているSerializedObject</param>
    /// <param name="p">取得してきたProperty</param>
    /// <returns>Propertyを取得できたか</returns>
    protected bool TryFindProperty(SerializedObject obj, out SerializedProperty p) => (p = obj.FindProperty(Name)) != null;
}
#endif
