using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Json<T> where T : new()
{
    /// <summary>フォルダ名</summary>
    [SerializeField] private string directoryName;

    /// <summary>ファイル名</summary>
    [SerializeField] private string fileName;

    /// <summary>フォルダパス</summary>
    private string FolderPath => ApplicationEx.ToAbsolutePath(directoryName);

    /// <summary>作成先のパス</summary>
    private string Path => $"{FolderPath}/{fileName}.json";

    /// <summary>データ</summary>
    public T Data { get; set; }

    /// <summary>ロード</summary>
    /// <returns>セーブデータ</returns>
    public T Load()
    {
        var path = Path;                                           //作成先パス

        if (File.Exists(path))                                     //セーブファイルが存在していたら
        {
            Data = JsonUtility.FromJson<T>(File.ReadAllText(path));//ロード
        }

        else Save(new());                                          //存在していなかったら/セーブ

        return Data;                                               //セーブデータを返す
    }

    /// <summary>データをセーブ</summary>
    public void Save() => Save(Data);

    /// <summary>データをセーブ</summary>
    public void Save(T data)
    {
        if (!Directory.Exists(FolderPath))                             //フォルダが存在していなかったら
        {
            Directory.CreateDirectory(FolderPath);                     //フォルダを作成
        }

        File.WriteAllText(Path, JsonUtility.ToJson(Data = data, true));//データをセーブ
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
