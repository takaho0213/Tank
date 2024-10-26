using UnityEngine;

public class TankTargetPredictionScript : MonoBehaviour
{
    /// <summary>ターゲットのトランスフォーム</summary>
    [SerializeField, LightColor] private Transform target;

    /// <summary>ターゲットの前フレームの位置</summary>
    private Vector3 previousTargetPos;

    /// <summary>ターゲットの位置</summary>
    public Vector3 TargetPos => target.position;

    /// <summary>ターゲットの移動先を予測</summary>
    /// <param name="pos">自身の場所</param>
    /// <param name="speed">弾速</param>
    /// <returns>ターゲットの移動先</returns>
    public Vector3 PredictionPos(Vector3 pos, float speed)
    {
        Vector3 targetPos = target.position;                       //ターゲットの位置

        Vector3 targetMoveDistance = targetPos - previousTargetPos;//ターゲットの移動距離

        return targetPos + targetMoveDistance * Vector3.Distance(pos, targetPos) / speed / Time.fixedDeltaTime;//タゲ位置 + タゲ移動距離 * (自身の位置とタゲ位置の距離) / 弾速 / デルタタイム
    }

    /// <summary>ターゲットの前フレームの位置を更新(PredictionPos()の後に呼び出す)</summary>
    public void UpdatePreviousPos() => previousTargetPos = target.position;//ターゲットの位置を更新
}
