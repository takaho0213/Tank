using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>選択させるフィールド</summary>
public class SelectFieldScript<T> : MonoBehaviour
{
    /// <summary>ボタン配列</summary>
    [SerializeField] protected SelectButtonScript<T>[] buttons;

    [SerializeField] protected SelectButtonScript<T> initButton;

    /// <summary>フィールド名を表示するTMP</summary>
    [SerializeField] protected TextMeshProUGUI fieldTMP;

    /// <summary>フィールド名</summary>
    [SerializeField] protected string fieldName;

    /// <summary>オンの際のColor</summary>
    [SerializeField] protected Color onColor;
    /// <summary>オフの際のColor</summary>
    [SerializeField] protected Color offColor;

    /// <summary>現在選択されているボタン</summary>
    protected SelectButtonScript<T> current;

    /// <summary>値</summary>
    public T Value => current.Value;

    protected void Start()
    {
        fieldTMP.text = fieldName;               //フィールド名を代入

        UnityAction<SelectButtonScript<T>> c = OnClick;//クリックされた際実行する関数

        foreach (var b in buttons)               //ボタン配列の要素数分繰り返す
        {
            b.SetOnClick(c);                     //クリックされた際実行する関数をセット

            b.SetTextColor = offColor;           //オフの際のColorを代入
        }

        OnClick(current = initButton);     //現在のボタンをセット
    }

    /// <summary></summary>
    /// <param name="button"></param>
    protected void OnClick(SelectButtonScript<T> button)
    {
        current.SetTextColor = offColor;//オフのColorを代入

        current = button;               //現在のボタンをセット

        current.SetTextColor = onColor; //オンのColorをセット
    }
}

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