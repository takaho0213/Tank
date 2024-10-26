using UnityEngine;
using System.Collections;

public class TankFillColorScript : MonoBehaviour
{
    /// <summary>色を変えるスプライト配列</summary>
    [SerializeField, LightColor] private SpriteRenderer[] fillSprites;

    /// <summary>タンクの色</summary>
    [SerializeField] private Color fillColor;

    /// <summary>待機時間</summary>
    private WaitForSeconds wait;

    /// <summary>色</summary>
    public Color Color => fillColor;

    /// <summary>エネミーの情報をセット</summary>
    /// <param name="i">エネミーの情報</param>
    public void SetInfo(TankEnemyInfoScript i) => SetColor(fillColor = i.FillColor);

    /// <summary>色をグレーにセット</summary>
    public void SetGray() => SetColor(Color.gray);

    /// <summary>色をセット</summary>
    public void SetColor() => SetColor(fillColor);

    /// <summary>色をセット</summary>
    /// <param name="color">セットする色</param>
    public void SetColor(Color color)
    {
        foreach (var s in fillSprites)//FillSpritesの要素数分繰り返す
        {
            s.color = color;          //引数の値を代入
        }
    }

    /// <summary>ダメージ演出</summary>
    /// <param name="waitTime">待機時間</param>
    public IEnumerator DamageColor(float waitTime)
    {
        SetGray();                                         //色をグレーにする

        yield return wait ??= new WaitForSeconds(waitTime);//待機

        SetColor();                                        //色をセット
    }
}
