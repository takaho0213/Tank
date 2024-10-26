using UnityEngine;
using System.Linq;
using System.Collections;

/// <summary>タンクの弾</summary>
public class BulletScript : MonoBehaviour
{
    /// <summary>全体オブジェクト</summary>
    [SerializeField, LightColor] private GameObject Obj;

    /// <summary>弾オブジェクト</summary>
    [SerializeField, LightColor] private GameObject BodyObj;

    /// <summary>爆発エフェクトオブジェクト</summary>
    [SerializeField, LightColor] private GameObject ExplosionObj;

    /// <summary>ジェットエフェクトオブジェクト</summary>
    [SerializeField, LightColor] private GameObject JetObj;

    /// <summary>全体トランスフォーム</summary>
    [SerializeField, LightColor] private Transform Trafo;

    /// <summary>Rigidbody</summary>
    [SerializeField, LightColor] private Rigidbody2D Ribo;

    /// <summary>コライダー</summary>
    [SerializeField, LightColor] private CapsuleCollider2D Coll;

    /// <summary>反射AudioSource</summary>
    [SerializeField, LightColor] private AudioSource ReflectionSource;

    /// <summary>爆発AudioSource</summary>
    [SerializeField, LightColor] private AudioSource ExplosionSource;

    /// <summary>塗りつぶすスプライトレンダラー配列</summary>
    [SerializeField, LightColor] private SpriteRenderer[] FillSprites;

    /// <summary>ジェットエフェクトを出現させる速度</summary>
    [SerializeField] private float JetEffectSpeed;

    /// <summary>レイが検知するレイヤー</summary>
    [SerializeField] private LayerMask Layer;

    /// <summary>停止時間</summary>
    private WaitForSeconds Wait;

    /// <summary>弾の情報</summary>
    private BulletInfo Info;

    /// <summary>現在の反射数</summary>
    private int ReflectionCount;

    /// <summary>アクティブか</summary>
    public bool IsActive
    {
        get => Obj.activeSelf;
        private set => Obj.SetActive(value);
    }

    /// <summary>非アクティブ</summary>
    public void Inactive()
    {
        ExplosionObj.SetActive(false);//爆発エフェクトオブジェクトを非アクティブ

        BodyObj.SetActive(true);      //弾オブジェクトをアクティブ

        IsActive = false;             //非アクティブ

        Coll.enabled = true;          //コライダーをオン

        ReflectionCount = default;    //反射数をリセット
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (!Info.IsDamage(hit)) return;                                                         //ダメージを受けなければ/終了

        if (Info.IsReflection(hit, ReflectionCount))                                             //反射できるなら
        {
            Ribo.velocity = Vector2.Reflect(Ribo.velocity, RayNormalVector());                   //反射角を代入

            Trafo.up = Ribo.velocity.normalized;                                                 //移動ベクトルを代入

            ReflectionCount++;                                                                   //反射数を + 1

            AudioScript.I.BulletAudio.Dictionary[BulletClip.Reflection].PlayOneShot(ReflectionSource);//反射SEを再生

            return;                                                                              //終了
        }

        StartCoroutine(ExplosionEffect());                                                       //爆発エフェクトを開始
    }

    /// <summary>レイを発射し法線ベクトルを返す</summary>
    /// <returns>法線ベクトル</returns>
    private Vector2 RayNormalVector()
    {
        var size = Coll.size * Trafo.localScale;                                                    //サイズ

        var type = CapsuleDirection2D.Vertical;                                                     //コライダーのタイプ

        var angle = Coll.transform.eulerAngles.z;                                                   //角度

        var dis = Coll.size.y;                                                                      //方向

        return Physics2D.CapsuleCast(Ribo.position, size, type, angle, Trafo.up, dis, Layer).normal;//Rayを発射し法制ベクトルを返す
    }

    /// <summary>非アクティブにするする際のエフェクト</summary>
    private IEnumerator ExplosionEffect()
    {
        Ribo.velocity = Vector2.zero;                                 //移動ベクトルを(0, 0)にする

        Coll.enabled = false;                                         //コライダーをオフ

        BodyObj.SetActive(false);                                   //弾オブジェクトを非アクティブ

        ExplosionObj.SetActive(true);                                 //爆発エフェクトオブジェクトをアクティブ

        var c = AudioScript.I.BulletAudio.Dictionary[BulletClip.Explosion];//爆発SEクリップ

        c.PlayOneShot(ExplosionSource);                               //爆発SEを再生

        yield return Wait ??= new WaitForSeconds(c.Clip.length);      //SEの秒数分停止

        Inactive();                                                   //非アクティブ
    }

    /// <summary>弾を発射</summary>
    /// <param name="info">発射する際の情報</param>
    public void Shoot(BulletInfo info)
    {
        IsActive = true;                               //アクティブ

        Info = info;                                   //情報を代入

        tag = info.Tag;                                //タグを代入

        JetObj.SetActive(info.Speed >= JetEffectSpeed);//弾速がジェットエフェクトを出現させる速度以上ならジェットエフェクトをアクティブ

        var trafo = info.GenerateInfo;                 //初期位置と角度

        Trafo.position = trafo.position;               //位置を代入
        Trafo.rotation = trafo.rotation;               //角度を代入

        Ribo.velocity = Trafo.up * info.Speed;         //方向 * 弾速を代入

        foreach (var s in FillSprites)                 //FillSprites分繰り返す
        {
            s.color = info.Color;                      //弾の色を代入
        }
    }
}

/// <summary>タンクの弾の情報</summary>
[System.Serializable]
public class BulletInfo
{
    /// <summary>初期位置と角度</summary>
    [field: SerializeField, LightColor] public Transform GenerateInfo { get; private set; }

    /// <summary>弾速</summary>
    [field: SerializeField] public float Speed { get; private set; }

    /// <summary>最大反射数</summary>
    [SerializeField] private int MaxReflectionCount;

    /// <summary>色</summary>
    [field: SerializeField] public Color Color { get; private set; }

    /// <summary>タグ</summary>
    [field: SerializeField, Tag] public string Tag { get; private set; }

    /// <summary>反射しないタグ</summary>
    [SerializeField, Tag] private string[] NoReflectionTags;
    /// <summary>ダメージを受けないタグ</summary>
    [SerializeField, Tag] private string[] NoDamageTags;

    /// <summary>弾速と最大反射数をセット</summary>
    /// <param name="speed">弾速</param>
    /// <param name="count">最大反射数</param>
    /// <param name="color">弾の色</param>
    public void Init(float speed, int count, Color color)
    {
        Speed = speed;
        MaxReflectionCount = count;
        Color = color;
    }

    /// <summary>ダメージを受けるか</summary>
    /// <param name="hit">接触したコライダー</param>
    /// <returns>ダメージを受けるか</returns>
    public bool IsDamage(Collider2D hit) => !NoDamageTags.Any((v) => hit.CompareTag(v));//ダメージを受けないタグが含まれていないか

    /// <summary>反射できるか</summary>
    /// <param name="hit">接触したコライダー</param>
    /// <param name="count">現在の反射数</param>
    /// <returns>反射できるか</returns>
    public bool IsReflection(Collider2D hit, int count)
    {
        bool isReflection = MaxReflectionCount > count;                      //反射できるか

        bool isNoReflection = NoReflectionTags.Any((v) => hit.CompareTag(v));//反射できないか

        return isReflection && !isNoReflection;                              //(反射できる かつ 反射できる)を返す
    }
}
