using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>テキストアセットを読み込む</summary>
[System.Serializable]
public class TextAssetReader
{
    /// <summary>区切る際の文字</summary>
    public const char Split = ',';

    /// <summary>読み込むファイル</summary>
    [SerializeField] private TextAsset file;

    /// <summary>キャッシュ用</summary>
    private string[][] text;

    /// <summary>ファイルテキスト二次元配列</summary>
    public string[][] Text => text ??= ReadText(file);

    /// <summary>テキスト読み取り</summary>
    public static string[][] ReadText(TextAsset file)
    {
        System.Collections.Generic.List<string[]> list = new();//string配列Listをインスタンス化

        System.IO.StringReader reader = new(file.text);        //StringReaderをインスタンス化

        while (reader.Peek() != -1)                            //テキストがある限り繰り返す
        {
            list.Add(reader.ReadLine().Split(Split));          //一行読み込み分割する
        }

        return list.ToArray();                                 //配列化
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TextAssetReader))]
public class TextAssetReaderDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var o = p.FindPropertyRelative("file");

        if (o != null) EditorGUI.PropertyField(r, o, l);
    }
}
#endif
