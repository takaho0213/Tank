using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>Text形式のファイルへのIOを行う</summary>
public class TextIOScript : MonoBehaviour
{
    /// <summary>フォルダ名</summary>
    [SerializeField] protected string FolderName;

    /// <summary>ファイル名</summary>
    [SerializeField] protected string FileName;

    /// <summary>フォルダの管理</summary>
    protected FolderIO folder;

    /// <summary>テキストファイルの管理</summary>
    protected TextFileIO file;

    /// <summary>フォルダパス</summary>
    protected string FolderPath => $"{Application.dataPath}/{FolderName}";

    /// <summary>ファイルパス</summary>
    protected string FilePath => $"{FolderPath}/{FileName}.{Extension}";

    /// <summary>拡張子</summary>
    protected virtual string Extension => "txt";

    protected virtual void Awake()
    {
        file = new(FilePath);    //インスタンス化
        folder = new(FolderPath);//インスタンス化
    }

    /// <summary>ロード</summary>
    /// <returns>セーブデータ</returns>
    public virtual T Load<T>() where T : new()
    {
        T data;                        //データ

        if (!file.TryGetJson(out data))//データがなければ
        {
            data = new T();            //データ作成

            Save(data);                //データをセーブ
        }

        return data;                   //データを返す
    }

    /// <summary>セーブ</summary>
    public virtual void Save(object data) => Save(data, false);

    /// <summary>セーブ</summary>
    public virtual void Save(object data, bool prettyPrint)
    {
        folder.Create();                    //フォルダを作成

        file.SetJson(data, prettyPrint);    //データをセーブ

        Refresh();                          //アセットを更新
    }

    /// <summary>アセットを更新</summary>
    protected void Refresh()
    {
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();//アセットを更新
        #endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TextIOScript))]
    public class TextIOScriptEditor : Editor
    {
        private TextIOEditor editor;

        private void OnEnable()
        {
            editor = new(target, "ファイル内容↓", () =>
            {
                var t = target as TextIOScript;

                return t.file.IsExists ? t.file.Text : "ファイルが存在しません";

            });
        }

        public override void OnInspectorGUI() => editor.Field();
    }

    public class TextIOEditor
    {
        private TextIOScript t;

        private string text;

        private string label;

        private System.Func<string> fileText;

        public TextIOEditor(Object target, string label, System.Func<string> text)
        {
            t = (TextIOScript)target;

            this.label = label;

            fileText = text;

            ReSet();
        }

        private void ReSet()
        {
            t.Awake();

            text = fileText.Invoke();
        }

        public void Field()
        {
            EditorGUILayout.LabelField(t.FolderPath);

            t.FolderName = EditorGUILayout.TextField(nameof(FolderName), t.FolderName);

            if (!t.folder.IsExists && GUILayout.Button("フォルダを作成"))
            {
                t.folder.Create();

                AssetDatabase.Refresh();

                ReSet();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField(t.FilePath);

            t.FileName = EditorGUILayout.TextField(nameof(FileName), t.FileName);

            if (!t.file.IsExists && GUILayout.Button("ファイルを作成"))
            {
                t.Save(new());

                ReSet();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField(label);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextArea(text);
            EditorGUI.EndDisabledGroup();
        }
    }
#endif
}
