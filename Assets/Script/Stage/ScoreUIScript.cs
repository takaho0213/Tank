using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>スコアUI</summary>
public class ScoreUIScript : MonoBehaviour
{
    /// <summary>変換する際のフォーマット</summary>
    private const string ScoreFormat = @"hh\:mm\:ss\.ff";

    /// <summary>スコアのセーブ</summary>
    [SerializeField, LightColor] private CrearTimeIOScript scoreIO;

    /// <summary>スコアイメージ</summary>
    [SerializeField, LightColor] private Image scoreImage;

    /// <summary>スコアTMP</summary>
    [SerializeField, LightColor] private TextMeshProUGUI scoreTMP;

    /// <summary>スコアテキスト</summary>
    [SerializeField] private StringReplace2 scoreTextR2;
    /// <summary>全クリテキスト</summary>
    [SerializeField] private StringReplace2 allClearTextR2;

    /// <summary>目標の色</summary>
    [SerializeField] private Color scoreImageColor;

    /// <summary>スコアイメージの補間値</summary>
    [SerializeField, Range01] private float scoreImageLerp;

    /// <summary>スコアテキスト加算インターバル</summary>
    [SerializeField] private float scoreTextInterval;

    /// <summary>表示するスコア数</summary>
    [SerializeField] private int displayScoreCount;

    /// <summary>文字列結合</summary>
    private StringUnion scoreUnion;

    /// <summary>SE待機</summary>
    private WaitForSeconds waitScoreSE;

    /// <summary>スコアリスト</summary>
    private SerializeList<float> scoreList;

    /// <summary>スコアテキスト</summary>
    public static string ToScoreText(float time) => System.TimeSpan.FromSeconds(time).ToString(ScoreFormat);

    public void Start()
    {
        scoreUnion ??= new(scoreTextInterval);                   //インスタンスを作成

        scoreList = scoreIO.Load<SerializeList<float>>();//スコアリスト
    }

    /// <summary>スコアリストをソートしセーブ</summary>
    /// <param name="current">現在のスコア</param>
    public void ScoreListSort(float current)
    {
        scoreList.Add(current);                         //スコアを追加

        scoreList.Sort();                               //スコアリストをソート

        int count = scoreList.Count - displayScoreCount;//削除数

        for (int i = default; i < count; i++)               //削除数分繰り返す
        {
            scoreList.Remove(scoreList.Last());     //最後の要素を削除
        }

        scoreIO.Save(scoreList, true);              //スコアをセーブ
    }

    /// <summary>スコアテキストを生成</summary>
    /// <param name="currentText">現在のスコアテキスト</param>
    /// <returns>表示テキスト</returns>
    private string ScoreText(string currentText)
    {
        string text = string.Empty;                                       //表示テキスト

        string number = default;                                          //順位テキスト
        string time = default;                                            //タイムテキスト

        for (int i = default; i < scoreList.Count; i++)               //スコアリスト分繰り返す
        {
            number = (i + 1).ToString();                                  //順位テキストを代入
            time = ToScoreText(scoreList[i]);                     //タイムテキストを代入

            text += scoreTextR2.Replace2(number, time) + StringEx.NewLine;//表示テキストを加算代入
        }

        return allClearTextR2.Replace2(currentText, text);                //表示テキストを置換
    }

    /// <summary>スコアを表示</summary>
    /// <param name="text">表示テキスト</param>
    /// <returns>文字列を全て結合し終えたら終了</returns>
    private IEnumerator ScoreDisplay(string text)
    {
        do                                                        //最低一回は処理したいのでdo whileを使用
        {
            scoreImage.LerpColor(scoreImageColor, scoreImageLerp);//補間値を代入

            scoreTMP.text = scoreUnion.Union(text);               //文字列を結合

            if (scoreUnion.IsAll) yield break;                    //全て結合し終えたら終了

            yield return null;                                    //1フレーム停止
        }
        while (!scoreUnion.IsAll);                                //文字列がすべて結合されていない限り繰り返す
    }

    /// <summary>UIをリセット</summary>
    /// <returns>SEが終わり次第終了</returns>
    private IEnumerator ReSetUI()
    {
        var audio = AudioScript.I.StageAudio[StageClip.Score];//スコアオーディオ

        audio.Play();                                         //SEを再生

        yield return waitScoreSE ??= new(audio.Clip.Length);  //待機

        scoreImage.color = Color.clear;                       //色をクリアにする

        scoreTMP.text = string.Empty;                         //TMPのテキストをEmptyにする
    }

    /// <summary>スコアUIを表示</summary>
    /// <param name="time">クリアタイム</param>
    /// <returns>スコアを表示し終え、SEの再生か終了したら終了</returns>
    public IEnumerator Display(float time)
    {
        ScoreListSort(time);                        //スコアリストをソートし現在のスコアを取得

        var text = ScoreText(ToScoreText(time));//スコアテキストを取得

        yield return ScoreDisplay(text);            //スコアを表示

        yield return ReSetUI();                     //UIをリセット
    }
}
