using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>タイトル</summary>
public class TitleScript : GraphicFaderScript
{
    /// <summary>チュートリアル</summary>
    [SerializeField, LightColor] private TutorialScript tutorial;

    /// <summary>メニュー</summary>
    [SerializeField, LightColor] private MenuScript menu;

    /// <summary>タイトルオブジェクト</summary>
    [SerializeField, LightColor] private GameObject titleObj;

    /// <summary>ゲームスタートボタン</summary>
    [SerializeField, LightColor] private Button gameStartButton;
    /// <summary>チュートリアルボタン</summary>
    [SerializeField, LightColor] private Button tutorialButton;
    /// <summary>ゲーム終了ボタン</summary>
    [SerializeField, LightColor] private Button gameQuitButton;

    /// <summary>ステージBGM</summary>
    private AudioInfo StageBGM;

    /// <summary>フェードインする際実行する関する関数</summary>
    private UnityAction onStartFadeIn;
    /// <summary>フェードアウトする際実行する関する関数</summary>
    private UnityAction onStartFadeOut;

    private void Start()
    {
        StageBGM = AudioScript.I.StageAudio[StageClip.BGM];//ステージBGM

        FadeActive(null);                                  //ステージをアクティブ

        gameStartButton.onClick.AddListener(OnGameStart);  //ゲーム開始ボタンが押された際実行する関数を追加

        tutorialButton.onClick.AddListener(OnTutorial);    //チュートリアルボタンが押された際実行する関数を追加

        gameQuitButton.onClick.AddListener(OnGameQuit);    //ゲーム終了ボタンが押された際実行する関するを追加

        menu.AddOnClickGameQuitButton(OnGameQuit);         //メニューのゲーム終了ボタンが押された際実行する関数を追加
    }

    /// <summary>初期化</summary>
    /// <param name="onStartFadeIn">フェードインする際実行する関する関数</param>
    /// <param name="onStartFadeOut">フェードアウトする際実行する関する関数</param>
    public void Init(UnityAction onStartFadeIn, UnityAction onStartFadeOut)
    {
        this.onStartFadeIn = onStartFadeIn;  //フェードインする際実行する関する関数を代入
        this.onStartFadeOut = onStartFadeOut;//フェードアウトする際実行する関する関数を代入
    }

    /// <summary>リスタート時実行</summary>
    /// <param name="c">フェードイン時実行する関数</param>
    public void FadeActive(UnityAction c)
    {
        c += () => titleObj.SetActive(true);//タイトルをアクティブにする関数を加算代入

        StageBGM.Stop();                    //BGMをストップ

        Run(c, StageBGM.Play);              //フェードを開始 フェードイン時ステージをアクティブ フェードアウト時BGMを再生
    }

    /// <summary>フェードイン時実行する関数</summary>
    private void OnFadeIn()
    {
        titleObj.SetActive(false);//タイトルをアクティブ

        StageBGM.Stop();          //BGMを停止

        onStartFadeIn?.Invoke();  //リスタートのフェードアウト時実行する関数を実行
    }

    /// <summary>ゲームを開始</summary>
    private void OnGameStart()
    {
        if (IsRun) return;            //フェード中なら/終了

        Run(OnFadeIn, onStartFadeOut);//フェード開始 フェードイン時OnFadeIn関数を実行 フェードアウト時OnReStartFadeIn関数を実行
    }

    /// <summary>チュートリアルボタンが押された際実行する関数を追加</summary>
    private void OnTutorial()
    {
        if (IsRun) return;                              //フェード中なら/終了

        UnityAction i = () => titleObj.SetActive(false);//タイトルを非アクティブ

        Run(tutorial.OnFadeIn + i, tutorial.OnFadeOut); //フェード
    }

    /// <summary>ゲーム終了ボタンが押された際実行する関数を追加</summary>
    private void OnGameQuit()
    {
        if (IsRun) return;      //フェード中なら/終了

        Run(ApplicationEx.Quit);//フェードしゲームを終了
    }
}
