using System;
using System.Linq;
using System.Collections.Generic;

public static class EnumerableEx
{
    /// <summary>要素数</summary>
    public static int Length<T>(this IEnumerable<T> source) => source switch
    {
        Array a => a.Length,
        ICollection<T> c => c.Count,
        _ => source.Count(),
    };

    /// <summary>インデックスが配列内か</summary>
    public static bool IsIndexInOfRange<T>(this IEnumerable<T> source, int index) => default(int) <= index && index < source.Length();

    /// <summary>インデックスが配列外か</summary>
    public static bool IsIndexOutOfRange<T>(this IEnumerable<T> source, int index) => !IsIndexInOfRange(source, index);

    /// <summary>最初のインデックスか</summary>
    public static bool IsIndexOfFirst<T>(this IEnumerable<T> source, int index) => index == default;
    /// <summary>最後のインデックスか</summary>
    public static bool IsIndexOfLast<T>(this IEnumerable<T> source, int index) => source.LastIndex() == index;

    /// <summary>配列の長さか</summary>
    public static bool IsLengthEqual<T>(this IEnumerable<T> source, int index) => source.Length() == index;

    /// <summary>最後のインデックス</summary>
    public static int LastIndex<T>(this IEnumerable<T> source) => source.Length() - 1;

    /// <summary>要素が空か？</summary>
    public static bool IsEmpty<T>(this IEnumerable<T> source) => source.Length() == default;

    /// <summary>最初のインデックスにセット</summary>
    public static void SetFirst<T>(IList<T> source, T value) => source[default] = value;
    /// <summary>最後のインデックスにセット</summary>
    public static void SetLast<T>(IList<T> source, T value) => source[source.LastIndex()] = value;

    /// <summary>要素をスワップ</summary>
    public static void Swap<T>(this IList<T> source, int l, int r)
    {
        var swap = source[l];
        source[l] = source[r];
        source[r] = swap;
    }

    /// <summary>昇順ソート(0, 1, 2, 3...)</summary>
    public static void Sort<T>(this T[] source) where T : IComparable<T> => Array.Sort(source);

    /// <summary>昇順ソート(0, 1, 2, 3...)</summary>
    public static void Sort<T>(this T[] array, Comparison<T> comparison) => Array.Sort(array, comparison);

    /// <summary>昇順ソート(0, 1, 2, 3...)</summary>
    public static void Sort<T, TC>(this T[] array, Func<T, TC> selector) where TC : IComparable<TC>
    {
        Array.Sort(array, (a, b) => selector(a).CompareTo(selector(b)));
    }

    /// <summary>昇順ソート(0, 1, 2, 3...)</summary>
    public static IEnumerable<T> Sort<T, TC>(this IEnumerable<T> source, Func<T, TC> selector) where TC : IComparable<TC>
    {
        return source.OrderBy(selector);
    }

    /// <summary>昇順ソート(0, 1, 2, 3...)</summary>
    public static IEnumerable<T> Sort<T>(this IEnumerable<T> source) where T : IComparable<T>
    {
        return source.OrderBy((v) => v);
    }

    /// <summary>配列の中からランダムに要素を返す</summary>
    public static T Random<T>(this IEnumerable<T> array) => RandomEx.Array(array);

    /// <summary>インデックスをリピート</summary>
    public static int RepeatIndex<T>(this IEnumerable<T> array, int i) => i % array.Length();

    /// <summary>インデックスをリピート</summary>
    public static T Repeat<T>(this IEnumerable<T> array, int i) => array.ElementAt(RepeatIndex(array, i));

    /// <summary>インデックスを配列内に制限</summary>
    public static int ClampIndex<T>(this IEnumerable<T> array, int i) => MathEx.Clamp(i, default, array.LastIndex());
    /// <summary>インデックスを配列内に制限</summary>
    public static T Clamp<T>(this IEnumerable<T> array, int i) => array.ElementAt(ClampIndex(array, i));

    /// <summary>インデックスを折り返す</summary>
    public static int PingPongIndex<T>(this IEnumerable<T> array, int i) => (int)UnityEngine.Mathf.PingPong(i, array.LastIndex());
    /// <summary>インデックスを折り返す</summary>
    public static T PingPong<T>(this IEnumerable<T> array, int i) => array.ElementAt(PingPongIndex(array, i));

    /// <summary>インデックスの比率</summary>
    public static float RatioIndex<T>(this IEnumerable<T> array, int i) => MathEx.Ratio(default, array.Length(), i);

    /// <summary>補間</summary>
    public static T Lerp<T>(this IEnumerable<T> array, float t) => array.ElementAt(MathEx.Lerp(default, array.LastIndex(), t));

    /// <summary>要素を入手できるか試す</summary>
    public static bool TryElementAt<T>(this IEnumerable<T> source, int i, out T result)
    {
        bool isIn = source.IsIndexInOfRange(i);

        result = isIn ? source.ElementAt(i) : default;

        return isIn;
    }

    /// <summary>要素を入手出来なかったDefaultを返す</summary>
    public static T ElementAtOrDefault<T>(this IEnumerable<T> source, int i)
    {
        return source.IsIndexInOfRange(i) ? source.ElementAt(i) : default;
    }

    /// <summary>合計値を返す</summary>
    public static TR Total<T, TR>(this IEnumerable<T> source, System.Func<T, TR> selector, System.Func<TR, TR, TR> add)
    {
        TR total = default;

        foreach (var item in source)
        {
            total = add.Invoke(total, selector.Invoke(item));
        }

        return total;
    }

    /// <summary>合計値を返す</summary>
    public static int Total<T>(this IEnumerable<T> source, System.Func<T, int> selector)
    {
        int total = default;

        foreach (var item in source)
        {
            total += selector.Invoke(item);
        }

        return total;
    }

    /// <summary>合計値を返す</summary>
    public static float Total<T>(this IEnumerable<T> source, System.Func<T, float> selector)
    {
        float total = default;

        foreach (var item in source)
        {
            total += selector.Invoke(item);
        }

        return total;
    }

    /// <summary>配列のサイズを追加</summary>
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

    /// <summary>配列のサイズを削除</summary>
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

    /// <summary>配列をクリア</summary>
    public static void Clear<T>(this T[] array) => System.Array.Clear(array, default, array.Length);

    /// <summary>重複した要素をカウント</summary>
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

    /// <summary>最初の要素を削除</summary>
    public static void RemoveFirst<T>(this ICollection<T> source) => source.Remove(source.First());
    /// <summary>最後の要素を削除</summary>
    public static void RemoveLast<T>(this ICollection<T> source) => source.Remove(source.Last());

    /// <summary>範囲削除</summary>
    public static void RemoveFirstRange<T>(this List<T> source, int count) => source.RemoveRange(default, count);
    /// <summary>範囲削除</summary>
    public static void RemoveLastRange<T>(this List<T> source, int count) => source.RemoveRange(source.IndexOf(source.Last()) - count, count);
}
