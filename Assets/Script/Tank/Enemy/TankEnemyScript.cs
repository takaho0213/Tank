using UnityEngine;
using System.Collections;

/// <summary>エネミーのタンク</summary>
public class TankEnemyScript : TankScript
{
    /// <summary>攻撃タイプ</summary>
    public enum AttackType
    {
        /// <summary>ノーマル</summary>
        Normal,
        /// <summary>偏差撃ち</summary>
        Deviation,
        /// <summary>射線にターゲットがいるなら</summary>
        Target,
        /// <summary>射線反射先にターゲットがいるなら</summary>
        TargetReflection,
        /// <summary>ランダム</summary>
        Random,
    }

    /// <summary>自動移動</summary>
    [SerializeField, LightColor] private TankAutoMoveScript autoMove;

    /// <summary>ターゲット</summary>
    [SerializeField, LightColor] private TankTargetPredictionScript prediction;

    /// <summary>サーチ</summary>
    [SerializeField, LightColor] private TankTargetSearchScript search;

    /// <summary>攻撃タイプ</summary>
    [SerializeField] private AttackType attackType;

    /// <summary>AttackType配列</summary>
    private readonly AttackType[] AttackTypes = (AttackType[])System.Enum.GetValues(typeof(AttackType));

    /// <summary>個体値をセット</summary>
    /// <param name="info">個体情報</param>
    /// <param name="onDeath">死亡時に実行するコールバック</param>
    public void SetInfo(TankEnemyInfoScript info, System.Func<BulletScript> pool, System.Func<IEnumerator> onDeath)
    {
        autoMove.Stop();                                  //移動を停止

        cannon.BulletPool = pool;                         //弾プール関数をセット

        parts.SetInfo(info);                              //エネミーの情報をセット

        damage.SetInfo(info);                             //エネミーの情報をセット

        fillColor.SetInfo(info);                          //エネミーの情報をセット

        line.SetLineColor(info.FillColor);                //エネミーの情報をセット

        cannon.SetInfo(info);                             //エネミーの情報をセット

        autoMove.SetInfo(info);                           //エネミーの情報をセット

        line.Crear();                                     //弾道をリセット

        SetPosAndRot(info.PosAndRot);                     //場所と角度をセット

        shootInterval.Time = info.ShootInterval;          //発射間隔をセット

        attackType = info.AttackType;                           //攻撃タイプをセット

        base.onDeath = () =>                                   //死亡した再実行する関数をセット
        {
            IsActive = false;                             //非アクティブ

            stageManager.StartCoroutine(onDeath.Invoke());//死亡時の処理を開始
        };

        IsActive = true;                                  //アクティブ

        while (attackType == AttackType.Random)                                 //攻撃タイプがランダムな限り繰り返す
        {
            attackType = AttackTypes[Random.Range(default, AttackTypes.Length)];//ランダムにセット
        }
    }

    /// <summary>タレットをターゲットの方向に向ける</summary>
    private void LookAt()
    {
        Vector3 pos = attackType != AttackType.Deviation ? prediction.TargetPos : prediction.PredictionPos(autoMove.Pos, cannon.BulletSpeed);//攻撃タイプが偏差撃ちじゃなければ ? 現在のターゲットの位置 : ターゲットの移動予測先

        parts.TargetLookAt(pos - autoMove.Pos);                                                                          //タレットを(ターゲットの位置 - 自身の現在位置)の方向に向ける
    }

    /// <summary>レイがヒット かつ ターゲットと一致か</summary>
    private bool IsSearchRayHit() => search.Ray(prediction.TargetPos, out var r) && r.transform.position == prediction.TargetPos;//レイがヒット かつ ターゲットと一致なら

    /// <summary>処理をまとめた関数</summary>
    private void Move()
    {
        var isTargetType = attackType == AttackType.Target;                       //攻撃タイプがターゲットか

        var isTargetRType = attackType == AttackType.TargetReflection;            //攻撃タイプがターゲット反射か

        var isTarget = (isTargetType || isTargetRType) && !IsSearchRayHit();//(攻撃タイプがターゲット or 反射) かつ レイがヒット かつ ターゲットと一致なら

        line.CreateLine();

        if (!isTargetRType) LookAt();                                       //攻撃タイプがターゲット反射じゃなければ/タレットをターゲットの方向に向ける

        if (isTarget)                                                       //レイにターゲットがヒットしていたら
        {
            if (isTargetRType)         //攻撃タイプがターゲット反射 かつ 反射先でターゲットがヒットしたら
            {
                var ray = search.ReflectionRay();                      //反射先の結果

                var targetPos = ray ? ray.transform.position : default;//rayが当たっていたら ? 当たったオブジェクトの座標 : (0, 0, 0)

                parts.TargetLookAt(search.SearchRot);                  //

                if (targetPos != prediction.TargetPos) return;
            }
            else return;                                                    //それ以外なら/終了
        }
        else if (isTargetRType) LookAt();                                   //それ以外で攻撃タイプがターゲット反射なら/タレットをターゲットの方向へ向ける

        if (shootInterval)                                                  //発射間隔を越えていたら
        {
            cannon.Shoot();                                                 //弾を発射

            autoMove.VectorChange();                                        //移動ベクトルを変更
        }

        prediction.UpdatePreviousPos();                                                //ターゲット位置を更新
    }

