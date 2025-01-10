using System.Linq;
using UnityEngine;

/// <summary>弾の反射</summary>
public class BulletReflectScript : MonoBehaviour
{
    /// <summary>コライダー</summary>
    [SerializeField, LightColor] private CapsuleCollider2D Coll;

    /// <summary>反射AudioSource</summary>
    [SerializeField, LightColor] private AudioSource reflectSource;

    /// <summary>レイが検知するレイヤー</summary>
    [SerializeField] private LayerMask reflectLayer;

    /// <summary>反射しないタグ配列</summary>
    private string[] noReflectTags;

    /// <summary>最大反射数</summary>
    private int maxReflectCount;

    /// <summary>現在の反射数</summary>
    private int reflectCount;

    /// <summary>反射SE</summary>
    private ClipInfo reflectSE;

    /// <summary>初期化</summary>
    /// <param name="info">弾の情報</param>
    public void Init(BulletInfo info)
    {
        noReflectTags = info.NoReflectTags;    //反射しないタグ配列を代入

        maxReflectCount = info.MaxReflectCount;//反射数を代入
    }

    /// <summary>反射できるか</summary>
    /// <param name="hit">接触したコライダー</param>
    /// <returns>反射できるか</returns>
    public bool IsReflection(Collider2D hit)
    {
        bool isReflection = maxReflectCount > reflectCount;               //反射できるか

        bool isNoReflection = noReflectTags.Any((v) => hit.CompareTag(v));//反射できないか

        return isReflection && !isNoReflection;                           //反射できるかを返す
    }

    /// <summary>反射角を返す</summary>
    /// <param name="ribo">弾のRigidbody</param>
    /// <returns>反射角</returns>
    public Vector2 Reflection(Rigidbody2D ribo)
    {
        return Reflection(ribo.velocity, RayNormalVector(ribo.transform));//反射角を代入
    }

    /// <summary>反射角を返す</summary>
    /// <param name="velocity">移動ベクトル</param>
    /// <param name="normal">法線ベクトル</param>
    /// <returns>反射角</returns>
    private Vector2 Reflection(Vector2 velocity, Vector2 normal)
    {
        reflectSE ??= AudioScript.I.BulletAudio[BulletClip.Reflection];//反射SEクリップを代入

        reflectSE.PlayOneShot(reflectSource);                          //反射SEを再生

        reflectCount++;                                                //反射数を + 1

        return Vector2.Reflect(velocity, normal);                      //反射角を代入
    }

    /// <summary>レイを発射し法線ベクトルを返す</summary>
    /// <returns>法線ベクトル</returns>
    private Vector2 RayNormalVector(Transform trafo)
    {
        var size = Coll.size * trafo.localScale;                                                            //サイズ

        var type = CapsuleDirection2D.Vertical;                                                             //コライダーのタイプ

        var angle = Coll.transform.eulerAngles.z;                                                           //角度

        var dis = Coll.size.y;                                                                              //方向

        return Physics2D.CapsuleCast(trafo.position, size, type, angle, trafo.up, dis, reflectLayer).normal;//Rayを発射し法制ベクトルを返す
    }

    /// <summary>反射数をリセット</summary>
    public void ReSetReflectionCount() => reflectCount = default;
}
