# if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyLines
{
    public const float Indent = 14;
    private const float LineWidth = 1f;
    private const float Alpha = 0.2f;

    private static readonly Color[] Colors =
    {
        Color.black,
        Color.white,
    };

    static HierarchyLines() => EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;

    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect rect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (obj)
        {
            DrawVerticalLines(rect, GetDepth(obj.transform));
        }
    }

    /// <summary>ê[Ç≥ÇéÊìæ</summary>
    private static int GetDepth(Transform transform)
    {
        int depth = default;

        while (transform.parent != null)
        {
            depth++;
            transform = transform.parent;
        }

        return depth;
    }

    private static void DrawVerticalLines(Rect selectionRect, int depth)
    {
        Handles.BeginGUI();

        for (int i = default; i < depth; i++)
        {
            float indent = Indent + (Indent * i) + (Indent / MathEx.TwoF);
            float lineX = selectionRect.x - indent;

            var newRect = selectionRect;

            newRect.x = lineX;
            newRect.width = LineWidth;

            var lineColor = GetColorByDepth(depth - i).SetAlpha(Alpha);

            Handles.DrawSolidRectangleWithOutline(newRect, lineColor, lineColor);
        }

        Handles.EndGUI();
    }

    public static Color GetColorByDepth(int depth) => Colors.Repeat(depth);
}
#endif