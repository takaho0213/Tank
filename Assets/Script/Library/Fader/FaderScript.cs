using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FaderScript : MonoBehaviour
{
    /// <summary>�ҋ@�C���^�[�o��</summary>
    [SerializeField] protected Interval wait;

    /// <summary>�t�F�[�h���x</summary>
    [SerializeField] protected float speed;

    /// <summary>�t�F�[�h�C���I�������s����R�[���o�b�N</summary>
    protected UnityAction onFadeInEnd;

    /// <summary>�t�F�[�h�A�E�g�I�������s����R�[���o�b�N</summary>
    protected UnityAction onFadeOutEnd;

    /// <summary>�ҋ@�C���^�[�o��</summary>
    public IInterval Wait => wait;

    /// <summary>�t�F�[�h�C���I�����̑ҋ@����</summary>
    public float WaitTime { get => wait.Time; set => wait.Time = value; }

    /// <summary>�t�F�[�h���x</summary>
    public float Speed { get => speed; set => speed = value; }

    /// <summary>�t�F�[�h�C�����̃A���t�@�l</summary>
    protected virtual float InValue => MathEx.OneF;//MathEx.OneF = 1f;
    /// <summary>�t�F�[�h�A�E�g���̃A���t�@�l</summary>
    protected virtual float OutValue => MathEx.ZeroF;//MathEx.ZeroF = 0f;

    /// <summary>�t�F�[�h������l</summary>
    public virtual float FadeValue { get; set; }

    /// <summary>�ڕW�̒l</summary>
    public float Target { get; protected set; }

    /// <summary>�t�F�[�h�A�E�g����</summary>
    public bool IsFadeOut { get; protected set; }

    /// <summary>�t�F�[�h����</summary>
    public bool IsRun { get; protected set; }

    /// <summary>�t�F�[�h���J�n</summary>
    /// <param name="onFadeInEnd">�t�F�[�h�C���I�������s����R�[���o�b�N</param>
    /// <param name="onFadeOutEnd">�t�F�[�h�A�E�g�I�������s����R�[���o�b�N</param>
    public virtual void Run(UnityAction onFadeInEnd, UnityAction onFadeOutEnd)
    {
        if (IsRun) return;               //�t�F�[�h���Ȃ�/�I��

        this.onFadeInEnd = onFadeInEnd;  //�t�F�[�h�C���I�������s����R�[���o�b�N����
        this.onFadeOutEnd = onFadeOutEnd;//�t�F�[�h�A�E�g�I�������s����R�[���o�b�N����

        Target = InValue;                //�ڕW�̒l����

        IsRun = true;                    //�t�F�[�h������true
    }

    /// <summary>�t�F�[�h���J�n</summary>
    /// <param name="onFadeInEnd">�t�F�[�h�C���I�������s����R�[���o�b�N</param>
    public virtual void Run(UnityAction onFadeInEnd) => Run(onFadeInEnd, null);

    /// <summary>�t�F�[�h���J�n</summary>
    public virtual void Run() => Run(null, null);

    /// <summary>�t�F�[�h�C���I�������s</summary>
    protected virtual void OnFadeInEnd()
    {
        onFadeInEnd?.Invoke();//�t�F�[�h�C���I�����̃R�[���o�b�N���Ăяo��

        wait.ReSet();         //�C���^�[�o�������Z�b�g
    }

    /// <summary>�t�F�[�h�A�E�g�I�������s</summary>
    protected virtual void OnFadeOutEnd()
    {
        onFadeOutEnd?.Invoke();//�R�[���o�b�N���Ăяo��

        IsRun = false;         //�t�F�[�h���I��
    }

    /// <summary>�t�F�[�h</summary>
    protected virtual void Fade()
    {
        var value = FadeValue;                      //�t�F�[�h������l����

        if (Mathf.Approximately(value, Target))     //�ڕW�̒l�Ƃقړ�����������
        {
            FadeValue = Target;                     //�ڕW�̒l����

            Target = IsFadeOut ? InValue : OutValue;//���̖ڕW�̒l����

            if (IsFadeOut) OnFadeOutEnd();          //�t�F�[�h�A�E�g���Ȃ� �t�F�[�h�A�E�g�I�������s����֐����Ăяo��
            else OnFadeInEnd();                     //����ȊO�Ȃ�         �t�F�[�h�C��  �I�������s����֐����Ăяo��

            IsFadeOut = !IsFadeOut;                 //���]

            return;                                 //�I��
        }

        if (wait.IsOver)                                                         //�ҋ@���łȂ����
        {
            FadeValue = Mathf.MoveTowards(value, Target, speed * Time.deltaTime);//�t�F�[�h�������l����
        }
    }

    protected virtual void Update()
    {
        if (IsRun) Fade();//�t�F�[�h���Ȃ�/�t�F�[�h
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(FaderScript), true)]
    public class FaderEditor : Editor
    {
        protected FaderScript script;

        protected virtual void OnEnable() => script = (FaderScript)target;

        protected virtual void Field()
        {
            script.wait.Time = GUIL.Field(nameof(WaitTime), script.wait.Time);

            script.speed = GUIL.Field(nameof(Speed), script.speed);

            GUIL.Space();

            try
            {
                var value = script.FadeValue;

                script.FadeValue = GUIL.Field(nameof(script.FadeValue), script.FadeValue, script.OutValue, script.InValue);

                if (value != script.FadeValue)
                {
                    EditorApplication.QueuePlayerLoopUpdate();
                }
            }
            catch (System.Exception e)
            {
                EditorGUILayout.HelpBox(e.Message, MessageType.Error);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Field();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUIL.FlexibleLabel("------------------------------base------------------------------");
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}

//�t�F�[�h�̒�`

//0 => 1 : �t�F�[�h�C��
//1 => 0 : �t�F�[�h�A�E�g