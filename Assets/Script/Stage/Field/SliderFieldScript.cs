using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderFieldScript : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI valueTMP;
    [SerializeField] private TextMeshProUGUI fieldTMP;

    [SerializeField] private string fieldName;

    [SerializeField] private float min;
    [SerializeField] private float max;

    [SerializeField] private int digits;

    [SerializeField] private bool isInt;

    public float Value => slider.value;

    public int IntValue => slider.value.RoundToInt();

    private void Start()
    {
        fieldTMP.text = fieldName;

        slider.minValue = isInt ? min.RoundToInt() : min;
        slider.maxValue = isInt ? max.RoundToInt() : max;

        slider.onValueChanged.AddListener(OnValueChanged);

        OnValueChanged(Value);
    }

    private void OnValueChanged(float value)
    {
        if (isInt) slider.value = value.RoundToInt();

        valueTMP.text = (isInt ? value.RoundToInt() : value.Round(digits)).ToString();
    }
}
