using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[System.Serializable]
public class NewDirectoryIO
{
    [SerializeField] private string path;

    private DirectoryInfo info;

    public string Path => ApplicationEx.ToAbsolutePath(path);

    public DirectoryInfo Info => info ??= new(Path);

    public bool IsExists => Directory.Exists(Path);

    public void Create()
    {
        if (!IsExists)
        {
            Directory.CreateDirectory(Path);
        }
    }

    [CustomPropertyDrawer(typeof(NewDirectoryIO), true)]
    public class TestDirectoryIODrawer : PropertyDrawer
    {
        private DefaultAsset Asset;

        private SerializedProperty pathProperty;

        public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
        {
            EditorGUI.BeginChangeCheck();

            pathProperty ??= p.FindPropertyRelative("path");

            string path = pathProperty.stringValue;

            string assetPath = ApplicationEx.AssetsPath + path;

            if (!string.IsNullOrEmpty(path) && Directory.Exists(assetPath))
            {
                Asset ??= AssetDatabase.LoadAssetAtPath<DefaultAsset>(assetPath);
            }

            Asset = EditorGUI.ObjectField(r, l, Asset, typeof(DefaultAsset), true) as DefaultAsset;

            string newPath;

            if (EditorGUI.EndChangeCheck() && Directory.Exists(newPath = AssetDatabase.GetAssetPath(Asset)))
            {
                pathProperty.stringValue = newPath.Replace(ApplicationEx.AssetsPath, string.Empty);

                Debug.Log(pathProperty.stringValue);
            }
        }
    }
}
#endif