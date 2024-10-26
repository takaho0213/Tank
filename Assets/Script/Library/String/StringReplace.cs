using UnityEngine;

[System.Serializable]
public class StringReplace
{
    /// <summary>テキスト</summary>
    [SerializeField, TextArea] protected string text;

    /// <summary>検索テキスト</summary>
    [SerializeField] protected string old;

    /// <summary>テキスト</summary>
    public string Text { get => text; set => text = value; }

    /// <summary>検索テキスト</summary>
    public string Old { get => old; set => old = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old">検索テキスト</param>
    public StringReplace(string text, string old)
    {
        this.text = text;
        this.old = old;
    }

    /// <summary>置換</summary>
    /// <param name="text">置換テキスト</param>
    /// <returns>置換済みテキスト</returns>
    public string Replace(string text) => this.text.Replace(old, text);
}

[System.Serializable]
public class StringReplace2 : StringReplace
{
    /// <summary>検索テキスト2</summary>
    [SerializeField] protected string old2;

    /// <summary>検索テキスト2</summary>
    public string Old2 { get => old2; set => old2 = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old1">検索テキスト1</param>
    /// <param name="old2">検索テキスト2</param>
    public StringReplace2(string text, string old1, string old2) : base(text, old1)
    {
        this.old2 = old2;
    }

    /// <summary>置換</summary>
    /// <param name="text1">置換テキスト1</param>
    /// <param name="text2">置換テキスト2</param>
    /// <returns>置換済みテキスト</returns>
    public string Replace2(string text1, string text2)
    {
        return Replace(text1).Replace(old2, text2);
    }
}

[System.Serializable]
public class StringReplace3 : StringReplace2
{
    /// <summary>検索テキスト3</summary>
    [SerializeField] protected string old3;

    /// <summary>検索テキスト3</summary>
    public string Old3 { get => old3; set => old3 = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old1">検索テキスト1</param>
    /// <param name="old2">検索テキスト2</param>
    /// <param name="old3">検索テキスト3</param>
    public StringReplace3(string text, string old1, string old2, string old3) : base(text, old1, old2)
    {
        this.old3 = old3;
    }

    /// <summary>3か所の置換</summary>
    /// <param name="text1">置換テキスト1</param>
    /// <param name="text2">置換テキスト2</param>
    /// <param name="text3">置換テキスト3</param>
    /// <returns>置換済みテキスト</returns>
    public string Replace3(string text1, string text2, string text3)
    {
        return Replace2(text1, text2).Replace(old3, text3);
    }
}

[System.Serializable]
public class StringReplace4 : StringReplace3
{
    /// <summary>検索テキスト4</summary>
    [SerializeField] protected string old4;

    /// <summary>検索テキスト4</summary>
    public string Old4 { get => old4; set => old4 = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old1">検索テキスト1</param>
    /// <param name="old2">検索テキスト2</param>
    /// <param name="old3">検索テキスト3</param>
    /// <param name="old4">検索テキスト4</param>
    public StringReplace4(string text, string old1, string old2, string old3, string old4) : base(text, old1, old2, old3)
    {
        this.old4 = old4;
    }

    /// <summary>4か所の置換</summary>
    /// <param name="text1">置換テキスト1</param>
    /// <param name="text2">置換テキスト2</param>
    /// <param name="text3">置換テキスト3</param>
    /// <param name="text4">置換テキスト4</param>
    /// <returns>置換済みテキスト</returns>
    public string Replace4(string text1, string text2, string text3, string text4)
    {
        return Replace3(text1, text2, text3).Replace(old4, text4);
    }
}

[System.Serializable]
public class StringReplace5 : StringReplace4
{
    /// <summary>検索テキスト5</summary>
    [SerializeField] protected string old5;

    /// <summary>検索テキスト5</summary>
    public string Old5 { get => old5; set => old5 = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old1">検索テキスト1</param>
    /// <param name="old2">検索テキスト2</param>
    /// <param name="old3">検索テキスト3</param>
    /// <param name="old4">検索テキスト4</param>
    /// <param name="old5">検索テキスト5</param>
    public StringReplace5(string text, string old1, string old2, string old3, string old4, string old5) : base(text, old1, old2, old3, old4)
    {
        this.old5 = old5;
    }

    /// <summary>5か所の置換</summary>
    /// <param name="text1">置換テキスト1</param>
    /// <param name="text2">置換テキスト2</param>
    /// <param name="text3">置換テキスト3</param>
    /// <param name="text4">置換テキスト4</param>
    /// <param name="text5">置換テキスト5</param>
    /// <returns>置換済みテキスト</returns>
    public string Replace5(string text1, string text2, string text3, string text4, string text5)
    {
        return Replace4(text1, text2, text3, text4).Replace(old5, text5);
    }
}

[System.Serializable]
public class StringReplace6 : StringReplace5
{
    /// <summary>検索テキスト6</summary>
    [SerializeField] protected string old6;

