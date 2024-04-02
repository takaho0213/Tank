using TMPro;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Tank
{
    /// <summary>スコアUI</summary>
    public class ScoreUIScript : MonoBehaviour
    {
        /// <summary>スコアのセーブ</summary>
        [SerializeField, LightColor] private TextIOScript ClearTimeIO;

        /// <summary>スコアイメージ</summary>
        [SerializeField, LightColor] private Image ScoreImage;

        /// <summary>スコアTMP</summary>
        [SerializeField, LightColor] private TextMeshProUGUI ScoreTMP;

        /// <summary>スコアテキスト</summary>
        [SerializeField] private StringReplace2 ScoreTextR2;
        /// <summary>全クリテキスト</summary>
        [SerializeField] private StringReplace2 AllClearTextR2;

        /// <summary>目標の色</summary>
        [SerializeField] private Color ScoreImageColor;

        /// <summary>スコアイメージの補間値</summary>
        [SerializeField, Range01] private float ScoreImageLerp;

        /// <summary>スコアテキスト加算インターバル</summary>
        [SerializeField] private float ScoreTextInterval;

        /// <summary>表示するスコア数</summary>
        [SerializeField] private int DisplayScoreCount;

        /// <summary>文字列結合</summary>
        private StringUnion ScoreUnion;

        /// <summary>SE待機</summary>
        private WaitForSeconds WaitScoreSE;

        /// <summary>スコアリスト</summary>
        private SerializList<Score> ScoreList;

        public void Start()
        {
            ScoreUnion ??= new(ScoreTextInterval);           //インスタンスを作成

            ScoreList = ClearTimeIO.Load<SerializList<Score>>();//スコアリスト
        }

        /// <summary>スコアリストをソートしセーブ</summary>
        /// <param name="current">現在のスコア</param>
        public void ScoreListSort(Score current)
        {
            ScoreList.Add(current);                            //スコアを追加

            ScoreList.Sort((a, b) => a.Time.CompareTo(b.Time));//スコアリストをソート

            int count = ScoreList.Count - DisplayScoreCount;   //削除数

            for (int i = default; i < count; i++)              //削除数分繰り返す
            {
                ScoreList.Remove(ScoreList.Last());            //最後の要素を削除
            }

            ClearTimeIO.Save(ScoreList, true);                      //スコアをセーブ
        }

        /// <summary>スコアテキストを生成</summary>
        /// <param name="currentText">現在のスコアテキスト</param>
        /// <returns>表示テキスト</returns>
        private string ScoreText(string currentText)
        {
            string text = string.Empty;                                             //表示テキスト

            for (int i = default; i < ScoreList.Count; i++)                         //スコアリスト分繰り返す
            {
                text += ScoreTextR2.Replace2((i + 1).ToString(), ScoreList[i].Text);//表示テキストを加算代入
            }

            return AllClearTextR2.Replace2(currentText, text);                      //表示テキストを置換
        }

        /// <summary>スコアを表示</summary>
        /// <param name="text">表示テキスト</param>
        /// <returns>文字列を全て結合し終えたら終了</returns>
        private IEnumerator ScoreDisplay(string text)
        {
            do                                                   //最低一回は処理したいのでdo whileを使用
            {
                ScoreImage.Lerp(ScoreImageColor, ScoreImageLerp);//補間値を代入

                ScoreTMP.text = ScoreUnion.Union(text);          //文字列を結合

                if (ScoreUnion.IsAll) yield break;               //全て結合し終えたら終了

                yield return null;                               //1フレーム停止
            }
            while (!ScoreUnion.IsAll);                           //文字列がすべて結合されていない限り繰り返す
        }

        /// <summary>UIをリセット</summary>
        /// <returns>SEが終わり次第終了</returns>
        private IEnumerator UIReSet()
        {
            var audio = AudioScript.I.StageAudio.Audios[StageClip.Score];//スコアオーディオ

            audio.Play();                                                //SEを再生

            yield return WaitScoreSE ??= new(audio.Clip.Clip.length);    //待機

            ScoreImage.color = Color.clear;                              //色をクリアにする

            ScoreTMP.text = string.Empty;                                //TMPのテキストをEmptyにする
        }

        /// <summary>スコアUIを表示</summary>
        /// <param name="time">クリアタイム</param>
        /// <returns>スコアを表示し終え、SEの再生か終了したら終了</returns>
        public IEnumerator Display(float time)
        {
            var current = new Score(time);     //現在のスコア

            ScoreListSort(current);            //スコアリストをソートし現在のスコアを取得

            var text = ScoreText(current.Text);//スコアテキストを取得

            yield return ScoreDisplay(text);   //スコアを表示

            yield return UIReSet();            //UIをリセット
        }
    }

    [System.Serializable]
    public class Score
    {
        /// <summary>変換する際のフォーマット</summary>
        private const string Format = @"hh\:mm\:ss\.ff";

        /// <summary>スコアテキスト</summary>
        public string Text;

        /// <summary>クリア時間</summary>
        public float Time;

        /// <param name="time">クリア時間</param>
        public Score(float time) => Text = TimeSpan.FromSeconds(Time = time).ToString(Format);//スコアテキストを作成
    }
}