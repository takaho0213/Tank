using UnityEngine;

/// <summary>Audioを管理するクラス</summary>
public class AudioScript : MonoBehaviour
{
    /// <summary>シングルトンインスタンス</summary>
    public static AudioScript I { get; private set; }

    private void Awake() => I = this;//インスタンスを代入

    /// <summary>ステージオーディオ</summary>
    [field: SerializeField] public AudioInfoDictionary<StageClip> StageAudio { get; private set; }

    /// <summary>タンクオーディオ</summary>
    [field: SerializeField] public ClipInfoDictionary<TankClip> TankAudio { get; private set; }

    /// <summary>弾オーディオ</summary>
    [field: SerializeField] public ClipInfoDictionary<BulletClip> BulletAudio { get; private set; }
}

/// <summary>ステージのクリップ</summary>
public enum StageClip
{
    /// <summary>開始</summary>
    Start,
    /// <summary>クリア</summary>
    Clear,
    /// <summary>全クリ</summary>
    AllClear,
    /// <summary>アイキャッチ</summary>
    EyeCatch,
    /// <summary>ライフ追加</summary>
    LifeAdd,
    /// <summary>ライフ減少</summary>
    LifeReMove,
    /// <summary>プレイヤー死亡</summary>
    PlayerDeath,
    /// <summary>スコア</summary>
    Score,
    /// <summary>BGM</summary>
    BGM,
}

/// <summary>タンクのクリップ</summary>
public enum TankClip
{
    /// <summary>移動</summary>
    Move,
    /// <summary>射撃</summary>
    Shoot,
    /// <summary>ダメージ</summary>
    Damage,
    /// <summary>爆発</summary>
    Explosion,
}

/// <summary>弾のクリップ</summary>
public enum BulletClip
{
    /// <summary>反射</summary>
    Reflection,
    /// <summary>爆発</summary>
    Explosion,
}
