using System.Collections.Generic;

public static class StringEx
{
    /// <summary>���</summary>
    public const string Empty = "";
    /// <summary>�X�y�[�X</summary>
    public const string Space = " ";
    /// <summary>���s�R�[�h</summary>
    public const string NewLine = "\n";//System.Environment.NewLine
    /// <summary>�R���}</summary>
    public const string Comma = ",";
    /// <summary>null�I�[����</summary>
    public const string Final = "\0";

    /// <summary>�o�b�N�X�y�[�X</summary>
    public const string BackSpace = "\b";

    /// <summary>�N�I�[�e�[�V����</summary>
    public const string Quotation = "\'";
    /// <summary>�_�u���N�I�[�e�[�V����</summary>
    public const string DoubleQuotation = "\"";

    /// <summary>0 �` 9�܂ł̐���</summary>
    public const string Numbers = "0123456789";
    /// <summary>�啶���A���t�@�x�b�g</summary>
    public const string AlphabetsUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    /// <summary>�������A���t�@�x�b�g</summary>
    public const string AlphabetsLower = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>�p����</summary>
    public const string Alphanumerics = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    /// <summary>�v�f��A��</summary>
    public static string Join<T>(this IEnumerable<T> values) => values.Join(string.Empty);
    /// <summary>�v�f��A��</summary>
    /// <param name="separator">��؂镶����</param>
    public static string Join<T>(this IEnumerable<T> values, string separator) => string.Join(separator, values);
    /// <summary>�v�f���R���}��؂�ŘA��</summary>
    public static string JoinComma<T>(this IEnumerable<T> values) => values.Join(Comma);
    /// <summary>�v�f�����s�R�[�h�ŘA��</summary>
    public static string JoinNewLine<T>(this IEnumerable<T> values) => values.Join(NewLine);

    /// <summary>�R���}��؂蕪��</summary>
    public static string[] SplitComma(this string text) => text.Split(Comma);
    /// <summary>���s�R�[�h��؂蕪��</summary>
    public static string[] SplitNewLine(this string text) => text.Split(NewLine);

    /// <summary>�����񂪂����܂܂�Ă��邩</summary>
    public static int ContainsCount(this string text, string value) => text.Length - text.Replace(value, string.Empty).Length;

    /// <summary>�R���}�������܂܂�Ă��邩</summary>
    public static int CommaCount(this string text) => text.ContainsCount(Comma);
    /// <summary>�s��(���s�R�[�h�̐� + 1)</summary>
    public static int LineCount(this string text) => text.ContainsCount(NewLine) + 1;

    /// <summary>������̒����𐧌�</summary>
    public static string Clamp(this string text, int max) => text.Length.IsInOfMaxRange(max) ? text : text[..max];

    /// <summary>������Empty���H</summary>
    public static bool IsEmpty(this string text) => text == string.Empty;

    /// <summary>������̓�(0 �` count)�͈̔͂��폜</summary>
    public static string RemoveFirst(this string text, int count) => text.Remove(default, count);
    /// <summary>������̓�(count �` LastIndex)�͈̔͂��폜</summary>
    public static string RemoveLast(this string text, int count) => text.Remove(text.Length - count, count);
    /// <summary>�ʒu�w��̒u��</summary>
    public static string ReplaceIndex(this string text, int index, string replace) => text.Remove(index, replace.Length).Insert(index, replace);

    /// <summary>�C���f�N�T�[</summary>
    public static string Indexer(this string text, int i, char c)
    {
        var chars = text.ToCharArray();

        chars[i] = c;

        return chars.Concat();
    }

    /// <summary>char[]��string�ɕϊ�</summary>
    public static string Concat(this char[] chars) => new(chars);
    /// <summary>char[]��string�ɕϊ�</summary>
    public static string Concat<T>(this IEnumerable<T> values) => string.Concat(values);

    /// <summary>�w�茅���̐�����������쐬(�ꌅ��1, �񌅈ȍ~0)</summary>
    public static string DigitMinNumber(int digit)
    {
        char[] chars = new char[digit];

        for (int i = default; i < chars.Length; i++)
        {
            chars[i] = i == default ? CharEx.One : CharEx.FirstNumber;
        }

        return new(chars);
    }

    /// <summary>�w�茅���̐�����������쐬(�����͂��ׂ�9)</summary>
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
