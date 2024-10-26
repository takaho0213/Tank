using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Move01
{
    [SerializeField] protected float speed;

    protected UnityAction<float> SetValue;
    protected System.Func<float> GetValue;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float Value
    {
        get => GetValue != null ? GetValue.Invoke() : default;
        set => SetValue?.Invoke(value);
    }

    /// <summary>実行中か</summary>
    public bool IsRun { get; protected set; }

    public Move01(System.Func<float> get, UnityAction<float> set, float speed)
    {
        GetValue = get;
        SetValue = set;
        this.speed = speed;
    }

    public IEnumerator Move(bool isFadeOut)
    {
        if (IsRun) yield break;

        float target = isFadeOut ? MathEx.ZeroF : MathEx.OneF;

        do
        {
            Value = Mathf.MoveTowards(Value, target, speed * Time.deltaTime);

            yield return null;
        }
        while (IsRun = !Mathf.Approximately(Value, target));
    }

    public IEnumerator Move(bool isFadeOut, UnityAction c)
    {
        if (IsRun) yield break;

        yield return Move(isFadeOut);

        c?.Invoke();
    }

    public IEnumerator Move(bool isFadeOut, System.Func<IEnumerator> c)
    {
        if (IsRun) yield break;

        yield return Move(isFadeOut);

        yield return c?.Invoke();
    }

    public static IEnumerator Move(bool isOn, System.Func<float> get, UnityAction<float> set, float speed)
    {
        float target = isOn ? MathEx.OneF : MathEx.ZeroF;

        do
        {
            set.Invoke(Mathf.MoveTowards(get.Invoke(), target, speed * Time.deltaTime));

            yield return null;
        }
        while (!Mathf.Approximately(get.Invoke(), target));
    }
}
