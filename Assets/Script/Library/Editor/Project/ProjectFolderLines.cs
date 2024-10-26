# if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Linq;

[InitializeOnLoad]
public static class ProjectFolderLines
{
    static ProjectFolderLines() => EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemOnGUI;

    private static void OnProjectWindowItemOnGUI(string guid, Rect rect)
    {
        int depth = AssetDatabase.GUIDToAssetPath(guid).Count((v) => v == '/');

        if (depth > default(int))
        {
            Handles.BeginGUI();

            for (int i = default; i <= depth; i++)
            {
                if (i == default) continue;

                float lineX = rect.x - HierarchyLines.Indent * i - HierarchyLines.Indent / 2;

                Handles.color = HierarchyLines.GetColorByDepth(depth - i + 1);

                Handles.DrawLine(new(lineX, rect.y), new(lineX, rect.yMax));
            }

            Handles.EndGUI();
        }
    }
}
#endif
