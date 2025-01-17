using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>ステージシステム</summary>
public class StageSystemScript : MonoBehaviour
{
    /// <summary>タイトル</summary>
    [SerializeField, LightColor] private TitleScript title;

    /// <summary>ステージ終了UI</summary>
    [SerializeField, LightColor] private StageEndUIScript stageEndUI;

    /// <summary>スコアUI</summary>
    [SerializeField, LightColor] private ScoreUIScript scoreUI;

    /// <summary>アイキャッチ</summary>
    [SerializeField, LightColor] private StageEyeCatchScript eyecatch;

    /// <summary>タンクの管理者</summary>
    [SerializeField, LightColor] private StageTankManagerScript tankManager;

    /// <summary>ステージの管理者</summary>
    [SerializeField, LightColor] private StageManagerScript stageManager;

    /// <summary>メニュー</summary>
    [SerializeField, LightColor] private MenuScript menu;

    /// <summary>チュートリアル</summary>
    [SerializeField, LightColor] private TutorialScript tutorial;

    /// <summary>フレームレート</summary>
    [SerializeField, ReadOnly] private int frameRate;

    /// <summary>終了条件</summary>
    private System.Func<bool> isBreak;

    /// <summary>タンクが動けない状態か</summary>
    private bool isNotMove;

    /// <summary>ステージのAudio</summary>
    private AudioInfoDictionary<StageClip> stageAudio;

    /// <summary>チュートリアル中か</summary>
    public bool IsTutorial => tutorial.IsActive;

    /// <summary>タンクが動けない状態か</summary>
    public bool IsNotMove => title.IsRun || eyecatch.IsNotMove || isNotMove;

    private void Start()
    {
        ApplicationEx.SetFrameRate(frameRate);                 //フレームレートを固定

        stageAudio = AudioScript.I.StageAudio;                 //ステージのオーディオ

        isBreak = () => !stageAudio[StageClip.Clear].IsPlaying;//終了条件を代入

        title.Init(OnReStartFadeIn, () => Fade(ActiveStage));  //タイトルを初期化

        tankManager.Init(OnPlayerDeath);                       //プレイヤーか死亡した際実行する関数をセット

        tutorial.SetOnStartAction(() => isNotMove = false);    //チュートリアルを初期化

        menu.AddOnClickBackTitleButton(() =>                   //タイトルに戻るボタンが押された際実行する関数を追加
        {
            isNotMove = stageManager.ClearTime.IsStop = true;  //移動出来ない状態にする

            title.FadeActive(() =>                             //フェードしアクティブにする際実行する関数を追加
            {
                InActiveStage();                               //ステージを非アクティブ

                tutorial.OnQuit();                             //チュートリアルを終了
            });
        });

        tankManager.Player.ReSetTank();                        //プレイヤータンクをリセット

        isNotMove = true;                                      //移動できない状態にする
    }

    /// <summary>リスタートのフェードイン時実行</summary>
    private void OnReStartFadeIn()
    {
        eyecatch.DisplayUI();        //アイキャッチを表示

        tankManager.Player.ReStart();//リスタート

        InActiveStage();             //ステージリセット
    }

    /// <summary>プレイヤー死亡時に実行</summary>
    /// <param name="isNoLife">ライフが残っていないか</param>
    private IEnumerator OnPlayerDeath(bool isNoLife)
    {
        if (isNotMove) yield break;                                   //動かせない状態なら/終了

        isNotMove = stageManager.ClearTime.IsStop = true;             //動けない状態かをtrue

        var audio = stageAudio[StageClip.PlayerDeath];                //プレイヤー死亡Audio

        audio.Play();                                                 //SEを再生

        var type = isNoLife ? UI.GameOver : UI.Retry;                 //表示UI

        yield return stageEndUI.Display(type, () => !audio.IsPlaying);//UIを表示

        if (isNoLife) OnPlayerGameOver();                             //ライフが無ければ/OnPlayerGameOver()を実行
        else Fade(OnPlayerRetry);                                     //それ以外なら/アイキャッチをフェードし、フェードイン時OnPlayerRetry()を実行

        isNotMove = false;                                            //動けない状態かをfalse
    }

    /// <summary>アイキャッチをフェードする関数</summary>
    /// <param name="c">フェードイン時実行する関数</param>
    private void Fade(UnityAction c)
    {
        eyecatch.Fade(c, () => isNotMove = stageManager.ClearTime.IsStop = false);//アイキャッチをフェードさせる
    }

    /// <summary>Playerがゲームオーバーした際実行</summary>
    private void OnPlayerGameOver()
    {
        stageManager.ReSetStageCount();    //ステージを0にする

        title.FadeActive(() =>             //フェードしアクティブにする際の関数をセット
        {
            InActiveStage();               //ステージを非アクティブ

            tankManager.Player.ReSetTank();//プレイヤータンクをリセット
        });
    }

    /// <summary>Playerがリトライした際実行</summary>
    private void OnPlayerRetry()
    {
        tankManager.Player.OnRetry();//プレイヤーをリトライ

        tankManager.InActive();      //現在のステージをリセット

        ActiveStage();               //現在のステージを生成

        stageManager.Retry();        //敵タンクの倒された数を0にする
    }

    /// <summary>現在のステージを生成</summary>
    private void ActiveStage()
    {
        stageManager.ActiveStage(tankManager, OnAllEnemysDeath);                         //ステージをアクティブ

        eyecatch.SetText(stageManager.NextStageCount, tankManager.Player.Life.LifeCount);//ステージテキストをセット
    }

    /// <summary>ステージを非アクティブ</summary>
    private void InActiveStage()
    {
        tankManager.InActive();      //タンクを非アクティブ

        stageManager.InActiveStage();//ステージを非アクティブ
    }

    /// <summary>Enemyをすべて倒した際実行</summary>
    private IEnumerator OnAllEnemysDeath()
    {
        if (isNotMove) yield break;                                          //動けない状態なら/終了

        isNotMove = stageManager.ClearTime.IsStop = true;                    //移動出来ない状態にする

        stageAudio[StageClip.BGM].Stop();                                    //BGMを停止

        bool isAllClear = stageManager.IsLastStage;                          //次のステージ数が全ステージ数以上なら

        stageAudio.Play(isAllClear ? StageClip.AllClear : StageClip.Clear);  //オールクリアSEを再生

        var type = isAllClear ? UI.AllClear : UI.Clear;                      //表示UI

        yield return stageEndUI.Display(type, isBreak);                      //UIを表示

        if (isAllClear)                                                      //全クリなら
        {
            yield return scoreUI.Display(stageManager.ClearTime.ElapsedTime);//スコアUIを表示

            stageManager.ClearTime.ReSetTime();                              //クリアタイムをリセット

            OnPlayerGameOver();                                              //Playerがゲームオーバーした際実行
        }
        else Fade(NextStage);                                                //アイキャッチをフェードし、フェードイン時NextStage関数を実行
    }

    /// <summary>次のステージへ移行</summary>
    private void NextStage()
    {
        InActiveStage();                                   //ステージを非アクティブ

        stageManager.AddStageCount();                      //ステージの数を + 1

        tankManager.PlayerAddLife(stageManager.StageCount);//プレイヤータンクのライフを増やす

        ActiveStage();                                     //現在のステージを生成
    }

    private void Update()
    {
        if (!isNotMove && Input.GetKeyDown(KeyCode.Space))//動ける状態 かつ スペースが押されたら
        {
            menu.OpenOrClose();                           //メニューをオープンまたはクローズ
        }
    }
}
