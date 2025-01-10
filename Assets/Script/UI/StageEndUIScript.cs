using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>UIのタイプ</summary>
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
    [SerializeField, LightColor] private Transform tmpTarget;

    /// <summary>ステージ終了イメージ</summary>
    [SerializeField, LightColor] private Image image;

    /// <summary>ステージ終了時に動かすテキスト配列</summary>
    [SerializeField] private MoveText[] moveTexts;

    /// <summary>TMPの目的の色</summary>
    [SerializeField] private Color tmpColor;

    /// <summary>目標の色</summary>
    [SerializeField] private Color imageColor;

    /// <summary>UIの補間値</summary>
    [SerializeField, Range01] private float uiLerp;

    /// <summary>全クリテキスト</summary>
    [SerializeField] private string allClearText;
    /// <summary>クリアテキスト</summary>
    [SerializeField] private string clearText;
    /// <summary>ゲームオーバーテキスト</summary>
    [SerializeField] private string gameOverText;
    /// <summary>リトライテキスト</summary>
    [SerializeField] private string retryText;

    public void Start()
    {
        foreach (var t in moveTexts)//StageEndTextsの要素数分繰り返す
        {
            t.Init();               //初期化
        }
    }

    /// <summary>UIを表示</summary>
    /// <param name="type">表示UI</param>
    /// <param name="isBreak">終了条件</param>
    /// <returns>終了条件を満たしたら終了</returns>
    public IEnumerator Display(UI type, System.Func<bool> isBreak)
    {
        var target = tmpTarget.position;               //目的座標

        string text = type switch                      //分岐条件UIの種類
        {
            UI.Clear => clearText,                     //クリア        なら クリアテキスト
            UI.AllClear => allClearText,               //全クリ        なら 全クリテキスト
            UI.Retry => retryText,                     //リトライ      なら リトライテキスト
            UI.GameOver => gameOverText,               //ゲームオーバーなら ゲームオーバーテキスト
            _ => string.Empty                          //それ以外      なら Empty
        };

        foreach (var t in moveTexts) t.TMP.text = text;//テキストを代入

        while (!isBreak.Invoke())                      //終了条件にならない限り繰り返す
        {
            image.LerpColor(imageColor, uiLerp);       //補間値を代入

            foreach (var t in moveTexts)               //MoveTextsの要素数分繰り返す
            {
                t.TMP.LerpColor(tmpColor, uiLerp);     //補間値を代入

                t.Trafo.LerpPosition(target, uiLerp);  //補間値を代入
            }

            yield return null;                         //1フレーム待機
        }

        image.color = Color.clear;                     //ImageのColorに(0, 0, 0, 0)を代入

        foreach (var t in moveTexts)                   //MoveTextsの要素数分繰り返す
        {
            t.ReSet();                                 //リセット
        }
    }
}
