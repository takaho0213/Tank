using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�e�L�X�g�A�Z�b�g��ǂݍ���</summary>
[System.Serializable]
public class TextAssetReader
{
    /// <summary>��؂�ۂ̕���</summary>
    public const char Split = ',';

    /// <summary>�ǂݍ��ރt�@�C��</summary>
    [SerializeField] private TextAsset file;

    /// <summary>�L���b�V���p</summary>
    private string[][] text;

    /// <summary>�t�@�C���e�L�X�g�񎟌��z��</summary>
    public string[][] Text => text ??= ReadText(file);

    /// <summary>�e�L�X�g�ǂݎ��</summary>
    public static string[][] ReadText(TextAsset file)
    {
        System.Collections.Generic.List<string[]> list = new();//string�z��List���C���X�^���X��

        System.IO.StringReader reader = new(file.text);        //StringReader���C���X�^���X��

        while (reader.Peek() != -1)                            //�e�L�X�g���������J��Ԃ�
        {
            list.Add(reader.ReadLine().Split(Split));          //��s�ǂݍ��ݕ�������
        }

        return list.ToArray();                                 //�z��
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
