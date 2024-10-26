using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ColorEx
{
    /// <summary>カラーコードのハッシュ</summary>
    public const string Html = "#";

    /// <summary>Maxの値</summary>
    public const float Max = 1f;
    /// <summary>Minの値</summary>
    public const float Min = 0f;

    /// <summary>Redをセット</summary>
    public static Color SetRed(this Color color, float r)
    {
        color.r = r;

        return color;
    }

    /// <summary>Greenをセット</summary>
    public static Color SetGreen(this Color color, float g)
    {
        color.g = g;

        return color;
    }

    /// <summary>Blueをセット</summary>
    public static Color SetBlue(this Color color, float b)
    {
        color.b = b;

        return color;
    }

    /// <summary>Alphaをセット</summary>
    public static Color SetAlpha(this Color color, float a)
    {
        color.a = a;

        return color;
    }

    /// <summary>Red, Green, Blueをセット</summary>
    public static Color SetRGB(this Color color, Color a)
    {
        a.a = color.a;

        return a;
    }

    /// <summary>RedをMax</summary>
    public static Color RedMax(this Color color) => SetRed(color, Max);
    /// <summary>GreenをMax</summary>
    public static Color GreenMax(this Color color) => SetGreen(color, Max);
    /// <summary>BlueをMax</summary>
    public static Color BlueMax(this Color color) => SetBlue(color, Max);
    /// <summary>AlphaをMax</summary>
    public static Color AlphaMax(this Color color) => SetAlpha(color, Max);

    /// <summary>RedをMin</summary>
    public static Color RedMin(this Color color) => SetRed(color, Min);
    /// <summary>GreenをMin</summary>
    public static Color GreenMin(this Color color) => SetGreen(color, Min);
    /// <summary>BlueをMin</summary>
    public static Color BlueMin(this Color color) => SetBlue(color, Min);
    /// <summary>AlphaをMin</summary>
    public static Color AlphaMin(this Color color) => SetAlpha(color, Min);

    /// <summary>a.color = Color.Lerp(a.color, b, t)</summary>
    public static void LerpColor(this SpriteRenderer a, Color b, float t) => a.color = Color.Lerp(a.color, b, t);

    /// <summary>a.color = Color.Lerp(a.color, b, t)</summary>
    public static void LerpColor(this TextMeshProUGUI a, Color b, float t) => a.color = Color.Lerp(a.color, b, t);

    /// <summary>a.color = Color.Lerp(a.color, b, t)</summary>
    public static void LerpColor(this Image a, Color b, float t) => a.color = Color.Lerp(a.color, b, t);

    /// <summary>a.color = Vector4.MoveTowards(a.color, b, t)</summary>
    public static void MoverTowardsColor(this SpriteRenderer a, Color b, float t) => a.color = Vector4.MoveTowards(a.color, b, t);

    /// <summary>a.color = Vector4.MoveTowards(a.color, b, t)</summary>
    public static void MoveTowardsColor(this TextMeshProUGUI a, Color b, float t) => a.color = Vector4.MoveTowards(a.color, b, t);

    /// <summary>a.color = Vector4.MoveTowards(a.color, b, t)</summary>
    public static void MoveTowardsColor(this Image a, Color b, float t) => a.color = Vector4.MoveTowards(a.color, b, t);

    /// <summary>カラーコードに変換</summary>
    public static string ToHtml(this Color color) => ColorUtility.ToHtmlStringRGBA(color);

    /// <summary>カラーコードをColor</summary>
    public static bool TryToColor(this string html, out Color color) => ColorUtility.TryParseHtmlString(html, out color);

    /// <summary>テキストをカラーテキストに変換</summary>
    public static string ToRichColorText(this string text, Color color) => $"<color=#{color.ToHtml()}>{text}</color>";

    /// <summary>レインボー</summary>
    public static Color Rainbow => Color.HSVToRGB(Time.time % Max, Max, Max);

# if UNITY_EDITOR
    /// <summary>EditorOnlyレインボー</summary>
    public static Color EditorOnlyRainbow => Color.HSVToRGB(((float)UnityEditor.EditorApplication.timeSinceStartup) % Max, Max, Max);
#endif
}
