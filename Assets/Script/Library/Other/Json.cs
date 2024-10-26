using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Json<T> where T : new()
{
    /// <summary>�t�H���_��</summary>
    [SerializeField] private string directoryName;

    /// <summary>�t�@�C����</summary>
    [SerializeField] private string fileName;

    /// <summary>�t�H���_�p�X</summary>
    private string FolderPath => ApplicationEx.ToAbsolutePath(directoryName);

    /// <summary>�쐬��̃p�X</summary>
    private string Path => $"{FolderPath}/{fileName}.json";

    /// <summary>�f�[�^</summary>
    public T Data { get; set; }

    /// <summary>���[�h</summary>
    /// <returns>�Z�[�u�f�[�^</returns>
    public T Load()
    {
        var path = Path;                                           //�쐬��p�X

        if (File.Exists(path))                                     //�Z�[�u�t�@�C�������݂��Ă�����
        {
            Data = JsonUtility.FromJson<T>(File.ReadAllText(path));//���[�h
        }

        else Save(new());                                          //���݂��Ă��Ȃ�������/�Z�[�u

        return Data;                                               //�Z�[�u�f�[�^��Ԃ�
    }

    /// <summary>�f�[�^���Z�[�u</summary>
    public void Save() => Save(Data);

    /// <summary>�f�[�^���Z�[�u</summary>
    public void Save(T data)
    {
        if (!Directory.Exists(FolderPath))                             //�t�H���_�����݂��Ă��Ȃ�������
        {
            Directory.CreateDirectory(FolderPath);                     //�t�H���_���쐬
        }

        File.WriteAllText(Path, JsonUtility.ToJson(Data = data, true));//�f�[�^���Z�[�u
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Json<>), true)]
public class JsonEditor : MultiplePropertyDrawer
{
    protected override string[] PropertyNames => new string[] { "directoryName", "fileName" };
    //public const string directoryName = nameof(directoryName);
    //public const string fileName = nameof(fileName);

    //private SerializedProperty Directory;
    //private SerializedProperty File;

    //private string folderName;
    //private string fileName;

    //public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    //{
    //    r.height = Drawer.Height;

    //    EditorGUI.LabelField(r, l);

    //    r.y += Drawer.HeightAndGap;

    //    l.fileText = folderName;

    //    EditorGUI.PropertyField(r, Directory, l);

    //    r.y += Drawer.HeightAndGap;

    //    l.fileText = fileName;

    //    EditorGUI.PropertyField(r, File, l);
    //}

    //public override float GetPropertyHeight(SerializedProperty p, GUIContent l)
    //{
    //    Directory ??= p.FindPropertyRelative(directoryName);
    //    File ??= p.FindPropertyRelative(fileName);

    //    folderName ??= Drawer.Indent(directoryName);
    //    fileName ??= Drawer.Indent(fileName);

    //    return Drawer.GetPropertyHeight(3);
    //}
}
#endif
