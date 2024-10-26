using System.Collections.Generic;

public static class StringEx
{
    /// <summary>空間</summary>
    public const string Empty = "";
    /// <summary>スペース</summary>
    public const string Space = " ";
    /// <summary>改行コード</summary>
    public const string NewLine = "\n";//System.Environment.NewLine
    /// <summary>コンマ</summary>
    public const string Comma = ",";
    /// <summary>null終端文字</summary>
    public const string Final = "\0";

    /// <summary>バックスペース</summary>
    public const string BackSpace = "\b";

    /// <summary>クオーテーション</summary>
    public const string Quotation = "\'";
    /// <summary>ダブルクオーテーション</summary>
    public const string DoubleQuotation = "\"";

    /// <summary>0 ～ 9までの数字</summary>
    public const string Numbers = "0123456789";
    /// <summary>大文字アルファベット</summary>
    public const string AlphabetsUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    /// <summary>小文字アルファベット</summary>
    public const string AlphabetsLower = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>英数字</summary>
    public const string Alphanumerics = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    /// <summary>要素を連結</summary>
    public static string Join<T>(this IEnumerable<T> values) => values.Join(string.Empty);
    /// <summary>要素を連結</summary>
    /// <param name="separator">区切る文字列</param>
    public static string Join<T>(this IEnumerable<T> values, string separator) => string.Join(separator, values);
    /// <summary>要素をコンマ区切りで連結</summary>
    public static string JoinComma<T>(this IEnumerable<T> values) => values.Join(Comma);
    /// <summary>要素を改行コードで連結</summary>
    public static string JoinNewLine<T>(this IEnumerable<T> values) => values.Join(NewLine);

    /// <summary>コンマ区切り分割</summary>
    public static string[] SplitComma(this string text) => text.Split(Comma);
    /// <summary>改行コード区切り分割</summary>
    public static string[] SplitNewLine(this string text) => text.Split(NewLine);

    /// <summary>文字列がいくつ含まれているか</summary>
    public static int ContainsCount(this string text, string value) => text.Length - text.Replace(value, string.Empty).Length;

    /// <summary>コンマがいくつ含まれているか</summary>
    public static int CommaCount(this string text) => text.ContainsCount(Comma);
    /// <summary>行数(改行コードの数 + 1)</summary>
    public static int LineCount(this string text) => text.ContainsCount(NewLine) + 1;

    /// <summary>文字列の長さを制限</summary>
    public static string Clamp(this string text, int max) => text.Length.IsInOfMaxRange(max) ? text : text[..max];

    /// <summary>文字列がEmptyか？</summary>
    public static bool IsEmpty(this string text) => text == string.Empty;

    /// <summary>文字列の内(0 ～ count)の範囲を削除</summary>
    public static string RemoveFirst(this string text, int count) => text.Remove(default, count);
    /// <summary>文字列の内(count ～ LastIndex)の範囲を削除</summary>
    public static string RemoveLast(this string text, int count) => text.Remove(text.Length - count, count);
    /// <summary>位置指定の置換</summary>
    public static string ReplaceIndex(this string text, int index, string replace) => text.Remove(index, replace.Length).Insert(index, replace);

    /// <summary>インデクサー</summary>
    public static string Indexer(this string text, int i, char c)
    {
        var chars = text.ToCharArray();

        chars[i] = c;

        return chars.Concat();
    }

    /// <summary>char[]をstringに変換</summary>
    public static string Concat(this char[] chars) => new(chars);
    /// <summary>char[]をstringに変換</summary>
    public static string Concat<T>(this IEnumerable<T> values) => string.Concat(values);

    /// <summary>指定桁数の数字文字列を作成(一桁目1, 二桁以降0)</summary>
    public static string DigitMinNumber(int digit)
    {
        char[] chars = new char[digit];

        for (int i = default; i < chars.Length; i++)
        {
            chars[i] = i == default ? CharEx.One : CharEx.FirstNumber;
        }

        return new(chars);
    }

    /// <summary>指定桁数の数字文字列を作成(数字はすべて9)</summary>
    public static string DigitMaxNumber(int digit)
    {
        char[] chars = new char[digit];

        for (int i = default; i < chars.Length; i++)
        {
            chars[i] = CharEx.LastNumber;
        }

        return new(chars);
    }
}
