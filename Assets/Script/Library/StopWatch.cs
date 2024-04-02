using UnityEngine;
using UnityEngine.Events;

/// <summary>ˆ—‘¬“x‚ğ‘ª‚éƒNƒ‰ƒX</summary>
public static class StopWatch
{
    private static readonly System.Diagnostics.Stopwatch Watch = new();

    public static void Time(UnityAction c, int count, string text) => Debug.Log($"{text}{Time(c, count)}ms");

    public static long Time(UnityAction c, int count)
    {
        Watch.Start();

        for (int i = default; i < count; i++) c.Invoke();

        Watch.Stop();

        var time = Watch.ElapsedMilliseconds;

        Watch.Restart();

        return time;
    }
}
