using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>Colorを設定するフィールド</summary>
public class ColorFieldScript : MonoBehaviour
{
    /// <summary>Colorを表示するGraphic</summary>
    [SerializeField] private Graphic graphic;

    /// <summary>ColorのR値を設定するスライダー</summary>
    [SerializeField] private Slider rSlider;
    /// <summary>ColorのG値を設定するスライダー</summary>
    [SerializeField] private Slider gSlider;
    /// <summary>ColorのB値を設定するスライダー</summary>
    [SerializeField] private Slider bSlider;

    /// <summary>フィールド名を表示するTMP</summary>
    [SerializeField] private TextMeshProUGUI fieldTMP;

    /// <summary>フィールド名</summary>
    [SerializeField] private string fieldName;

    /// <summary>ボタン配列</summary>
    [SerializeField] private ColorButtonScript[] buttons;

    [SerializeField] private ColorButtonScript initButton;

    /// <summary>値</summary>
    public Color Value => graphic.color;

    private void Start()
    {
        fieldTMP.text = fieldName;                         //フィールド名を代入

        rSlider.onValueChanged.AddListener(OnValueChanged);//スライダーの値が変更された際、実行する関数を追加
        gSlider.onValueChanged.AddListener(OnValueChanged);
        bSlider.onValueChanged.AddListener(OnValueChanged);

        OnValueChanged(default);                           //Colorを表示するGraphicにスライダーの値をセット

        UnityAction<Color> c = OnClick;                    //ボタンがクリックされた際実行するする関数

        foreach (var b in buttons)
        {
            b.Init(c);                                     //ボタンを初期化
        }

        initButton.OnClick();
    }

    /// <summary>Colorを表示するGraphicにスライダーの値をセット</summary>
    /// <param name="value">スライダーの値</param>
    private void OnValueChanged(float value)
    {
        var color = graphic.color;//現在のColorを代入

        color.r = rSlider.value;  //R値を代入
        color.g = gSlider.value;  //G値を代入
        color.b = bSlider.value;  //B値を代入

        graphic.color = color;    //値を代入
    }

    /// <summary>ボタンがクリックされた際実行するする関数</summary>
    /// <param name="color">ボタンのColor</param>
    private void OnClick(Color color)
    {
        rSlider.value = color.r;//R値を代入
        gSlider.value = color.g;//G値を代入
        bSlider.value = color.b;//B値を代入

        OnValueChanged(default);//Colorを表示するGraphicにスライダーの色をセット
    }
}