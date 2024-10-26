# if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Events;

[InitializeOnLoad]
public static class PlayModeState
{
    /// <summary>エディター開始</summary>
    public static UnityAction OnEnteredEditMode { get; set; }
    /// <summary>エディター終了</summary>
    public static UnityAction OnExitingEditMode { get; set; }
    /// <summary>プレイモード開始</summary>
    public static UnityAction OnEnteredPlayMode { get; set; }
    /// <summary>プレイモード終了</summary>
    public static UnityAction OnExitingPlayMode { get; set; }

    static PlayModeState() => EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.EnteredEditMode: OnEnteredEditMode?.Invoke(); break;
            case PlayModeStateChange.ExitingEditMode: OnExitingEditMode?.Invoke(); break; 
            case PlayModeStateChange.EnteredPlayMode: OnEnteredPlayMode?.Invoke(); break; 
            case PlayModeStateChange.ExitingPlayMode: OnExitingPlayMode?.Invoke(); break; 
        }
    }
}
# endif