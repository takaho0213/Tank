using UnityEngine;

/// <summary>エネミータンクの情報</summary>
public class TankEnemyInfoScript : MonoBehaviour
{
    /// <summary>初期位置と角度</summary>
    [field: AddLabel("初期位置と角度")]
    [field: SerializeField, LightColor] public Transform PosAndRot { get; private set; }

    [field: Space]

    /// <summary>体力</summary>
    [field: AddLabel("体力")]
    [field: SerializeField] public int Health { get; private set; }

    [field: Space]

    /// <summary>移動タイプ</summary>
    [field: AddLabel("移動タイプ")]
    [field: SerializeField] public TankAutoMoveScript.MoveType MoveType { get; private set; }

    /// <summary>移動速度</summary>
    [field: AddLabel("移動速度")]
    [field: SerializeField] public float MoveSpeed { get; private set; }

    /// <summary>移動ベクトルを正規化するか</summary>
    [field: AddLabel("移動ベクトルを正規化するか")]
    [field: SerializeField] public bool IsMoveVectorNormalized { get; private set; }

    [field: Space]

    /// <summary>タレット回転時の補間値</summary>
    [field: AddLabel("タレット回転時の補間値")]
    [field: SerializeField, Range01()] public float TurretLerp { get; private set; }

    [field: Space]

    /// <summary>攻撃タイプ</summary>
    [field: AddLabel("攻撃タイプ")]
    [field: SerializeField] public TankEnemyScript.AttackType AttackType { get; private set; }

    /// <summary>発射間隔</summary>
    [field: AddLabel("発射間隔(秒)")]
    [field: SerializeField] public float ShootInterval { get; private set; }

    /// <summary>弾速</summary>
    [field: AddLabel("弾速")]
    [field: SerializeField] public float BulletSpeed { get; private set; }

    /// <summary>弾の反射数</summary>
    [field: AddLabel("弾の反射数")]
    [field: SerializeField] public int BulletReflectionCount { get; private set; }

    [field: Space]

    /// <summary>色</summary>
    [field: AddLabel("色")]
    [field: SerializeField] public Color FillColor { get; private set; }
}
