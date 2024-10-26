using System.IO;

public class DirectoryIO
{
    /// <summary>ディレクトリの絶対パス</summary>
    public readonly string Path;

    /// <summary>ディレクトリの情報</summary>
    public readonly DirectoryInfo Info;

    /// <summary>ディレクトリが存在しているか</summary>
    public bool IsExists => Directory.Exists(Path);

    /// <summary>ディレクトリが存在していなければ作成</summary>
    public void Create()
    {
        if (!IsExists)
        {
            Directory.CreateDirectory(Path);
        }
    }

    /// <param name="path">ディレクトリの絶対パス</param>
    public DirectoryIO(string path) => Info = new(Path = path);
}
