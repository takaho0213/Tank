# if UNITY_EDITOR
using UnityEditor;
# endif

/// <summary>CSV�`���̃t�@�C���ւ�IO���s��</summary>
public class CsvIOScript : TextIOScript
{
    protected override string Extension => "csv";

    public override T Load<T>()
    {
        T csv;                                                          //�f�[�^

        if (!file.IsExists || !file.Text.TryByteStringFormJson(out csv))//�t�@�C�����Ȃ� �܂��� �ϊ��ł��Ȃ����
        {
            csv = new T();                                              //�f�[�^�쐬

            Save(csv);                                                  //�f�[�^���Z�[�u
        }

        return csv;                                                     //�f�[�^��Ԃ�
    }

    public override void Save(object data, bool prettyPrint)
    {
        folder.Create();                                 //�t�H���_���쐬

        file.Text = data.ObjectToByteString(prettyPrint);//byte������ɕϊ����t�@�C���ɕۑ�

        Refresh();                                       //�A�Z�b�g���X�V
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CsvIOScript))]
    public class CSVScriptEditor : Editor
    {
        private TextIOEditor editor;

        private void OnEnable()
        {
            editor = new(target, "�ϊ��ς݃t�@�C�����e��", () =>
            {
                var t = (CsvIOScript)target;

                if (!t.file.IsExists) return "�t�@�C�������݂��܂���";

                return t.file.Text.TryByteStringToString(out var r) ? r : "�t�@�C�����ϊ��o���܂���";
            });
        }

        public override void OnInspectorGUI() => editor.Field();
    }
# endif
}
