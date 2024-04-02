# if UNITY_EDITOR
using UnityEditor;
# endif

/// <summary>Json形式のファイルへのIOを行う</summary>
public class JsonIOScript : TextIOScript
{
    protected override string Extension => "json";

#if UNITY_EDITOR
    [CustomEditor(typeof(JsonIOScript))]
    public class JsonScriptEditor : Editor
    {
        private TextIOEditor editor;

        private void OnEnable()
        {
            editor = new(target, "ファイル内容↓", () =>
            {
                var t = (JsonIOScript)target;

                return t.file.IsExists ? t.file.Text : "ファイルが存在しません";
            });
        }

        public override void OnInspectorGUI() => editor.Field();
    }
#endif
}
