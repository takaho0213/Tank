using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//#if UNITY_EDITOR
//[UnityEditor.CustomEditor(typeof(�N���X��), true)]
//public class �N���X��Editor : GenericJsonIOScriptEditor { }
//#endif
public abstract class GenericJsonIOScript<T> : JsonIOScript where T : new()
{
    /// <summary>�f�V���A���C�Y�������l</summary>
    [SerializeField] protected T data;

    /// <summary>�f�V���A���C�Y�������l</summary>
    public virtual T Data { get => data; set => data = value; }

    protected override void Awake()
    {
        base.Awake();

        Load();
    }

    /// <summary>���[�h</summary>
    public virtual T Load() => data = Load<T>();

    /// <summary>�Z�[�u</summary>
    public virtual void Save(bool prettyPrint = true) => Save((object)data, prettyPrint);

    /// <summary>�Z�[�u</summary>
    public virtual void Save(T value, bool prettyPrint = true) => Save((object)value, prettyPrint);

    public override TBase Load<TBase>()
    {
        return typeof(T) == typeof(TBase) ? base.Load<TBase>() : throw new($"�w�肳�ꂽ�f�[�^�^\"{typeof(TBase)}\"��\"{typeof(T)}\"�ł͂���܂���B");
    }

    public override void Save(object data, bool prettyPrint = false)
    {
        if (data is not T) throw new($"�������̒l�̃f�[�^�^��\"{data.GetType()}\"�ł��B\"{typeof(T)}\"�ł͂���܂���B");

        base.Save(data, prettyPrint);
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GenericJsonIOScript<>), true)]
    public class GenericJsonIOScriptEditor : BaseIOScriptEditor
    {
        /// <summary>GenericJsonIOScript�̎Q��</summary>
        protected GenericJsonIOScript<T> jsonIO;

        protected override bool IsCallAwake => false;

        protected override void OnEnable()
        {
            jsonIO = target as GenericJsonIOScript<T>;                   //�L���X�g

            jsonIO.File = new(jsonIO.FilePath);                          //�C���X�^���X��
            jsonIO.Directory = new(jsonIO.DirectoryPath);                //�C���X�^���X��

            base.OnEnable();                                             //�x�[�X��OnEnable���Ăяo��

            jsonIO.data = jsonIO.File.IsExists ? jsonIO.Load() : default;//�t�@�C�������݂��Ă�����/���[�h�����f�[�^:����ȊO�Ȃ�/default����
        }

        protected override void DataField()
        {
            if (jsonIO.File.IsExists)                                                      //�t�@�C�������݂��Ă���Ȃ�
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(data)));//�t�@�C�����f�V���A���C�Y�������l�̃t�B�[���h��\��
            }

            if (GUILayout.Button(nameof(Save)))                                            //�Z�[�u�{�^����\��
            {
                jsonIO.Save();                                                             //�Z�[�u

                OnEnable();                                                                //���Z�b�g
            }

            EditorGUILayout.Space();                                                       //�X�y�[�X��\��

            base.DataField();                                                              //�x�[�X��DataField���Ăяo��
        }
    }
#endif
}
