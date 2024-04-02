using UnityEngine;

/// <summary>文字列の置換を容易に行うクラス</summary>
[System.Serializable]
public class StringReplace
{
    /// <summary>テキスト</summary>
    [field: SerializeField, TextArea] public string Text { get; private set; }
    /// <summary>置換テキスト</summary>
    [field: SerializeField] public string ReplaceText { get; private set; }

    public StringReplace(string text, string replace)
    {
        Text = text;
        ReplaceText = replace;
    }

    public string Replace(string text1) => Text.Replace(ReplaceText, text1);
}

/// <summary>文字列の2ヶ所の置換を容易に行う</summary>
[System.Serializable]
public class StringReplace2 : StringReplace
{
    [field: SerializeField] public string ReplaceText2 { get; private set; }

    public StringReplace2(string text, string replace1, string replace2) : base(text, replace1) => ReplaceText2 = replace2;

    public string Replace2(string text1, string text2) => Replace(text1).Replace(ReplaceText2, text2);
}

/// <summary>文字列の3ヶ所の置換を容易に行う</summary>
[System.Serializable]
public class StringReplace3 : StringReplace2
{
    [field: SerializeField] public string ReplaceText3 { get; private set; }

    public StringReplace3(string text, string replace1, string replace2, string replace3) : base(text, replace1, replace2) => ReplaceText3 = replace3;

    public string Replace3(string text1, string text2, string text3) => Replace2(text1, text2).Replace(ReplaceText3, text3);
}

/// <summary>文字列の4ヶ所の置換を容易に行う</summary>
[System.Serializable]
public class StringReplace4 : StringReplace3
{
    [field: SerializeField] public string ReplaceText4 { get; private set; }

    public StringReplace4(string text, string replace1, string replace2, string replace3, string replace4) : base(text, replace1, replace2, replace3) => ReplaceText4 = replace4;

    public string Replace4(string text1, string text2, string text3, string text4) => Replace3(text1, text2, text3).Replace(ReplaceText4, text4);
}

/// <summary>文字列の5ヶ所の置換を容易に行う</summary>
[System.Serializable]
public class StringReplace5 : StringReplace4
{
    [field: SerializeField] public string ReplaceText5 { get; private set; }

    public StringReplace5(string text, string replace1, string replace2, string replace3, string replace4, string replace5) : base(text, replace1, replace2, replace3, replace4) => ReplaceText5 = replace5;

    public string Replace5(string text1, string text2, string text3, string text4, string text5) => Replace4(text1, text2, text3, text4).Replace(ReplaceText5, text5);
}

public class StringReplaceArray
{
    [field: SerializeField] public string Text { get; private set; }

    [field: SerializeField] public string[] ReplaceTexts { get; private set; }

    public StringReplaceArray(string text, string[] replaceTexts)
    {
        Text = text;

        ReplaceTexts = replaceTexts;
    }

    public string Replace(string[] texts)
    {
        int length = UnityEngine.Mathf.Min(texts.Length, ReplaceTexts.Length);

        string text = Text;

        for (int i = default; i < length; i++)
        {
            text = text.Replace(ReplaceTexts[i], texts[i]);
        }

        return text;
    }
}
