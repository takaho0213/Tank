using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>メニュー</summary>
public class MenuScript : MonoBehaviour
{
    /// <summary>チュートリアルの際表示するメニュー</summary>
    [SerializeField, LightColor] private TutorialMenuScript tutorialMenu;

    /// <summary>オブジェクト</summary>
    [SerializeField, LightColor] private GameObject obj;

    /// <summary>タイトルに戻るボタン</summary>
    [SerializeField, LightColor] private Button backTitleButton;
    /// <summary>ゲーム終了ボタン</summary>
    [SerializeField, LightColor] private Button gameQuitButton;

    /// <summary>タイトルに戻るボタンが押された際実行する関数</summary>
    private UnityAction onBackTitle;
    /// <summary>ゲーム終了ボタンが押された際実行する関数</summary>
    private UnityAction onGameQuit;

    /// <summary>タイトルに戻るボタンが押された際実行する関数を追加</summary>
    /// <param name="c">追加する関数</param>
    public void AddOnClickBackTitleButton(UnityAction c) => onBackTitle += c;

    /// <summary>ゲーム終了ボタンが押された際実行する関数を追加</summary>
    /// <param name="c">追加する関数</param>
    public void AddOnClickGameQuitButton(UnityAction c) => onGameQuit += c;

    private void Start()
    {
        backTitleButton.onClick.AddListener(OnBackTitle);//タイトルに戻るボタンが押された際実行する関数を追加

        gameQuitButton.onClick.AddListener(OnGameQuit);  //ゲーム終了ボタンが押された際実行する関数を追加
    }

    /// <summary>タイトルへ戻る際の処理</summary>
    private void OnBackTitle()
    {
        OpenOrClose();        //オープンまたはクローズさせる

        onBackTitle?.Invoke();//タイトルに戻るボタンが押された際のコールバックを実行
    }

    /// <summary>ゲームを終了する際の処理</summary>
    private void OnGameQuit()
    {
        OpenOrClose();       //オープンまたはクローズさせる

        onGameQuit?.Invoke();//ゲーム終了ボタンが押された際のコールバックを実行
    }

    /// <summary>オープンまたはクローズさせる</summary>
    public void OpenOrClose()
    {
        bool isActive = !obj.activeSelf;                       //アクティブ状態

        obj.SetActive(isActive);                               //アクティブ状態を設定

        Time.timeScale = isActive ? MathEx.ZeroF : MathEx.OneF;//時間の進み方を設定

        tutorialMenu.OpenOrClose();                            //チュートリアルメニューのオープンまたはクローズした際の関数を呼び出す
    }
}