    /// <summary>検索テキスト6</summary>
    public string Old6 { get => old6; set => old6 = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old1">検索テキスト1</param>
    /// <param name="old2">検索テキスト2</param>
    /// <param name="old3">検索テキスト3</param>
    /// <param name="old4">検索テキスト4</param>
    /// <param name="old5">検索テキスト5</param>
    /// <param name="old6">検索テキスト6</param>
    public StringReplace6(string text, string old1, string old2, string old3, string old4, string old5, string old6)
        : base(text, old1, old2, old3, old4, old5)
    {
        this.old6 = old6;
    }

    /// <summary>6か所の置換</summary>
    /// <param name="text1">置換テキスト1</param>
    /// <param name="text2">置換テキスト2</param>
    /// <param name="text3">置換テキスト3</param>
    /// <param name="text4">置換テキスト4</param>
    /// <param name="text5">置換テキスト5</param>
    /// <param name="text6">置換テキスト6</param>
    /// <returns>置換済みテキスト</returns>
    public string Replace6(string text1, string text2, string text3, string text4, string text5, string text6)
    {
        return Replace5(text1, text2, text3, text4, text5).Replace(old6, text6);
    }
}

[System.Serializable]
public class StringReplace7 : StringReplace6
{
    /// <summary>検索テキスト7</summary>
    [SerializeField] protected string old7;

    /// <summary>検索テキスト7</summary>
    public string Old7 { get => old7; set => old7 = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old1">検索テキスト1</param>
    /// <param name="old2">検索テキスト2</param>
    /// <param name="old3">検索テキスト3</param>
    /// <param name="old4">検索テキスト4</param>
    /// <param name="old5">検索テキスト5</param>
    /// <param name="old6">検索テキスト6</param>
    /// <param name="old7">検索テキスト7</param>
    public StringReplace7(string text, string old1, string old2, string old3, string old4, string old5, string old6, string old7)
        : base(text, old1, old2, old3, old4, old5, old6)
    {
        this.old7 = old7;
    }

    /// <summary>7か所の置換</summary>
    /// <param name="text1">置換テキスト1</param>
    /// <param name="text2">置換テキスト2</param>
    /// <param name="text3">置換テキスト3</param>
    /// <param name="text4">置換テキスト4</param>
    /// <param name="text5">置換テキスト5</param>
    /// <param name="text6">置換テキスト6</param>
    /// <param name="text7">置換テキスト7</param>
    /// <returns>置換済みテキスト</returns>
    public string Replace7(string text1, string text2, string text3, string text4, string text5, string text6, string text7)
    {
        return Replace6(text1, text2, text3, text4, text5, text6).Replace(old7, text7);
    }
}

[System.Serializable]
public class StringReplace8 : StringReplace7
{
    /// <summary>検索テキスト8</summary>
    [SerializeField] protected string old8;

    /// <summary>検索テキスト8</summary>
    public string Old8 { get => old8; set => old8 = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old1">検索テキスト1</param>
    /// <param name="old2">検索テキスト2</param>
    /// <param name="old3">検索テキスト3</param>
    /// <param name="old4">検索テキスト4</param>
    /// <param name="old5">検索テキスト5</param>
    /// <param name="old6">検索テキスト6</param>
    /// <param name="old7">検索テキスト7</param>
    /// <param name="old8">検索テキスト8</param>
    public StringReplace8(string text, string old1, string old2, string old3, string old4, string old5, string old6, string old7, string old8)
        : base(text, old1, old2, old3, old4, old5, old6, old7)
    {
        this.old8 = old8;
    }

    /// <summary>8か所の置換</summary>
    /// <param name="text1">置換テキスト1</param>
    /// <param name="text2">置換テキスト2</param>
    /// <param name="text3">置換テキスト3</param>
    /// <param name="text4">置換テキスト4</param>
    /// <param name="text5">置換テキスト5</param>
    /// <param name="text6">置換テキスト6</param>
    /// <param name="text7">置換テキスト7</param>
    /// <param name="text8">置換テキスト8</param>
    /// <returns>置換済みテキスト</returns>
    public string Replace8(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8)
    {
        return Replace7(text1, text2, text3, text4, text5, text6, text7).Replace(old8, text8);
    }
}

[System.Serializable]
public class StringReplace9 : StringReplace8
{
    /// <summary>検索テキスト9</summary>
    [SerializeField] protected string old9;

    /// <summary>検索テキスト9</summary>
    public string Old9 { get => old9; set => old9 = value; }

    /// <param name="text">テキスト</param>
    /// <param name="old1">検索テキスト1</param>
    /// <param name="old2">検索テキスト2</param>
    /// <param name="old3">検索テキスト3</param>
    /// <param name="old4">検索テキスト4</param>
    /// <param name="old5">検索テキスト5</param>
    /// <param name="old6">検索テキスト6</param>
    /// <param name="old7">検索テキスト7</param>
    /// <param name="old8">検索テキスト8</param>
    /// <param name="old9">検索テキスト9</param>
    public StringReplace9(string text, string old1, string old2, string old3, string old4, string old5, string old6, string old7, string old8, string old9)
        : base(text, old1, old2, old3, old4, old5, old6, old7, old8)
    {
        this.old9 = old9;
    }

    /// <summary>9か所の置換</summary>
    /// <param name="text1">置換テキスト1</param>
    /// <param name="text2">置換テキスト2</param>
    /// <param name="text3">置換テキスト3</param>
    /// <param name="text4">置換テキスト4</param>
    /// <param name="text5">置換テキスト5</param>
    /// <param name="text6">置換テキスト6</param>
    /// <param name="text7">置換テキスト7</param>
    /// <param name="text8">置換テキスト8</param>
    /// <param name="text9">置換テキスト9</param>
    /// <returns>置換済みテキスト</returns>
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
