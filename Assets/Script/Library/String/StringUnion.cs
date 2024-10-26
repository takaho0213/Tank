/// <summary>�����񌋍�</summary>
public class StringUnion
{
    /// <summary>StringBuilder</summary>
    public readonly System.Text.StringBuilder Builder = new();

    /// <summary>�C���^�[�o��</summary>
    public readonly Interval Interval;

    /// <summary>���݂̕�����</summary>
    public int Length => Builder.Length;

    /// <summary>�S�Č���������</summary>
    public bool IsAll => Target.Length == Builder.Length;

    public bool Equal => Target == Builder.ToString();

    /// <summary>���݂̕�����</summary>
    public string Current => Builder.ToString();

    /// <summary>�\�����镶����</summary>
    public string Target { get; private set; }

    /// <param name="time">�ꕶ����������Ԋu</param>
    public StringUnion(float time)
    {
        Target = string.Empty;
        Interval = new(time, true);
    }

    /// <param name="i">Interval�̎Q��</param>
    public StringUnion(Interval i)
    {
        Interval = i;
        Target = string.Empty;
    }

    /// <summary>��������N���A</summary>
    /// <param name="text">�������镶����</param>
    public void Clear(string text)
    {
        Target = text;

        Builder.Clear();
    }

    /// <summary>�����������</summary>
    /// <param name="text">�������镶����</param>
    /// <returns>���݌����ς݂̕�����</returns>
    public string Union(string text)
    {
        if (Target != text) Clear(text);

        if (Interval && !IsAll) Builder.Append(Target[Builder.Length]);

        return Current;
    }

    /// <summary>���������������TMP�ɑ��</summary>
    /// <param name="text">�������镶����</param>
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
