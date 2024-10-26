using System.IO;

public class TextFileIO
{
    /// <summary>ファイルの絶対パス</summary>
    public readonly string Path;

    /// <summary>ファイルの情報</summary>
    public readonly FileInfo Info;

    /// <summary>ファイルが存在するか</summary>
    public bool IsExists => File.Exists(Path);

    /// <summary>ファイルテキスト</summary>
    public string Text
    {
        get => File.ReadAllText(Path);
        set => File.WriteAllText(Path, value);
    }

    /// <summary>ファイルが存在していなければ、ファイルを作成</summary>
    public void Create()
    {
        if (!IsExists)        //ファイルが存在していなければ
        {
            File.Create(Path);//ファイルを作成
        }
    }

    /// <param name="path">絶対パス</param>
    public TextFileIO(string path) => Info = new(Path = path);
}
