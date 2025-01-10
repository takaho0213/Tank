using System;
using UnityEngine;

/// <summary>タンクのキャノン</summary>
public class TankCannonScript : MonoBehaviour
{
    /// <summary>弾を撃つSEを再生するAudioSource</summary>
    [SerializeField, LightColor] private AudioSource shootSource;

    /// <summary>弾の情報</summary>
    [SerializeField] private BulletInfo bulletInfo;

    /// <summary>発射する間隔</summary>
    [SerializeField] private float shootIntervalTime;

    /// <summary>発射するインターバル</summary>
    private Interval shootInterval;

    /// <summary>プールしてある弾を取得する関数</summary>
    public Func<BulletScript> BulletPool { get; set; }

    /// <summary>弾速</summary>
    public float BulletSpeed => bulletInfo.Speed;

    /// <summary>発射する間隔</summary>
    public float ShootIntervalTime { set => shootInterval.Time = value; }

    /// <summary>発射間隔が来ているか</summary>
    public bool IsShoot => shootInterval;

    /// <summary>初期化</summary>
    public void Init()
    {
        shootInterval = new Interval(shootIntervalTime, true);
    }

    /// <summary>エネミーの情報をセットする関数</summary>
    /// <param name="i">エネミーの情報</param>
    public void SetInfo(TankEnemyInfoScript i)
    {
        bulletInfo.Init(i.BulletSpeed, i.BulletReflectionCount, i.FillColor);//弾の情報をセットする関数を呼び出す
    }

    /// <summary>弾を発射</summary>
    public void Shoot()
    {
        BulletPool.Invoke().Shoot(bulletInfo);                                 //プールされている弾を取得し、発射する関数の引数に弾の情報を入れ呼び出す

        AudioScript.I.TankAudio[TankClip.Shoot].PlayOneShot(shootSource);//発射する際のSEを再生する
    }

    /// <summary>発射間隔をリセット</summary>
    public void ReSetShootInterval() => shootInterval.ReSet();
}
