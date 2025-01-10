using UnityEngine;
using UnityEngine.UI;

/// <summary>タンクのダメージ処理のUI</summary>
public class TankDamageUIScript : MonoBehaviour
{
    /// <summary>赤の体力ゲージ</summary>
    [SerializeField, LightColor] private Image redHealthGauge;

    /// <summary>緑の体力ゲージ</summary>
    [SerializeField, LightColor] private Image greenHealthGauge;

    /// <summary>赤体力ゲージの補間値</summary>
    [SerializeField, Range01] private float redHealthBarLerp;

    /// <summary>体力ゲージの値をセット</summary>
    /// <param name="v">割合</param>
    public void SetHealthGauge(float v)
    {
        v = Mathf.Clamp01(v);                                                                  //体力の割合

        greenHealthGauge.fillAmount = v;                                                       //緑ゲージを更新

        redHealthGauge.fillAmount = Mathf.Lerp(redHealthGauge.fillAmount, v, redHealthBarLerp);//赤ゲージを補間値で更新
    }
}