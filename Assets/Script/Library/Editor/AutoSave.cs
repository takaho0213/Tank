# if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class AutoSave
{
    private const double Minute = 60d;

    private const double Time = Minute * 5;

    private static readonly EditorInterval interval;

    static AutoSave()
    {
        interval = new(Time, true);

        interval.ReSet();

        EditorApplication.update += Update;
    }

    private static void Update()
    {
        if (interval && !EditorApplication.isPlaying && EditorSceneManager.SaveOpenScenes())//�C���^�[�o�������� ���� �G�f�B�^�[�����s������Ȃ� ���� �Z�[�u���o������
        {
            Debug.Log($"AutoSave SceneName \"{SceneManager.GetActiveScene().name}\"");
        }
    }
}
#endif