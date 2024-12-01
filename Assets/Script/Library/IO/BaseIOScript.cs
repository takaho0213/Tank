using UnityEditor;
using UnityEngine;

public interface IIOScript
{
    /// <summary>ディレクトリ名</summary>
    public string DirectoryName { get; }

    /// <summary>ファイル名</summary>
    public string FileName { get; }

    /// <summary>ディレクトリパス</summary>
    public string DirectoryPath { get; }

    /// <summary>ファイルパス</summary>
    public string FilePath { get; }

    /// <summary>拡張子</summary>
    public abstract string Extension { get; }
}

public abstract class BaseIOScript : MonoBehaviour, IIOScript
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
    public abstract string Extension { get; }

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
        File = new(FilePath);          //DirectoryIOをインスタンス化
        Directory = new(DirectoryPath);//TextFileIOをインスタンス化
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
    [CustomEditor(typeof(BaseIOScript), true)]
    public class BaseIOScriptEditor : Editor
    {
        /// <summary>BaseIOScriptの参照</summary>
        protected BaseIOScript baseIO;

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
            baseIO = target as BaseIOScript;                                             //スクリプトにキャスト

            if (IsCallAwake) baseIO.Awake();                                             //Awakeを呼び出すなら/Awakeを呼び出す

            fileText = baseIO.File.IsExists ? baseIO.FileText : "ファイルが存在しません";//ファイルテキストを代入、ファイルが存在するなら前者、それ以外なら後者

            int length = ApplicationEx.Path.Length - ApplicationEx.AssetsPath.Length;    //Assets以前のパスの文字列の長さ

            string filePath = baseIO.FilePath[length..];

            string directoryPath = baseIO.DirectoryPath[length..];

            if (baseIO.File.Info.Extension.Contains(baseIO.Extension))
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
                baseIO.directoryName = AssetDatabase.GetAssetPath(directory)[ApplicationEx.AssetsPath.Length..];
            }

            baseIO.directoryName = EditorGUILayout.DelayedTextField(nameof(DirectoryName), baseIO.directoryName);//ディレクトリ名を表示するフィールド

            directory = GUIL.Field(nameof(Directory), directory);

            if (!baseIO.Directory.IsExists && GUILayout.Button("ディレクトリを作成"))                            //ディレクトリがない場合、作成するボタン
            {
                baseIO.Directory.Create();                                                                       //ディレクトリを作成

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
                baseIO.fileName = file.name;
            }

            baseIO.fileName = EditorGUILayout.DelayedTextField(nameof(FileName), baseIO.fileName);//ファイル名を表示するフィールド

            file = GUIL.Field(nameof(File), file);

            if (!baseIO.File.IsExists && GUILayout.Button("ファイルを作成"))                      //ファイルがない場合、作成するボタン
            {
                baseIO.Save(new());                                                               //新しいオブジェクトでセーブ

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

        /// <summary>ベースのインスペクター</summary>
        public void BaseOnInspectorGUI() => base.OnInspectorGUI();
    }
#endif
}