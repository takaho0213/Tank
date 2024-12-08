using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorFieldScript : MonoBehaviour
{
    [SerializeField] private Graphic graphic;

    [SerializeField] private Slider rSlider;
    [SerializeField] private Slider gSlider;
    [SerializeField] private Slider bSlider;

    [SerializeField] private TextMeshProUGUI fieldTMP;

    [SerializeField] private string fieldName;

    [SerializeField] private ColorButtonScript[] buttons;

    private void Start()
    {
        fieldTMP.text = fieldName;

        rSlider.onValueChanged.AddListener(OnValueChanged);
        gSlider.onValueChanged.AddListener(OnValueChanged);
        bSlider.onValueChanged.AddListener(OnValueChanged);

        OnValueChanged(default);

        UnityAction<Color> c = OnClick;

        foreach (var b in buttons)
        {
            b.Init(c);
        }
    }

    private void OnValueChanged(float value)
    {
        var color = Color.black;

        color.r = rSlider.value;
        color.g = gSlider.value;
        color.b = bSlider.value;

        graphic.color = color;
    }

    private void OnClick(Color color)
    {
        rSlider.value = color.r;
        gSlider.value = color.g;
        bSlider.value = color.b;

        OnValueChanged(default);
    }
}

[System.Serializable]
public class ColorButton
{
    [SerializeField] private TextMeshProUGUI colorTMP;

    [SerializeField] private string colorName;

    [SerializeField] private Color color;

    private UnityAction<Color> onClick;

    public void Init(UnityAction<Color> onClick)
    {
        colorTMP.text = colorName;
        colorTMP.color = color;

        this.onClick = onClick;
    }

    public void OnClick()
    {
        onClick.Invoke(color);
    }
}