using System.Linq;
using UnityEngine;
using System.Collections.Generic;

/// <summary>Randomの拡張クラス</summary>
public static class RandomEx
{
    /// <summary>bool型の値を返す</summary>
    public static bool Bool => RangeMax(2) == default;

    /// <summary>0～引数の値 - 1を返す</summary>
    /// <param name="max">最大値</param>
    public static int RangeMax(int max) => UnityEngine.Random.Range(default, max);

    /// <summary>0～引数の値を返す</summary>
    /// <param name="max">最大値</param>
    public static float RangeMax(float max) => UnityEngine.Random.Range(default, max);

    /// <summary>要素を返す</summary>
    public static T Element<T>(IEnumerable<T> source) => source.ElementAt(Index(source));

    /// <summary>インデックスを返す</summary>
    public static int Index<T>(IEnumerable<T> source) => RangeMax(source.Count());

    /// <summary>0～1</summary>
    /// <param name="value">確率</param>
    public static bool Probability(float value) => UnityEngine.Random.value <= Mathf.Clamp01(value);

    /// <summary>重み付きの抽選</summary>
    /// <param name="selector">封入率を返す関数</param>
    public static T Element<T>(IEnumerable<T> source, System.Func<T, float> selector)
    {
        float total = default;

        foreach (var item in source) total += selector.Invoke(item);

        return Element(source, selector, total);
    }

    /// <summary>重み付きの抽選</summary>
    /// <param name="source"></param>
    /// <param name="selector">封入率を返す関数</param>
    /// <param name="total">封入率の合計</param>
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
