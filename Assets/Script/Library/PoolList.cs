using System;
using System.Collections.Generic;
using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�I�u�W�F�N�g�v�[����e�ՂɎ�������N���X</summary>
[System.Serializable]
public class PoolList<T> where T : MonoBehaviour
{
    /// <summary>������</summary>
    [SerializeField] private T original;

    /// <summary>�v�[�����X�g</summary>
    private List<T> list = new();

    /// <summary>������</summary>
    public T Original
    {
        get => original;
        set => original = value;
    }

    /// <summary>�ǂݎ���p�̃��X�g</summary>
    public IReadOnlyList<T> List => list;

    /// <summary>�A�N�e�B�u��</summary>
    private bool IsActive(T v) => v.gameObject.activeSelf;

    /// <summary>�C���X�^���X�𐶐�</summary>
    private T Instantiate() => UnityEngine.Object.Instantiate(original);

    /// <summary>��A�N�e�B�u�ȃv�[������Ă���C���X�^���X��Ԃ�</summary>
    public T Pool() => Pool(list, IsActive, Instantiate);

    /// <summary>�����̏����̃v�[������Ă���C���X�^���X��Ԃ�</summary>
    /// <param name="isActive">�v�[������Ă���C���X�^���X��Ԃ������̊֐�</param>
    public T Pool(Func<T, bool> isActive) => Pool(list, isActive, Instantiate);

    /// <summary>��A�N�e�B�u�ȃv�[������Ă���C���X�^���X��Ԃ�</summary>
    /// <param name="instantiate">�C���X�^���X�𐶐�����֐�</param>
    public T Pool(Func<T> instantiate) => Pool(list, IsActive, instantiate);

    /// <summary>�����̏����̃v�[������Ă���C���X�^���X��Ԃ�</summary>
    /// <param name="isActive">�v�[������Ă���C���X�^���X��Ԃ������̊֐�</param>
    /// <param name="instantiate">�C���X�^���X�𐶐�����֐�</param>
    public T Pool(Func<T, bool> isActive, Func<T> instantiate) => Pool(list, isActive, instantiate);

    /// <summary>�����̏����̃v�[������Ă���C���X�^���X��Ԃ�</summary>
    /// <param name="list">�v�[�����X�g</param>
    /// <param name="isActive">�v�[������Ă���C���X�^���X��Ԃ������̊֐�</param>
    /// <param name="instantiate">�C���X�^���X�𐶐�����֐�</param>
    /// <returns></returns>
    public static T Pool(List<T> list, Func<T, bool> isActive, Func<T> instantiate)
    {
        foreach (var v in list) if (!isActive.Invoke(v)) return v;

        var value = instantiate.Invoke();

        list.Add(value);

        return value;
    }

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(PoolList<>))]
public class PoolListDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var o = p.FindPropertyRelative("original");

        if (o != null) EditorGUI.PropertyField(r, o, l);
    }
}
#endif
