using UnityEngine;
using UnityEngine.Events;

public static class StopWatch
{
    public const int Count = 1000000;

    public static void TimeLog(UnityAction c, int count = Count) => Debug.Log($"{Time(c, count)}ms");

    public static void TimeLog(UnityAction c, string text, int count = Count) => Debug.Log($"{text}{Time(c, count)}ms");

    public static long Time(UnityAction c, int count)
    {
        System.Diagnostics.Stopwatch sw = new();

        sw.Start();

        for (int i = default; i < count; i++) c.Invoke();

        sw.Stop();

        return sw.ElapsedMilliseconds;
    }
}
