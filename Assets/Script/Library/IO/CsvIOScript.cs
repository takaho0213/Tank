# if UNITY_EDITOR
using UnityEditor;
# endif

/// <summary>CSV形式のファイルへのIOを行う</summary>
public class CsvIOScript : TextIOScript
{
    protected override string Extension => "csv";

    public override T Load<T>()
    {
        T csv;                                                          //データ

        if (!file.IsExists || !file.Text.TryByteStringFormJson(out csv))//ファイルがない または 変換できなければ
        {
            csv = new T();                                              //データ作成

            Save(csv);                                                  //データをセーブ
        }

        return csv;                                                     //データを返す
    }

    public override void Save(object data, bool prettyPrint)
    {
        folder.Create();                                 //フォルダを作成

        file.Text = data.ObjectToByteString(prettyPrint);//byte文字列に変換しファイルに保存

        Refresh();                                       //アセットを更新
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CsvIOScript))]
    public class CSVScriptEditor : Editor
    {
        private TextIOEditor editor;

        private void OnEnable()
        {
            editor = new(target, "変換済みファイル内容↓", () =>
            {
                var t = (CsvIOScript)target;

                if (!t.file.IsExists) return "ファイルが存在しません";

                return t.file.Text.TryByteStringToString(out var r) ? r : "ファイルが変換出来ません";
            });
        }

        public override void OnInspectorGUI() => editor.Field();
    }
# endif
}
