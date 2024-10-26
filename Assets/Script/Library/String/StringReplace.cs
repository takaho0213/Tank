using UnityEngine;

[System.Serializable]
public class StringReplace
{
    /// <summary>�e�L�X�g</summary>
    [SerializeField, TextArea] protected string text;

    /// <summary>�����e�L�X�g</summary>
    [SerializeField] protected string old;

    /// <summary>�e�L�X�g</summary>
    public string Text { get => text; set => text = value; }

    /// <summary>�����e�L�X�g</summary>
    public string Old { get => old; set => old = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old">�����e�L�X�g</param>
    public StringReplace(string text, string old)
    {
        this.text = text;
        this.old = old;
    }

    /// <summary>�u��</summary>
    /// <param name="text">�u���e�L�X�g</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace(string text) => this.text.Replace(old, text);
}

[System.Serializable]
public class StringReplace2 : StringReplace
{
    /// <summary>�����e�L�X�g2</summary>
    [SerializeField] protected string old2;

    /// <summary>�����e�L�X�g2</summary>
    public string Old2 { get => old2; set => old2 = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old1">�����e�L�X�g1</param>
    /// <param name="old2">�����e�L�X�g2</param>
    public StringReplace2(string text, string old1, string old2) : base(text, old1)
    {
        this.old2 = old2;
    }

    /// <summary>�u��</summary>
    /// <param name="text1">�u���e�L�X�g1</param>
    /// <param name="text2">�u���e�L�X�g2</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace2(string text1, string text2)
    {
        return Replace(text1).Replace(old2, text2);
    }
}

[System.Serializable]
public class StringReplace3 : StringReplace2
{
    /// <summary>�����e�L�X�g3</summary>
    [SerializeField] protected string old3;

    /// <summary>�����e�L�X�g3</summary>
    public string Old3 { get => old3; set => old3 = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old1">�����e�L�X�g1</param>
    /// <param name="old2">�����e�L�X�g2</param>
    /// <param name="old3">�����e�L�X�g3</param>
    public StringReplace3(string text, string old1, string old2, string old3) : base(text, old1, old2)
    {
        this.old3 = old3;
    }

    /// <summary>3�����̒u��</summary>
    /// <param name="text1">�u���e�L�X�g1</param>
    /// <param name="text2">�u���e�L�X�g2</param>
    /// <param name="text3">�u���e�L�X�g3</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace3(string text1, string text2, string text3)
    {
        return Replace2(text1, text2).Replace(old3, text3);
    }
}

[System.Serializable]
public class StringReplace4 : StringReplace3
{
    /// <summary>�����e�L�X�g4</summary>
    [SerializeField] protected string old4;

    /// <summary>�����e�L�X�g4</summary>
    public string Old4 { get => old4; set => old4 = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old1">�����e�L�X�g1</param>
    /// <param name="old2">�����e�L�X�g2</param>
    /// <param name="old3">�����e�L�X�g3</param>
    /// <param name="old4">�����e�L�X�g4</param>
    public StringReplace4(string text, string old1, string old2, string old3, string old4) : base(text, old1, old2, old3)
    {
        this.old4 = old4;
    }

    /// <summary>4�����̒u��</summary>
    /// <param name="text1">�u���e�L�X�g1</param>
    /// <param name="text2">�u���e�L�X�g2</param>
    /// <param name="text3">�u���e�L�X�g3</param>
    /// <param name="text4">�u���e�L�X�g4</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace4(string text1, string text2, string text3, string text4)
    {
        return Replace3(text1, text2, text3).Replace(old4, text4);
    }
}

[System.Serializable]
public class StringReplace5 : StringReplace4
{
    /// <summary>�����e�L�X�g5</summary>
    [SerializeField] protected string old5;

