using System.IO;

public class DirectoryIO
{
    /// <summary>�f�B���N�g���̐�΃p�X</summary>
    public readonly string Path;

    /// <summary>�f�B���N�g���̏��</summary>
    public readonly DirectoryInfo Info;

    /// <summary>�f�B���N�g�������݂��Ă��邩</summary>
    public bool IsExists => Directory.Exists(Path);

    /// <summary>�f�B���N�g�������݂��Ă��Ȃ���΍쐬</summary>
    public void Create()
    {
        if (!IsExists)
        {
            Directory.CreateDirectory(Path);
        }
    }

    /// <param name="path">�f�B���N�g���̐�΃p�X</param>
    public DirectoryIO(string path) => Info = new(Path = path);
}
