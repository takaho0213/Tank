using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class NewTextFileIO
{
    [SerializeField] private string path;

    private FileInfo info;

    public string Path => ApplicationEx.ToAbsolutePath(path);

    public FileInfo Info => info ??= new(Path);

    [CustomPropertyDrawer(typeof(NewTextFileIO), true)]
    public class TestSingleTextFileIODrawer : PropertyDrawer
    {
        private TextAsset Asset;

        private SerializedProperty pathProperty;

        public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
        {
            EditorGUI.BeginChangeCheck();

            pathProperty ??= p.FindPropertyRelative("path");

            string path = pathProperty.stringValue;

            if (!string.IsNullOrEmpty(path))
            {
                Asset ??= AssetDatabase.LoadAssetAtPath<TextAsset>(ApplicationEx.AssetsPath + path);
            }

            Asset = EditorGUI.ObjectField(r, l, Asset, typeof(TextAsset), true) as TextAsset;

            if (EditorGUI.EndChangeCheck())
            {
                pathProperty.stringValue = AssetDatabase.GetAssetPath(Asset).Replace(ApplicationEx.AssetsPath, string.Empty);

                Debug.Log(pathProperty.stringValue);
            }
        }
    }
}
