# if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoad]
public static class HierarchyMonoIcon
{
    private const int IconSize = 16;

    private static readonly Dictionary<int, MonoBehaviour> Objects = new();

    static HierarchyMonoIcon() => EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;

    private static void HierarchyWindowItemOnGUI(int instanceID, Rect rect)
    {
        MonoBehaviour behaviour;

        if (!Objects.TryGetValue(instanceID, out behaviour))
        {
            behaviour = (EditorUtility.InstanceIDToObject(instanceID) as GameObject)?.GetComponent<MonoBehaviour>();

            if (!behaviour) return;
        }

        rect.width = rect.height = IconSize;

        GUI.DrawTexture(rect, AssetPreview.GetMiniThumbnail(behaviour));
    }
}
# endif