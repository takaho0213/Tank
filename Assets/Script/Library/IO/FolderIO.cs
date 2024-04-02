/// <summary>�t�H���_�ւ�IO���s���N���X</summary>
public class FolderIO
{
    /// <summary>�쐬��p�X</summary>
    public readonly string Path;

    /// <summary>���݂��Ă��邩</summary>
    public bool IsExists => System.IO.Directory.Exists(Path);

    /// <param name="path">�쐬��p�X</param>
    public FolderIO(string path) => Path = path;

    /// <summary>�t�H���_���쐬</summary>
    public void Create()
    {
        if (!IsExists)
        {
            System.IO.Directory.CreateDirectory(Path);
        }
    }
}
