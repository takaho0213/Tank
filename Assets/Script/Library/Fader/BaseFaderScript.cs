using UnityEngine;
using UnityEngine.Events;

public abstract class BaseFaderScript : MonoBehaviour
{
    /// <summary>フェード速度</summary>
    [SerializeField] protected float speed;

    /// <summary>フェードイン終了時実行するコールバック</summary>
    protected UnityAction onFadeInEnd;

    /// <summary>フェードアウト終了時実行するコールバック</summary>
    protected UnityAction onFadeOutEnd;

    /// <summary>フェード速度</summary>
    public float Speed { get => speed; set => speed = value; }

    /// <summary>フェードイン時のアルファ値</summary>
    protected abstract float InValue { get; }
    /// <summary>フェードアウト時のアルファ値</summary>
    protected abstract float OutValue { get; }

    /// <summary>フェードを待つか？</summary>
    protected abstract bool IsWait { get; }

    /// <summary>フェードさせる値</summary>
    public abstract float FadeValue { get; protected set; }

    /// <summary>目標の値</summary>
    public float Target { get; protected set; }

    /// <summary>フェードアウト中か</summary>
    public bool IsFadeOut { get; protected set; }

    /// <summary>フェード中か</summary>
    public bool IsRun { get; protected set; }

    /// <summary>フェードを開始</summary>
    /// <param name="onFadeInEnd">フェードイン終了時実行するコールバック</param>
    /// <param name="onFadeOutEnd">フェードアウト終了時実行するコールバック</param>
    public virtual void Run(UnityAction onFadeInEnd, UnityAction onFadeOutEnd)
    {
        if (IsRun) return;               //フェード中なら/終了

        this.onFadeInEnd = onFadeInEnd;  //フェードイン終了時実行するコールバックを代入
        this.onFadeOutEnd = onFadeOutEnd;//フェードアウト終了時実行するコールバックを代入

        Target = InValue;                //目標の値を代入

        IsRun = true;                    //フェード中かをtrue
    }

    /// <summary>フェードを開始</summary>
    /// <param name="onFadeInEnd">フェードイン終了時実行するコールバック</param>
    public virtual void Run(UnityAction onFadeInEnd) => Run(onFadeInEnd, null);

    /// <summary>フェードを開始</summary>
    public virtual void Run() => Run(null, null);

    /// <summary>フェードイン終了時実行</summary>
    protected virtual void OnFadeInEnd()
    {
        onFadeInEnd?.Invoke();//フェードイン終了時のコールバックを呼び出す

        onFadeInEnd = null;  //コールバックを削除
    }

    /// <summary>フェードアウト終了時実行</summary>
    protected virtual void OnFadeOutEnd()
    {
        onFadeOutEnd?.Invoke();//コールバックを呼び出す

        onFadeOutEnd = null;   //コールバックを削除

        IsRun = false;         //フェードを終了
    }

    /// <summary>フェード</summary>
    protected virtual void Fade()
    {
        if (IsWait) return;                         //待機中なら/終了

        var value = FadeValue;                      //フェードさせる値を代入

        if (Mathf.Approximately(value, Target))     //目標の値とほぼ等しかったら
        {
            FadeValue = Target;                     //目標の値を代入

            Target = IsFadeOut ? InValue : OutValue;//次の目標の値を代入

            if (IsFadeOut) OnFadeOutEnd();          //フェードアウト中なら フェードアウト終了時実行する関数を呼び出す
            else OnFadeInEnd();                     //それ以外なら         フェードイン  終了時実行する関数を呼び出す

            IsFadeOut = !IsFadeOut;                 //反転

            return;                                 //終了
        }

        FadeValue = Mathf.MoveTowards(value, Target, speed * Time.deltaTime);//フェードさせた値を代入
    }

    protected virtual void Update()
    {
        if (IsRun) Fade();//フェード中なら/フェード
    }
}
