using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//#if UNITY_EDITOR
//[UnityEditor.CustomEditor(typeof(クラス名), true)]
//public class クラス名Editor : GenericJsonIOScriptEditor { }
//#endif
public abstract class GenericJsonIOScript<T> : JsonIOScript where T : new()
{
    /// <summary>デシリアライズ化した値</summary>
    [SerializeField] protected T data;

    /// <summary>デシリアライズ化した値</summary>
    public virtual T Data { get => data; set => data = value; }

    protected override void Awake()
    {
        base.Awake();

        Load();
    }

    /// <summary>ロード</summary>
    public virtual T Load() => data = Load<T>();

    /// <summary>セーブ</summary>
    public virtual void Save(bool prettyPrint = true) => Save((object)data, prettyPrint);

    /// <summary>セーブ</summary>
    public virtual void Save(T value, bool prettyPrint = true) => Save((object)value, prettyPrint);

    public override TBase Load<TBase>()
    {
        return typeof(T) == typeof(TBase) ? base.Load<TBase>() : throw new($"指定されたデータ型\"{typeof(TBase)}\"は\"{typeof(T)}\"ではありません。");
    }

    public override void Save(object data, bool prettyPrint = false)
    {
        if (data is not T) throw new($"第一引数の値のデータ型は\"{data.GetType()}\"です。\"{typeof(T)}\"ではありません。");

        base.Save(data, prettyPrint);
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GenericJsonIOScript<>), true)]
    public class GenericJsonIOScriptEditor : BaseIOScriptEditor
    {
        /// <summary>GenericJsonIOScriptの参照</summary>
        protected GenericJsonIOScript<T> jsonIO;

        protected override bool IsCallAwake => false;

        protected override void OnEnable()
        {
            jsonIO = target as GenericJsonIOScript<T>;                   //キャスト

            jsonIO.File = new(jsonIO.FilePath);                          //インスタンス化
            jsonIO.Directory = new(jsonIO.DirectoryPath);                //インスタンス化

            base.OnEnable();                                             //ベースのOnEnableを呼び出す

            jsonIO.data = jsonIO.File.IsExists ? jsonIO.Load() : default;//ファイルが存在していたら/ロードしたデータ:それ以外なら/defaultを代入
        }

        protected override void DataField()
        {
            if (jsonIO.File.IsExists)                                                      //ファイルが存在しているなら
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(data)));//ファイルをデシリアライズ化した値のフィールドを表示
            }

            if (GUILayout.Button(nameof(Save)))                                            //セーブボタンを表示
            {
                jsonIO.Save();                                                             //セーブ

                OnEnable();                                                                //リセット
            }

            EditorGUILayout.Space();                                                       //スペースを表示

            base.DataField();                                                              //ベースのDataFieldを呼び出す
        }
    }
#endif
}
