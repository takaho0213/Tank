using UnityEngine;

/// <summary>Csv�R���}��؂�̃e�L�X�g�t�@�C����ǂݎ��</summary>
[System.Serializable]
public class CsvAssetReader
{
    /// <summary>�ǂݍ��ރt�@�C��</summary>
    [SerializeField] private TextAsset file;

    /// <summary>�ǂݍ��ރt�@�C��</summary>
    public TextAsset File { get => file; set => file = value; }

    /// <summary>�L���b�V���p</summary>
    private string[][] text;

    /// <summary>�t�@�C���e�L�X�g�񎟌��z��</summary>
    public string[][] Text => text ??= ReadText();

    /// <summary>�e�L�X�g�ǂݎ��</summary>
    public string[][] ReadText()
    {
        var lineTexts = file.text.SplitNewLine();          //���s�R�[�h���ƂɃX�v���b�g

        string[][] result = new string[lineTexts.Length][];//�z����쐬

        for (int i = default; i < lineTexts.Length; i++)   //�v�f�����J��Ԃ�
        {
            var texts = lineTexts[i].SplitComma();         //�R���}���ƂɃX�v���b�g

            for (int t = default; t < texts.Length; t++)   //�v�f���J��Ԃ�
            {
                texts[t] = texts[t].Trim();                //�O��̋󔒂��폜
            }

            result[i] = texts;                             //�e�L�X�g����
        }

        return result;                                     //���ʂ�Ԃ�
    }

    /// <summary>�e�L�X�g�����Z�b�g</summary>
    /// <param name="file">�V�����e�L�X�g�A�Z�b�g</param>
    public void Reset(TextAsset file)
    {
        text = null;

        this.file = file;
    }
}
