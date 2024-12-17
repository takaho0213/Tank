using UnityEngine;
using static TankEnemyScript;
using static TankAutoMoveScript;

/// <summary>エネミータンクの情報</summary>
public class TankEnemyInfoScript : MonoBehaviour
{
    /// <summary>初期位置と角度</summary>
    [field: AddLabel("初期位置と角度"), SerializeField, LightColor]
    public Transform PosAndRot { get; private set; }

    [field: Space]

    /// <summary>体力</summary>
    [field: AddLabel("体力"), SerializeField] 
    public int Health { get; private set; }

    [field: Space]

    /// <summary>移動タイプ</summary>
    [field: AddLabel("移動タイプ"), SerializeField] 
    public MoveType MoveType { get; private set; }

    /// <summary>移動速度</summary>
    [field: AddLabel("移動速度"), SerializeField] 
    public float MoveSpeed { get; private set; }

    /// <summary>移動ベクトルを正規化するか</summary>
    [field: AddLabel("移動ベクトルを正規化するか"), SerializeField] 
    public bool IsMoveVectorNormalized { get; private set; }

    [field: Space]

    /// <summary>タレット回転時の補間値</summary>
    [field: AddLabel("タレット回転時の補間値"), SerializeField, Range01()]
    public float TurretLerp { get; private set; }

    [field: Space]

    /// <summary>攻撃タイプ</summary>
    [field: AddLabel("攻撃タイプ"), SerializeField]
    public AttackType AttackType { get; private set; }

    /// <summary>発射間隔</summary>
    [field: AddLabel("発射間隔(秒)"), SerializeField]
    public float ShootInterval { get; private set; }

    /// <summary>弾速</summary>
    [field: AddLabel("弾速"), SerializeField]
    public float BulletSpeed { get; private set; }

    /// <summary>弾の反射数</summary>
    [field: AddLabel("弾の反射数"), SerializeField]
    public int BulletReflectionCount { get; private set; }

    [field: Space]

    /// <summary>色</summary>
    [field: AddLabel("色"), SerializeField]
    public Color FillColor { get; private set; }

    /// <summary>値をセット</summary>
    /// <param name="p">初期位置と角度</param>
    /// <param name="h">体力</param>
    /// <param name="m">移動タイプ</param>
    /// <param name="s">移動速度</param>
    /// <param name="n">移動ベクトルを正規化するか</param>
    /// <param name="l">タレット回転時の補間値</param>
    /// <param name="a">攻撃タイプ</param>
    /// <param name="i">発射間隔</param>
    /// <param name="b">弾速</param>
    /// <param name="r">弾の反射数</param>
    /// <param name="c">色</param>
    public void Set(int h, MoveType m, float s, bool n, float l,  AttackType a, float i, float b, int r, Color c)
    {
        Health = h;                     //体力を代入
        MoveType = m;                   //移動タイプを代入
        MoveSpeed = s;                  //移動速度を代入
        IsMoveVectorNormalized = n;     //移動ベクトルを正規化するかを代入
        TurretLerp = l;                 //タレット回転時の補間値を代入
        AttackType = a;                 //攻撃タイプを代入
        ShootInterval = i;              //発射間隔を代入
        BulletSpeed = b;                //弾速を代入
        BulletReflectionCount = r;      //弾の反射数を代入
        FillColor = c;                  //色を代入
    }
}
