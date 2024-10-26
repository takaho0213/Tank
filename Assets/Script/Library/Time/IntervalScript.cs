using UnityEngine;

public class IntervalScript : MonoBehaviour, IInterval
{
    /// <summary>時間</summary>
    [SerializeField] private float time;
    /// <summary>自動でリセットするか</summary>
    [SerializeField] private bool isAutoReSet;
    /// <summary>ストップ中か</summary>
    [SerializeField] private bool isStop;

    /// <summary>時間</summary>
    public float Time
    {
        get => time;
        set => time = value;
    }

    /// <summary>自動でリセットするか</summary>
    public bool IsAutoReSet
    {
        get => isAutoReSet;
        set => isAutoReSet = value;
    }

    /// <summary>ストップ中か</summary>
    public bool IsStop
    {
        get => isStop;
        set => isStop = value;
    }

    public float UpdateTime { get; private set; }

    /// <summary>リセット時の時間</summary>
    public float LastTime { get; private set; }

    /// <summary>経過時間</summary>
    public float ElapsedTime => UpdateTime - LastTime;

    /// <summary>残り時間</summary>
    public float TimeLimit => time - ElapsedTime;

    /// <summary>インターバルを越えたか</summary>
    public bool IsOver
    {
        get
        {
            bool isOver = ElapsedTime >= time;

            if (isOver && isAutoReSet) ReSetTime();

            return isOver;
        }
    }

    /// <summary>リセット</summary>
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