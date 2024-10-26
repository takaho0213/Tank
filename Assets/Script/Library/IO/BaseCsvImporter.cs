using System;
using UnityEngine;

public delegate bool TryParse<T>(string s, out T result);

public abstract class BaseCsvImporter
{
    /// <summary>CsvファイルReader</summary>
    [SerializeField] protected CsvAssetReader CsvAsset;

    /// <summary>読み取る要素の数</summary>
    public abstract int Length { get; }

    /// <summary>最初の列</summary>
    public int FirstRow { get; set; }

    /// <summary>Csvテキストの行数</summary>
    public int TextsLength => CsvAsset.Text.Length;

    /// <summary>CsvAssetReaderのSetter</summary>
    public void SetCsv(CsvAssetReader csv) => CsvAsset = csv;

    /// <summary>CsvAssetReaderのSetter</summary>
    public void SetCsv(BaseCsvImporter copy) => CsvAsset = copy.CsvAsset;
}

public static class CsvImporter
{
    /// <summary>投げる例外</summary>
    /// <param name="text">変換に失敗したテキスト</param>
    /// <param name="type">変換するデータ型</param>
    /// <returns>例外</returns>
    private static ArgumentException Exception(string text, Type type) => new($"\"{text}\" は {type} に変換できませんでした");

    /// <summary>TryParseに例外を追加</summary>
    /// <param name="tryParse">TryParse</param>
    /// <returns>例外を追加したTryParse</returns>
    public static TryParse<T> AddException<T>(this TryParse<T> tryParse)
    {
        return (string text, out T value) => tryParse(text, out value) ? true : throw Exception(text, typeof(T));
    }

    /// <summary>byte</summary>
    public static bool TryParse(string s, out byte result) => byte.TryParse(s, out result);
    /// <summary>sbyte</summary>
    public static bool TryParse(string s, out sbyte result) => sbyte.TryParse(s, out result);
    /// <summary>short</summary>
    public static bool TryParse(string s, out short result) => short.TryParse(s, out result);
    /// <summary>ushort</summary>
    public static bool TryParse(string s, out ushort result) => ushort.TryParse(s, out result);
    /// <summary>int</summary>
    public static bool TryParse(string s, out int result) => int.TryParse(s, out result);
    /// <summary>uint</summary>
    public static bool TryParse(string s, out uint result) => uint.TryParse(s, out result);
    /// <summary>long</summary>
    public static bool TryParse(string s, out long result) => long.TryParse(s, out result);
    /// <summary>ulong</summary>
    public static bool TryParse(string s, out ulong result) => ulong.TryParse(s, out result);

    /// <summary>float</summary>
    public static bool TryParse(string s, out float result) => float.TryParse(s, out result);
    /// <summary>double</summary>
    public static bool TryParse(string s, out double result) => double.TryParse(s, out result);
    /// <summary>decimal</summary>
    public static bool TryParse(string s, out decimal result) => decimal.TryParse(s, out result);

    /// <summary>bool</summary>
    public static bool TryParse(string s, out bool result) => bool.TryParse(s, out result);
    /// <summary>char</summary>
    public static bool TryParse(string s, out char result) => char.TryParse(s, out result);
    /// <summary>string</summary>
    public static bool TryParse(string s, out string result) => (result = s) != null;

    /// <summary>eunm</summary>
    public static bool TryParse<T>(string s, out T result) where T : struct, Enum => Enum.TryParse(s, out result);

    /// <summary>DateTime</summary>
    public static bool TryParse(string s, out DateTime result) => DateTime.TryParse(s, out result);
    /// <summary>DateTimeOffset</summary>
    public static bool TryParse(string s, out DateTimeOffset result) => DateTimeOffset.TryParse(s, out result);
}
