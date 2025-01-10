using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class IntervalScript : MonoBehaviour, IInterval
{
    /// <summary>����</summary>
    [SerializeField] private float time;

    /// <summary>�����Ń��Z�b�g���邩</summary>
    [SerializeField] private bool isAutoReSet;

    /// <summary>�X�g�b�v����</summary>
    [SerializeField] private bool isStop;

    /// <summary>����</summary>
    public float Time { get => time; set => time = value; }

    /// <summary>�����Ń��Z�b�g���邩</summary>
    public bool IsAutoReSet { get => isAutoReSet; set => isAutoReSet = value; }

    /// <summary>�X�g�b�v����</summary>
    public bool IsStop { get => isStop; set => isStop = value; }

    /// <summary>���Z�b�g���̎���</summary>
    public float LastTime { get; private set; }

    /// <summary>�o�ߎ���</summary>
    public float ElapsedTime => UpdateTime - LastTime;

    /// <summary>�c�莞��</summary>
    public float TimeLimit => time - ElapsedTime;

    /// <summary>�X�V����</summary>
    public float UpdateTime {  get; private set; }

    /// <summary>�C���^�[�o�����z������</summary>
    public bool IsOver
    {
        get
        {
            bool isOver = ElapsedTime >= time;     //�C���^�[�o�����z������

            if (isOver && isAutoReSet) ReSetTime();//�C���^�[�o�����z���� ���� �I�[�g���Z�b�g�Ȃ�/���Z�b�g

            return isOver;                         //�C���^�[�o�����z��������Ԃ�
        }
    }

    /// <summary>���Z�b�g</summary>
    public void ReSetTime() => LastTime = UpdateTime;

    private void Update()
    {
        if (!isStop)                                 //��~���Ȃ����
        {
            UpdateTime += UnityEngine.Time.deltaTime;//�f���^�^�C�������Z���
        }
    }

    public static implicit operator bool(IntervalScript i) => i.IsOver;

#if UNITY_EDITOR
    [CustomEditor(typeof(IntervalScript))]
    public class IntervalScriptEditor : Editor
    {
        private IntervalScript instance;

        private void OnEnable() => instance = (IntervalScript)target;

        public override void OnInspectorGUI()
        {
            instance.time = EditorGUILayout.DelayedFloatField(nameof(Time), instance.time);
            instance.isAutoReSet = EditorGUILayout.Toggle(nameof(IsAutoReSet), instance.isAutoReSet);
            instance.isStop = EditorGUILayout.Toggle(nameof(IsStop), instance.isStop);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            using (new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.FloatField(nameof(LastTime), instance.LastTime);
                EditorGUILayout.FloatField(nameof(ElapsedTime), instance.ElapsedTime);
                EditorGUILayout.FloatField(nameof(TimeLimit), instance.TimeLimit);
                EditorGUILayout.FloatField(nameof(UpdateTime), instance.UpdateTime);
            }
        }
    }
#endif
}