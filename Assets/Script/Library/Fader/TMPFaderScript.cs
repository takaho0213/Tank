using UnityEngine;

/// <summary>TMPのフェーダークラス</summary>
public class TMPFaderScript : BaseFaderScript
{
    /// <summary>フェードさせるTMP配列</summary>
    [SerializeField] protected TMPro.TextMeshProUGUI[] TMPs;

    public override Color FadeColor
    {
        get => TMPs[default].color;
        set { foreach (var t in TMPs) t.color = value; }
    }
}
