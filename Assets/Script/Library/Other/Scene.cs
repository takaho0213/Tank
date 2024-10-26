using UnityEngine;

[System.Serializable]
public class Scene
{
    /// <summary>�V�[����</summary>
    [SerializeField] private string Name;

    /// <param name="name">�V�[����</param>
    public Scene(string name) => Name = name;

    /// <summary>�V�[�������[�h</summary>
    public void Load() => UnityEngine.SceneManagement.SceneManager.LoadScene(Name);
}
