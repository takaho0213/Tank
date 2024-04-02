# if UNITY_EDITOR
using UnityEditor;
# endif

/// <summary>Json�`���̃t�@�C���ւ�IO���s��</summary>
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
            editor = new(target, "�t�@�C�����e��", () =>
            {
                var t = (JsonIOScript)target;

                return t.file.IsExists ? t.file.Text : "�t�@�C�������݂��܂���";
            });
        }

        public override void OnInspectorGUI() => editor.Field();
    }
#endif
}
