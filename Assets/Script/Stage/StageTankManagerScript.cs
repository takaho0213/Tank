using UnityEngine;
using System.Collections;

public class StageTankManagerScript : MonoBehaviour
{
    /// <summary>プレイヤータンク</summary>
    [SerializeField, LightColor] private TankPlayerScript player;

    /// <summary>エネミーのプールリスト</summary>
    [SerializeField] private PoolList<TankEnemyScript> enemyList;

    /// <summary>弾のプールリスト</summary>
    [SerializeField] private PoolList<BulletScript> bulletList;

    public TankPlayerScript Player => player;

    public System.Func<TankEnemyScript> GetEnemy { get; private set; }

    public System.Func<BulletScript> GetBullet { get; private set; }

    public void Init(System.Func<bool, IEnumerator> onPlayerDeath)
    {
        GetEnemy = enemyList.GetObject;
        GetBullet = bulletList.GetObject;

        player.Init(GetBullet, onPlayerDeath);                        //プレイヤーの関数をセット
    }

    public void PlayerAddLife(int stageCount)
    {
        if (player.Life.IsAddLife(stageCount))//ライフを増やすステージ数なら
        {
            player.Life.AddLife();            //プレイヤーのライフを増やす
        }

        player.HealthRecovery();              //体力を回復
    }

    /// <summary></summary>
    public void InActive()
    {
        player.IsActive = false;     //プレイヤーを非アクティブ

        foreach (var e in enemyList)
        {
            e.IsActive = false;//エネミーを非アクティブ
        }

        foreach (var b in bulletList)//弾のプールリスト分繰り返す
        {
            b.Inactive();            //弾を非アクティブ
        }
    }
}
