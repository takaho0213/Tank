using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>ステージ</summary>
public class StageScript : MonoBehaviour
{
    /// <summary>ステージ全体のオブジェクト</summary>
    [SerializeField, LightColor] private GameObject StageObject;

    /// <summary>プレイヤーの初期位置と角度</summary>
    [SerializeField, LightColor] private Transform PlayerPosAndRot;

    /// <summary>エネミーの個体値</summary>
    [SerializeField, LightColor] private TankEnemyInfoScript[] EnemyInfos;

    /// <summary>エネミーがすべて倒された際実行</summary>
    private Func<IEnumerator> OnAllEnemysDeath;

    /// <summary>エネミーが倒された数</summary>
    private int EnemysDeathCount;

    /// <summary>ステージを生成</summary>
    /// <param name="enemyPool">EnemyTankを返す関数</param>
    /// <param name="bulletPool">弾を返す関数</param>
    /// <param name="player">PLayerTank</param>
    /// <param name="onAllEnemysDeath">全てのEnemyを倒した際の処理</param>
    public void Generate(Func<TankEnemyScript> enemyPool, Func<BulletScript> bulletPool, TankScript player, Func<IEnumerator> onAllEnemysDeath)
    {
        StageObject.SetActive(true);                                   //ステージをアクティブ

        player.IsActive = true;                                        //プレイヤーをアクティブ

        player.SetPosAndRot(PlayerPosAndRot);                          //場所と角度をセット

        OnAllEnemysDeath = onAllEnemysDeath;                           //全てのEnemyを倒した際の処理をセット

        foreach (var info in EnemyInfos)                               //EnemyInfosの要素数分繰り返す
        {
            enemyPool.Invoke().SetInfo(info, bulletPool, OnEnemyDeath);//エネミーの個体値をセットしアクティブ
        }
    }

    /// <summary>Enemyが死亡した際呼び出す</summary>
    private IEnumerator OnEnemyDeath()
    {
        if (++EnemysDeathCount >= EnemyInfos.Length)//エネミーを倒した数がエネミーの生成数以上なら
        {
            yield return OnAllEnemysDeath.Invoke(); //エネミーがすべて倒された際実行する関数を呼び出す
        }
    }

    /// <summary>ステージをクリア</summary>
    /// <param name="enemys">エネミーのプールリスト</param>
    public void Clear(IReadOnlyList<TankEnemyScript> enemys)
    {
        EnemysDeathCount = default;                  //カウントをリセット

        StageObject.SetActive(false);                //オブジェクトを非アクティブ

        foreach (var e in enemys) e.IsActive = false;//エネミーを非アクティブ
    }
}
