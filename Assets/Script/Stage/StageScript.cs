using System;
using UnityEngine;
using System.Collections;

/// <summary>ステージ</summary>
public class StageScript : MonoBehaviour
{
    /// <summary>ステージ全体のオブジェクト</summary>
    [SerializeField, LightColor] private GameObject stageObject;

    /// <summary>プレイヤーの初期位置と角度</summary>
    [SerializeField, LightColor] private Transform playerPosAndRot;

    /// <summary>エネミーの個体値</summary>
    [SerializeField, LightColor] private TankEnemyInfoScript[] enemyInfos;

    /// <summary>エネミーがすべて倒された際実行</summary>
    private Func<IEnumerator> OnAllEnemysDeath;

    /// <summary>エネミーが倒された数</summary>
    private int enemysDeathCount;

    /// <summary>ステージを生成</summary>
    /// <param name="onAllEnemysDeath">全てのEnemyを倒した際の処理</param>
    public void Active(StageTankManagerScript tankManager, Func<IEnumerator> onAllEnemysDeath)
    {
        stageObject.SetActive(true);                                   //ステージをアクティブ

        OnAllEnemysDeath = onAllEnemysDeath;                           //全てのEnemyを倒した際の処理をセット

        tankManager.Player.IsActive = true;                                        //プレイヤーをアクティブ

        tankManager.Player.SetPosAndRot(playerPosAndRot);                          //場所と角度をセット

        foreach (var info in enemyInfos)                               //EnemyInfosの要素数分繰り返す
        {
            tankManager.GetEnemy.Invoke().SetInfo(info, tankManager.GetBullet, OnEnemyDeath);//エネミーの個体値をセットしアクティブ
        }
    }

    /// <summary>Enemyが死亡した際呼び出す</summary>
    private IEnumerator OnEnemyDeath()
    {
        if (++enemysDeathCount >= enemyInfos.Length)//エネミーを倒した数がエネミーの生成数以上なら
        {
            yield return OnAllEnemysDeath.Invoke(); //エネミーがすべて倒された際実行する関数を呼び出す
        }
    }

    /// <summary>ステージをクリア</summary>
    public void InActive()
    {
        enemysDeathCount = default;                  //カウントをリセット

        stageObject.SetActive(false);                //オブジェクトを非アクティブ
    }

    /// <summary>エネミーが倒された数をリセット</summary>
    public void ReSetEnemyDeathCount() => enemysDeathCount = default;//カウントをリセット
}
