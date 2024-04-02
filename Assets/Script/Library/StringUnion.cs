using UnityEngine;

/// <summary>文字列の結合を行うクラス</summary>
[System.Serializable]
public class StringUnion
{
    /// <summary>インターバル</summary>
    [SerializeField] private Interval interval;

    /// <summary>StringBuilder</summary>
    private System.Text.StringBuilder builder;

    /// <summary>インターバル</summary>
    public Interval Interval => interval;

    /// <summary>全て結合したか</summary>
    public bool IsAll => Text.Length == builder.Length;

    /// <summary>表示する文字列</summary>
    public string Text { get; private set; }

    /// <summary>現在の文字数</summary>
    public int Count { get; private set; }

    /// <param name="time">一文字結合する間隔</param>
    public StringUnion(float time)
    {
        builder ??= new();
        Text = string.Empty;
        interval = new(time, true);
    }

    /// <param name="i">Intervalの参照</param>
    public StringUnion(Interval i)
    {
        interval = i;
        builder ??= new();
        Text = string.Empty;
    }

    /// <summary>結合をリセット</summary>
    /// <param name="text">結合する文字列</param>
    public void ReSet(string text)
    {
        Text = text;

        builder.Length = Count = default;
    }

    /// <summary>文字列を結合</summary>
    /// <param name="text">結合する文字列</param>
    /// <returns>現在結合済みの文字列</returns>
    public string Union(string text)
    {
        if (Text != text) ReSet(text);

        if (interval && !IsAll) builder.Append(text[Count++]);

        return ToString();
    }

    /// <returns>現在結合済みの文字列</returns>
    public override string ToString() => builder.ToString();

    public char this[int i] => builder[i];

    public static implicit operator string(StringUnion c) => c.builder.ToString();
}
