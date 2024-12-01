using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class RandomEx
{
    /// <summary>Systemのランダム</summary>
    public static readonly System.Random SystemRandom = new();

    /// <summary>ランダムにbool型の値を返す</summary>
    public static bool Bool => Random.value <= MathEx.Half;

    /// <summary>Min ～ Max内の値を返す</summary>
    public static int Range(int min, int max) => MathEx.Lerp(min, max, (float)SystemRandom.NextDouble());
    /// <summary>Min ～ Max内の値を返す</summary>
    public static float Range(float min, float max) => Mathf.Lerp(min, max, (float)SystemRandom.NextDouble());

    /// <summary>ランダムにどちらかの値を返す</summary>
    public static T Either<T>(T a, T b) => Bool ? a : b;

    /// <summary>ランダムに0 ～ 9のchar型の値を返す</summary>
    public static char Number() => (char)Range(CharEx.FirstNumber, CharEx.LastNumber);

    /// <summary>ランダムに大文字アルファベットを返す</summary>
    public static char AlphabetUpper() => (char)Range(CharEx.FirstAlphabetUpper, CharEx.LastAlphabetUpper);

    /// <summary>ランダムに子文字アルファベットを返す</summary>
    public static char AlphabetLower() => (char)Range(CharEx.FirstAlphabetLower, CharEx.LastAlphabetLower);

    /// <summary>ランダムに英数字を返す</summary>
    public static char Alphanumeric()
    {
        char value = (char)Range(CharEx.FirstNumber, CharEx.LastAlphabetLower);

        return value.IsAlphanumeric() ? value : Alphanumeric();
    }

    /// <summary>ランダムに指定桁数の数字をstring型で返す</summary>
    public static string DigitNumber(int digit)
    {
        char[] chars = new char[digit];

        for (int i = default; i < chars.Length; i++)
        {
            chars[i] = Number();
        }

        return new(chars);
    }

    /// <summary>指定桁数の数字を返す</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">値</param>
    /// <returns>数字が作成できたか</returns>
    public static bool TryDigitNumber(int digit, out sbyte value) => sbyte.TryParse(DigitNumber(digit), out value);
    /// <summary>指定桁数の数字を返す</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">値</param>
    /// <returns>数字が作成できたか</returns>
    public static bool TryDigitNumber(int digit, out byte value) => byte.TryParse(DigitNumber(digit), out value);
    /// <summary>指定桁数の数字を返す</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">値</param>
    /// <returns>数字が作成できたか</returns>
    public static bool TryDigitNumber(int digit, out short value) => short.TryParse(DigitNumber(digit), out value);
    /// <summary>指定桁数の数字を返す</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">値</param>
    /// <returns>数字が作成できたか</returns>
    public static bool TryDigitNumber(int digit, out ushort value) => ushort.TryParse(DigitNumber(digit), out value);
    /// <summary>指定桁数の数字を返す</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">値</param>
    /// <returns>数字が作成できたか</returns>
    public static bool TryDigitNumber(int digit, out int value) => int.TryParse(DigitNumber(digit), out value);
    /// <summary>指定桁数の数字を返す</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">値</param>
    /// <returns>数字が作成できたか</returns>
    public static bool TryDigitNumber(int digit, out uint value) => uint.TryParse(DigitNumber(digit), out value);
    /// <summary>指定桁数の数字を返す</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">値</param>
    /// <returns>数字が作成できたか</returns>
    public static bool TryDigitNumber(int digit, out long value) => long.TryParse(DigitNumber(digit), out value);
    /// <summary>指定桁数の数字を返す</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">値</param>
    /// <returns>数字が作成できたか</returns>
    public static bool TryDigitNumber(int digit, out ulong value) => ulong.TryParse(DigitNumber(digit), out value);

    /// <summary>0～Maxの値を返す</summary>
    public static int RangeMax(int max) => Range(default, max);
    /// <summary>0～Maxの値を返す</summary>
    public static float RangeMax(float max) => Range(default, max);

    /// <summary>ランダムに要素を返す</summary>
    public static T Array<T>(IEnumerable<T> array) => array.ElementAt(RangeMax(array.LastIndex()));

    /// <summary>ランダムに範囲内のインデックスを返す</summary>
    public static int Index<T>(IEnumerable<T> array) => RangeMax(array.LastIndex());

    /// <summary>0 ～ 1内の確率</summary>
    /// <param name="value">確率</param>
    /// <returns>valueがランダム(0 ～ 1)な値より上か</returns>
    public static bool Probability01(float value)
    {
        value = Mathf.Clamp01(value);

        var rand = UnityEngine.Random.value;

        return rand == MathEx.OneF || rand < value;
    }

    /// <summary>最小値 ～ 最大値内の確率</summary>
    /// <param name="value">値</param>
    /// <param name="min">最小値</param>
    /// <param name="max">最大値</param>
    /// <returns>値がランダムな値(最小値 ～ 最大値)より上か</returns>
    public static bool Probability(float value, float min, float max)
    {
        return Probability01(Mathf.InverseLerp(min, max, value));
    }

    /// <summary>最小値 ～ 最大値内の確率</summary>
    /// <param name="value">値</param>
    /// <param name="min">最小値</param>
    /// <param name="max">最大値</param>
    /// <returns>値がランダムな値(最小値 ～ 最大値)より上か</returns>
    public static bool Probability(int value, int min, int max)
    {
        var rand = Range(min, max);

        return rand == max || rand < value;
    }

    /// <summary>0 ～ 100内の確率</summary>
    /// <param name="value">値</param>
    /// <returns>値がランダムな値(最小値 ～ 最大値)より上か</returns>
    public static bool Probability0100(int value)
    {
        value = MathEx.Clamp(value, default, MathEx.OneHundred);

        var rand = Range(default, MathEx.OneHundred);

        return rand == MathEx.OneHundred || rand < value;
    }

    /// <summary>0 ～ 最大値内の確率</summary>
    /// <param name="value">値</param>
    /// <param name="max">最大値</param>
    /// <returns>値がランダムな値(0 ～ 最大値)より上か</returns>
    public static bool Probability(float value, float max)
    {
        return Probability01(Mathf.InverseLerp(default, max, value));
    }

    /// <summary>確率</summary>
    /// <param name="value">値</param>
    /// <param name="min">最小値</param>
    /// <param name="max">最大値</param>
    /// <param name="rand">ランダムな値</param>
    /// <returns>値がランダムな値より上か</returns>
    public static bool Probability(float value, float min, float max, float rand)
    {
        rand = Mathf.Clamp(rand, min, max);

        value = Mathf.Clamp(value, min, max);

        return rand == max || rand <= value;
    }

    /// <summary>重み付きの抽選</summary>
    /// <returns>当たった要素</returns>
    public static T Lottery<T>(IEnumerable<T> source, System.Func<T, float> selector)
    {
        float total = default;

        foreach (var item in source) total += selector.Invoke(item);

        return Lottery(source, selector, total);
    }

    /// <summary>重み付きの抽選</summary>
    /// <param name="total">合計値</param>
    /// <returns>当たった要素</returns>
    public static T Lottery<T>(IEnumerable<T> source, System.Func<T, float> selector, float total)
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
