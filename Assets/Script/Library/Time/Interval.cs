using UnityEngine;

/// <summary>���Ԋu���Ƃɂ��鏈���̎�����e�Ղɂ���N���X</summary>
[System.Serializable]
public class Interval : IInterval
{
    /// <summary>���s����</summary>
    private static float TIME => UnityEngine.Time.time;
    /// <summary>�f���^�^�C��</summary>
    private static float DELTATIME => UnityEngine.Time.deltaTime;

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
    public float ElapsedTime => TIME - LastTime;

    /// <summary>�c�莞��</summary>
    public float TimeLimit => time - ElapsedTime;

    /// <summary>�C���^�[�o�����z������</summary>
    public bool IsOver
    {
        get
        {
            bool isOver = ElapsedTime >= time;

            if (isStop) Stop();

            if (isOver && isAutoReSet) ReSet();

            return isOver;
        }
    }

    /// <summary>�ꎞ��~</summary>
    public void Stop() => LastTime += DELTATIME;

    /// <summary>���Z�b�g</summary>
    public void ReSet() => LastTime = TIME;

    /// <summary�N���[���C���X�^���X���쐬</summary>
    public Interval Clone() => new(time, isAutoReSet, isStop, LastTime);

    /// <summary>�l���R�s�[</summary>
    public void Copy(Interval i)
    {
        time = i.time;

        isAutoReSet = i.isAutoReSet;

        isStop = i.isStop;

        LastTime = i.LastTime;
    }

    /// <param name="time">����</param>
    /// <param name="isAutoReSet">�����Ń��Z�b�g���邩</param>
    /// <param name="lastTime">ReSet���̎���</param>
    /// <param name="isStop">��~���邩</param>
    public Interval(float time = default, bool isAutoReSet = default, bool isStop = default, float lastTime = default)
    {
        this.time = time;
        this.isAutoReSet = isAutoReSet;
        this.isStop = isStop;
        LastTime = lastTime;
    }

    public static implicit operator bool(Interval i) => i.IsOver;
}
