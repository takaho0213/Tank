# if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EditorInterval
{
    private static double TIME => EditorApplication.timeSinceStartup;

    /// <summary>時間</summary>
    [SerializeField] private double time;
    /// <summary>自動でリセットするか</summary>
    [SerializeField] private bool isAutoReSet;

    /// <summary>時間</summary>
    public double Time { get => time; set => time = value; }

    /// <summary>自動でリセットするか</summary>
    public bool IsAutoReSet { get => isAutoReSet; set => isAutoReSet = value; }

    /// <summary>リセット時の時間</summary>
    public double LastTime { get; private set; }

    /// <summary>経過時間</summary>
    public double ElapsedTime => TIME - LastTime;

    /// <summary>残り時間</summary>
    public double TimeLimit => time - ElapsedTime;

    /// <summary>インターバルを越えたか</summary>
    public bool IsOver
    {
        get
        {
            bool isOver = ElapsedTime >= time;

            if (isOver && isAutoReSet) ReSet();

            return isOver;
        }
    }

    /// <summary>リセット</summary>
    public void ReSet() => LastTime = TIME;

    public EditorInterval(double time = default, bool isAutoReSet = default, double lastTime = default)
    {
        this.time = time;
        this.isAutoReSet = isAutoReSet;
        LastTime = lastTime;
    }

    public static implicit operator bool(EditorInterval i) => i.IsOver;
}
# endif