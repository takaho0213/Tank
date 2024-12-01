using UnityEditor;
using UnityEngine;

public interface IIOScript
{
    /// <summary>�f�B���N�g����</summary>
    public string DirectoryName { get; }

    /// <summary>�t�@�C����</summary>
    public string FileName { get; }

    /// <summary>�f�B���N�g���p�X</summary>
    public string DirectoryPath { get; }

    /// <summary>�t�@�C���p�X</summary>
    public string FilePath { get; }

    /// <summary>�g���q</summary>
    public abstract string Extension { get; }
}

public abstract class BaseIOScript : MonoBehaviour, IIOScript
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
    public abstract string Extension { get; }

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
        File = new(FilePath);          //DirectoryIO���C���X�^���X��
        Directory = new(DirectoryPath);//TextFileIO���C���X�^���X��
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
    [CustomEditor(typeof(BaseIOScript), true)]
    public class BaseIOScriptEditor : Editor
    {
        /// <summary>BaseIOScript�̎Q��</summary>
        protected BaseIOScript baseIO;

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
            baseIO = target as BaseIOScript;                                             //�X�N���v�g�ɃL���X�g

            if (IsCallAwake) baseIO.Awake();                                             //Awake���Ăяo���Ȃ�/Awake���Ăяo��

            fileText = baseIO.File.IsExists ? baseIO.FileText : "�t�@�C�������݂��܂���";//�t�@�C���e�L�X�g�����A�t�@�C�������݂���Ȃ�O�ҁA����ȊO�Ȃ���

            int length = ApplicationEx.Path.Length - ApplicationEx.AssetsPath.Length;    //Assets�ȑO�̃p�X�̕�����̒���

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
                baseIO.directoryName = AssetDatabase.GetAssetPath(directory)[ApplicationEx.AssetsPath.Length..];
            }

            baseIO.directoryName = EditorGUILayout.DelayedTextField(nameof(DirectoryName), baseIO.directoryName);//�f�B���N�g������\������t�B�[���h

            directory = GUIL.Field(nameof(Directory), directory);

            if (!baseIO.Directory.IsExists && GUILayout.Button("�f�B���N�g�����쐬"))                            //�f�B���N�g�����Ȃ��ꍇ�A�쐬����{�^��
            {
                baseIO.Directory.Create();                                                                       //�f�B���N�g�����쐬

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
                baseIO.fileName = file.name;
            }

            baseIO.fileName = EditorGUILayout.DelayedTextField(nameof(FileName), baseIO.fileName);//�t�@�C������\������t�B�[���h

            file = GUIL.Field(nameof(File), file);

            if (!baseIO.File.IsExists && GUILayout.Button("�t�@�C�����쐬"))                      //�t�@�C�����Ȃ��ꍇ�A�쐬����{�^��
            {
                baseIO.Save(new());                                                               //�V�����I�u�W�F�N�g�ŃZ�[�u

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

        /// <summary>�x�[�X�̃C���X�y�N�^�[</summary>
        public void BaseOnInspectorGUI() => base.OnInspectorGUI();
    }
#endif
}