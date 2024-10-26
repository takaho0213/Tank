using UnityEngine;
using UnityEngine.UI;

public class FillAmountFaderScript : FaderScript
{
    /// <summary>フェードさせるイメージ</summary>
    [SerializeField] protected Image image;

    [SerializeField] protected bool isFade;

    public override float FadeValue
    {
        get => image.fillAmount;
        set
        {
            image.fillAmount = value;

            if (isFade)
            {
                image.color = image.color.SetAlpha(value);
            }
        }
    }
}
