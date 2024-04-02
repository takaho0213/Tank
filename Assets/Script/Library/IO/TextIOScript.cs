using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>Text�`���̃t�@�C���ւ�IO���s��</summary>
public class TextIOScript : MonoBehaviour
{
    /// <summary>�t�H���_��</summary>
    [SerializeField] protected string FolderName;

    /// <summary>�t�@�C����</summary>
    [SerializeField] protected string FileName;

    /// <summary>�t�H���_�̊Ǘ�</summary>
    protected FolderIO folder;

    /// <summary>�e�L�X�g�t�@�C���̊Ǘ�</summary>
    protected TextFileIO file;

    /// <summary>�t�H���_�p�X</summary>
    protected string FolderPath => $"{Application.dataPath}/{FolderName}";

    /// <summary>�t�@�C���p�X</summary>
    protected string FilePath => $"{FolderPath}/{FileName}.{Extension}";

    /// <summary>�g���q</summary>
    protected virtual string Extension => "txt";

    protected virtual void Awake()
    {
        file = new(FilePath);    //�C���X�^���X��
        folder = new(FolderPath);//�C���X�^���X��
    }

    /// <summary>���[�h</summary>
    /// <returns>�Z�[�u�f�[�^</returns>
    public virtual T Load<T>() where T : new()
    {
        T data;                        //�f�[�^

        if (!file.TryGetJson(out data))//�f�[�^���Ȃ����
        {
            data = new T();            //�f�[�^�쐬

            Save(data);                //�f�[�^���Z�[�u
        }

        return data;                   //�f�[�^��Ԃ�
    }

    /// <summary>�Z�[�u</summary>
    public virtual void Save(object data) => Save(data, false);

    /// <summary>�Z�[�u</summary>
    public virtual void Save(object data, bool prettyPrint)
    {
        folder.Create();                    //�t�H���_���쐬

        file.SetJson(data, prettyPrint);    //�f�[�^���Z�[�u

        Refresh();                          //�A�Z�b�g���X�V
    }

    /// <summary>�A�Z�b�g���X�V</summary>
    protected void Refresh()
    {
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();//�A�Z�b�g���X�V
        #endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TextIOScript))]
    public class TextIOScriptEditor : Editor
    {
        private TextIOEditor editor;

        private void OnEnable()
        {
            editor = new(target, "�t�@�C�����e��", () =>
            {
                var t = target as TextIOScript;

                return t.file.IsExists ? t.file.Text : "�t�@�C�������݂��܂���";

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

            if (!t.folder.IsExists && GUILayout.Button("�t�H���_���쐬"))
            {
                t.folder.Create();

                AssetDatabase.Refresh();

                ReSet();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField(t.FilePath);

            t.FileName = EditorGUILayout.TextField(nameof(FileName), t.FileName);

            if (!t.file.IsExists && GUILayout.Button("�t�@�C�����쐬"))
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
