using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>Text形式のファイルへのI/Oを行う</summary>
public class TextIOScript : MonoBehaviour
{
    /// <summary>ディレクトリ名</summary>
    [SerializeField] protected string directoryName;

    /// <summary>ファイル名</summary>
    [SerializeField] protected string fileName;

    /// <summary>ディレクトリ名</summary>
    public string DirectoryName => directoryName;

    /// <summary>ファイル名</summary>
    public string FileName => fileName;

    /// <summary>ディレクトリパス</summary>
    public string DirectoryPath => ApplicationEx.ToAbsolutePath(directoryName);

    /// <summary>ファイルパス</summary>
    public string FilePath => $"{DirectoryPath}/{fileName}.{Extension}";

    /// <summary>拡張子</summary>
    private const string txt = nameof(txt);

    /// <summary>拡張子</summary>
    public virtual string Extension => txt;

    /// <summary>ディレクトリの管理</summary>
    public DirectoryIO Directory { get; protected set; }

    /// <summary>テキストファイルの管理</summary>
    public TextFileIO File { get; protected set; }

    /// <summary>ファイルテキスト</summary>
    protected virtual string FileText
    {
        get => File.Text;
        set => File.Text = value;
    }

    protected virtual void Awake()
    {
        File = new(FilePath);          //インスタンス化
        Directory = new(DirectoryPath);//インスタンス化
    }

    /// <summary>ロード</summary>
    /// <returns>セーブデータ</returns>
    public virtual T Load<T>() where T : new()
    {
        if (!File.IsExists || !FileText.TryFormJson(out T data))//ファイルが存在しない または デシリアライズ化に失敗したら
        {
            data = new();                                       //新たなオブジェクトをインスタンス化

            Save(data);                                         //オブジェクトをセーブ
        }

        return data;                                            //オブジェクトを返す
    }

    /// <summary>セーブ</summary>
    /// <param name="data">セーブするオブジェクト</param>
    /// <param name="prettyPrint">シリアライズ化する際テキストを整えるか</param>
    public virtual void Save(object data, bool prettyPrint = default)
    {
        Directory.Create();                 //ディレクトリが無ければディレクトリを作成

        FileText = data.ToJson(prettyPrint);//オブジェクトをシリアライズ化し、ファイルに書き込む

        ApplicationEx.Refresh();            //アセットを更新
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TextIOScript), true)]
    public class TextIOScriptEditor : Editor
    {
        /// <summary>TextIOScriptの参照</summary>
        protected TextIOScript textIO;

        /// <summary>ファイルのテキスト</summary>
        protected string fileText;

        /// <summary>表示する際ファイルのパス</summary>
        protected string filePath;
        /// <summary>表示する際ディレクトリのパス</summary>
        protected string directoryPath;

        protected TextAsset file;

        protected DefaultAsset directory;

        /// <summary>Awakeを呼び出すか？</summary>
        protected virtual bool IsCallAwake => true;

        protected virtual void OnEnable()
        {
            textIO = target as TextIOScript;                                             //スクリプトにキャスト

            if (IsCallAwake) textIO.Awake();                                             //Awakeを呼び出すなら/Awakeを呼び出す

            fileText = textIO.File.IsExists ? textIO.FileText : "ファイルが存在しません";//ファイルテキストを代入、ファイルが存在するなら前者、それ以外なら後者

            int length = ApplicationEx.Path.Length - ApplicationEx.AssetsPath.Length;    //Assets以前のパスの文字列の長さ

            string filePath = textIO.FilePath[length..];

            string directoryPath = textIO.DirectoryPath[length..];

            if (textIO.File.Info.Extension.Contains(textIO.Extension))
            {
                file = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
            }

            if (System.IO.Directory.Exists(directoryPath))
            {
                directory = AssetDatabase.LoadAssetAtPath<DefaultAsset>(directoryPath);
            }

            this.filePath = $"{nameof(FilePath)} : {filePath}";                            //表示するファイルパスを代入

            this.directoryPath = $"{nameof(DirectoryPath)} : {directoryPath}";                  //表示するディレクトリを代入
        }

        /// <summary>ディレクトリを表示するフィールド</summary>
        protected virtual void DirectoryField()
        {
            EditorGUILayout.LabelField(directoryPath);                                                           //ディレクトリのパスを表示

            //GUIL.Field(nameof(DirectoryPath), directoryPath);

            var old = directory;

            if (old != directory)
            {
                textIO.directoryName = AssetDatabase.GetAssetPath(directory)[ApplicationEx.AssetsPath.Length..];
            }

            textIO.directoryName = EditorGUILayout.DelayedTextField(nameof(DirectoryName), textIO.directoryName);//ディレクトリ名を表示するフィールド

            directory = GUIL.Field(nameof(Directory), directory);

            if (!textIO.Directory.IsExists && GUILayout.Button("ディレクトリを作成"))                            //ディレクトリがない場合、作成するボタン
            {
                textIO.Directory.Create();                                                                       //ディレクトリを作成

                AssetDatabase.Refresh();                                                                         //アセットを更新

                OnEnable();                                                                                      //リセット
            }
        }

        /// <summary>ファイルを表示するフィールド</summary>
        protected virtual void FileField()
        {
            EditorGUILayout.LabelField(filePath);                                                 //ファイルのパスを表示

            //GUIL.Field(nameof(FilePath), filePath);

            var old = file;

            if (old != file)
            {
                textIO.fileName = file.name;
            }

            textIO.fileName = EditorGUILayout.DelayedTextField(nameof(FileName), textIO.fileName);//ファイル名を表示するフィールド

            file = GUIL.Field(nameof(File), file);

            if (!textIO.File.IsExists && GUILayout.Button("ファイルを作成"))                      //ファイルがない場合、作成するボタン
            {
                textIO.Save(new());                                                               //新しいオブジェクトでセーブ

                OnEnable();                                                                       //リセット
            }
        }

        /// <summary>データを表示するフィールド</summary>
        protected virtual void DataField()
        {
            EditorGUILayout.LabelField("ファイル内容↓");//ラベル

            EditorGUI.BeginDisabledGroup(true);          //読み取り専用フィールド開始
            EditorGUILayout.TextArea(fileText);          //ファイル内容を表示
            EditorGUI.EndDisabledGroup();                //読み取り専用フィールド終了
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();                 //更新

            EditorGUI.BeginChangeCheck();              //変更があったかのチェック開始

            DirectoryField();                          //ディレクトリフィールド

            EditorGUILayout.Space();                   //スペース

            FileField();                               //ファイルフィールド

            EditorGUILayout.Space();                   //スペース

            DataField();                               //データフィールド

            if (EditorGUI.EndChangeCheck()) OnEnable();//変更があったかのチェック終了、変更があればリセット

            serializedObject.ApplyModifiedProperties();//更新
        }
    }
#endif
}