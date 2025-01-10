using UnityEngine;
using UnityEngine.Events;

/// <summary>ステージのアイキャッチ</summary>
public class StageEyeCatchScript : MonoBehaviour
{
    /// <summary>アイキャッチ</summary>
    [SerializeField, LightColor] private StageEyeCatchUIScript eyecatchUI;

    /// <summary>ステージ開始時の待機時間</summary>
    [SerializeField] private float staegStartWaitTime;

    /// <summary>ステージ開始時の待機時間</summary>
    private Interval stageStartWaitInterval;

    /// <summary>アイキャッチのSEを再生する関数</summary>
    private UnityAction playEyeCatchSE;

    /// <summary>BGMのInfo</summary>
    private AudioInfo audioBGM;

    /// <summary>フェーダー</summary>
    public GraphicsFaderScript Fader => eyecatchUI.Fader;

    /// <summary>タンクが動けない状態か</summary>
    public bool IsNotMove => Fader.IsRun || !stageStartWaitInterval.IsOver;

    private void Start()
    {
        stageStartWaitInterval = new(time: staegStartWaitTime);            //インスタンス化

        playEyeCatchSE = AudioScript.I.StageAudio[StageClip.EyeCatch].Play;//アイキャッチSEを再生する関数を代入

        audioBGM = AudioScript.I.StageAudio[StageClip.BGM];                //BGMオーディオ
    }

    /// <summary>アイキャッチをフェードする関数</summary>
    /// <param name="i">フェードイン時実行する関数</param>
    /// <param name="o">フェードアウト時実行する関数</param>
    public void Fade(UnityAction i, UnityAction o)
    {
        audioBGM.Stop();                                //BGMを停止

        i += playEyeCatchSE;                            //アイキャッチSEを再生する関数を加算代入

        Fader.Run(i + playEyeCatchSE, o + OnStageStart);//フェード開始 フェードインした際引数の関数を実行
    }

    /// <summary>ステージを開始した際実行</summary>
    private void OnStageStart()
    {
        stageStartWaitInterval.ReSet();//インターバルをリセット

        audioBGM.Play();               //BGMを再生
    }

    public void DisplayUI() => eyecatchUI.Display();

    /// <summary>ステージ数,ライフ数を表示するテキストをセット</summary>
    /// <param name="stage">ステージ数</param>
    /// <param name="life">ライフ数</param>
    public void SetText(int stage, int life) => eyecatchUI.SetText(stage, life);
}
