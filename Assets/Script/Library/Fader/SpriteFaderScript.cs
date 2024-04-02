using UnityEngine;

/// <summary>スプライトのフェーダークラス</summary>
public class SpriteFaderScript : BaseFaderScript
{
    /// <summary>フェードさせるスプライトレンダラー配列</summary>
    [SerializeField] protected SpriteRenderer[] renderers;

    public override Color FadeColor
    {
        get => renderers[default].color;
        set { foreach (var r in renderers) r.color = value; }
    }
}
