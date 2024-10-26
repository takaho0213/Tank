using UnityEngine;
using UnityEngine.UI;

/// <summary>タイトル</summary>
public class TitleScript : GraphicFaderScript
{
    [SerializeField] private GraphicFaderScript Fader;

    /// <summary>ステージマネージャー</summary>
    [SerializeField, LightColor] private StageManagerScript StageManager;

    //[SerializeField, LightColor] private TutorialScript Tutorial;

    /// <summary>タイトルオブジェクト</summary>
    [SerializeField, LightColor] private GameObject TitleObj;

    [SerializeField] private Button GameStartButton;
    [SerializeField] private Button GameQuitButton;
    //[SerializeField] private Button TutorialButton;

    private void Start()
    {
        ReStart();                         //リスタート

        StageManager.OnGameClear = ReStart;//ゲームクリア時実行する関数を代入

        GameStartButton.onClick.AddListener(GameStart);

        //TutorialButton.onClick.AddListener(Tutorial.Active);

        GameQuitButton.onClick.AddListener(() => Fader.Run(ApplicationEx.Quit));
    }

    /// <summary>リスタート時実行</summary>
    private void ReStart()
    {
        Run(() => TitleObj.SetActive(true), () => AudioScript.I.StageAudio.Play(StageClip.BGM));//フェードを開始 フェードイン時ステージをアクティブ フェードアウト時BGMを再生
    }

    /// <summary>フェードイン時実行する関数</summary>
    private void OnFadeIn()
    {
        TitleObj.SetActive(false);                                   //タイトルをアクティブ

        AudioScript.I.StageAudio.Dictionary[StageClip.BGM].Source.Stop();//BGMを停止

        StageManager.OnReStartFadeIn();                              //リスタートのフェードアウト時実行する関数を実行
    }

    /// <summary>ゲームを開始</summary>
    private void GameStart()
    {
        if (IsRun) return;                           //フェード中なら/終了

        Run(OnFadeIn, StageManager.OnReStartFadeOut);//フェード開始 フェードイン時OnFadeIn関数を実行 フェードアウト時OnReStartFadeIn関数を実行
    }

    private void OnTutorial()
    {

    }
}
