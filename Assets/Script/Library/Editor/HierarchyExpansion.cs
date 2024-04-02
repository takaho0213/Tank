# if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class HierarchyExpansion : MonoBehaviour
{
    private const int IconSize = 16;

    [InitializeOnLoadMethod]
    private static void Init() => EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;

    private static void HierarchyWindowItemOnGUI(int instanceID, Rect rect)
    {
        GameObject obj = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

        if (obj != null)
        {
            Component component = obj.GetComponent<MonoBehaviour>();

            if (component != null)
            {
                rect.width = rect.height = IconSize;

                GUI.DrawTexture(rect, AssetPreview.GetMiniThumbnail(component));
            }
        }
    }
}
# endif