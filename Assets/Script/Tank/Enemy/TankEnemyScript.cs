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
        Reflection,
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

    protected override TankMoveScript BaseMove => autoMove;

    /// <summary>個体値をセット</summary>
    /// <param name="info">個体情報</param>
    /// <param name="onDeath">死亡時に実行するコールバック</param>
    public void SetInfo(TankEnemyInfoScript info, System.Func<BulletScript> pool, System.Func<IEnumerator> onDeath)
    {
        IsActive = true;                                  //アクティブ

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

        cannon.ShootIntervalTime = info.ShootInterval;          //発射間隔をセット

        attackType = info.AttackType;                           //攻撃タイプをセット

        base.onDeath = () =>                                   //死亡した再実行する関数をセット
        {
            IsActive = false;                             //非アクティブ

            stageSystem.StartCoroutine(onDeath.Invoke());//死亡時の処理を開始
        };

        while (attackType == AttackType.Random)                                 //攻撃タイプがランダムな限り繰り返す
        {
            attackType = EnumEx<AttackType>.Values.Random();
        }
    }

    /// <summary>タレットをターゲットの方向に向ける</summary>
    private void LookAt()
    {
        parts.TargetLookAt(prediction.TargetPos - autoMove.Pos);//タレットを(ターゲットの位置 - 自身の現在位置)の方向に向ける
    }

    /// <summary>レイがヒット かつ ターゲットと一致か</summary>
    private bool Ray() => search.Ray(prediction.TargetPos);//レイがヒット かつ ターゲットと一致なら

    private bool AttackNormal()
    {
        LookAt();

        return true;
    }

    private bool AttackDeviation()
    {
        Vector2 pos = prediction.PredictionPos(autoMove.Pos, cannon.BulletSpeed);//

        parts.TargetLookAt(pos - autoMove.Pos);                                  //

        return true;
    }

    private bool AttackTarget()
    {
        LookAt();    //

        return Ray();//
    }

    private bool AttackReflection()
    {
        if (Ray())                          //
        {
            LookAt();                       //

            return true;                    //
        }

        parts.TargetLookAt(search.Rotate());//

        return search.ReflectionRay();      //
    }

    private bool AttackMove() => attackType switch
    {
        AttackType.Normal     => AttackNormal(),
        AttackType.Deviation  => AttackDeviation(),
        AttackType.Target     => AttackTarget(),
        AttackType.Reflection => AttackReflection(),
        _ => default,
    };

    /// <summary>処理をまとめた関数</summary>
    protected override void Move()
    {
        if (AttackMove() && cannon.IsShoot)//発射間隔を越えていたら
        {
            cannon.Shoot();                //弾を発射

            autoMove.VectorChange();       //移動ベクトルを変更
        }

        prediction.UpdatePreviousPos();    //ターゲット位置を更新

        base.Move();                       //
    }

    protected override void NotMove()
    {
        cannon.ReSetShootInterval();//インターバルをリセット

        base.NotMove();
    }
}
