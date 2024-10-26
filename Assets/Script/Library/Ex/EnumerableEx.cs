using System;
using System.Linq;
using System.Collections.Generic;

public static class EnumerableEx
{
    /// <summary>�v�f��</summary>
    public static int Length<T>(this IEnumerable<T> source) => source switch
    {
        Array a => a.Length,
        ICollection<T> c => c.Count,
        _ => source.Count(),
    };

    /// <summary>�C���f�b�N�X���z�����</summary>
    public static bool IsIndexInOfRange<T>(this IEnumerable<T> source, int index) => default(int) <= index && index < source.Length();

    /// <summary>�C���f�b�N�X���z��O��</summary>
    public static bool IsIndexOutOfRange<T>(this IEnumerable<T> source, int index) => !IsIndexInOfRange(source, index);

    /// <summary>�ŏ��̃C���f�b�N�X��</summary>
    public static bool IsIndexOfFirst<T>(this IEnumerable<T> source, int index) => index == default;
    /// <summary>�Ō�̃C���f�b�N�X��</summary>
    public static bool IsIndexOfLast<T>(this IEnumerable<T> source, int index) => source.LastIndex() == index;

    /// <summary>�z��̒�����</summary>
    public static bool IsLengthEqual<T>(this IEnumerable<T> source, int index) => source.Length() == index;

    /// <summary>�Ō�̃C���f�b�N�X</summary>
    public static int LastIndex<T>(this IEnumerable<T> source) => source.Length() - 1;

    /// <summary>�v�f���󂩁H</summary>
    public static bool IsEmpty<T>(this IEnumerable<T> source) => source.Length() == default;

    /// <summary>�ŏ��̃C���f�b�N�X�ɃZ�b�g</summary>
    public static void SetFirst<T>(IList<T> source, T value) => source[default] = value;
    /// <summary>�Ō�̃C���f�b�N�X�ɃZ�b�g</summary>
    public static void SetLast<T>(IList<T> source, T value) => source[source.LastIndex()] = value;

    /// <summary>�v�f���X���b�v</summary>
    public static void Swap<T>(this IList<T> source, int l, int r)
    {
        var swap = source[l];
        source[l] = source[r];
        source[r] = swap;
    }

    /// <summary>�����\�[�g(0, 1, 2, 3...)</summary>
    public static void Sort<T>(this T[] source) where T : IComparable<T> => Array.Sort(source);

    /// <summary>�����\�[�g(0, 1, 2, 3...)</summary>
    public static void Sort<T>(this T[] array, Comparison<T> comparison) => Array.Sort(array, comparison);

    /// <summary>�����\�[�g(0, 1, 2, 3...)</summary>
    public static void Sort<T, TC>(this T[] array, Func<T, TC> selector) where TC : IComparable<TC>
    {
        Array.Sort(array, (a, b) => selector(a).CompareTo(selector(b)));
    }

    /// <summary>�����\�[�g(0, 1, 2, 3...)</summary>
    public static IEnumerable<T> Sort<T, TC>(this IEnumerable<T> source, Func<T, TC> selector) where TC : IComparable<TC>
    {
        return source.OrderBy(selector);
    }

    /// <summary>�����\�[�g(0, 1, 2, 3...)</summary>
    public static IEnumerable<T> Sort<T>(this IEnumerable<T> source) where T : IComparable<T>
    {
        return source.OrderBy((v) => v);
    }

    /// <summary>�z��̒����烉���_���ɗv�f��Ԃ�</summary>
    public static T Random<T>(this IEnumerable<T> array) => RandomEx.Array(array);

    /// <summary>�C���f�b�N�X�����s�[�g</summary>
    public static int RepeatIndex<T>(this IEnumerable<T> array, int i) => i % array.Length();

    /// <summary>�C���f�b�N�X�����s�[�g</summary>
    public static T Repeat<T>(this IEnumerable<T> array, int i) => array.ElementAt(RepeatIndex(array, i));

    /// <summary>�C���f�b�N�X��z����ɐ���</summary>
    public static int ClampIndex<T>(this IEnumerable<T> array, int i) => MathEx.Clamp(i, default, array.LastIndex());
    /// <summary>�C���f�b�N�X��z����ɐ���</summary>
    public static T Clamp<T>(this IEnumerable<T> array, int i) => array.ElementAt(ClampIndex(array, i));

