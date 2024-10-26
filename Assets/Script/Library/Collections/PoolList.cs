using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PoolList<T> : List<T>, IPool<T> where T : MonoBehaviour
{
    /// <summary>�v�[������I�u�W�F�N�g�̃x�[�X</summary>
    [SerializeField] private T original;

    /// <summary>�v�[������I�u�W�F�N�g�̃x�[�X</summary>
    public T Original { get => original; set => original = value; }

    /// <summary>�v�[������Ă���I�u�W�F�N�g��Ԃ����H��Ԃ��֐��̎Q��</summary>
    public System.Func<T, bool> IsReturn { get; private set; }

    /// <summary>�I�u�W�F�N�g�𐶐�����֐��̎Q��</summary>
    public System.Func<T, T> Instantiate { get; private set; }

    /// <summary>�f�t�H���g�̃v�[������Ă���I�u�W�F�N�g��Ԃ�����</summary>
    private bool DefaultIsReturn(T v) => !v.gameObject.activeSelf;

    /// <summary>�f�t�H���g�̃I�u�W�F�N�g�𐶐�����֐�</summary>
    private T DefaultInstantiate(T original) => Object.Instantiate(original);

    /// <summary>������</summary>
    /// <param name="isReturn">�v�[������Ă���I�u�W�F�N�g��Ԃ����H</param>
    /// <param name="instantiate">�I�u�W�F�N�g�𐶐�����֐��̎Q��</param>
    public void Initialize(System.Func<T, bool> isReturn, System.Func<T, T> instantiate)
    {
        IsReturn = isReturn;
        Instantiate = instantiate;
    }

    /// <summary>�I�u�W�F�N�g���擾</summary>
    public T GetObject()
    {
        IsReturn ??= DefaultIsReturn;           //null�Ȃ�f�t�H���g�̊֐�����

        T result;                               //�Ԃ��I�u�W�F�N�g

        for (int i = default; i < Count; i++)   //�v�f�����J��Ԃ�
        {
            result = this[i];                   //i�Ԗڂ̗v�f����

            if (IsReturn(result)) return result;//�Ԃ������̂��̂�����ΕԂ�
        }

        Instantiate ??= DefaultInstantiate;     //null�Ȃ�f�t�H���g�̊֐�����

        result = Instantiate.Invoke(original);  //�V���ɃC���X�^���X�𐶐�

        Add(result);                            //���X�g�ɒǉ�

        return result;                          //�C���X�^���X��Ԃ�
    }
}
