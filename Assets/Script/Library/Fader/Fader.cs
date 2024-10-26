using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Fader
{
    /// <summary>�t�F�[�h���x</summary>
    public float Speed;

    protected Color FadeColor
    {
        get => get.Invoke();
        set => set.Invoke(value);
    }

    protected UnityAction<Color> set;
    protected System.Func<Color> get;

    /// <summary>�ڕW�̃A���t�@�l</summary>
    protected float target;

    /// <summary>�t�F�[�h�A�E�g���̃A���t�@�l</summary>
    protected readonly float outAlpha = Color.clear.a;
    /// <summary>�t�F�[�h�C�����̃A���t�@�l</summary>
    protected readonly float inAlpha = Color.black.a;

    /// <summary>�t�F�[�h����</summary>
    public bool IsRun { get; protected set; }

    public Fader(System.Func<Color> get, UnityAction<Color> set, float speed)
    {
        this.get = get;
        this.set = set;
        Speed = speed;
    }

    public IEnumerator Fade(bool isFadeOut)
    {
        if (IsRun) yield break;

        IsRun = true;

        target = isFadeOut ? outAlpha : inAlpha;

        Color color = FadeColor;

        while (IsRun)
        {
            color.a = Mathf.MoveTowards(color.a, target, Speed * Time.deltaTime);//�A���t�@�l���t�F�[�h

            FadeColor = color;

            yield return null;

            IsRun = !Mathf.Approximately(color.a, target);
        }
    }

    public IEnumerator Fade(bool isFadeOut, UnityAction c)
    {
        if (IsRun) yield break;

        yield return Fade(isFadeOut);

        c?.Invoke();
    }

    public IEnumerator Fade(bool isFadeOut, System.Func<IEnumerator> c)
    {
        if (IsRun) yield break;

        yield return Fade(isFadeOut);

        yield return c?.Invoke();
    }
}
