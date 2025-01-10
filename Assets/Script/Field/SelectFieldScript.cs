using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>選択させるフィールド</summary>
public class SelectFieldScript<T> : MonoBehaviour
{
    /// <summary>ボタン配列</summary>
    [SerializeField] protected SelectButtonScript<T>[] buttons;

    /// <summary>初期値のボタン</summary>
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
        fieldTMP.text = fieldName;                     //フィールド名を代入

        UnityAction<SelectButtonScript<T>> c = OnClick;//クリックされた際実行する関数

        foreach (var b in buttons)                     //ボタン配列の要素数分繰り返す
        {
            b.SetOnClick(c);                           //クリックされた際実行する関数をセット

            b.SetTextColor = offColor;                 //オフの際のColorを代入
        }

        OnClick(current = initButton);                 //現在のボタンをセット
    }

    /// <summary>クリックされた際の処理</summary>
    /// <param name="button">ボタン</param>
    protected void OnClick(SelectButtonScript<T> button)
    {
        current.SetTextColor = offColor;//オフのColorを代入

        current = button;               //現在のボタンをセット

        current.SetTextColor = onColor; //オンのColorをセット
    }
}
