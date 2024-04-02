using UnityEngine;

/// <summary>イメージのフェーダークラス</summary>
public class ImageFaderScript : BaseFaderScript
{
    /// <summary>フェードさせるイメージ配列</summary>
    [SerializeField] protected UnityEngine.UI.Image[] images;

    public override Color FadeColor
    {
        get => images[default].color;
        set { foreach (var i in images) i.color = value; }
    }
}
