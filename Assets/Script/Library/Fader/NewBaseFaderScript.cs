using UnityEngine;

public abstract class NewBaseFaderScript : MonoBehaviour
{
    /// <summary>フェード速度</summary>
    [SerializeField] protected float speed;

    /// <summary>フェード速度</summary>
    public float Speed { get => speed; set => speed = value; }

    /// <summary>フェード中か</summary>
    public bool IsRun { get; protected set; }

    /// <summary>フェードアウト中か</summary>
    public bool IsFadeOut { get; protected set; }

    /// <summary></summary>
    public float TargetValue { get; protected set; }

    /// <summary></summary>
    public virtual float FadeValue { get; protected set; }

    /// <summary></summary>
    public virtual float InValue => MathEx.OneF;

    /// <summary></summary>
    public virtual float OutValue => MathEx.ZeroF;

    /// <summary></summary>
    protected abstract bool IsWait();

    public virtual void Run()
    {
        if (IsRun) return;

        TargetValue = InValue;

        IsRun = true;
    }

    protected abstract void OnFadeIn();

    protected virtual void OnFadeOut()
    {
        IsRun = false;
    }

    protected virtual void Update()
    {
        if (!IsRun) return;                                                       //

        float value = FadeValue;                                                  //

        if (Mathf.Approximately(value, TargetValue))                              //目標の色になったら
        {
            TargetValue = IsFadeOut ? InValue : OutValue;                         //目標の色のアルファ値に

            if (IsFadeOut) OnFadeOut();                                           //
            else OnFadeIn();                                                      //

            IsFadeOut = !IsFadeOut;                                               //反転

            return;                                                               //終了
        }

        if (!IsWait())                                                            //待機中でなければ
        {
            value = Mathf.MoveTowards(value, TargetValue, Speed * Time.deltaTime);//アルファ値をフェード

            FadeValue = value;                                                    //フェードさせた値を代入
        }
    }
}