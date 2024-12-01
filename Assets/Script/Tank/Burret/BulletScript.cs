using UnityEngine;
using System.Collections;

/// <summary>タンクの弾</summary>
public class BulletScript : MonoBehaviour
{
    [SerializeField, LightColor] private BulletEffectScript effect;

    [SerializeField, LightColor] private BulletReflectScript reflect;

    /// <summary>全体オブジェクト</summary>
    [SerializeField, LightColor] private GameObject obj;

    /// <summary>弾オブジェクト</summary>
    [SerializeField, LightColor] private GameObject bodyObj;

    /// <summary>全体トランスフォーム</summary>
    [SerializeField, LightColor] private Transform trafo;

    /// <summary>Rigidbody</summary>
    [SerializeField, LightColor] private Rigidbody2D ribo;

    /// <summary>塗りつぶすスプライトレンダラー配列</summary>
    [SerializeField, LightColor] private SpriteRenderer[] fillSprites;

    /// <summary>弾の情報</summary>
    private BulletInfo Info;

    /// <summary>アクティブか</summary>
    public bool IsActive
    {
        get => obj.activeSelf;
        private set => obj.SetActive(value);
    }

    /// <summary>非アクティブ</summary>
    public void Inactive()
    {
        IsActive = false;              //非アクティブ

        effect.IsActive = false;       //爆発エフェクトオブジェクトを非アクティブ

        bodyObj.SetActive(true);       //弾オブジェクトをアクティブ

        reflect.ReSetReflectionCount();//
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (!Info.IsDamage(hit)) return;             //ダメージを受けなければ/終了

        if (reflect.IsReflection(hit))               //反射できるなら
        {
            ribo.velocity = reflect.Reflection(ribo);//

            trafo.up = ribo.velocity.normalized;     //移動ベクトルを代入

            return;                                  //終了
        }

        StartCoroutine(ExplosionEffect());           //爆発エフェクトを開始
    }
    /// <summary>非アクティブにするする際のエフェクト</summary>
    private IEnumerator ExplosionEffect()
    {
        ribo.velocity = Vector2.zero;         //移動ベクトルを(0, 0)にする

        bodyObj.SetActive(false);             //弾オブジェクトを非アクティブ

        yield return effect.ExplosionEffect();//爆発エフェクトを表示

        Inactive();                           //非アクティブ
    }

    /// <summary>弾を発射</summary>
    /// <param name="info">発射する際の情報</param>
    public void Shoot(BulletInfo info)
    {
        Init(info);                           //

        IsActive = true;                      //アクティブ

        ribo.velocity = trafo.up * info.Speed;//方向 * 弾速を代入
    }

    /// <summary></summary>
    /// <param name="info"></param>
    private void Init(BulletInfo info)
    {
        Info = info;                    //情報を代入

        bodyObj.tag = info.Tag;         //タグを代入

        effect.JetSetActive(info.Speed);//ジェットエフェクトのアクティブ状態をセット

        reflect.Init(info);             //

        var t = info.GenerateInfo;      //初期位置と角度

        trafo.position = t.position;    //位置を代入
        trafo.rotation = t.rotation;    //角度を代入

        foreach (var s in fillSprites)  //FillSprites分繰り返す
        {
            s.color = info.Color;       //弾の色を代入
        }
    }
}