    /// <summary>�����e�L�X�g5</summary>
    public string Old5 { get => old5; set => old5 = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old1">�����e�L�X�g1</param>
    /// <param name="old2">�����e�L�X�g2</param>
    /// <param name="old3">�����e�L�X�g3</param>
    /// <param name="old4">�����e�L�X�g4</param>
    /// <param name="old5">�����e�L�X�g5</param>
    public StringReplace5(string text, string old1, string old2, string old3, string old4, string old5) : base(text, old1, old2, old3, old4)
    {
        this.old5 = old5;
    }

    /// <summary>5�����̒u��</summary>
    /// <param name="text1">�u���e�L�X�g1</param>
    /// <param name="text2">�u���e�L�X�g2</param>
    /// <param name="text3">�u���e�L�X�g3</param>
    /// <param name="text4">�u���e�L�X�g4</param>
    /// <param name="text5">�u���e�L�X�g5</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace5(string text1, string text2, string text3, string text4, string text5)
    {
        return Replace4(text1, text2, text3, text4).Replace(old5, text5);
    }
}

[System.Serializable]
public class StringReplace6 : StringReplace5
{
    /// <summary>�����e�L�X�g6</summary>
    [SerializeField] protected string old6;

    /// <summary>�����e�L�X�g6</summary>
    public string Old6 { get => old6; set => old6 = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old1">�����e�L�X�g1</param>
    /// <param name="old2">�����e�L�X�g2</param>
    /// <param name="old3">�����e�L�X�g3</param>
    /// <param name="old4">�����e�L�X�g4</param>
    /// <param name="old5">�����e�L�X�g5</param>
    /// <param name="old6">�����e�L�X�g6</param>
    public StringReplace6(string text, string old1, string old2, string old3, string old4, string old5, string old6)
        : base(text, old1, old2, old3, old4, old5)
    {
        this.old6 = old6;
    }

    /// <summary>6�����̒u��</summary>
    /// <param name="text1">�u���e�L�X�g1</param>
    /// <param name="text2">�u���e�L�X�g2</param>
    /// <param name="text3">�u���e�L�X�g3</param>
    /// <param name="text4">�u���e�L�X�g4</param>
    /// <param name="text5">�u���e�L�X�g5</param>
    /// <param name="text6">�u���e�L�X�g6</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace6(string text1, string text2, string text3, string text4, string text5, string text6)
    {
        return Replace5(text1, text2, text3, text4, text5).Replace(old6, text6);
    }
}

[System.Serializable]
public class StringReplace7 : StringReplace6
{
    /// <summary>�����e�L�X�g7</summary>
    [SerializeField] protected string old7;

    /// <summary>�����e�L�X�g7</summary>
    public string Old7 { get => old7; set => old7 = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old1">�����e�L�X�g1</param>
    /// <param name="old2">�����e�L�X�g2</param>
    /// <param name="old3">�����e�L�X�g3</param>
    /// <param name="old4">�����e�L�X�g4</param>
    /// <param name="old5">�����e�L�X�g5</param>
    /// <param name="old6">�����e�L�X�g6</param>
    /// <param name="old7">�����e�L�X�g7</param>
    public StringReplace7(string text, string old1, string old2, string old3, string old4, string old5, string old6, string old7)
        : base(text, old1, old2, old3, old4, old5, old6)
    {
        this.old7 = old7;
    }

    /// <summary>7�����̒u��</summary>
    /// <param name="text1">�u���e�L�X�g1</param>
    /// <param name="text2">�u���e�L�X�g2</param>
    /// <param name="text3">�u���e�L�X�g3</param>
    /// <param name="text4">�u���e�L�X�g4</param>
    /// <param name="text5">�u���e�L�X�g5</param>
    /// <param name="text6">�u���e�L�X�g6</param>
    /// <param name="text7">�u���e�L�X�g7</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace7(string text1, string text2, string text3, string text4, string text5, string text6, string text7)
    {
        return Replace6(text1, text2, text3, text4, text5, text6).Replace(old7, text7);
    }
}

[System.Serializable]
public class StringReplace8 : StringReplace7
{
    /// <summary>�����e�L�X�g8</summary>
    [SerializeField] protected string old8;

