using UnityEngine;

/// <summary>ターゲットを探す</summary>
public class TankTargetSearchScript : MonoBehaviour
{
    /// <summary>ターゲットのコライダー</summary>
    [SerializeField, LightColor] private Collider2D targetColl;

    /// <summary>発射するレイの形</summary>
    [SerializeField, LightColor] private CapsuleCollider2D rayColl;

    /// <summary>探す際回転させるトランスフォーム</summary>
    [SerializeField, LightColor] private Transform trafo;

    /// <summary>回転させる方向</summary>
    [SerializeField] private Vector3 angle;

    /// <summary>レイが検知するレイヤー</summary>
    [SerializeField] private LayerMask rayLayer;

    /// <summary>プレイヤーのレイヤー</summary>
    [SerializeField] private LayerMask playerLayer;

    /// <summary>ラインの幅</summary>
    [SerializeField] private float lineWidth;

    /// <summary>発射するレイの形のトランスフォーム</summary>
    private Transform rayCollTrafo;

    /// <summary>カプセル型のRayを発射</summary>
    /// <param name="o">発射する座標</param>
    /// <param name="d">発射するベクトル</param>
    /// <param name="l">レイヤー</param>
    /// <returns>レイの情報</returns>
    private RaycastHit2D CapsuleRayCast(Vector2 o, Vector2 d, LayerMask l)
    {
        rayCollTrafo ??= rayColl.transform;                                          //nullなら代入

        var size = rayColl.size * rayCollTrafo.localScale;                           //サイズ

        var type = CapsuleDirection2D.Vertical;                                      //方向

        var angle = rayCollTrafo.eulerAngles.z;                                      //角度

        var ray = Physics2D.CapsuleCast(o, size, type, angle, d, Mathf.Infinity, l); //レイ

        return ray;                                                                  //レイの情報を返す
    }

    /// <summary>カプセル型のRayを発射</summary>
    /// <param name="target">ターゲット座標または方向</param>
    /// <param name="hit">接触したオブジェクトの情報</param>
    /// <returns>ヒットしたか</returns>
    public bool Ray(Vector3 target)
    {
        var pos = trafo.position;                             //自身の位置

        var hit = CapsuleRayCast(pos, target - pos, rayLayer);//カプセル型のレイを発射

        return hit && hit.collider == targetColl;             //ヒットしたか
    }

    /// <summary>サーチする角度を変更</summary>
    /// <returns>角度を返す</returns>
    public Vector2 Rotate()
    {
        trafo.Rotate(angle);//回転

        return trafo.up;    //upを返す
    }

    /// <summary>反射するカプセル型のRayを発射</summary>
    /// <returns>反射先にヒットしたら ? 一度目のレイの方向 : (0, 0)</returns>
    public bool ReflectionRay()
    {
        var pos = trafo.position;                                                              //自身の位置

        var d = trafo.up;                                                                      //レイの方向

        var ray1 = CapsuleRayCast(pos, d, rayLayer);                                           //レイの情報

        var ray2 = CapsuleRayCast(ray1.centroid, Vector2.Reflect(d, ray1.normal), playerLayer);//ヒットした場所からレイを発射しヒットしたか

        return ray2 && ray2.collider == targetColl;                                            //ヒットしたら ? 一度目のレイの方向 : (0, 0)
    }
}
