#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ProjectLines
{
    private const int Y = 4;
    private const int Size = 16;

    private const float ColorAlpha = 0.1f;

    private static readonly Color TargetColor = Color.clear.SetAlpha(ColorAlpha);

    static ProjectLines() => EditorApplication.projectWindowItemOnGUI += OnGUI;

    private static void OnGUI(string guid, Rect rect)
    {
        if (MathEx.IsEven((int)(rect.y - Y) / Size)) return;

        var pos = rect;
        pos.x = default;
        pos.xMax = rect.xMax;

        var color = GUI.color;
        GUI.color = TargetColor;
        GUI.Box(pos, string.Empty);
        GUI.color = color;
    }
}
#endif