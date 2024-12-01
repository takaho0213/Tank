using UnityEngine;
using UnityEngine.UI;

public class TankDamageUIScript : MonoBehaviour
{
    /// <summary>赤の体力ゲージ</summary>
    [SerializeField, LightColor] private Image redHealthGauge;

    /// <summary>緑の体力ゲージ</summary>
    [SerializeField, LightColor] private Image greenHealthGauge;

    /// <summary>赤体力ゲージの補間値</summary>
    [SerializeField, Range01] private float redHealthBarLerp;

    public void SetHealthGauge(float v)
    {
        v = Mathf.Clamp01(v);

        greenHealthGauge.fillAmount = v;                                                       //緑ゲージを更新

        redHealthGauge.fillAmount = Mathf.Lerp(redHealthGauge.fillAmount, v, redHealthBarLerp);//赤ゲージを補間値で更新
    }
}