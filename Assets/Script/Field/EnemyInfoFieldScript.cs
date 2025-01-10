using UnityEngine;

/// <summary>EnemyTankの情報を入力するフィールド</summary>
public class EnemyInfoFieldScript : MonoBehaviour
{
    /// <summary>体力のフィールド</summary>
    [SerializeField, LightColor] private SliderFieldScript health;

    /// <summary>移動タイプのフィールド</summary>
    [SerializeField, LightColor] private MoveTypeFieldScript moveType;

    /// <summary>移動速度のフィールド</summary>
    [SerializeField, LightColor] private SliderFieldScript moveSpeed;

    /// <summary>移動速度固定のフィールド</summary>
    [SerializeField, LightColor] private IsNormalizedFieldScript isMoveVectorNormalized;

    /// <summary>タレット回転速度のフィールド</summary>
    [SerializeField, LightColor] private SliderFieldScript turretLerp;

    /// <summary>攻撃タイプのフィールド</summary>
    [SerializeField, LightColor] private AttackTypeFieldScript attackType;

    /// <summary>射撃間隔のフィールド</summary>
    [SerializeField, LightColor] private SliderFieldScript shootInterval;

    /// <summary>弾速のフィールド</summary>
    [SerializeField, LightColor] private SliderFieldScript bulletSpeed;

    /// <summary>弾の反射数のフィールド</summary>
    [SerializeField, LightColor] private SliderFieldScript bulletReflectionCount;

    /// <summary>カラーのフィールド</summary>
    [SerializeField, LightColor] private ColorFieldScript fillColor;

    /// <summary>EnemyTankの情報</summary>
    [SerializeField, LightColor] private TankEnemyInfoScript info;

    /// <summary>フィールドの情報をセット</summary>
    /// <returns>EnemyTankの情報</returns>
    public TankEnemyInfoScript SetInfo()
    {
        var h = health.IntValue;               //体力の値
        var m = moveType.Value;                //移動タイプの値
        var s = moveSpeed.Value;               //移動速度の値
        var n = isMoveVectorNormalized.Value;  //移動速度固定の値
        var l = turretLerp.Value;              //タレットの回転速度の値
        var a = attackType.Value;              //攻撃タイプの値
        var i = shootInterval.Value;           //射撃間隔の値
        var b = bulletSpeed.Value;             //弾速の値
        var r = bulletReflectionCount.IntValue;//弾の反射数の値
        var c = fillColor.Value;               //カラーの値

        info.Set(h, m, s, n, l, a, i, b, r, c);//情報をセット

        return info;                           //EnemyTankの情報を返す
    }
}
