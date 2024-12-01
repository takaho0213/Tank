using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>タイトル</summary>
public class TitleScript : GraphicFaderScript
{
    [SerializeField, LightColor] private TutorialScript tutorial;

    /// <summary>タイトルオブジェクト</summary>
    [SerializeField, LightColor] private GameObject titleObj;

    [SerializeField, LightColor] private Button gameStartButton;
    [SerializeField, LightColor] private Button tutorialButton;
    [SerializeField, LightColor] private Button gameQuitButton;

    private AudioInfo StageBGM;

    private UnityAction onReStartFadeIn;
    private UnityAction onReStartFadeOut;

    private void Start()
    {
        StageBGM = AudioScript.I.StageAudio[StageClip.BGM];

        OnGameClear(null);                         //リスタート

        gameStartButton.onClick.AddListener(OnGameStart);

        tutorialButton.onClick.AddListener(OnTutorial);

        gameQuitButton.onClick.AddListener(OnGameQuit);

        tutorial.Init(OnGameClear);
    }

    public void Init(UnityAction onReStartFadeIn, UnityAction onReStartFadeOut)
    {
        this.onReStartFadeIn = onReStartFadeIn;
        this.onReStartFadeOut = onReStartFadeOut;
    }

    /// <summary>リスタート時実行</summary>
    public void OnGameClear(UnityAction c)
    {
        c += () => titleObj.SetActive(true);

        Run(c, StageBGM.Play);//フェードを開始 フェードイン時ステージをアクティブ フェードアウト時BGMを再生
    }

    /// <summary>フェードイン時実行する関数</summary>
    private void OnFadeIn()
    {
        titleObj.SetActive(false);     //タイトルをアクティブ

        StageBGM.Stop();        //BGMを停止

        onReStartFadeIn?.Invoke();//リスタートのフェードアウト時実行する関数を実行
    }

    /// <summary>ゲームを開始</summary>
    private void OnGameStart()
    {
        if (IsRun) return;                           //フェード中なら/終了

        Run(OnFadeIn, onReStartFadeOut);//フェード開始 フェードイン時OnFadeIn関数を実行 フェードアウト時OnReStartFadeIn関数を実行
    }

    private void OnTutorial()
    {
        if (IsRun) return;                           //フェード中なら/終了

        UnityAction i = () => titleObj.SetActive(false);

        Run(tutorial.OnFadeIn + i, tutorial.OnFadeOut);
    }

    private void OnGameQuit()
    {
        if (IsRun) return;

        Run(ApplicationEx.Quit);
    }
}
