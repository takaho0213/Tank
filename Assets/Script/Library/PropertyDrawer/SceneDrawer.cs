#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Scene))]
public class SceneDrawer : PropertyDrawer
{
    /// <summary>新たなシーンをセット</summary>
    /// <param name="name">シーン名</param>
    private static SceneAsset AddScene(string name)
    {
        if (Drawer.TryFindAsset(name, out SceneAsset scene))
        {
            var array = EditorBuildSettings.scenes.AddArraySize(MathEx.OneI);

            array[array.LastIndex()] = new(scene.name, true);

            Debug.Log($"Add => {scene.name}");

            EditorBuildSettings.scenes = array;
        }

        return scene;
    }

    private SceneAsset GetScene(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;

        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.path.IndexOf(name) >= default(int))
            {
                return AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset)) as SceneAsset;
            }
        }

        return AddScene(name);
    }

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var property = p.FindPropertyRelative("Name");

        var scene = GetScene(property.stringValue);

        scene = EditorGUI.ObjectField(r, l, scene, typeof(SceneAsset), false) as SceneAsset;

        property.stringValue = scene ? scene.name : string.Empty;
    }
}
#endif
