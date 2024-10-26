using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�t�B�[���h���Q�Ƃ��鑮��</summary>
public abstract class FieldReferenceAttribute : PropertyAttribute
{
    /// <summary>�t�B�[���h��</summary>
    public readonly string Name;

    /// <param name="name">�t�B�[���h��</param>
    public FieldReferenceAttribute(string name) => Name = name;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(FieldReferenceAttribute))]
public class GetFieldDrawer : PropertyDrawer
{
    /// <summary>�t�B�[���h��</summary>
    protected string Name => ((FieldReferenceAttribute)attribute).Name;

    /// <summary>�v���p�e�B��T��</summary>
    /// <param name="obj">�T���v���p�e�B���܂܂�Ă���SerializedObject</param>
    /// <param name="p">�擾���Ă���Property</param>
    /// <returns>Property���擾�ł�����</returns>
    protected bool TryFindProperty(SerializedObject obj, out SerializedProperty p) => (p = obj.FindProperty(Name)) != null;
}
#endif
