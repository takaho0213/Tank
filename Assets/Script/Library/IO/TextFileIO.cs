/// <summary>�e�L�X�g�`���̃t�@�C���ւ�IO���s���N���X</summary>
public class TextFileIO
{
    /// <summary>�t�@�C���p�X</summary>
    public readonly string Path;

    /// <summary>�t�@�C�������݂��邩</summary>
    public bool IsExists => System.IO.File.Exists(Path);

    /// <summary>�t�@�C���e�L�X�g</summary>
    public string Text
    {
        get => System.IO.File.ReadAllText(Path);
        set => System.IO.File.WriteAllText(Path, value);
    }

    /// <param name="path">�t�@�C���p�X</param>
    public TextFileIO(string path) => Path = path;

    /// <summary>Json�e�L�X�g����f�[�^���擾</summary>
    /// <param name="r">�f�[�^</param>
    /// <returns>�擾�ł�����</returns>
    public bool TryGetJson<T>(out T r)
    {
        if (!IsExists)
        {
            r = default;

            return false;
        }

        return Text.TryFormJson(out r);
    }

    /// <summary>Json�`���ɕϊ����t�@�C���ɏ�������</summary>
    /// <param name="data">�f�[�^</param>
    /// <param name="prettyPrint">�e�L�X�g�𐮂��邩</param>
    public void SetJson(object data, bool prettyPrint = false) => Text = data.ToJson(prettyPrint);
}