    /// <summary>�����e�L�X�g8</summary>
    public string Old8 { get => old8; set => old8 = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old1">�����e�L�X�g1</param>
    /// <param name="old2">�����e�L�X�g2</param>
    /// <param name="old3">�����e�L�X�g3</param>
    /// <param name="old4">�����e�L�X�g4</param>
    /// <param name="old5">�����e�L�X�g5</param>
    /// <param name="old6">�����e�L�X�g6</param>
    /// <param name="old7">�����e�L�X�g7</param>
    /// <param name="old8">�����e�L�X�g8</param>
    public StringReplace8(string text, string old1, string old2, string old3, string old4, string old5, string old6, string old7, string old8)
        : base(text, old1, old2, old3, old4, old5, old6, old7)
    {
        this.old8 = old8;
    }

    /// <summary>8�����̒u��</summary>
    /// <param name="text1">�u���e�L�X�g1</param>
    /// <param name="text2">�u���e�L�X�g2</param>
    /// <param name="text3">�u���e�L�X�g3</param>
    /// <param name="text4">�u���e�L�X�g4</param>
    /// <param name="text5">�u���e�L�X�g5</param>
    /// <param name="text6">�u���e�L�X�g6</param>
    /// <param name="text7">�u���e�L�X�g7</param>
    /// <param name="text8">�u���e�L�X�g8</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace8(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8)
    {
        return Replace7(text1, text2, text3, text4, text5, text6, text7).Replace(old8, text8);
    }
}

[System.Serializable]
public class StringReplace9 : StringReplace8
{
    /// <summary>�����e�L�X�g9</summary>
    [SerializeField] protected string old9;

    /// <summary>�����e�L�X�g9</summary>
    public string Old9 { get => old9; set => old9 = value; }

    /// <param name="text">�e�L�X�g</param>
    /// <param name="old1">�����e�L�X�g1</param>
    /// <param name="old2">�����e�L�X�g2</param>
    /// <param name="old3">�����e�L�X�g3</param>
    /// <param name="old4">�����e�L�X�g4</param>
    /// <param name="old5">�����e�L�X�g5</param>
    /// <param name="old6">�����e�L�X�g6</param>
    /// <param name="old7">�����e�L�X�g7</param>
    /// <param name="old8">�����e�L�X�g8</param>
    /// <param name="old9">�����e�L�X�g9</param>
    public StringReplace9(string text, string old1, string old2, string old3, string old4, string old5, string old6, string old7, string old8, string old9)
        : base(text, old1, old2, old3, old4, old5, old6, old7, old8)
    {
        this.old9 = old9;
    }

    /// <summary>9�����̒u��</summary>
    /// <param name="text1">�u���e�L�X�g1</param>
    /// <param name="text2">�u���e�L�X�g2</param>
    /// <param name="text3">�u���e�L�X�g3</param>
    /// <param name="text4">�u���e�L�X�g4</param>
    /// <param name="text5">�u���e�L�X�g5</param>
    /// <param name="text6">�u���e�L�X�g6</param>
    /// <param name="text7">�u���e�L�X�g7</param>
    /// <param name="text8">�u���e�L�X�g8</param>
    /// <param name="text9">�u���e�L�X�g9</param>
    /// <returns>�u���ς݃e�L�X�g</returns>
    public string Replace9(string text1, string text2, string text3, string text4, string text5,string text6, string text7, string text8, string text9)
    {
        return Replace8(text1, text2, text3, text4, text5, text6, text7, text8).Replace(old9, text9);
    }
}

public class StringReplaceArray
{
    [SerializeField, TextArea] private string text;

    [SerializeField] private string[] olds;

    public string Text { get => text; set => text = value; }

    public string[] Olds { get => olds; set => olds = value; }

    public StringReplaceArray(string text, string[] olds)
    {
        this.text = text;
        this.olds = olds;
    }

    public string Replace(params string[] texts)
    {
        int length = UnityEngine.Mathf.Min(texts.Length, olds.Length);

        string text = this.text;

        for (int i = default; i < length; i++)
        {
            text = text.Replace(olds[i], texts[i]);
        }

        return text;
    }
}
