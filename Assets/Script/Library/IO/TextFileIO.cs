using System.IO;

public class TextFileIO
{
    /// <summary>�t�@�C���̐�΃p�X</summary>
    public readonly string Path;

    /// <summary>�t�@�C���̏��</summary>
    public readonly FileInfo Info;

    /// <summary>�t�@�C�������݂��邩</summary>
    public bool IsExists => File.Exists(Path);

    /// <summary>�t�@�C���e�L�X�g</summary>
    public string Text
    {
        get => File.ReadAllText(Path);
        set => File.WriteAllText(Path, value);
    }

    /// <summary>�t�@�C�������݂��Ă��Ȃ���΁A�t�@�C�����쐬</summary>
    public void Create()
    {
        if (!IsExists)        //�t�@�C�������݂��Ă��Ȃ����
        {
            File.Create(Path);//�t�@�C�����쐬
        }
    }

    /// <param name="path">��΃p�X</param>
    public TextFileIO(string path) => Info = new(Path = path);
}
