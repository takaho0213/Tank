/// <summary>Intervalの読み取り専用インターフェイス</summary>
public interface IInterval
{
    /// <summary>時間</summary>
    public float Time { get; }

    /// <summary>自動でリセットするか</summary>
    public bool IsAutoReSet { get; }

    /// <summary>ストップ中か</summary>
    public bool IsStop { get; }

    /// <summary>リセット時の時間</summary>
    public float LastTime { get; }

    /// <summary>経過時間</summary>
    public float ElapsedTime { get; }

    /// <summary>残り時間</summary>
    public float TimeLimit { get; }

    /// <summary>インターバルを越えたか</summary>
    public bool IsOver { get; }
}
