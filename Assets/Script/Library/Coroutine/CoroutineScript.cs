using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoroutineScript : MonoBehaviour//CoroutineManager
{
    public static readonly WaitForFixedUpdate WaitFixedUpdate = new();

    private readonly Dictionary<int, Coroutine> coroutines = new();

    public IReadOnlyDictionary<int, Coroutine> Coroutines => coroutines;

    private void Test()
    {
        var s = new WaitForSeconds(10);
    }

    public int Run(IEnumerator routine)
    {
        int key = coroutines.Count;

        coroutines.Add(key, StartCoroutine(routine));

        return key;
    }

    public void Stop(int key)
    {
        if (coroutines.TryGetValue(key, out var coroutine) && coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
