#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
internal static class HierarchyStripes
{
    private const int RowHeight = 16;
    private const int OffSetY = -4;
    private const int X = 32;

    private const float ColorAlpha = 0.05f;

    static HierarchyStripes() => EditorApplication.hierarchyWindowItemOnGUI += OnGUI;

    private static void OnGUI(int instanceID, Rect rect)
    {
        if (MathEx.IsEven((int)(rect.y + OffSetY) / RowHeight)) return;

        rect.x = X;

        rect.xMax += RowHeight;

        EditorGUI.DrawRect(rect, Color.clear.SetAlpha(ColorAlpha));
    }
}
#endif