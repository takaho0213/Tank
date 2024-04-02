using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>�L�[�ƒl�̃y�A�Z�b�g�N���X</summary>
[System.Serializable]
public class KeyValue<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public KeyValue(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

/// <summary>KeyValue�̊g��</summary>
public static class KeyValuEx
{
    /// <summary>Dictionary�^�ɕϊ�</summary>
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this KeyValue<TKey, TValue>[] array) => array.ToDictionary((v) => v.key, (v) => v.value);
}
