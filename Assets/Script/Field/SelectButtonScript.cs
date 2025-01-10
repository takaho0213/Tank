using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>選択ボタン</summary>
[System.Serializable]
public class SelectButtonScript<T> : MonoBehaviour
{
    /// <summary>ボタン</summary>
    [SerializeField] protected Button button;

    /// <summary>ボタンの文字列を表示するTMP</summary>
    [SerializeField] protected TextMeshProUGUI buttonTMP;

    /// <summary>値</summary>
    [SerializeField] protected T value;

    /// <summary>ボタンの名前</summary>
    [SerializeField] protected string buttonName;

    /// <summary>クリックされた際実行する関数</summary>
    private UnityAction<SelectButtonScript<T>> onClick;

    /// <summary>値</summary>
    public T Value => value;

    /// <summary>ボタンテキストのColorをセット</summary>
    public Color SetTextColor { set => buttonTMP.color = value; }

    /// <summary>クリックされた際実行する関数をセット</summary>
    /// <param name="onClick">クリックされた際実行する関数</param>
    public void SetOnClick(UnityAction<SelectButtonScript<T>> onClick)
    {
        this.onClick = onClick;             //クリックされた際実行する関数を代入

        button.onClick.AddListener(OnClick);//クリックされた際実行する関数を追加

        buttonTMP.text = buttonName;        //ボタン名を代入
    }

    /// <summary>クリックされた際実行する関数</summary>
    public void OnClick()
    {
        onClick.Invoke(this);//クリックされた際実行する関数を実行
    }
}