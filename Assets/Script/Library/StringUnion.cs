using UnityEngine;

/// <summary>������̌������s���N���X</summary>
[System.Serializable]
public class StringUnion
{
    /// <summary>�C���^�[�o��</summary>
    [SerializeField] private Interval interval;

    /// <summary>StringBuilder</summary>
    private System.Text.StringBuilder builder;

    /// <summary>�C���^�[�o��</summary>
    public Interval Interval => interval;

    /// <summary>�S�Č���������</summary>
    public bool IsAll => Text.Length == builder.Length;

    /// <summary>�\�����镶����</summary>
    public string Text { get; private set; }

    /// <summary>���݂̕�����</summary>
    public int Count { get; private set; }

    /// <param name="time">�ꕶ����������Ԋu</param>
    public StringUnion(float time)
    {
        builder ??= new();
        Text = string.Empty;
        interval = new(time, true);
    }

    /// <param name="i">Interval�̎Q��</param>
    public StringUnion(Interval i)
    {
        interval = i;
        builder ??= new();
        Text = string.Empty;
    }

    /// <summary>���������Z�b�g</summary>
    /// <param name="text">�������镶����</param>
    public void ReSet(string text)
    {
        Text = text;

        builder.Length = Count = default;
    }

    /// <summary>�����������</summary>
    /// <param name="text">�������镶����</param>
    /// <returns>���݌����ς݂̕�����</returns>
    public string Union(string text)
    {
        if (Text != text) ReSet(text);

        if (interval && !IsAll) builder.Append(text[Count++]);

        return ToString();
    }

    /// <returns>���݌����ς݂̕�����</returns>
    public override string ToString() => builder.ToString();

    public char this[int i] => builder[i];

    public static implicit operator string(StringUnion c) => c.builder.ToString();
}
