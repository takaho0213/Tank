using UnityEngine;
using UnityEngine.Events;

/// <summary>ベースのフェーダー</summary>
public abstract class BaseFaderScript : FaderScript, IFade
{
    public abstract Color FadeColor { get; set; }

    /// <summary>フェードを開始</summary>
    public void Run() => Run(this, null, null, null);

    /// <summary>フェードを開始</summary>
    /// <param name="i">フェードイン時実行</param>
    public void Run(UnityAction i) => Run(this, i, null, null);

    /// <summary>フェードを開始</summary>
    /// <param name="i">フェードイン時実行</param>
    /// <param name="o">フェードアウト時実行</param>
    public void Run(UnityAction i, UnityAction o) => Run(this, i, o, null);

    /// <summary>フェードを開始</summary>
    /// <param name="i">フェードイン時実行</param>
    /// <param name="o">フェードアウト時実行</param>
    /// <param name="w">待機中か</param>
    public void Run(UnityAction i, UnityAction o, System.Func<bool> w) => Run(this, i, o, w);
}
