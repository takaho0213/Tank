using System;
using UnityEngine;
using System.Collections;

/// <summary>ステージの管理</summary>
public class StageManagerScript : MonoBehaviour
{
    /// <summary>全ステージ</summary>
    [SerializeField] private StageScript[] stages;

    /// <summary>現在のステージ</summary>
    private StageScript currentStage;

    /// <summary>前ステージクリアタイム計測用</summary>
    private Interval allStageClearTime;

    /// <summary>現在のステージ数</summary>
    public int StageCount { get; private set; }

    /// <summary>ステージのAudio</summary>
    private AudioInfoDictionary<StageClip> stageAudio;

    /// <summary>次のステージ数</summary>
    public int NextStageCount
    {
        get
        {
            const int AddCount = 1;

            return StageCount + AddCount;
        }
    }

    public float ElapsedTime => allStageClearTime.ElapsedTime;

    public bool IsMaxStage => NextStageCount >= stages.Length;

    private void Start()
    {
        stageAudio = AudioScript.I.StageAudio;

        allStageClearTime = new Interval();                                      //Intervalをインスタンス化

        currentStage = stages[default];                                          //最初のステージを代入
    }

    /// <summary></summary>
    public void ReSetTime() => allStageClearTime.ReSet();//スコアをリセット

    /// <summary></summary>
    public void ReSetStageCount() => StageCount = default;//現在のステージ数を0にする

    public void Retry() => currentStage.ReSetEnemyDeathCount();

    /// <summary>現在のステージを生成</summary>
    public void ActiveStage(StageTankManagerScript tankManager, Func<IEnumerator> onAllEnemysDeath)
    {
        currentStage = stages[StageCount];                 //現在のステージを代入

        currentStage.Active(tankManager, onAllEnemysDeath);//現在のステージを生成
    }

    public void InActiveStage() => currentStage.InActive();

    /// <summary>次のステージへ移行</summary>
    public void NextStage() => StageCount++;//ステージ数を増やす
}
