/// <summary>フォルダへのIOを行うクラス</summary>
public class FolderIO
{
    /// <summary>作成先パス</summary>
    public readonly string Path;

    /// <summary>存在しているか</summary>
    public bool IsExists => System.IO.Directory.Exists(Path);

    /// <param name="path">作成先パス</param>
    public FolderIO(string path) => Path = path;

    /// <summary>フォルダを作成</summary>
    public void Create()
    {
        if (!IsExists)
        {
            System.IO.Directory.CreateDirectory(Path);
        }
    }
}
