/// <summary>�ǂݎ���p��Interval</summary>
public interface IReadOnlyInterval
{
    /// <summary>�o�ߎ���</summary>
    public static float TIME { get; }

    /// <summary>�O��̃t���[������̌o�ߎ���</summary>
    public static float DELTATIME { get; }

    /// <summary>ReSet���̎���</summary>
    public float LastTime { get; }

    /// <summary>ReSet������̌o�ߎ���</summary>
    public float ElapsedTime { get; }

    /// <summary>�c�莞��</summary>
    public float TimeLimit { get; }
}

/// <summary>Interval�̃C���^�[�t�F�C�X</summary>
public interface IInterval : IReadOnlyInterval
{
    /// <summary>�C���^�[�o�����z������</summary>
    public bool IsOver { get; }

    /// <summary>�X�g�b�v����</summary>
    public bool IsStop { get; set; }

    /// <summary>�f�B�[�v�R�s�[</summary>
    public void Copy(Interval i);

    /// <summary�N���[���C���X�^���X���쐬</summary>
    public Interval Clone();

    /// <summary>�ꎞ��~</summary>
    public void Stop();

    /// <summary>���Z�b�g</summary>
    public void ReSet();
}

/// <summary>���Ԋu���Ƃɂ��鏈���̎�����e�Ղɂ���N���X</summary>
[System.Serializable]
public class Interval : IInterval
{
    /// <summary>����</summary>
    public float Time;
    /// <summary>�����Ń��Z�b�g���邩</summary>
    public bool IsAutoReSet;

    public static float TIME => UnityEngine.Time.time;
    public static float DELTATIME => UnityEngine.Time.deltaTime;

    public float LastTime { get; private set; }

    public bool IsStop { get; set; }

    public float ElapsedTime => TIME - LastTime;

    public float TimeLimit => Time - ElapsedTime;

    public bool IsTimeLimit => TimeLimit <= (float)default;

    public bool IsTimeOver => ElapsedTime >= Time;

    public bool IsOver
    {
        get
        {
            bool isTimeOver = IsTimeOver;

            if (IsStop) Stop();

            if (isTimeOver && IsAutoReSet) ReSet();

            return isTimeOver;
        }
    }

    /// <param name="Time">����</param>
    /// <param name="IsAutoReSet">�����Ń��Z�b�g���邩</param>
    /// <param name="LastTime">ReSet���̎���</param>
    /// <param name="IsStop">��~���邩</param>
    public Interval(float Time = default, bool IsAutoReSet = default, float LastTime = default, bool IsStop = default)
    {
        this.Time = Time;

        this.IsAutoReSet = IsAutoReSet;

        this.LastTime = LastTime;

        this.IsStop = IsStop;
    }

    public void Stop() => LastTime += DELTATIME;

    public void ReSet() => LastTime = TIME;

    public Interval Clone() => new(Time, IsAutoReSet, LastTime, IsStop);

    public void Copy(Interval i)
    {
        Time = i.Time;

        IsAutoReSet = i.IsAutoReSet;

        LastTime = i.LastTime;

        IsStop = i.IsStop;
    }

    public static implicit operator bool(Interval i) => i.IsOver;
}

