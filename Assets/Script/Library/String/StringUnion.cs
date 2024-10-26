/// <summary>文字列結合</summary>
public class StringUnion
{
    /// <summary>StringBuilder</summary>
    public readonly System.Text.StringBuilder Builder = new();

    /// <summary>インターバル</summary>
    public readonly Interval Interval;

    /// <summary>現在の文字数</summary>
    public int Length => Builder.Length;

    /// <summary>全て結合したか</summary>
    public bool IsAll => Target.Length == Builder.Length;

    public bool Equal => Target == Builder.ToString();

    /// <summary>現在の文字列</summary>
    public string Current => Builder.ToString();

    /// <summary>表示する文字列</summary>
    public string Target { get; private set; }

    /// <param name="time">一文字結合する間隔</param>
    public StringUnion(float time)
    {
        Target = string.Empty;
        Interval = new(time, true);
    }

    /// <param name="i">Intervalの参照</param>
    public StringUnion(Interval i)
    {
        Interval = i;
        Target = string.Empty;
    }

    /// <summary>文字列をクリア</summary>
    /// <param name="text">結合する文字列</param>
    public void Clear(string text)
    {
        Target = text;

        Builder.Clear();
    }

    /// <summary>文字列を結合</summary>
    /// <param name="text">結合する文字列</param>
    /// <returns>現在結合済みの文字列</returns>
    public string Union(string text)
    {
        if (Target != text) Clear(text);

        if (Interval && !IsAll) Builder.Append(Target[Builder.Length]);

        return Current;
    }

    /// <summary>結合した文字列をTMPに代入</summary>
    /// <param name="text">結合する文字列</param>
    /// <param name="TMP">VolumeTMP</param>
    public void Union(string text, TMPro.TextMeshProUGUI TMP)
    {
        if (Target != text) Clear(text);

        if (Interval && !IsAll)
        {
            Builder.Append(Target[Builder.Length]);

            TMP.text = Current;
        }
    }
}
