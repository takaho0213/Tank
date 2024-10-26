using UnityEngine;

[System.Serializable]
public class Scene
{
    /// <summary>シーン名</summary>
    [SerializeField] private string Name;

    /// <param name="name">シーン名</param>
    public Scene(string name) => Name = name;

    /// <summary>シーンをロード</summary>
    public void Load() => UnityEngine.SceneManagement.SceneManager.LoadScene(Name);
}
