using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class MathEx
{
    /// <summary>float�^��0</summary>
    public const float ZeroF = 0.0f;
    /// <summary>float�^��1</summary>
    public const float OneF = 1.0f;
    /// <summary>float�^��2</summary>
    public const float TwoF = 2.0f;

    /// <summary>int�^��0</summary>
    public const int ZeroI = 0;
    /// <summary>int�^��1</summary>
    public const int OneI = 1;
    /// <summary>int�^��2</summary>
    public const int TwoI = 2;

    /// <summary>0.5</summary>
    public const float Half = 0.5f;

    /// <summary>100</summary>
    public const int OneHundred = 100;

    /// <summary>�A���t�@�x�b�g�̑S������</summary>
    public const int Alphabet = 26;

    /// <summary>��i��</summary>
    public const int Binary = 2;
    /// <summary>�\�i�@</summary>
    public const int Decimal = 10;
    /// <summary>�\�Z�i��</summary>
    public const int HexaDecimal = 16;

    /// <summary>����</summary>
    public const int Hour = 60;

    /// <summary>10�i�� => 2�i��</summary>
    public static string ToBinary(int value) => System.Convert.ToString(value, Binary);
    /// <summary>10�i�� => 16�i��</summary>
    public static string ToHexaDecimal(int value) => System.Convert.ToString(value, HexaDecimal);

    /// <summary>2�i�� => 10�i��</summary>
    public static int BinaryToDecimal(string value) => System.Convert.ToInt32(value, Binary);
    /// <summary>2�i�� => 16�i��</summary>
    public static string BinaryToHexaDecimal(string value) => ToHexaDecimal(BinaryToDecimal(value));

    /// <summary>16�i�� => 2�i��</summary>
    public static string HexaDecimalToBinary(string value) => ToBinary(HexaDecimalToDecimal(value));
    /// <summary>16�i�� => 10�i��</summary>
    public static int HexaDecimalToDecimal(string value) => System.Convert.ToInt32(value, HexaDecimal);

    /// <summary>�������Z</summary>
    public static int ToHour(this float value) => (int)(value / Hour / Hour);
    /// <summary>�������Z</summary>
    public static int ToHour(this double value) => (int)(value / Hour / Hour);

    /// <summary>�������Z</summary>
    public static int ToMinute(this float value) => (int)(value / Hour % Hour);
    /// <summary>�������Z</summary>
    public static int ToMinute(this double value) => (int)(value / Hour % Hour);

    /// <summary>�b�����Z</summary>
    public static float ToSecond(this float value) => value % Hour;
    /// <summary>�b�����Z</summary>
    public static double ToSecond(this double value) => value % Hour;

    /// <summary>���Ԋ��Z</summary>
    public static string ToTime(this float time) => $"{time.ToHour()}h : {time.ToMinute()}m : {(int)time.ToSecond()}s";
    /// <summary>���Ԋ��Z</summary>
    public static string ToTime(this double time) => $"{time.ToHour()}h : {time.ToMinute()}m : {(int)time.ToSecond()}s";

    /// <summary>�ŏ��l��Ԃ�</summary>
    public static T Min<T>(T a, T b) where T : System.IComparable<T>
    {
        return a.CompareTo(b) < default(int) ? a : b;
    }

    /// <summary>�ŏ��l��Ԃ�</summary>
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

    /// <summary>�ŏ��l��Ԃ�</summary>
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

    /// <summary>�ő�l��Ԃ�</summary>
    public static T Max<T>(T a, T b) where T : System.IComparable<T>
    {
        return (a.CompareTo(b) > default(int)) ? a : b;
    }

    /// <summary>�ő�l��Ԃ�</summary>
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

    /// <summary>�ő�l��Ԃ�</summary>
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

    /// <summary>�l�𐧌�</summary>
    public static T Clamp<T>(T value, T min, T max) where T : System.IComparable<T>
    {
        if (value.CompareTo(min) < default(int)) return min;
        if (value.CompareTo(max) > default(int)) return max;

        return value;
    }

    /// <summary>Max�l�𐧌�</summary>
    public static T ClampMax<T>(T value, T max) where T : System.IComparable<T>
    {
        return value.CompareTo(max) > default(int) ? max : value;
    }

    /// <summary>Min�l�𐧌�</summary>
    public static T ClampMin<T>(T value, T min) where T : System.IComparable<T>
    {
        return value.CompareTo(min) < default(int) ? min : value;
    }

    /// <summary>�܂�Ԃ�</summary>
    public static int PingPong(int value, int length)
    {
        int result = value % length;

        if (!IsEven(value / length))
        {
            result = length - result;
        }

        return result;
    }

    /// <summary>�������H</summary>
    public static bool IsEven(int num) => num % 2 == default;

    /// <summary>���</summary>
    /// <param name="a">0�n�_</param>
    /// <param name="b">1�n�_</param>
    /// <param name="t">��Ԓl</param>
    public static int Lerp(int a, int b, float t) => Mathf.RoundToInt(a + (b - a) * t);

    /// <summary>���</summary>
    /// <param name="a">0�n�_</param>
    /// <param name="b">1�n�_</param>
    /// <param name="t">��Ԓl</param>
    public static int Lerp01(int a, int b, float t) => Lerp(a, b, Mathf.Clamp01(t));

    /// <summary>����</summary>
    /// <param name="a">0�n�_</param>
    /// <param name="b">1�n�_</param>
    /// <param name="t">�l</param>
    public static float Ratio(float a, float b, float t) => a != b ? (t - a) / (b - a) : default;

    /// <summary>����</summary>
    /// <param name="a">0�n�_</param>
    /// <param name="b">1�n�_</param>
    /// <param name="t">�l</param>
    public static float Ratio01(float a, float b, float t) => Mathf.Clamp01(Ratio(a, b, t));

    /// <summary>����</summary>
    /// <param name="a">0�n�_</param>
    /// <param name="b">1�n�_</param>
    /// <param name="t">�l</param>
    public static float Ratio(int a, int b, int t) => a != b ? (t - a) / (float)(b - a) : default;

    /// <summary>����</summary>
    /// <param name="a">0�n�_</param>
    /// <param name="b">1�n�_</param>
    /// <param name="t">�l</param>
    public static float Ratio01(int a, int b, int t) => Mathf.Clamp01(Ratio(a, b, t));

    /// <summary>����</summary>
    public static int Digit(int num) => num >= default(int) ? num.ToString().Length : -(-num).ToString().Length;

    /// <summary>�����w�� �l�̌ܓ�</summary>
    public static float Round(this float value, int num) => (float)Round((double)value, num);

    /// <summary>�����w�� �l�̌ܓ�</summary>
    public static double Round(this double value, int num) => System.Math.Round(value, num, System.MidpointRounding.AwayFromZero);

    /// <summary>�l�̌ܓ�</summary>
    public static float Round(this float value) => Mathf.Round(value);
    /// <summary>�l�̌ܓ�ToInt</summary>
    public static int RoundToInt(this float value) => Mathf.RoundToInt(value);

    /// <summary>�؂�̂�</summary>
    public static float Floor(this float value) => Mathf.Floor(value);
    /// <summary>�؂�̂�ToInt</summary>
    public static float FloorToInt(this float value) => Mathf.FloorToInt(value);

    /// <summary>�؂�グ</summary>
    public static float Ceil(this float value) => Mathf.Ceil(value);
    /// <summary>�؂�グToInt</summary>
    public static int CeilToInt(this float value) => Mathf.CeilToInt(value);

    /// <summary>�قړ������H</summary>
    public static bool Approximately(this float l, float r) => UnityEngine.Mathf.Approximately(l, r);

    /// <summary>�w�茅���̐������쐬(�ꌅ��1, �񌅈ȍ~0)</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�쐬��������</param>
    /// <returns>�쐬�ł�����</returns>
    public static bool TryDigitMinNumber(int digit, out int value) => int.TryParse(StringEx.DigitMinNumber(digit), out value);

    /// <summary>�w�茅���̐������쐬(�����͂��ׂ�9)</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�쐬��������</param>
    /// <returns>�쐬�ł�����</returns>
    public static bool TryDigitMaxNumber(int digit, out int value) => int.TryParse(StringEx.DigitMaxNumber(digit), out value);

    public static float MoveTowards(float current, float target, float maxDelta)
    {
        if (Mathf.Abs(target - current) <= maxDelta)
        {
            return target;
        }

        return current + Mathf.Sign(target - current) * maxDelta;
    }

    /// <summary>�ő����</summary>
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

    /// <summary>�䗦</summary>
    public static Vector2Int VectorRatio(Vector2Int v)
    {
        var g = Gcd(v.x, v.y);

        v.x /= g;
        v.y /= g;

        return v;
    }
}
