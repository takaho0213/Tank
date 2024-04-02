using UnityEngine;
using UnityEngine.Events;

public class FaderScript : MonoBehaviour
{
    /// <summary>フェードイン時の待機時間</summary>
    public float WaitTime;

    /// <summary>フェード速度</summary>
    public float Speed;

    /// <summary>フェードさせるカラーを実装するインターフェイス</summary>
    protected IFade fade;

    /// <summary>待機中か</summary>
    protected System.Func<bool> isWait;

    /// <summary>フェードイン時実行する関数</summary>
    protected UnityAction onFadeIn;
    /// <summary>フェードアウト時実行する関数</summary>
    protected UnityAction onFadeOut;

    /// <summary>フェード終了時実行する関数</summary>
    protected UnityAction onFadeEnd;

    /// <summary>待機インターバル</summary>
    protected Interval waitInterval;

    /// <summary>目標のアルファ値</summary>
    protected float target;

    /// <summary>フェードイン時のアルファ値</summary>
    protected readonly float inAlpha = Color.black.a;
    /// <summary>フェードアウト時のアルファ値</summary>
    protected readonly float outAlpha = Color.clear.a;

    /// <summary>フェードアウト中か</summary>
    public bool IsFadeOut { get; protected set; }

    /// <summary>フェード中か</summary>
    public bool IsRun { get; protected set; }

    protected virtual void Awake() => waitInterval ??= new(Time: WaitTime);//インスタンスを作成

    /// <summary>フェードを開始</summary>
    /// <param name="f">フェードさせるColor</param>
    /// <param name="i">フェードイン時実行</param>
    /// <param name="o">フェードアウト時実行</param>
    /// <param name="w">待機中か</param>
    public virtual void Run(IFade f, UnityAction i, UnityAction o, System.Func<bool> w)
    {
        if (IsRun) return;                 //フェード中なら/終了

        onFadeEnd ??= () => IsRun = false; //フェード終了時の関数を実行

        waitInterval.Time = WaitTime;      //待機時間を代入

        IsRun = true;                      //フェード中かをtrue

        fade = f;                          //フェードさせるColorを代入

        onFadeIn = i + waitInterval.ReSet; //(フェードイン時実行 + インターバルをリセット)関数を代入
        onFadeOut = o + onFadeEnd;         //(フェードアウト時実行 + フェード終了時実行)関数を代入

        isWait = w ??= () => !waitInterval;//待機中か/nullならインターバルを越えていないかを代入

        target = inAlpha;                  //アルファ値を代入
    }

    /// <summary>フェード</summary>
    protected virtual void Fade()
    {
        var color = fade.FadeColor;

        if (Mathf.Approximately(color.a, target))        //目標の色になったら
        {
            target = IsFadeOut ? inAlpha : outAlpha;     //目標の色のアルファ値に

            (IsFadeOut ? onFadeOut : onFadeIn)?.Invoke();//(フェードアウトならフェードアウト時実行/それ以外ならフェードイン時実行)する関数を実行

            IsFadeOut = !IsFadeOut;                      //反転

            return;                                      //終了
        }

        if (!isWait.Invoke())                            //待機中でなければ
        {
            color.a = Mathf.MoveTowards(color.a, target, Speed * Time.deltaTime);//アルファ値をフェード

            fade.FadeColor = color;                                              //フェードさせた値を代入
        }
    }

    protected virtual void Update() { if (IsRun) Fade(); }//フェード中なら/フェード
}

public interface IFade
{
    /// <summary>フェードさせるカラー</summary>
    public Color FadeColor { get; set; }
}

//フェードの定義

//0 => 1 : フェードイン
//1 => 0 : フェードアウト