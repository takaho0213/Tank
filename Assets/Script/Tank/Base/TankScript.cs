using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

/// <summary>ベースのタンク</summary>
public abstract class TankScript : MonoBehaviour
{
    /// <summary>マネージャー</summary>
    [SerializeField, LightColor] protected StageSystemScript stageSystem;

    /// <summary>自身のObject</summary>
    [SerializeField, LightColor] protected GameObject obj;

    /// <summary>自身のTransform</summary>
    [SerializeField, LightColor] protected Transform trafo;

    /// <summary>武器</summary>
    [SerializeField, LightColor] protected TankCannonScript cannon;

    /// <summary>タレット キャタピラー</summary>
    [SerializeField, LightColor] protected TankMovingPartsScript parts;

    /// <summary>ダメージ処理</summary>
    [SerializeField, LightColor] protected TankDamageScript damage;

    /// <summary>色</summary>
    [SerializeField, LightColor] protected TankFillColorScript fillColor;

    [SerializeField, LightColor] protected TankBulletLineScript line;

    /// <summary>死亡演出が終わった際実行</summary>
    protected UnityAction onDeath;

    protected abstract TankMoveScript BaseMove { get; }

    /// <summary>移動できない状態か</summary>
    protected bool IsNotMove => stageSystem.IsNotMove || damage.IsDeath;

    /// <summary>アクティブか</summary>
    public bool IsActive
    {
        get => obj.activeSelf;
        set => obj.SetActive(value);
    }

    protected virtual void OnEnable() => fillColor.SetColor();//アクティブになった際/色をセット

    protected virtual void Awake()
    {
        fillColor.SetColor();              //色をセット

        line.Init(fillColor.Color);        //

        damage.OnHealthGone = OnHealthGone;//体力が0になった際実行する関数をセット

        BaseMove.Init();

        cannon.Init();
    }

    protected virtual void OnTriggerEnter2D(Collider2D hit)
    {
        if (IsNotMove) return;                                 //移動できない状態なら

        StartCoroutine(damage.Hit(hit, fillColor.DamageColor));//ダメージエフェクトを開始
    }

    /// <summary>positionとrotationをセット</summary>
    /// <param name="trafo">セットするpositionとrotation</param>
    public void SetPosAndRot(Transform trafo)
    {
        this.trafo.position = trafo.position;   //位置を代入
        parts.SetRotation = trafo.rotation;//角度を代入

        line.Crear();
    }

    /// <summary>Healthが0になった際実行</summary>
    protected virtual void OnHealthGone()
    {
        fillColor.SetGray();                        //色をグレーにする

        line.Crear();

        StartCoroutine(damage.DeathEffect(onDeath));//死亡演出を開始
    }

    protected virtual void Move()
    {
        line.CreateLine();
    }

    protected virtual void NotMove()
    {
        BaseMove.Stop();
    }

    protected virtual void Always()
    {
        damage.UpdateHealthGauge();            //体力バーを更新

        parts.CaterpillarLookAt(BaseMove.Velocity);//キャタピラを回転

        BaseMove.MoveSE();
    }

    protected virtual void FixedUpdate()
    {
        Always();

        if (IsNotMove) NotMove();
        else Move();
    }
}
