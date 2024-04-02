using UnityEngine;

/// <summary>�V�[���������N���X</summary>
[System.Serializable]
public class Scene
{
    /// <summary>�V�[����</summary>
    [SerializeField] private string Name;

    public const string FieldName = nameof(Name);

    /// <param name="name">�V�[����</param>
    public Scene(string name) => Name = name;

    /// <summary>�V�[�������[�h</summary>
    public void Load() => UnityEngine.SceneManagement.SceneManager.LoadScene(Name);

    public static implicit operator string(Scene scene) => scene.Name;

    public static implicit operator Scene(string name) => new(name);
}
