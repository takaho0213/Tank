using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>チュートリアル</summary>
public class TutorialScript : MonoBehaviour
{
    /// <summary>アイキャッチ</summary>
    [SerializeField, LightColor] private StageEyeCatchUIScript stageEyeCatchUI;

    /// <summary>タンクの管理者</summary>
    [SerializeField, LightColor] private StageTankManagerScript tankManager;

    /// <summary>ステージ</summary>
    [SerializeField, LightColor] private StageScript stage;

    /// <summary>チュートリアルのメニュー</summary>
    [SerializeField, LightColor] private TutorialMenuScript tutorialMenu;

    /// <summary>ステージ名</summary>
    [SerializeField] private string stageName;

    /// <summary>スタートした際実行する関数</summary>
    private UnityAction onStart;

    /// <summary>チュートリアルがアクティブ中か</summary>
    public bool IsActive => tutorialMenu.IsActive;

    /// <summary>スタートした際実行する関数をセット</summary>
    /// <param name="onStart">スタートした際実行する関数</param>
    public void SetOnStartAction(UnityAction onStart) => this.onStart = onStart;

    /// <summary>フェードインした際の処理</summary>
    public void OnFadeIn()
    {
        stageEyeCatchUI.Display();                  //UIを表示

        tankManager.Player.ReStart();               //プレイヤーをリスタート

        stageEyeCatchUI.SetStageCount(stageName);   //ステージ名をセット

        stage.Active(tankManager, OnAllEnemysDeath);//ステージをアクティブ

        tutorialMenu.IsActive = true;               //チュートリアルメニューをアクティブ
    }

    /// <summary>フェードアウトした際の処理</summary>
    public void OnFadeOut()
    {
        stageEyeCatchUI.Fader.Run(null, onStart);//アイキャッチのフェードを開始
    }

    /// <summary>すべての敵タンクを倒した際の処理</summary>
    private IEnumerator OnAllEnemysDeath()
    {
        yield break;
    }

    /// <summary>チュートリアル終了時の処理</summary>
    public void OnQuit()
    {
        tankManager.InActive();       //タンクを非アクティブ

        stage.InActive();             //ステージを非アクティブ

        tutorialMenu.IsActive = false;//チュートリアルメニューを非アクティブ
    }
}