    /// <summary>�C���f�b�N�X��܂�Ԃ�</summary>
    public static int PingPongIndex<T>(this IEnumerable<T> array, int i) => (int)UnityEngine.Mathf.PingPong(i, array.LastIndex());
    /// <summary>�C���f�b�N�X��܂�Ԃ�</summary>
    public static T PingPong<T>(this IEnumerable<T> array, int i) => array.ElementAt(PingPongIndex(array, i));

    /// <summary>�C���f�b�N�X�̔䗦</summary>
    public static float RatioIndex<T>(this IEnumerable<T> array, int i) => MathEx.Ratio(default, array.Length(), i);

    /// <summary>���</summary>
    public static T Lerp<T>(this IEnumerable<T> array, float t) => array.ElementAt(MathEx.Lerp(default, array.LastIndex(), t));

    /// <summary>�v�f�����ł��邩����</summary>
    public static bool TryElementAt<T>(this IEnumerable<T> source, int i, out T result)
    {
        bool isIn = source.IsIndexInOfRange(i);

        result = isIn ? source.ElementAt(i) : default;

        return isIn;
    }

    /// <summary>�v�f�����o���Ȃ�����Default��Ԃ�</summary>
    public static T ElementAtOrDefault<T>(this IEnumerable<T> source, int i)
    {
        return source.IsIndexInOfRange(i) ? source.ElementAt(i) : default;
    }

    /// <summary>���v�l��Ԃ�</summary>
    public static TR Total<T, TR>(this IEnumerable<T> source, System.Func<T, TR> selector, System.Func<TR, TR, TR> add)
    {
        TR total = default;

        foreach (var item in source)
        {
            total = add.Invoke(total, selector.Invoke(item));
        }

        return total;
    }

    /// <summary>���v�l��Ԃ�</summary>
    public static int Total<T>(this IEnumerable<T> source, System.Func<T, int> selector)
    {
        int total = default;

        foreach (var item in source)
        {
            total += selector.Invoke(item);
        }

        return total;
    }

    /// <summary>���v�l��Ԃ�</summary>
    public static float Total<T>(this IEnumerable<T> source, System.Func<T, float> selector)
    {
        float total = default;

        foreach (var item in source)
        {
            total += selector.Invoke(item);
        }

        return total;
    }

    /// <summary>�z��̃T�C�Y��ǉ�</summary>
    public static T[] AddArraySize<T>(this T[] array, int value)
    {
        T[] newArray = new T[array.Length + value];

        int length = MathEx.Min(array.Length, newArray.Length);

        for (int i = default; i < length; i++)
        {
            newArray[i] = array[i];
        }

        return newArray;
    }

    /// <summary>�z��̃T�C�Y���폜</summary>
    public static T[] RemoveArraySize<T>(this T[] array, int value)
    {
        T[] newArray = new T[array.Length - value];

        int length = MathEx.Min(array.Length, newArray.Length);

        for (int i = default; i < length; i++)
        {
            newArray[i] = array[i];
        }

        return newArray;
    }

    /// <summary>�z����N���A</summary>
    public static void Clear<T>(this T[] array) => System.Array.Clear(array, default, array.Length);

    /// <summary>�d�������v�f���J�E���g</summary>
    public static KeyValue<T, int>[] DuplicationCount<T>(this IEnumerable<T> source)
    {
        var group = source.GroupBy(x => x);

        int count = group.Count();

        var piars = new KeyValue<T, int>[count];

        for (int i = default; i < count; i++)
        {
            var element = group.ElementAt(i);

            piars[i] = new(element.Key, element.Count());
        }

        return piars;
    }

    /// <summary>�ŏ��̗v�f���폜</summary>
    public static void RemoveFirst<T>(this ICollection<T> source) => source.Remove(source.First());
    /// <summary>�Ō�̗v�f���폜</summary>
    public static void RemoveLast<T>(this ICollection<T> source) => source.Remove(source.Last());

    /// <summary>�͈͍폜</summary>
    public static void RemoveFirstRange<T>(this List<T> source, int count) => source.RemoveRange(default, count);
    /// <summary>�͈͍폜</summary>
    public static void RemoveLastRange<T>(this List<T> source, int count) => source.RemoveRange(source.IndexOf(source.Last()) - count, count);
}
