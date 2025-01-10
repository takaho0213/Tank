using UnityEngine;

/// <summary>一定間隔ごとにする処理の実装を容易にするクラス</summary>
[System.Serializable]
public class Interval : IInterval
{
    /// <summary>実行時間</summary>
    private static float TIME => UnityEngine.Time.time;
    /// <summary>デルタタイム</summary>
    private static float DELTATIME => UnityEngine.Time.deltaTime;

    /// <summary>時間</summary>
    [SerializeField] private float time;

    /// <summary>自動でリセットするか</summary>
    [SerializeField] private bool isAutoReSet;

    /// <summary>ストップ中か</summary>
    [SerializeField] private bool isStop;

    /// <summary>時間</summary>
    public float Time { get => time; set => time = value; }

    /// <summary>自動でリセットするか</summary>
    public bool IsAutoReSet { get => isAutoReSet; set => isAutoReSet = value; }

    /// <summary>ストップ中か</summary>
    public bool IsStop { get => isStop; set => isStop = value; }

    /// <summary>リセット時の時間</summary>
    public float LastTime { get; private set; }

    /// <summary>経過時間</summary>
    public float ElapsedTime => TIME - LastTime;

    /// <summary>残り時間</summary>
    public float TimeLimit => time - ElapsedTime;

    /// <summary>インターバルを越えたか</summary>
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

    /// <summary>一時停止</summary>
    public void Stop() => LastTime += DELTATIME;

    /// <summary>リセット</summary>
    public void ReSet() => LastTime = TIME;

    /// <summaryクローンインスタンスを作成</summary>
    public Interval Clone() => new(time, isAutoReSet, isStop, LastTime);

    /// <summary>値をコピー</summary>
    public void Copy(Interval i)
    {
        time = i.time;

        isAutoReSet = i.isAutoReSet;

        isStop = i.isStop;

        LastTime = i.LastTime;
    }

    /// <param name="time">時間</param>
    /// <param name="isAutoReSet">自動でリセットするか</param>
    /// <param name="lastTime">ReSet時の時間</param>
    /// <param name="isStop">停止するか</param>
    public Interval(float time = default, bool isAutoReSet = default, bool isStop = default, float lastTime = default)
    {
        this.time = time;
        this.isAutoReSet = isAutoReSet;
        this.isStop = isStop;
        LastTime = lastTime;
    }

    public static implicit operator bool(Interval i) => i.IsOver;
}
