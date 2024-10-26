using UnityEngine;

public class TankMovingPartsScript : MonoBehaviour
{
    /// <summary>タレットのTransform</summary>
    [SerializeField, LightColor] private Transform turret;

    /// <summary>キャタピラのTran</summary>
    [SerializeField, LightColor] private Transform caterpillar;

    /// <summary>タレット回転時の補間値</summary>
    [SerializeField, Range01] float turretLerp;

    /// <summary>キャタピラ回転時の補間値</summary>
    [SerializeField, Range01] private float caterpillarLerp;

    /// <summary>タレットとキャタピラのRotationをセット</summary>
    public Quaternion SetRotation { set => caterpillar.rotation = turret.rotation = value; }

    public Vector2 TurretForward => turret.forward;

    /// <summary>エネミーの情報をセット</summary>
    /// <param name="i">エネミーの情報</param>
    public void SetInfo(TankEnemyInfoScript i) => turretLerp = i.TurretLerp;

    /// <summary>タレットをターゲットの方向へ向ける</summary>
    /// <param name="target">ターゲットの方向</param>
    public void TargetLookAt(Vector3 target)
    {
        turret.LerpRotation(target, turretLerp, Vector3.forward);//タレットをターゲットの方向への補間値を代入
    }

    /// <summary>キャタピラーを移動方向に向ける</summary>
    /// <param name="velocity">移動ベクトル</param>
    public void CaterpillarLookAt(Vector2 velocity)
    {
        if (velocity != Vector2.zero)                                            //移動ベクトルが(0, 0)以外なら
        {
            caterpillar.LerpRotation(velocity, caterpillarLerp, Vector3.forward);//キャタピラーを移動方向への補間値を代入
        }
    }
}
