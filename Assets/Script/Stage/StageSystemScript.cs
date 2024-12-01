using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StageSystemScript : MonoBehaviour
{
    [SerializeField, LightColor] private TitleScript title;

    /// <summary>ステージ終了UI</summary>
    [SerializeField, LightColor] private StageEndUIScript stageEndUI;

    /// <summary>スコアUI</summary>
    [SerializeField, LightColor] private ScoreUIScript scoreUI;

    /// <summary>アイキャッチ</summary>
    [SerializeField, LightColor] private StageEyeCatchScript eyecatch;

    [SerializeField, LightColor] private StageTankManagerScript tankManager;

    [SerializeField, LightColor] private StageManagerScript stageManager;

    [SerializeField, LightColor] private TutorialScript tutorial;

    /// <summary>フレームレート</summary>
    [SerializeField, ReadOnly] private int frameRate;

    /// <summary>終了条件</summary>
    private System.Func<bool> isBreak;

    /// <summary>タンクが動けない状態か</summary>
    private bool isNotMove;

    /// <summary>ステージのAudio</summary>
    private AudioInfoDictionary<StageClip> stageAudio;

    /// <summary>タンクが動けない状態か</summary>
    public bool IsNotMove => eyecatch.IsNotMove　|| tutorial.IsNotMove || isNotMove;

    private void Start()
    {
        ApplicationEx.SetFrameRate(frameRate);                              //フレームレートを固定

        stageAudio = AudioScript.I.StageAudio;

        isBreak = () => !stageAudio[StageClip.Clear].IsPlaying;//終了条件を代入

        title.Init(OnReStartFadeIn, () => Fade(ActiveStage));

        tankManager.Init(OnPlayerDeath);                        //
    }

    /// <summary>リスタートのフェードイン時実行</summary>
    private void OnReStartFadeIn()
    {
        stageManager.ReSetTime();    //スコアをリセット

        eyecatch.DisplayUI();        //アイキャッチを表示

        tankManager.Player.ReStart();//リスタート

        InActiveStage();      //ステージリセット
    }

    /// <summary>プレイヤー死亡時に実行</summary>
    /// <param name="isNoLife">ライフが残っていないか</param>
    private IEnumerator OnPlayerDeath(bool isNoLife)
    {
        if (isNotMove) yield break;                                          //動かせない状態なら/終了

        isNotMove = true;                                                    //動けない状態かをtrue

        var audio = stageAudio[StageClip.PlayerDeath];            //プレイヤー死亡Audio

        audio.Play();                                                        //SEを再生

        var type = isNoLife ? UI.GameOver : UI.Retry;                        //表示UI

        yield return stageEndUI.Display(type, () => !audio.IsPlaying);//UIを表示

        if (isNoLife) OnPlayerGameOver();
        else Fade(OnPlayerRetry);//アイキャッチをフェードし、フェードイン時リトライ関数を実行

        isNotMove = false;                                                   //動けない状態かをfalse
    }

    /// <summary>アイキャッチをフェードする関数</summary>
    /// <param name="c">フェードイン時実行する関数</param>
    private void Fade(UnityAction c) => eyecatch.Fade(c, () => isNotMove = false);

    /// <summary>Playerがゲームオーバーした際実行</summary>
    private void OnPlayerGameOver()
    {
        stageManager.ReSetStageCount();

        stageAudio[StageClip.BGM].Stop();

        title.OnGameClear(InActiveStage);
    }

    /// <summary>Playerがリトライした際実行</summary>
    private void OnPlayerRetry()
    {
        tankManager.Player.OnRetry();//プレイヤーをリトライ

        tankManager.InActive();//現在のステージをリセット

        ActiveStage();    //現在のステージを生成

        stageManager.Retry();
    }

    /// <summary>現在のステージを生成</summary>
    private void ActiveStage()
    {
        stageManager.ActiveStage(tankManager, OnAllEnemysDeath);

        eyecatch.SetText(stageManager.NextStageCount, tankManager.Player.Life.LifeCount);
    }

    private void InActiveStage()
    {
        tankManager.InActive();

        stageManager.InActiveStage();
    }

    /// <summary>Enemyをすべて倒した際実行</summary>
    private IEnumerator OnAllEnemysDeath()
    {
        if (isNotMove) yield break;                                        //動けない状態なら/終了

        isNotMove = true;                                                  //動けない状態かをtrue

        stageAudio[StageClip.BGM].Stop();                //BGMを停止

        bool isAllClear = stageManager.IsMaxStage;                 //次のステージ数が全ステージ数以上なら

        stageAudio.Play(isAllClear ? StageClip.AllClear : StageClip.Clear);//オールクリアSEを再生

        var type = isAllClear ? UI.AllClear : UI.Clear;                    //表示UI

        yield return stageEndUI.Display(type, isBreak);                    //UIを表示

        if (isAllClear)                                                    //全クリなら
        {
            yield return scoreUI.Display(stageManager.ElapsedTime);   //スコアUIを表示

            OnPlayerGameOver();
        }
        else Fade(NextStage);                                              //アイキャッチをフェードし、フェードイン時NextStage関数を実行
    }

    /// <summary>次のステージへ移行</summary>
    private void NextStage()
    {
        InActiveStage();

        stageManager.NextStage();

        tankManager.PlayerAddLife(stageManager.StageCount);//

        ActiveStage();                        //現在のステージを生成
    }
}
