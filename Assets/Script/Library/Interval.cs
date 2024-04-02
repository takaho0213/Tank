/// <summary>読み取り専用のInterval</summary>
public interface IReadOnlyInterval
{
    /// <summary>経過時間</summary>
    public static float TIME { get; }

    /// <summary>前回のフレームからの経過時間</summary>
    public static float DELTATIME { get; }

    /// <summary>ReSet時の時間</summary>
    public float LastTime { get; }

    /// <summary>ReSet時からの経過時間</summary>
    public float ElapsedTime { get; }

    /// <summary>残り時間</summary>
    public float TimeLimit { get; }
}

/// <summary>Intervalのインターフェイス</summary>
public interface IInterval : IReadOnlyInterval
{
    /// <summary>インターバルを越えたか</summary>
    public bool IsOver { get; }

    /// <summary>ストップ中か</summary>
    public bool IsStop { get; set; }

    /// <summary>ディープコピー</summary>
    public void Copy(Interval i);

    /// <summaryクローンインスタンスを作成</summary>
    public Interval Clone();

    /// <summary>一時停止</summary>
    public void Stop();

    /// <summary>リセット</summary>
    public void ReSet();
}

/// <summary>一定間隔ごとにする処理の実装を容易にするクラス</summary>
[System.Serializable]
public class Interval : IInterval
{
    /// <summary>時間</summary>
    public float Time;
    /// <summary>自動でリセットするか</summary>
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

    /// <param name="Time">時間</param>
    /// <param name="IsAutoReSet">自動でリセットするか</param>
    /// <param name="LastTime">ReSet時の時間</param>
    /// <param name="IsStop">停止するか</param>
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

