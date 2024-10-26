using UnityEngine;

public class IntervalScript : MonoBehaviour, IInterval
{
    /// <summary>����</summary>
    [SerializeField] private float time;
    /// <summary>�����Ń��Z�b�g���邩</summary>
    [SerializeField] private bool isAutoReSet;
    /// <summary>�X�g�b�v����</summary>
    [SerializeField] private bool isStop;

    /// <summary>����</summary>
    public float Time
    {
        get => time;
        set => time = value;
    }

    /// <summary>�����Ń��Z�b�g���邩</summary>
    public bool IsAutoReSet
    {
        get => isAutoReSet;
        set => isAutoReSet = value;
    }

    /// <summary>�X�g�b�v����</summary>
    public bool IsStop
    {
        get => isStop;
        set => isStop = value;
    }

    public float UpdateTime { get; private set; }

    /// <summary>���Z�b�g���̎���</summary>
    public float LastTime { get; private set; }

    /// <summary>�o�ߎ���</summary>
    public float ElapsedTime => UpdateTime - LastTime;

    /// <summary>�c�莞��</summary>
    public float TimeLimit => time - ElapsedTime;

    /// <summary>�C���^�[�o�����z������</summary>
    public bool IsOver
    {
        get
        {
            bool isOver = ElapsedTime >= time;

            if (isOver && isAutoReSet) ReSetTime();

            return isOver;
        }
    }

    /// <summary>���Z�b�g</summary>
    public void ReSetTime() => LastTime = UpdateTime;

    private void Update()
    {
        if (!isStop)
        {
            UpdateTime += UnityEngine.Time.deltaTime;
        }
    }

    public static implicit operator bool(IntervalScript i) => i.IsOver;
}