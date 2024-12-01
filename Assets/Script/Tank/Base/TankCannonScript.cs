using System;
using UnityEngine;

public class TankCannonScript : MonoBehaviour
{
    /// <summary>弾を撃つSEを再生するAudioSource</summary>
    [SerializeField, LightColor] private AudioSource shootSource;

    /// <summary>弾の情報</summary>
    [SerializeField] private BulletInfo bulletInfo;

    [SerializeField] private float shootIntervalTime;

    /// <summary></summary>
    private Interval shootInterval;

    /// <summary>プールしてある弾を取得する関数</summary>
    public Func<BulletScript> BulletPool { get; set; }

    /// <summary>弾速</summary>
    public float BulletSpeed => bulletInfo.Speed;

    public float ShootIntervalTime { set => shootInterval.Time = value; }

    public bool IsShoot => shootInterval;

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

    public void ReSetShootInterval() => shootInterval.ReSet();
}
