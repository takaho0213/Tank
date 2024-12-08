using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorButtonScript : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI colorTMP;

    [SerializeField] private Color color;

    [SerializeField] private string colorName;

    private UnityAction<Color> onClick;

    public void Init(UnityAction<Color> onClick)
    {
        colorTMP.text = colorName;
        colorTMP.color = color;

        this.onClick = onClick;

        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        onClick.Invoke(color);
    }
}
