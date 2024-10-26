using System.Collections;

public struct WaitTime : IEnumerator
{
    private float time;

    private float lastTime;

    public float Time { get => time; set => time = value; }

    public WaitTime(float time)
    {
        this.time = time;

        lastTime = UnityEngine.Time.time;
    }

    public object Current => default;

    public bool MoveNext() => UnityEngine.Time.time - lastTime <= time;

    public void Reset() => lastTime = UnityEngine.Time.time;
}