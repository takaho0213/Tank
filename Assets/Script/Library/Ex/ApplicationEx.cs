using UnityEngine;

public static class ApplicationEx
{
    public const string AssetsPath = "Assets/";

    public const string Slash = "/";

    /// <summary>�W���t���[�����[�g</summary>
    public const int FrameRate60 = 60;

    /// <summary>Application.dataPath + /</summary>
    public static string Path => Application.dataPath + Slash;

    /// <summary>�t���[�����[�g��60�ɐݒ�</summary>
    public static void SetFrameRate60() => SetFrameRate(FrameRate60);

    /// <summary>�t���[�����[�g��ݒ�</summary>
    /// <param name="rate">�t���[�����[�g</param>
    public static void SetFrameRate(int rate)
    {
        Application.targetFrameRate = rate;

        Time.fixedDeltaTime = MathEx.OneF / rate;
    }

    /// <summary>�𑜓x�A�t���[�����[�g��ݒ�</summary>
    /// <param name="res">�𑜓x�A�t���[�����[�g</param>
    /// <param name="mode">�t���X�N���[���̏��</param>
    public static void SetResolution(Resolution res, FullScreenMode mode)
    {
        SetFrameRate(res.refreshRate);

        Screen.SetResolution(res.width, res.height, mode, res.refreshRate);
    }

    /// <summary>Assets/�ȉ��̑��΃p�X���΃p�X�ɕϊ�</summary>
    /// <param name="path">Assets/�ȉ��̑��΃p�X</param>
    /// <returns>��΃p�X</returns>
    public static string ToAbsolutePath(string path) => Path + path;

    /// <summary>��΃p�X�𑊑΃p�X�ɕϊ�</summary>
    /// <param name="path">��΃p�X</param>
    /// <returns>���΃p�X</returns>
    public static string ToRelativePath(string path) => path.Replace(Path, string.Empty);

    /// <summary>�A�v���P�[�V�������I��</summary>
    public static void Quit()
    {
        #if UNITY_EDITOR                                //Editor���Ȃ�

        UnityEditor.EditorApplication.isPlaying = false;//�v���C���~

        #else                                           //����ȊO�Ȃ�

        UnityEngine.Application.Quit();                 //�A�v���P�[�V�������I��

        #endif                                          //�I��
    }

    /// <summary>�A�Z�b�g���X�V</summary>
    public static void Refresh()
    {
        #if UNITY_EDITOR

        UnityEditor.AssetDatabase.Refresh();//�A�Z�b�g���X�V

        #endif
    }
}
