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
    /// <summary>�t�B�[���h��</summary>
    protected string Name => (attribute as FieldNameAttribute).Name;

    /// <summary>�v���p�e�B��T��</summary>
    /// <param name="obj">�T���v���p�e�B���܂܂�Ă���SerializedObject</param>
    /// <param name="p">�擾���Ă���Property</param>
    /// <returns>Property���擾�ł�����</returns>
    protected bool TryFindProperty(SerializedObject obj, out SerializedProperty p) => (p = obj.FindProperty(Name)) != null;
}
#endif
