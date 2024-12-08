using System.Linq;
using UnityEngine;

[System.Serializable]
public abstract class BaseGUIField
{
    [SerializeField] protected string name;

    protected GUIStyle labelStyle;
    protected GUIStyle fieldStyle;

    protected float space;

    public abstract void Init();

    public virtual void SetStyle(GUIStyle labelStyle, GUIStyle fieldStyle, float space)
    {
        this.labelStyle = labelStyle;
        this.fieldStyle = fieldStyle;

        this.space = space;
    }

    public abstract void Field();
}

[System.Serializable]
public class GUISelectField<T> : BaseGUIField
{
    private const int XCount = 2;

    [SerializeField] private KeyValue<string, T>[] pairs;

    protected string[] items;

    protected int selectIndex;

    public T Value => pairs[selectIndex].Value;

    public override void Init()
    {
        items = pairs.Select(pair => pair.Key).ToArray();
    }

    public override void Field()
    {
        GUILayout.Label(name, labelStyle);

        GUILayout.Space(space);

        selectIndex = GUILayout.SelectionGrid(selectIndex, items, XCount, fieldStyle);
    }
}

[System.Serializable]
public abstract class GUIBaseSliderField : BaseGUIField
{
    protected float value;

    protected abstract float Min { get; }
    protected abstract float Max { get; }

    protected abstract string ValueLabel { get; }

    protected GUIStyle thumbStyle;
    protected GUIStyle valueStyle;

    public override void Init() { }

    public override void SetStyle(GUIStyle labelStyle, GUIStyle fieldStyle, float space)
    {
        base.SetStyle(labelStyle, fieldStyle, space);

        if (thumbStyle == null)
        {
            thumbStyle = new(GUI.skin.horizontalSliderThumb)
            {
                font = fieldStyle.font,
                fontSize = fieldStyle.fontSize,
            };

            valueStyle = new(labelStyle)
            {
                font = fieldStyle.font,
                fontSize = fieldStyle.fontSize,
            };
        }
    }

    public override void Field()
    {
        GUILayout.Label(name, labelStyle);

        GUILayout.Space(space);

        GUILayout.BeginHorizontal();

        GUILayout.Label(ValueLabel, valueStyle);

        value = GUILayout.HorizontalSlider(value, Min, Max, fieldStyle, fieldStyle);

        GUILayout.EndHorizontal();
    }
}

[System.Serializable]
public class GUISliderField : GUIBaseSliderField
{
    [SerializeField] private float min;
    [SerializeField] private float max;

    protected override float Min => min;
    protected override float Max => max;

    protected override string ValueLabel => value.Round(Digits).ToString();

    public float Value => value;

    private const int Digits = 2;
}

[System.Serializable]
public class GUIIntSliderField : GUIBaseSliderField
{
    [SerializeField] private int min;
    [SerializeField] private int max;

    protected override float Min => min;
    protected override float Max => max;

    protected override string ValueLabel => value.ToString();

    public int Value => Mathf.CeilToInt(value);

    public override void Field()
    {
        base.Field();

        value = Mathf.CeilToInt(value);
    }
}