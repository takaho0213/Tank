using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class RandomEx
{
    /// <summary>System�̃����_��</summary>
    public static readonly System.Random SystemRandom = new();

    /// <summary>�����_����bool�^�̒l��Ԃ�</summary>
    public static bool Bool => Random.value <= MathEx.Half;

    /// <summary>Min �` Max���̒l��Ԃ�</summary>
    public static int Range(int min, int max) => MathEx.Lerp(min, max, (float)SystemRandom.NextDouble());
    /// <summary>Min �` Max���̒l��Ԃ�</summary>
    public static float Range(float min, float max) => Mathf.Lerp(min, max, (float)SystemRandom.NextDouble());

    /// <summary>�����_���ɂǂ��炩�̒l��Ԃ�</summary>
    public static T Either<T>(T a, T b) => Bool ? a : b;

    /// <summary>�����_����0 �` 9��char�^�̒l��Ԃ�</summary>
    public static char Number() => (char)Range(CharEx.FirstNumber, CharEx.LastNumber);

    /// <summary>�����_���ɑ啶���A���t�@�x�b�g��Ԃ�</summary>
    public static char AlphabetUpper() => (char)Range(CharEx.FirstAlphabetUpper, CharEx.LastAlphabetUpper);

    /// <summary>�����_���Ɏq�����A���t�@�x�b�g��Ԃ�</summary>
    public static char AlphabetLower() => (char)Range(CharEx.FirstAlphabetLower, CharEx.LastAlphabetLower);

    /// <summary>�����_���ɉp������Ԃ�</summary>
    public static char Alphanumeric()
    {
        char value = (char)Range(CharEx.FirstNumber, CharEx.LastAlphabetLower);

        return value.IsAlphanumeric() ? value : Alphanumeric();
    }

    /// <summary>�����_���Ɏw�茅���̐�����string�^�ŕԂ�</summary>
    public static string DigitNumber(int digit)
    {
        char[] chars = new char[digit];

        for (int i = default; i < chars.Length; i++)
        {
            chars[i] = Number();
        }

        return new(chars);
    }

    /// <summary>�w�茅���̐�����Ԃ�</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�l</param>
    /// <returns>�������쐬�ł�����</returns>
    public static bool TryDigitNumber(int digit, out sbyte value) => sbyte.TryParse(DigitNumber(digit), out value);
    /// <summary>�w�茅���̐�����Ԃ�</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�l</param>
    /// <returns>�������쐬�ł�����</returns>
    public static bool TryDigitNumber(int digit, out byte value) => byte.TryParse(DigitNumber(digit), out value);
    /// <summary>�w�茅���̐�����Ԃ�</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�l</param>
    /// <returns>�������쐬�ł�����</returns>
    public static bool TryDigitNumber(int digit, out short value) => short.TryParse(DigitNumber(digit), out value);
    /// <summary>�w�茅���̐�����Ԃ�</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�l</param>
    /// <returns>�������쐬�ł�����</returns>
    public static bool TryDigitNumber(int digit, out ushort value) => ushort.TryParse(DigitNumber(digit), out value);
    /// <summary>�w�茅���̐�����Ԃ�</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�l</param>
    /// <returns>�������쐬�ł�����</returns>
    public static bool TryDigitNumber(int digit, out int value) => int.TryParse(DigitNumber(digit), out value);
    /// <summary>�w�茅���̐�����Ԃ�</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�l</param>
    /// <returns>�������쐬�ł�����</returns>
    public static bool TryDigitNumber(int digit, out uint value) => uint.TryParse(DigitNumber(digit), out value);
    /// <summary>�w�茅���̐�����Ԃ�</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�l</param>
    /// <returns>�������쐬�ł�����</returns>
    public static bool TryDigitNumber(int digit, out long value) => long.TryParse(DigitNumber(digit), out value);
    /// <summary>�w�茅���̐�����Ԃ�</summary>
    /// <param name="digit">����</param>
    /// <param name="value">�l</param>
    /// <returns>�������쐬�ł�����</returns>
    public static bool TryDigitNumber(int digit, out ulong value) => ulong.TryParse(DigitNumber(digit), out value);

    /// <summary>0�`Max�̒l��Ԃ�</summary>
    public static int RangeMax(int max) => Range(default, max);
    /// <summary>0�`Max�̒l��Ԃ�</summary>
    public static float RangeMax(float max) => Range(default, max);

    /// <summary>�����_���ɗv�f��Ԃ�</summary>
    public static T Array<T>(IEnumerable<T> array) => array.ElementAt(RangeMax(array.LastIndex()));

    /// <summary>�����_���ɔ͈͓��̃C���f�b�N�X��Ԃ�</summary>
    public static int Index<T>(IEnumerable<T> array) => RangeMax(array.LastIndex());

    /// <summary>0 �` 1���̊m��</summary>
    /// <param name="value">�m��</param>
    /// <returns>value�������_��(0 �` 1)�Ȓl���ォ</returns>
    public static bool Probability01(float value)
    {
        value = Mathf.Clamp01(value);

        var rand = UnityEngine.Random.value;

        return rand == MathEx.OneF || rand < value;
    }

    /// <summary>�ŏ��l �` �ő�l���̊m��</summary>
    /// <param name="value">�l</param>
    /// <param name="min">�ŏ��l</param>
    /// <param name="max">�ő�l</param>
    /// <returns>�l�������_���Ȓl(�ŏ��l �` �ő�l)���ォ</returns>
    public static bool Probability(float value, float min, float max)
    {
        return Probability01(Mathf.InverseLerp(min, max, value));
    }

    /// <summary>�ŏ��l �` �ő�l���̊m��</summary>
    /// <param name="value">�l</param>
    /// <param name="min">�ŏ��l</param>
    /// <param name="max">�ő�l</param>
    /// <returns>�l�������_���Ȓl(�ŏ��l �` �ő�l)���ォ</returns>
    public static bool Probability(int value, int min, int max)
    {
        var rand = Range(min, max);

        return rand == max || rand < value;
    }

    /// <summary>0 �` 100���̊m��</summary>
    /// <param name="value">�l</param>
    /// <returns>�l�������_���Ȓl(�ŏ��l �` �ő�l)���ォ</returns>
    public static bool Probability0100(int value)
    {
        value = MathEx.Clamp(value, default, MathEx.OneHundred);

        var rand = Range(default, MathEx.OneHundred);

        return rand == MathEx.OneHundred || rand < value;
    }

    /// <summary>0 �` �ő�l���̊m��</summary>
    /// <param name="value">�l</param>
    /// <param name="max">�ő�l</param>
    /// <returns>�l�������_���Ȓl(0 �` �ő�l)���ォ</returns>
    public static bool Probability(float value, float max)
    {
        return Probability01(Mathf.InverseLerp(default, max, value));
    }

    /// <summary>�m��</summary>
    /// <param name="value">�l</param>
    /// <param name="min">�ŏ��l</param>
    /// <param name="max">�ő�l</param>
    /// <param name="rand">�����_���Ȓl</param>
    /// <returns>�l�������_���Ȓl���ォ</returns>
    public static bool Probability(float value, float min, float max, float rand)
    {
        rand = Mathf.Clamp(rand, min, max);

        value = Mathf.Clamp(value, min, max);

        return rand == max || rand <= value;
    }

    /// <summary>�d�ݕt���̒��I</summary>
    /// <returns>���������v�f</returns>
    public static T Lottery<T>(IEnumerable<T> source, System.Func<T, float> selector)
    {
        float total = default;

        foreach (var item in source) total += selector.Invoke(item);

        return Lottery(source, selector, total);
    }

    /// <summary>�d�ݕt���̒��I</summary>
    /// <param name="total">���v�l</param>
    /// <returns>���������v�f</returns>
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
