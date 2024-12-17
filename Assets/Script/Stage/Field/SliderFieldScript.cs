using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>int or floatの値を設定するフィールド</summary>
public class SliderFieldScript : MonoBehaviour
{
    /// <summary>スライダー</summary>
    [SerializeField] private Slider slider;

    /// <summary>値を表示するTMP</summary>
    [SerializeField] private TextMeshProUGUI valueTMP;
    /// <summary>フィールド名を表示するTMP</summary>
    [SerializeField] private TextMeshProUGUI fieldTMP;

    /// <summary>フィールド名</summary>
    [SerializeField] private string fieldName;

    /// <summary>最小値</summary>
    [SerializeField] private float min;
    /// <summary>最大値</summary>
    [SerializeField] private float max;

    [SerializeField] private float initValue;

    /// <summary>表示桁数</summary>
    [SerializeField] private int digits;

    /// <summary>int型にするか？</summary>
    [SerializeField] private bool isInt;

    /// <summary>値</summary>
    public float Value => slider.value;

    /// <summary>int型の値</summary>
    public int IntValue => slider.value.RoundToInt();

    private void Start()
    {
        fieldTMP.text = fieldName;                        //フィールド名を代入

        slider.minValue = isInt ? min.RoundToInt() : min; //スライダーの最小値を代入
        slider.maxValue = isInt ? max.RoundToInt() : max; //スライダーの最大値を代入

        slider.onValueChanged.AddListener(OnValueChanged);//スライダーの値が変更された際、実行する関数を追加

        slider.value = Mathf.Clamp(initValue, min, max); //スライダーの値を設定
    }

    /// <summary>スライダーの値が変更された際、実行する</summary>
    /// <param name="value">スライダーの値</param>
    private void OnValueChanged(float value)
    {
        if (isInt) slider.value = value.RoundToInt();                                 //int型なら/スライダーの値をintに変換

        valueTMP.text = (isInt ? value.RoundToInt() : value.Round(digits)).ToString();//値を表示
    }
}
