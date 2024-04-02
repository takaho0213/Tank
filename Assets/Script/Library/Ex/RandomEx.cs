using System.Linq;
using UnityEngine;
using System.Collections.Generic;

/// <summary>Random�̊g���N���X</summary>
public static class RandomEx
{
    /// <summary>bool�^�̒l��Ԃ�</summary>
    public static bool Bool => RangeMax(2) == default;

    /// <summary>0�`�����̒l - 1��Ԃ�</summary>
    /// <param name="max">�ő�l</param>
    public static int RangeMax(int max) => UnityEngine.Random.Range(default, max);

    /// <summary>0�`�����̒l��Ԃ�</summary>
    /// <param name="max">�ő�l</param>
    public static float RangeMax(float max) => UnityEngine.Random.Range(default, max);

    /// <summary>�v�f��Ԃ�</summary>
    public static T Element<T>(IEnumerable<T> source) => source.ElementAt(Index(source));

    /// <summary>�C���f�b�N�X��Ԃ�</summary>
    public static int Index<T>(IEnumerable<T> source) => RangeMax(source.Count());

    /// <summary>0�`1</summary>
    /// <param name="value">�m��</param>
    public static bool Probability(float value) => UnityEngine.Random.value <= Mathf.Clamp01(value);

    /// <summary>�d�ݕt���̒��I</summary>
    /// <param name="selector">��������Ԃ��֐�</param>
    public static T Element<T>(IEnumerable<T> source, System.Func<T, float> selector)
    {
        float total = default;

        foreach (var item in source) total += selector.Invoke(item);

        return Element(source, selector, total);
    }

    /// <summary>�d�ݕt���̒��I</summary>
    /// <param name="source"></param>
    /// <param name="selector">��������Ԃ��֐�</param>
    /// <param name="total">�������̍��v</param>
    public static T Element<T>(IEnumerable<T> source, System.Func<T, float> selector, float total)
    {
        total *= UnityEngine.Random.value;

        foreach (var item in source)
        {
            var v = selector.Invoke(item);

            if (total < v) return item;

            total -= v;
        }

        return source.Last();
    }
}
