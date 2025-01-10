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
        UpdateInfo(info);                                 //情報を更新

        cannon.BulletPool = pool;                         //弾プール関数をセット

        base.onDeath = () =>                              //死亡した再実行する関数をセット
        {
            IsActive = false;                             //非アクティブ

            stageSystem.StartCoroutine(onDeath.Invoke());//死亡時の処理を開始
        };
    }

    /// <summary>情報を更新</summary>
    /// <param name="info">情報</param>
    public void UpdateInfo(TankEnemyInfoScript info)
    {
        IsActive = true;                                    //アクティブ

        line.Crear();                                       //弾道をリセット

        autoMove.Stop();                                    //移動を停止

        SetPosAndRot(info.PosAndRot);                       //場所と角度をセット

        parts.SetInfo(info);                                //エネミーの情報をセット

        damage.SetInfo(info);                               //エネミーの情報をセット

        fillColor.SetInfo(info);                            //エネミーの情報をセット

        line.SetLineColor(info.FillColor);                  //エネミーの情報をセット

        cannon.SetInfo(info);                               //エネミーの情報をセット

        autoMove.SetInfo(info);                             //エネミーの情報をセット

        cannon.ShootIntervalTime = info.ShootInterval;      //発射間隔をセット

        attackType = info.AttackType;                       //攻撃タイプをセット

        while (attackType == AttackType.Random)             //攻撃タイプがランダムな限り繰り返す
        {
            attackType = EnumEx<AttackType>.Values.Random();//ランダムな攻撃タイプを代入
        }
    }

    /// <summary>タレットをターゲットの方向に向ける</summary>
    private void LookAt()
    {
        parts.TargetLookAt(prediction.TargetPos - autoMove.Pos);//タレットを(ターゲットの位置 - 自身の現在位置)の方向に向ける
    }

    /// <summary>レイがヒット かつ ターゲットと一致か</summary>
    private bool Ray() => search.Ray(prediction.TargetPos);//レイがヒット かつ ターゲットと一致なら

    /// <summary>通常攻撃</summary>
    private bool AttackNormal()
    {
        LookAt();   //タレットをターゲットの方向に向ける

        return true;//trueを返す
    }

    /// <summary>偏差攻撃</summary>
    private bool AttackDeviation()
    {
        Vector2 pos = prediction.PredictionPos(autoMove.Pos, cannon.BulletSpeed);//移動予測先を代入

        parts.TargetLookAt(pos - autoMove.Pos);                                  //タレットを移動予測先に向ける

        return true;                                                             //trueを返す
    }

    /// <summary>ターゲット攻撃</summary>
    /// <returns>レイがヒットしたか</returns>
    private bool AttackTarget()
    {
        LookAt();    //タレットをターゲットの方向に向ける

        return Ray();//レイがヒットしたか
    }

    /// <summary>反射攻撃</summary>
    /// <returns>射線上 or 反射する射線上にターゲットがいるか</returns>
    private bool AttackReflection()
    {
        if (Ray())                          //レイがヒットしたら
        {
            LookAt();                       //タレットをターゲットの方向に向ける

            return true;                    //trueを返す
        }

        parts.TargetLookAt(search.Rotate());//タレットをサーチする方向に向ける

        return search.ReflectionRay();      //反射するレイがヒットしたか
    }

    /// <summary>攻撃する際の処理</summary>
    /// <returns>攻撃するか？</returns>
    private bool AttackMove() => attackType switch  //攻撃タイプで分岐
    {
        AttackType.Normal     => AttackNormal(),    //タイプが通常攻撃      なら/通常攻撃
        AttackType.Deviation  => AttackDeviation(), //タイプが偏差攻撃      なら/偏差攻撃
        AttackType.Target     => AttackTarget(),    //タイプがターゲット攻撃なら/ターゲット攻撃
        AttackType.Reflection => AttackReflection(),//タイプが反射攻撃      なら/反射攻撃
        _ => default,                               //それ以外              なら/default値
    };

    protected override void Move()
    {
        if (AttackMove() && cannon.IsShoot)//発射間隔を越えていたら
        {
            cannon.Shoot();                //弾を発射

            autoMove.VectorChange();       //移動ベクトルを変更
        }

        prediction.UpdatePreviousPos();    //ターゲット位置を更新

        base.Move();                       //ベースを呼び出す
    }

    protected override void NotMove()
    {
        cannon.ReSetShootInterval();//インターバルをリセット

        base.NotMove();             //ベースを呼び出す
    }
}
