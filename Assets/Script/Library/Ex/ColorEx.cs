using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Color‚ÌŠg’£ƒNƒ‰ƒX</summary>
public static class ColorEx
{
    /// <summary>a.color = Color.LerpPivot(a.color, b, t)</summary>
    public static void Lerp(this SpriteRenderer a, Color b, float t) => a.color = Color.Lerp(a.color, b, t);

    /// <summary>a.color = Color.LerpPivot(a.color, b, t)</summary>
    public static void Lerp(this TextMeshProUGUI a, Color b, float t) => a.color = Color.Lerp(a.color, b, t);

    /// <summary>a.color = Color.LerpPivot(a.color, b, t)</summary>
    public static void Lerp(this Image a, Color b, float t) => a.color = Color.Lerp(a.color, b, t);

    /// <summary>a.color = Vector4.MoveTowards(a.color, b, t)</summary>
    public static void MoveTowards(this SpriteRenderer a, Color b, float t) => a.color = Vector4.MoveTowards(a.color, b, t);

    /// <summary>a.color = Vector4.MoveTowards(a.color, b, t)</summary>
    public static void MoveTowards(this TextMeshProUGUI a, Color b, float t) => a.color = Vector4.MoveTowards(a.color, b, t);

    /// <summary>a.color = Vector4.MoveTowards(a.color, b, t)</summary>
    public static void MoveTowards(this Image a, Color b, float t) => a.color = Vector4.MoveTowards(a.color, b, t);
}
