using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>Text�`���̃t�@�C���ւ�I/O���s��</summary>
public class TextIOScript : MonoBehaviour
{
    /// <summary>�f�B���N�g����</summary>
    [SerializeField] protected string directoryName;

    /// <summary>�t�@�C����</summary>
    [SerializeField] protected string fileName;

    /// <summary>�f�B���N�g����</summary>
    public string DirectoryName => directoryName;

    /// <summary>�t�@�C����</summary>
    public string FileName => fileName;

    /// <summary>�f�B���N�g���p�X</summary>
    public string DirectoryPath => ApplicationEx.ToAbsolutePath(directoryName);

    /// <summary>�t�@�C���p�X</summary>
    public string FilePath => $"{DirectoryPath}/{fileName}.{Extension}";

    /// <summary>�g���q</summary>
    private const string txt = nameof(txt);

    /// <summary>�g���q</summary>
    public virtual string Extension => txt;

    /// <summary>�f�B���N�g���̊Ǘ�</summary>
    public DirectoryIO Directory { get; protected set; }

    /// <summary>�e�L�X�g�t�@�C���̊Ǘ�</summary>
    public TextFileIO File { get; protected set; }

    /// <summary>�t�@�C���e�L�X�g</summary>
    protected virtual string FileText
    {
        get => File.Text;
        set => File.Text = value;
    }

    protected virtual void Awake()
    {
        File = new(FilePath);          //�C���X�^���X��
        Directory = new(DirectoryPath);//�C���X�^���X��
    }

    /// <summary>���[�h</summary>
    /// <returns>�Z�[�u�f�[�^</returns>
    public virtual T Load<T>() where T : new()
    {
        if (!File.IsExists || !FileText.TryFormJson(out T data))//�t�@�C�������݂��Ȃ� �܂��� �f�V���A���C�Y���Ɏ��s������
        {
            data = new();                                       //�V���ȃI�u�W�F�N�g���C���X�^���X��

            Save(data);                                         //�I�u�W�F�N�g���Z�[�u
        }

        return data;                                            //�I�u�W�F�N�g��Ԃ�
    }

    /// <summary>�Z�[�u</summary>
    /// <param name="data">�Z�[�u����I�u�W�F�N�g</param>
    /// <param name="prettyPrint">�V���A���C�Y������ۃe�L�X�g�𐮂��邩</param>
    public virtual void Save(object data, bool prettyPrint = default)
    {
        Directory.Create();                 //�f�B���N�g����������΃f�B���N�g�����쐬

        FileText = data.ToJson(prettyPrint);//�I�u�W�F�N�g���V���A���C�Y�����A�t�@�C���ɏ�������

        ApplicationEx.Refresh();            //�A�Z�b�g���X�V
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TextIOScript), true)]
    public class TextIOScriptEditor : Editor
    {
        /// <summary>TextIOScript�̎Q��</summary>
        protected TextIOScript textIO;

        /// <summary>�t�@�C���̃e�L�X�g</summary>
        protected string fileText;

        /// <summary>�\������ۃt�@�C���̃p�X</summary>
        protected string filePath;
        /// <summary>�\������ۃf�B���N�g���̃p�X</summary>
        protected string directoryPath;

        protected TextAsset file;

        protected DefaultAsset directory;

        /// <summary>Awake���Ăяo�����H</summary>
        protected virtual bool IsCallAwake => true;

        protected virtual void OnEnable()
        {
            textIO = target as TextIOScript;                                             //�X�N���v�g�ɃL���X�g

            if (IsCallAwake) textIO.Awake();                                             //Awake���Ăяo���Ȃ�/Awake���Ăяo��

            fileText = textIO.File.IsExists ? textIO.FileText : "�t�@�C�������݂��܂���";//�t�@�C���e�L�X�g�����A�t�@�C�������݂���Ȃ�O�ҁA����ȊO�Ȃ���

            int length = ApplicationEx.Path.Length - ApplicationEx.AssetsPath.Length;    //Assets�ȑO�̃p�X�̕�����̒���

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

            this.filePath = $"{nameof(FilePath)} : {filePath}";                            //�\������t�@�C���p�X����

            this.directoryPath = $"{nameof(DirectoryPath)} : {directoryPath}";                  //�\������f�B���N�g������
        }

        /// <summary>�f�B���N�g����\������t�B�[���h</summary>
        protected virtual void DirectoryField()
        {
            EditorGUILayout.LabelField(directoryPath);                                                           //�f�B���N�g���̃p�X��\��

            //GUIL.Field(nameof(DirectoryPath), directoryPath);

            var old = directory;

            if (old != directory)
            {
                textIO.directoryName = AssetDatabase.GetAssetPath(directory)[ApplicationEx.AssetsPath.Length..];
            }

            textIO.directoryName = EditorGUILayout.DelayedTextField(nameof(DirectoryName), textIO.directoryName);//�f�B���N�g������\������t�B�[���h

            directory = GUIL.Field(nameof(Directory), directory);

            if (!textIO.Directory.IsExists && GUILayout.Button("�f�B���N�g�����쐬"))                            //�f�B���N�g�����Ȃ��ꍇ�A�쐬����{�^��
            {
                textIO.Directory.Create();                                                                       //�f�B���N�g�����쐬

                AssetDatabase.Refresh();                                                                         //�A�Z�b�g���X�V

                OnEnable();                                                                                      //���Z�b�g
            }
        }

        /// <summary>�t�@�C����\������t�B�[���h</summary>
        protected virtual void FileField()
        {
            EditorGUILayout.LabelField(filePath);                                                 //�t�@�C���̃p�X��\��

            //GUIL.Field(nameof(FilePath), filePath);

            var old = file;

            if (old != file)
            {
                textIO.fileName = file.name;
            }

            textIO.fileName = EditorGUILayout.DelayedTextField(nameof(FileName), textIO.fileName);//�t�@�C������\������t�B�[���h

            file = GUIL.Field(nameof(File), file);

            if (!textIO.File.IsExists && GUILayout.Button("�t�@�C�����쐬"))                      //�t�@�C�����Ȃ��ꍇ�A�쐬����{�^��
            {
                textIO.Save(new());                                                               //�V�����I�u�W�F�N�g�ŃZ�[�u

                OnEnable();                                                                       //���Z�b�g
            }
        }

        /// <summary>�f�[�^��\������t�B�[���h</summary>
        protected virtual void DataField()
        {
            EditorGUILayout.LabelField("�t�@�C�����e��");//���x��

            EditorGUI.BeginDisabledGroup(true);          //�ǂݎ���p�t�B�[���h�J�n
            EditorGUILayout.TextArea(fileText);          //�t�@�C�����e��\��
            EditorGUI.EndDisabledGroup();                //�ǂݎ���p�t�B�[���h�I��
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();                 //�X�V

            EditorGUI.BeginChangeCheck();              //�ύX�����������̃`�F�b�N�J�n

            DirectoryField();                          //�f�B���N�g���t�B�[���h

            EditorGUILayout.Space();                   //�X�y�[�X

            FileField();                               //�t�@�C���t�B�[���h

            EditorGUILayout.Space();                   //�X�y�[�X

            DataField();                               //�f�[�^�t�B�[���h

            if (EditorGUI.EndChangeCheck()) OnEnable();//�ύX�����������̃`�F�b�N�I���A�ύX������΃��Z�b�g

            serializedObject.ApplyModifiedProperties();//�X�V
        }
    }
#endif
}