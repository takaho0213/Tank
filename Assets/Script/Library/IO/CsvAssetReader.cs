using UnityEngine;

/// <summary>Csvコンマ区切りのテキストファイルを読み取る</summary>
[System.Serializable]
public class CsvAssetReader
{
    /// <summary>読み込むファイル</summary>
    [SerializeField] private TextAsset file;

    /// <summary>読み込むファイル</summary>
    public TextAsset File { get => file; set => file = value; }

    /// <summary>キャッシュ用</summary>
    private string[][] text;

    /// <summary>ファイルテキスト二次元配列</summary>
    public string[][] Text => text ??= ReadText();

    /// <summary>テキスト読み取り</summary>
    public string[][] ReadText()
    {
        var lineTexts = file.text.SplitNewLine();          //改行コードごとにスプリット

        string[][] result = new string[lineTexts.Length][];//配列を作成

        for (int i = default; i < lineTexts.Length; i++)   //要素数分繰り返す
        {
            var texts = lineTexts[i].SplitComma();         //コンマごとにスプリット

            for (int t = default; t < texts.Length; t++)   //要素分繰り返す
            {
                texts[t] = texts[t].Trim();                //前後の空白を削除
            }

            result[i] = texts;                             //テキストを代入
        }

        return result;                                     //結果を返す
    }

    /// <summary>テキストをリセット</summary>
    /// <param name="file">新しいテキストアセット</param>
    public void Reset(TextAsset file)
    {
        text = null;

        this.file = file;
    }
}
