/// <summary>アプリケーションクラス</summary>
public static class App
{
    /// <summary>アプリケーションを終了</summary>
    public static void QuitGame()
    {
        #if UNITY_EDITOR                                //Editor中なら

        UnityEditor.EditorApplication.isPlaying = false;//プレイを停止

        #else                                           //それ以外なら

        UnityEngine.Application.Quit();                 //アプリケーションを終了

        #endif                                          //終了
    }
}
