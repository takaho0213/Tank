using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary><see cref="ColorFieldScript"/>のボタン</summary>
public class ColorButtonScript : MonoBehaviour
{
    /// <summary>ボタン</summary>
    [SerializeField] private Button button;

    /// <summary>カラーとカラー名を表示するTMP</summary>
    [SerializeField] private TextMeshProUGUI colorTMP;

    /// <summary>カラー</summary>
    [SerializeField] private Color color;

    /// <summary>カラー名</summary>
    [SerializeField] private string colorName;

    /// <summary>ボタンがクリックされた際、実行する関数</summary>
    private UnityAction<Color> onClick;

    /// <summary>初期化</summary>
    /// <param name="onClick">ボタンがクリックされた際、実行する関数</param>
    public void Init(UnityAction<Color> onClick)
    {
        colorTMP.text = colorName;          //カラー名を代入
        colorTMP.color = color;             //カラーを代入

        this.onClick = onClick;             //ボタンがクリックされた際、実行する関数を代入

        button.onClick.AddListener(OnClick);//ボタンがクリックされた際、実行する関数を追加
    }

    /// <summary>ボタンがクリックされた際、実行する関数</summary>
    public void OnClick()
    {
        onClick.Invoke(color);//クリックされた際実行する関数を実行
    }
}
