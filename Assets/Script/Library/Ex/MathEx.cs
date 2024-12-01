using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class MathEx
{
    /// <summary>float型の0</summary>
    public const float ZeroF = 0.0f;
    /// <summary>float型の1</summary>
    public const float OneF = 1.0f;
    /// <summary>float型の2</summary>
    public const float TwoF = 2.0f;

    /// <summary>int型の0</summary>
    public const int ZeroI = 0;
    /// <summary>int型の1</summary>
    public const int OneI = 1;
    /// <summary>int型の2</summary>
    public const int TwoI = 2;

    /// <summary>0.5</summary>
    public const float Half = 0.5f;

    /// <summary>100</summary>
    public const int OneHundred = 100;

    /// <summary>アルファベットの全文字数</summary>
    public const int Alphabet = 26;

    /// <summary>二進数</summary>
    public const int Binary = 2;
    /// <summary>十進法</summary>
    public const int Decimal = 10;
    /// <summary>十六進数</summary>
    public const int HexaDecimal = 16;

    /// <summary>時間</summary>
    public const int Hour = 60;

    /// <summary>10進数 => 2進数</summary>
    public static string ToBinary(int value) => System.Convert.ToString(value, Binary);
    /// <summary>10進数 => 16進数</summary>
    public static string ToHexaDecimal(int value) => System.Convert.ToString(value, HexaDecimal);

    /// <summary>2進数 => 10進数</summary>
    public static int BinaryToDecimal(string value) => System.Convert.ToInt32(value, Binary);
    /// <summary>2進数 => 16進数</summary>
    public static string BinaryToHexaDecimal(string value) => ToHexaDecimal(BinaryToDecimal(value));

    /// <summary>16進数 => 2進数</summary>
    public static string HexaDecimalToBinary(string value) => ToBinary(HexaDecimalToDecimal(value));
    /// <summary>16進数 => 10進数</summary>
    public static int HexaDecimalToDecimal(string value) => System.Convert.ToInt32(value, HexaDecimal);

    /// <summary>時数換算</summary>
    public static int ToHour(this float value) => (int)(value / Hour / Hour);
    /// <summary>時数換算</summary>
    public static int ToHour(this double value) => (int)(value / Hour / Hour);

    /// <summary>分数換算</summary>
    public static int ToMinute(this float value) => (int)(value / Hour % Hour);
    /// <summary>分数換算</summary>
    public static int ToMinute(this double value) => (int)(value / Hour % Hour);

    /// <summary>秒数換算</summary>
    public static float ToSecond(this float value) => value % Hour;
    /// <summary>秒数換算</summary>
    public static double ToSecond(this double value) => value % Hour;

    /// <summary>時間換算</summary>
    public static string ToTime(this float time) => $"{time.ToHour()}h : {time.ToMinute()}m : {(int)time.ToSecond()}s";
    /// <summary>時間換算</summary>
    public static string ToTime(this double time) => $"{time.ToHour()}h : {time.ToMinute()}m : {(int)time.ToSecond()}s";

    /// <summary>最小値を返す</summary>
    public static T Min<T>(T a, T b) where T : System.IComparable<T>
    {
        return a.CompareTo(b) < default(int) ? a : b;
    }

    /// <summary>最小値を返す</summary>
    public static T Min<T>(IEnumerable<T> values) where T : System.IComparable<T>
    {
        T result = values.FirstOrDefault();

        foreach (var value in values)
        {
            if (value.CompareTo(result) < default(int))
            {
                result = value;
            }
        }

        return result;
    }

    /// <summary>最小値を返す</summary>
    public static T Min<T, TR>(IEnumerable<T> values, System.Func<T, TR> selector) where TR : System.IComparable<TR>
    {
        T result = values.FirstOrDefault();

        foreach (var value in values)
        {
            if (selector.Invoke(value).CompareTo(selector.Invoke(result)) < default(int))
            {
                result = value;
            }
        }

        return result;
    }

    /// <summary>最大値を返す</summary>
    public static T Max<T>(T a, T b) where T : System.IComparable<T>
    {
        return (a.CompareTo(b) > default(int)) ? a : b;
    }

    /// <summary>最大値を返す</summary>
    public static T Max<T>(IEnumerable<T> values) where T : System.IComparable<T>
    {
        T result = values.FirstOrDefault();

        foreach (var value in values)
        {
            if (value.CompareTo(result) > default(int))
            {
                result = value;
            }
        }

        return result;
    }

    /// <summary>最大値を返す</summary>
    public static T Max<T, TR>(IEnumerable<T> values, System.Func<T, TR> selector) where TR : System.IComparable<TR>
    {
        T result = values.FirstOrDefault();

        foreach (var value in values)
        {
            if (selector.Invoke(value).CompareTo(selector.Invoke(result)) > default(int))
            {
                result = value;
            }
        }

        return result;
    }

    /// <summary>値を制限</summary>
    public static T Clamp<T>(T value, T min, T max) where T : System.IComparable<T>
    {
        if (value.CompareTo(min) < default(int)) return min;
        if (value.CompareTo(max) > default(int)) return max;

        return value;
    }

    /// <summary>Max値を制限</summary>
    public static T ClampMax<T>(T value, T max) where T : System.IComparable<T>
    {
        return value.CompareTo(max) > default(int) ? max : value;
    }

    /// <summary>Min値を制限</summary>
    public static T ClampMin<T>(T value, T min) where T : System.IComparable<T>
    {
        return value.CompareTo(min) < default(int) ? min : value;
    }

    /// <summary>折り返し</summary>
    public static int PingPong(int value, int length)
    {
        int result = value % length;

        if (!IsEven(value / length))
        {
            result = length - result;
        }

        return result;
    }

    /// <summary>偶数か？</summary>
    public static bool IsEven(int num) => num % 2 == default;

    /// <summary>補間</summary>
    /// <param name="a">0地点</param>
    /// <param name="b">1地点</param>
    /// <param name="t">補間値</param>
    public static int Lerp(int a, int b, float t) => Mathf.RoundToInt(a + (b - a) * t);

    /// <summary>補間</summary>
    /// <param name="a">0地点</param>
    /// <param name="b">1地点</param>
    /// <param name="t">補間値</param>
    public static int Lerp01(int a, int b, float t) => Lerp(a, b, Mathf.Clamp01(t));

    /// <summary>割合</summary>
    /// <param name="a">0地点</param>
    /// <param name="b">1地点</param>
    /// <param name="t">値</param>
    public static float Ratio(float a, float b, float t) => a != b ? (t - a) / (b - a) : default;

    /// <summary>割合</summary>
    /// <param name="a">0地点</param>
    /// <param name="b">1地点</param>
    /// <param name="t">値</param>
    public static float Ratio01(float a, float b, float t) => Mathf.Clamp01(Ratio(a, b, t));

    /// <summary>割合</summary>
    /// <param name="a">0地点</param>
    /// <param name="b">1地点</param>
    /// <param name="t">値</param>
    public static float Ratio(int a, int b, int t) => a != b ? (t - a) / (float)(b - a) : default;

    /// <summary>割合</summary>
    /// <param name="a">0地点</param>
    /// <param name="b">1地点</param>
    /// <param name="t">値</param>
    public static float Ratio01(int a, int b, int t) => Mathf.Clamp01(Ratio(a, b, t));

    /// <summary>桁数</summary>
    public static int Digit(int num) => num >= default(int) ? num.ToString().Length : -(-num).ToString().Length;

    /// <summary>桁数指定 四捨五入</summary>
    public static float Round(this float value, int num) => (float)Round((double)value, num);

    /// <summary>桁数指定 四捨五入</summary>
    public static double Round(this double value, int num) => System.Math.Round(value, num, System.MidpointRounding.AwayFromZero);

    /// <summary>四捨五入</summary>
    public static float Round(this float value) => Mathf.Round(value);
    /// <summary>四捨五入ToInt</summary>
    public static int RoundToInt(this float value) => Mathf.RoundToInt(value);

    /// <summary>切り捨て</summary>
    public static float Floor(this float value) => Mathf.Floor(value);
    /// <summary>切り捨てToInt</summary>
    public static float FloorToInt(this float value) => Mathf.FloorToInt(value);

    /// <summary>切り上げ</summary>
    public static float Ceil(this float value) => Mathf.Ceil(value);
    /// <summary>切り上げToInt</summary>
    public static int CeilToInt(this float value) => Mathf.CeilToInt(value);

    /// <summary>ほぼ同じか？</summary>
    public static bool Approximately(this float l, float r) => UnityEngine.Mathf.Approximately(l, r);

    /// <summary>指定桁数の数字を作成(一桁目1, 二桁以降0)</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">作成した数字</param>
    /// <returns>作成できたか</returns>
    public static bool TryDigitMinNumber(int digit, out int value) => int.TryParse(StringEx.DigitMinNumber(digit), out value);

    /// <summary>指定桁数の数字を作成(数字はすべて9)</summary>
    /// <param name="digit">桁数</param>
    /// <param name="value">作成した数字</param>
    /// <returns>作成できたか</returns>
    public static bool TryDigitMaxNumber(int digit, out int value) => int.TryParse(StringEx.DigitMaxNumber(digit), out value);

    public static float MoveTowards(float current, float target, float maxDelta)
    {
        if (Mathf.Abs(target - current) <= maxDelta)
        {
            return target;
        }

        return current + Mathf.Sign(target - current) * maxDelta;
    }

    /// <summary>最大公約数</summary>
    public static int Gcd(int x, int y)
    {
        if (x < y)
        {
            var z = x;
            x = y;
            y = z;
        }

        while (true)
        {
            var z = x % y;

            if (z == default) return y;

            x = y;
            y = z;
        }
    }

    /// <summary>比率</summary>
    public static Vector2Int VectorRatio(Vector2Int v)
    {
        var g = Gcd(v.x, v.y);

        v.x /= g;
        v.y /= g;

        return v;
    }
}
