#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(Scene))]
public class SceneDrawer : PropertyDrawer
{
    private void LogError(string name) => Debug.LogError($"Scene–¼{name}‚Íƒrƒ‹ƒh‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");

    private SceneAsset GetScene(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;

        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.path.IndexOf(name) != -1)
            {
                return (SceneAsset)AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset));
            }
        }

        LogError(name);

        return null;
    }

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var scene = p.FindPropertyRelative(Scene.FieldName);

        var newScene = EditorGUI.ObjectField(r, l, GetScene(scene.stringValue), typeof(SceneAsset), false);

        if (newScene == null) scene.stringValue = string.Empty;

        else if (newScene.name != scene.stringValue)
        {
            if (GetScene(newScene.name) == null) LogError(newScene.name);

            else scene.stringValue = newScene.name;
        }
    }
}

#endif
