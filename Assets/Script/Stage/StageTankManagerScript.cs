using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>ステージ上のタンクの管理者</summary>
public class StageTankManagerScript : MonoBehaviour
{
    /// <summary>プレイヤータンク</summary>
    [SerializeField, LightColor] private TankPlayerScript player;

    /// <summary>エネミーのプールリスト</summary>
    [SerializeField] private PoolList<TankEnemyScript> enemyList;

    /// <summary>弾のプールリスト</summary>
    [SerializeField] private PoolList<BulletScript> bulletList;

    /// <summary>プレイヤータンク</summary>
    public TankPlayerScript Player => player;

    /// <summary>プールされている非アクティブな敵タンクを取得する関数</summary>
    public System.Func<TankEnemyScript> GetPoolEnemy { get; private set; }

    /// <summary>プールされている非アクティブな弾を取得する関数</summary>
    public System.Func<BulletScript> GetPoolBullet { get; private set; }

    /// <summary>プールされている敵タンクのリスト</summary>
    public IReadOnlyList<TankEnemyScript> PoolEnemyList => enemyList;

    /// <summary>初期化</summary>
    /// <param name="onPlayerDeath">プレイヤーが死亡した再実行する関数</param>
    public void Init(System.Func<bool, IEnumerator> onPlayerDeath)
    {
        GetPoolEnemy = enemyList.GetObject;       //プールされている非アクティブな敵タンクを取得する関数を代入
        GetPoolBullet = bulletList.GetObject;     //プールされている非アクティブな弾を取得する関数を代入

        player.Init(GetPoolBullet, onPlayerDeath);//プレイヤーの関数をセット
    }

    /// <summary>プレイヤーのライフを追加</summary>
    /// <param name="stageCount">現在のステージ数</param>
    public void PlayerAddLife(int stageCount)
    {
        if (player.Life.IsAddLife(stageCount))//ライフを増やすステージ数なら
        {
            player.Life.AddLife();            //プレイヤーのライフを増やす
        }

        player.HealthRecovery();              //体力を回復
    }

    /// <summary>非アクティブ</summary>
    public void InActive()
    {
        player.IsActive = false;     //プレイヤーを非アクティブ

        foreach (var e in enemyList) //敵タンクを数分繰り返す
        {
            e.IsActive = false;      //エネミーを非アクティブ
        }

        foreach (var b in bulletList)//弾のプールリスト分繰り返す
        {
            b.Inactive();            //弾を非アクティブ
        }
    }
}
