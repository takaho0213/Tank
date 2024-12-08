using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectFieldScript<T> : MonoBehaviour//IsNormalizedFieldScript
{
    [SerializeField] protected SelectButton<T>[] buttons;

    [SerializeField] protected TextMeshProUGUI fieldTMP;

    [SerializeField] protected string fieldName;

    [SerializeField] protected Color onColor;
    [SerializeField] protected Color offColor;

    protected SelectButton<T> current;

    public T Value => current.Value;

    protected void Start()
    {
        fieldTMP.text = fieldName;

        UnityAction<SelectButton<T>> c = OnClick;

        foreach (var b in buttons)
        {
            b.SetOnClick(c);

            b.SetTextColor = offColor;
        }

        OnClick(current = buttons[default]);
    }

    protected void OnClick(SelectButton<T> button)
    {
        current.SetTextColor = offColor;

        current = button;

        current.SetTextColor = onColor;
    }
}

[System.Serializable]
public class SelectButton<T> : MonoBehaviour
{
    [SerializeField] protected Button button;

    [SerializeField] protected TextMeshProUGUI buttonTMP;

    [SerializeField] protected T value;

    [SerializeField] protected string buttonName;

    private UnityAction<SelectButton<T>> onClick;

    public T Value => value;

    public Color SetTextColor { set => buttonTMP.color = value; }

    public void SetOnClick(UnityAction<SelectButton<T>> onClick)
    {
        this.onClick = onClick;

        button.onClick.AddListener(OnClick);

        buttonTMP.text = buttonName;
    }

    public void OnClick()
    {
        onClick.Invoke(this);
    }
}