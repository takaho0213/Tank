using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineManager
{
    private readonly static Dictionary<float, WaitForSeconds> waitForSecondsCashs = new();

    private static readonly ObjectPoolQueue<WaitTime> WaitTimes = new(() => new());

    public static IEnumerator WaitTime(float time)
    {
        var wait = WaitTimes.GetObject();

        wait.Time = time;

        wait.Reset();

        yield return wait;

        WaitTimes.Enqueue(wait);
    }

    public static WaitForSeconds GetWaitForSeconds(float time)
    {
        WaitForSeconds wait;

        if (!waitForSecondsCashs.TryGetValue(time, out wait))
        {
            wait = new(time);

            waitForSecondsCashs.Add(time, wait);
        }

        return wait;
    }
}
