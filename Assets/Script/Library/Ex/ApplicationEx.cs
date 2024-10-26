using UnityEngine;

public static class ApplicationEx
{
    public const string AssetsPath = "Assets/";

    public const string Slash = "/";

    /// <summary>標準フレームレート</summary>
    public const int FrameRate60 = 60;

    /// <summary>Application.dataPath + /</summary>
    public static string Path => Application.dataPath + Slash;

    /// <summary>フレームレートを60に設定</summary>
    public static void SetFrameRate60() => SetFrameRate(FrameRate60);

    /// <summary>フレームレートを設定</summary>
    /// <param name="rate">フレームレート</param>
    public static void SetFrameRate(int rate)
    {
        Application.targetFrameRate = rate;

        Time.fixedDeltaTime = MathEx.OneF / rate;
    }

    /// <summary>解像度、フレームレートを設定</summary>
    /// <param name="res">解像度、フレームレート</param>
    /// <param name="mode">フルスクリーンの状態</param>
    public static void SetResolution(Resolution res, FullScreenMode mode)
    {
        SetFrameRate(res.refreshRate);

        Screen.SetResolution(res.width, res.height, mode, res.refreshRate);
    }

    /// <summary>Assets/以下の相対パスを絶対パスに変換</summary>
    /// <param name="path">Assets/以下の相対パス</param>
    /// <returns>絶対パス</returns>
    public static string ToAbsolutePath(string path) => Path + path;

    /// <summary>絶対パスを相対パスに変換</summary>
    /// <param name="path">絶対パス</param>
    /// <returns>相対パス</returns>
    public static string ToRelativePath(string path) => path.Replace(Path, string.Empty);

    /// <summary>アプリケーションを終了</summary>
    public static void Quit()
    {
        #if UNITY_EDITOR                                //Editor中なら

        UnityEditor.EditorApplication.isPlaying = false;//プレイを停止

        #else                                           //それ以外なら

        UnityEngine.Application.Quit();                 //アプリケーションを終了

        #endif                                          //終了
    }

    /// <summary>アセットを更新</summary>
    public static void Refresh()
    {
        #if UNITY_EDITOR

        UnityEditor.AssetDatabase.Refresh();//アセットを更新

        #endif
    }
}
