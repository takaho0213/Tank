using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ColorEx
{
    /// <summary>�J���[�R�[�h�̃n�b�V��</summary>
    public const string Html = "#";

    /// <summary>Max�̒l</summary>
    public const float Max = 1f;
    /// <summary>Min�̒l</summary>
    public const float Min = 0f;

    /// <summary>Red���Z�b�g</summary>
    public static Color SetRed(this Color color, float r)
    {
        color.r = r;

        return color;
    }

    /// <summary>Green���Z�b�g</summary>
    public static Color SetGreen(this Color color, float g)
    {
        color.g = g;

        return color;
    }

    /// <summary>Blue���Z�b�g</summary>
    public static Color SetBlue(this Color color, float b)
    {
        color.b = b;

        return color;
    }

    /// <summary>Alpha���Z�b�g</summary>
    public static Color SetAlpha(this Color color, float a)
    {
        color.a = a;

        return color;
    }

    /// <summary>Red, Green, Blue���Z�b�g</summary>
    public static Color SetRGB(this Color color, Color a)
    {
        a.a = color.a;

        return a;
    }

    /// <summary>Red��Max</summary>
    public static Color RedMax(this Color color) => SetRed(color, Max);
    /// <summary>Green��Max</summary>
    public static Color GreenMax(this Color color) => SetGreen(color, Max);
    /// <summary>Blue��Max</summary>
    public static Color BlueMax(this Color color) => SetBlue(color, Max);
    /// <summary>Alpha��Max</summary>
    public static Color AlphaMax(this Color color) => SetAlpha(color, Max);

    /// <summary>Red��Min</summary>
    public static Color RedMin(this Color color) => SetRed(color, Min);
    /// <summary>Green��Min</summary>
    public static Color GreenMin(this Color color) => SetGreen(color, Min);
    /// <summary>Blue��Min</summary>
    public static Color BlueMin(this Color color) => SetBlue(color, Min);
    /// <summary>Alpha��Min</summary>
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

    /// <summary>�J���[�R�[�h�ɕϊ�</summary>
    public static string ToHtml(this Color color) => ColorUtility.ToHtmlStringRGBA(color);

    /// <summary>�J���[�R�[�h��Color</summary>
    public static bool TryToColor(this string html, out Color color) => ColorUtility.TryParseHtmlString(html, out color);

    /// <summary>�e�L�X�g���J���[�e�L�X�g�ɕϊ�</summary>
    public static string ToRichColorText(this string text, Color color) => $"<color=#{color.ToHtml()}>{text}</color>";

    /// <summary>���C���{�[</summary>
    public static Color Rainbow => Color.HSVToRGB(Time.time % Max, Max, Max);

# if UNITY_EDITOR
    /// <summary>EditorOnly���C���{�[</summary>
    public static Color EditorOnlyRainbow => Color.HSVToRGB(((float)UnityEditor.EditorApplication.timeSinceStartup) % Max, Max, Max);
#endif
}
