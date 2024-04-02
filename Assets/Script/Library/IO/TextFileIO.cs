/// <summary>テキスト形式のファイルへのIOを行うクラス</summary>
public class TextFileIO
{
    /// <summary>ファイルパス</summary>
    public readonly string Path;

    /// <summary>ファイルが存在するか</summary>
    public bool IsExists => System.IO.File.Exists(Path);

    /// <summary>ファイルテキスト</summary>
    public string Text
    {
        get => System.IO.File.ReadAllText(Path);
        set => System.IO.File.WriteAllText(Path, value);
    }

    /// <param name="path">ファイルパス</param>
    public TextFileIO(string path) => Path = path;

    /// <summary>Jsonテキストからデータを取得</summary>
    /// <param name="r">データ</param>
    /// <returns>取得できたか</returns>
    public bool TryGetJson<T>(out T r)
    {
        if (!IsExists)
        {
            r = default;

            return false;
        }

        return Text.TryFormJson(out r);
    }

    /// <summary>Json形式に変換しファイルに書き込む</summary>
    /// <param name="data">データ</param>
    /// <param name="prettyPrint">テキストを整えるか</param>
    public void SetJson(object data, bool prettyPrint = false) => Text = data.ToJson(prettyPrint);
}
