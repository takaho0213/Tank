using UnityEngine;

/// <summary>シーンを扱うクラス</summary>
[System.Serializable]
public class Scene
{
    /// <summary>シーン名</summary>
    [SerializeField] private string Name;

    public const string FieldName = nameof(Name);

    /// <param name="name">シーン名</param>
    public Scene(string name) => Name = name;

    /// <summary>シーンをロード</summary>
    public void Load() => UnityEngine.SceneManagement.SceneManager.LoadScene(Name);

    public static implicit operator string(Scene scene) => scene.Name;

    public static implicit operator Scene(string name) => new(name);
}
