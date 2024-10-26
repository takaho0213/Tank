public static class CompareEx
{
    /// <summary>Max�͈͓���</summary>
    public static bool IsInOfMaxRange<T>(this T value, T max) where T : System.IComparable<T> => value.CompareTo(max) <= default(int);
    /// <summary>Min�͈͓���</summary>
    public static bool IsInOfMinRange<T>(this T value, T min) where T : System.IComparable<T> => value.CompareTo(min) >= default(int);
    /// <summary>�͈͓���</summary>
    public static bool IsInOfRange<T>(this T value, T min, T max) where T : System.IComparable<T> => value.IsInOfMinRange(min) && value.IsInOfMaxRange(max);

    /// <summary>Max�͈͊O��</summary>
    public static bool IsOutOfMaxRange<T>(this T value, T max) where T : System.IComparable<T> => !IsInOfMaxRange(value, max);
    /// <summary>Min�͈͊O��</summary>
    public static bool IsOutOfMinRange<T>(this T value, T min) where T : System.IComparable<T> => !IsInOfMinRange(value, min);
    /// <summary>�͈͊O��</summary>
    public static bool IsOutOfRange<T>(this T value, T min, T max) where T : System.IComparable<T> => !IsInOfRange(value, min, max);
}
