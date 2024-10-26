using UnityEngine;

public static class Timer
{
    public enum Scale
    {
        Stop = 0,
        Normal = 1,
    }

    public static void Stop() => Time.timeScale = (int)Scale.Stop;
    public static void Normal() => Time.timeScale = (int)Scale.Normal;
}
