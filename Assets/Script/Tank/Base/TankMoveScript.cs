using UnityEngine;

/// <summary>タンクの移動</summary>
public class TankMoveScript : MonoBehaviour
{
    /// <summary>移動に使うRigidbody</summary>
    [SerializeField, LightColor] protected Rigidbody2D ribo;

    /// <summary>移動SEを再生するAudioSource</summary>
    [SerializeField, LightColor] protected AudioSource moveSource;

    /// <summary>移動速度</summary>
    [SerializeField] protected float moveSpeed;

    /// <summary>SEを再生するインターバル</summary>
    protected Interval seInterval;

    /// <summary>移動SE</summary>
    protected ClipInfo moveSE;

    /// <summary>場所</summary>
    public Vector2 Pos => ribo.position;

    /// <summary>移動ベクトル</summary>
    public Vector3 Velocity => ribo.velocity;

    /// <summary>初期化</summary>
    public virtual void Init()
    {
        moveSE ??= AudioScript.I.TankAudio[TankClip.Move];  //移動SEのクリップ

        seInterval ??= new(moveSE.Length / moveSpeed, true);//インターバルがnullならインスタンス化
    }

    /// <summary>停止</summary>
    public void Stop() => ribo.velocity = Vector2.zero;//移動ベクトルに(0, 0)を代入

    /// <summary>一定間隔で移動SEを再生</summary>
    public void MoveSE()
    {
        if (ribo.velocity != Vector2.zero && seInterval)//移動ベクトルが(0, 0)以外 かつ SE再生間隔を越えていたら
        {
            moveSE.PlayOneShot(moveSource);             //移動SEを再生
        }
    }
}
