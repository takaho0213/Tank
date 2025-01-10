using System;
using UnityEngine;
using System.Collections;

/// <summary>ステージの管理</summary>
public class StageManagerScript : MonoBehaviour
{
    /// <summary>全ステージ</summary>
    [SerializeField] private StageScript[] stages;

    /// <summary>全ステージクリアタイム計測用</summary>
    [SerializeField] private IntervalScript allStageClearTime;

    /// <summary>現在のステージ</summary>
    private StageScript currentStage;

    /// <summary>現在のステージ数</summary>
    public int StageCount { get; private set; }

    /// <summary>次のステージ数</summary>
    public int NextStageCount
    {
        get
        {
            const int AddCount = 1;      //追加する数

            return StageCount + AddCount;//現在のステージ数 + 追加する数を返す
        }
    }

    /// <summary>ラストステージか</summary>
    public bool IsLastStage => NextStageCount >= stages.Length;

    /// <summary>全ステージのクリアタイム</summary>
    public IntervalScript ClearTime => allStageClearTime;

    private void Start()
    {
        currentStage = stages[default];//最初のステージを代入
    }

    /// <summary>ステージ数を + 1</summary>
    public void AddStageCount() => StageCount++;//ステージ数を増やす

    /// <summary>ステージ数を0にする</summary>
    public void ReSetStageCount() => StageCount = default;//現在のステージ数を0にする

    /// <summary>敵タンクの倒された数を0にする</summary>
    public void Retry() => currentStage.ReSetEnemyDeathCount();//敵タンクの倒された数を0にする

    /// <summary>現在のステージを生成</summary>
    /// <param name="tankManager">タンクの管理者</param>
    /// <param name="onAllEnemysDeath">全ての敵タンクを倒した際実行する関数</param>
    public void ActiveStage(StageTankManagerScript tankManager, Func<IEnumerator> onAllEnemysDeath)
    {
        currentStage = stages[StageCount];                 //現在のステージを代入

        currentStage.Active(tankManager, onAllEnemysDeath);//現在のステージを生成
    }

    /// <summary>ステージを非アクティブ</summary>
    public void InActiveStage() => currentStage.InActive();//現在のステージを非アクティブ
}
