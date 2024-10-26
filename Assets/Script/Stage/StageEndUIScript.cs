using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary></summary>
public enum UI
{
    /// <summary>クリア</summary>
    Clear,
    /// <summary>全クリ</summary>
    AllClear,
    /// <summary>リトライ</summary>
    Retry,
    /// <summary>ゲームオーバー</summary>
    GameOver,
}

/// <summary>ステージ終了時のUI</summary>
public class StageEndUIScript : MonoBehaviour
{
    /// <summary>目的地</summary>
    [SerializeField, LightColor] private Transform StageEndTMPTarget;

    /// <summary>ステージ終了イメージ</summary>
    [SerializeField, LightColor] private Image StageEndImage;

    /// <summary>ステージ終了時に動かすテキスト配列</summary>
    [SerializeField] private MoveText[] StageEndTexts;

    /// <summary>TMPの目的の色</summary>
    [SerializeField] private Color TMPColor;

    /// <summary>目標の色</summary>
    [SerializeField] private Color ImageColor;

    /// <summary>UIの補間値</summary>
    [SerializeField, Range01] private float UILerp;

    /// <summary>全クリテキスト</summary>
    [SerializeField] private string AllClearText;
    /// <summary>クリアテキスト</summary>
    [SerializeField] private string ClearText;
    /// <summary>ゲームオーバーテキスト</summary>
    [SerializeField] private string GameOverText;
    /// <summary>リトライテキスト</summary>
    [SerializeField] private string RetryText;

    public void Start()
    {
        foreach (var t in StageEndTexts)//StageEndTextsの要素数分繰り返す
        {
            t.Init();                   //初期化
        }
    }

    /// <summary>UIを表示</summary>
    /// <param name="type">表示UI</param>
    /// <param name="isBreak">終了条件</param>
    /// <returns>終了条件を満たしたら終了</returns>
    public IEnumerator Display(UI type, System.Func<bool> isBreak)
    {
        var target = StageEndTMPTarget.position;           //目的座標

        string text = type switch                          //分岐条件UIの種類
        {
            UI.Clear => ClearText,                      //クリア        なら クリアテキスト
            UI.AllClear => AllClearText,                   //全クリ        なら 全クリテキスト
            UI.Retry => RetryText,                      //リトライ      なら リトライテキスト
            UI.GameOver => GameOverText,                   //ゲームオーバーなら ゲームオーバーテキスト
            _ => string.Empty                    //それ以外      なら Empty
        };

        foreach (var t in StageEndTexts) t.TMP.text = text;//テキストを代入

        while (!isBreak.Invoke())                          //終了条件にならない限り繰り返す
        {
            StageEndImage.LerpColor(ImageColor, UILerp);        //補間値を代入

            foreach (var t in StageEndTexts)               //MoveTextsの要素数分繰り返す
            {
                t.TMP.LerpColor(TMPColor, UILerp);              //補間値を代入

                t.Trafo.LerpPosition(target, UILerp);      //補間値を代入
            }

            yield return null;                             //1フレーム待機
        }

        StageEndImage.color = Color.clear;                 //ImageのColorに(0, 0, 0, 0)を代入

        foreach (var t in StageEndTexts)                   //MoveTextsの要素数分繰り返す
        {
            t.ReSet();                                     //リセット
        }
    }
}

/// <summary>移動するテキスト</summary>
[System.Serializable]
public class MoveText
{
    /// <summary>Transform</summary>
    [field: SerializeField, LightColor] public Transform Trafo { get; private set; }

    /// <summary>TextMeshProUGUI</summary>
    [field: SerializeField, LightColor] public TextMeshProUGUI TMP { get; private set; }

    /// <summary>初期位置</summary>
    private Vector3 InitPos;

    /// <summary>初期位置セット</summary>
    public void Init() => InitPos = Trafo.position;

    /// <summary>リセット</summary>
    public void ReSet()
    {
        TMP.color = Color.clear; //(0, 0, 0, 0)を代入

        TMP.text = string.Empty; //テキストにEmptyを代入

        Trafo.position = InitPos;//初期位置を代入
    }
}
