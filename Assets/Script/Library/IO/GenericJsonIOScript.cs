using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GenericJsonIOScript<T> : JsonIOScript where T : new()
{
    /// <summary>デシリアライズ化した値</summary>
    [SerializeField] protected T value;

    /// <summary>デシリアライズ化した値</summary>
    public T Value { get => value; set => this.value = value; }

    protected override void Awake()
    {
        base.Awake();

        Load();
    }

    /// <summary>ロード</summary>
    public T Load() => value = Load<T>();

    public override TBase Load<TBase>()
    {
        return typeof(T) == typeof(TBase) ? base.Load<TBase>() : throw new($"指定されたデータ型\"{typeof(TBase)}\"は\"{typeof(T)}\"ではありません。");
    }

    public override void Save(object data, bool prettyPrint = false)
    {
        if (data is not T) throw new($"第一引数の値のデータ型は\"{data.GetType()}\"です。\"{typeof(T)}\"ではありません。");

        base.Save(data, prettyPrint);
    }

    /// <summary>セーブ</summary>
    public void Save(bool prettyPrint = true) => Save((object)value, prettyPrint);

    /// <summary>セーブ</summary>
    public void Save(T value, bool prettyPrint = true) => Save((object)value, prettyPrint);

#if UNITY_EDITOR
    [CustomEditor(typeof(GenericJsonIOScript<>), true)]
    public class GenericJsonIOScriptEditor : TextIOScriptEditor
    {
        /// <summary>GenericJsonIOScriptの参照</summary>
        protected GenericJsonIOScript<T> jsonIO;

        protected override bool IsCallAwake => false;

        protected override void OnEnable()
        {
            jsonIO = target as GenericJsonIOScript<T>;   //キャスト

            jsonIO.File = new(jsonIO.FilePath);          //インスタンス化
            jsonIO.Directory = new(jsonIO.DirectoryPath);//インスタンス化

            base.OnEnable();                             //ベースのOnEnableを呼び出す

            jsonIO.value = jsonIO.File.IsExists ? jsonIO.Load() : default;
        }

        protected override void DataField()
        {
            if (jsonIO.File.IsExists)                          //ファイルが存在しているなら
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(value)));//ファイルをデシリアライズ化した値のフィールドを表示
            }

            if (GUILayout.Button(nameof(Save)))                                             //セーブボタンを表示
            {
                jsonIO.Save();                                                              //セーブ

                OnEnable();                                                                 //リセット
            }

            EditorGUILayout.Space();                                                        //スペース

            base.DataField();                                                               //ベースのDataFieldを呼び出す
        }
    }
#endif
}
