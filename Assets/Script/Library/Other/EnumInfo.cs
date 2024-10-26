using System;
using System.Linq;

/// <summary></summary>
public static class EnumInfo<T> where T : Enum
{
    /// <summary>タイプ</summary>
    public static readonly System.Type Type;

    /// <summary>項目数</summary>
    public static readonly int Length;

    /// <summary>項目配列</summary>
    public static readonly T[] Values;

    /// <summary>intに変換した項目配列</summary>
    public static readonly int[] IntValues;

    /// <summary>stringに変換した項目配列</summary>
    public static readonly string[] StringValues;

    static EnumInfo()
    {
        Type = typeof(T);
        Values = (T[])Enum.GetValues(Type);
        StringValues = Enum.GetNames(Type);
        IntValues = Values.Select(value => System.Convert.ToInt32(value)).ToArray();
        Length = Values.Length;
    }

    public static string Format(object value, string format) => Enum.Format(Type, value, format);
    public static string GetName(object value) => Enum.GetName(Type, value);
    public static Type GetUnderlyingType() => Enum.GetUnderlyingType(Type);
    public static bool IsDefined(object num) => Enum.IsDefined(Type, num);
    public static T Parse(string value) => (T)Enum.Parse(Type, value);
    public static T Parse(string value, bool ignoreCase) => (T)Enum.Parse(Type, value, ignoreCase);

    public static T ToObject(sbyte value)  => (T)Enum.ToObject(Type, value);
    public static T ToObject(short value)  => (T)Enum.ToObject(Type, value);
    public static T ToObject(int value)    => (T)Enum.ToObject(Type, value);
    public static T ToObject(long value)   => (T)Enum.ToObject(Type, value);
    public static T ToObject(byte value)   => (T)Enum.ToObject(Type, value);
    public static T ToObject(ushort value) => (T)Enum.ToObject(Type, value);
    public static T ToObject(uint value)   => (T)Enum.ToObject(Type, value);
    public static T ToObject(ulong value)  => (T)Enum.ToObject(Type, value);
    public static T ToObject(object value) => (T)Enum.ToObject(Type, value);

    public static bool TryParse(string text, out T result)
    {
        var num = System.Enum.TryParse(Type, text, out var r);

        result = (T)r;

        return num;
    }

    public static bool TryParse(string value, bool ignoreCase, out T result)
    {
        var num = Enum.TryParse(Type, value, ignoreCase, out var r);

        result = (T)r;

        return num;
    }
}