    private void FixedUpdate()
    {
        autoMove.MoveSE();                         //移動SEを再生

        damage.UpdateHealthGauge();                //体力UIを更新

        parts.CaterpillarLookAt(autoMove.Velocity);//キャタピラを回転

        if (IsNotMove)                             //移動できない状態なら
        {
            shootInterval.ReSet();                 //インターバルをリセット

            autoMove.Stop();                       //移動を停止

            return;                                //終了
        }

        Move();                                    //処理をまとめた関数を呼び出す
    }
}

/// <summary>タンクのターゲットを探すクラス</summary>
[System.Serializable]
public class TankSearch
{
    [SerializeField, LightColor] private LineRenderer lineRenderer;

    /// <summary>発射するレイの形</summary>
    [SerializeField, LightColor] private CapsuleCollider2D RayColl;

    /// <summary>探す際回転させるトランスフォーム</summary>
    [SerializeField, LightColor] private Transform Trafo;

    /// <summary>回転させる方向</summary>
    [SerializeField] private Vector3 Angle;

    /// <summary>レイが検知するレイヤー</summary>
    [SerializeField] private LayerMask RayLayer;

    /// <summary>プレイヤーのレイヤー</summary>
    [SerializeField] private LayerMask PlayerLayer;

    [SerializeField] private float lineWidth;

    /// <summary>発射するレイの形のトランスフォーム</summary>
    private Transform RayCollTrafo;

    public Vector2 SearchRot => Trafo.up;

    /// <summary>カプセル型のRayを発射</summary>
    /// <param name="o">発射する座標</param>
    /// <param name="d">発射するベクトル</param>
    /// <param name="l">レイヤー</param>
    /// <returns>レイの情報</returns>
    private RaycastHit2D CapsuleRayCast(Vector2 o, Vector2 d, LayerMask l)
    {
        RayCollTrafo ??= RayColl.transform;                                          //nullなら代入

        var size = RayColl.size * RayCollTrafo.localScale;                           //サイズ

        var type = CapsuleDirection2D.Vertical;                                      //方向

        var angle = RayCollTrafo.eulerAngles.z;                                      //角度

        var ray = Physics2D.CapsuleCast(o, size, type, angle, d, Mathf.Infinity, l); //レイ

        //if (ray) Debug.DrawRay(o, ray.centroid - o, Color.green);                    //レイがヒットしたらレイを可視化

        return ray;                                                                  //レイの情報を返す
    }

    /// <summary>カプセル型のRayを発射</summary>
    /// <param name="target">ターゲット座標または方向</param>
    /// <param name="hit">接触したオブジェクトの情報</param>
    /// <returns>ヒットしたか</returns>
    public bool Ray(Vector3 target, out RaycastHit2D hit)
    {
        var pos = Trafo.position;                         //自身の位置

        hit = CapsuleRayCast(pos, target - pos, RayLayer);//

        return hit;                                             //ヒットしたか
    }

    /// <summary>反射するカプセル型のRayを発射</summary>
    /// <returns>反射先にヒットしたら ? 一度目のレイの方向 : (0, 0)</returns>
    public RaycastHit2D ReflectionRay()
    {
        Trafo.Rotate(Angle);         //回転

        var pos = Trafo.position;                         //自身の位置

        var d = Trafo.up;                                      //レイの方向

        var ray1 = CapsuleRayCast(pos, d, RayLayer);//レイの情報

        var ray2 = CapsuleRayCast(ray1.centroid, Vector2.Reflect(d, ray1.normal), PlayerLayer);//ヒットした場所からレイを発射しヒットしたか

        //const int LineCount = 3;

        //if (lineRenderer.positionCount >= LineCount)
        //{
        //    lineRenderer.positionCount = LineCount;
        //}

        //int lineCount = default;                                //

        //lineRenderer.SetPosition(lineCount++, pos);             //

        //lineRenderer.SetPosition(lineCount++, ray1.centroid);    //

        //lineRenderer.SetPosition(lineCount++, ray2 ? ray2.centroid : ray1.centroid);    //

        return ray2;                   //ヒットしたら ? 一度目のレイの方向 : (0, 0)
    }
}
