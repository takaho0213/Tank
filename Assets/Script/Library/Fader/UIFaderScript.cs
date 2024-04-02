using UnityEngine;

/// <summary>イメージとTMPのフェーダークラス</summary>
public class UIFaderScript : BaseFaderScript
{
    /// <summary>フェードさせるイメージ配列</summary>
    [SerializeField] protected UnityEngine.UI.Image[] images;
    /// <summary>フェードさせるTMP配列</summary>
    [SerializeField] protected TMPro.TextMeshProUGUI[] TMPs;

    public override Color FadeColor
    {
        get => images[default].color;
        set
        {
            foreach (var i in images) i.color = value;
            foreach (var t in TMPs) t.color = value;
        }
    }
}
