using UnityEngine;

public class TankAutoMoveScript : MonoBehaviour
{
    /// <summary></summary>
    public enum MoveType
    {
        /// <summary>無し</summary>
        None,
        /// <summary>横</summary>
        Horizontal,
        /// <summary>縦</summary>
        Vertical,
        /// <summary>横 or 縦</summary>
        HorizontalOrVertical,
        /// <summary>横 and 縦</summary>
        HorizontalAndVertical,
        /// <summary>ランダム</summary>
        Random,
    }

    /// <summary>移動用Rigidbody</summary>
    [SerializeField, LightColor] private Rigidbody2D ribo;

    /// <summary>移動AudioSource</summary>
    [SerializeField, LightColor] private AudioSource source;

    /// <summary>移動タイプ</summary>
    [SerializeField] private MoveType type;

    /// <summary>移動速度</summary>
    [SerializeField] private float speed;

    /// <summary>移動ベクトルを正規化するか</summary>
    [SerializeField] private bool isNormalized;

    /// <summary>移動SEの再生間隔</summary>
    private Interval seInterval;

    /// <summary>移動タイプ配列</summary>
    private readonly MoveType[] MoveTypes = (MoveType[])System.Enum.GetValues(typeof(MoveType));

    /// <summary>ランダムに速度を返す</summary>
    private float RandomSpeed => Random.Range(-speed, speed);

    /// <summary>ランダムにtrue : falseを返す</summary>
    private bool RandomBool => Random.Range(default, 2) == default;

    /// <summary>自身の位置</summary>
    public Vector3 Pos => ribo.position;

    /// <summary>移動ベクトル</summary>
    public Vector3 Velocity => ribo.velocity;

    /// <summary>移動ベクトルの雛形</summary>
    private Vector2 MoveVector
    {
        get
        {
            var type = this.type == MoveType.Random ? MoveTypes[Random.Range(default, MoveTypes.Length)] : this.type;          //移動タイプがランダムなら

            return type switch                                                                                       //タイプで分岐
            {
                MoveType.Horizontal => new(RandomSpeed, default),                                         //(ランダム速度, 0)
                MoveType.Vertical => new(default, RandomSpeed),                                         //(0, ランダム速度)
                MoveType.HorizontalOrVertical => RandomBool ? new(RandomSpeed, default) : new(default, RandomSpeed),//ランダムbool ? (ランダム速度, 0) : (0, ランダム速度)
                MoveType.HorizontalAndVertical => new(RandomSpeed, RandomSpeed),                                     //(ランダム速度, ランダム速度)
                MoveType.Random => MoveVector,                                                        //移動ベクトルの雛形
                _ => default,                                                           //(0, 0)
            };
        }
    }

    /// <summary>エネミーの情報をセット</summary>
    /// <param name="i">エネミーの情報</param>
    public void SetInfo(TankEnemyInfoScript i)
    {
        type = i.MoveType;                      //移動の仕方をセット
        speed = i.MoveSpeed;                    //移動速度をセット
        isNormalized = i.IsMoveVectorNormalized;//移動ベクトルを正規化するかをセット
    }

    /// <summary>停止</summary>
    public void Stop() => ribo.velocity = Vector2.zero;//移動ベクトルに(0, 0)を代入

    /// <summary>移動ベクトルを変更</summary>
    public void VectorChange()
    {
        ribo.velocity = isNormalized ? MoveVector.normalized * speed : MoveVector;//移動ベクトルに(移動ベクトルを正規化するか ? 正規化した移動ベクトルの雛形 * 移動速度 : 移動ベクトルの雛形)を代入
    }

    /// <summary></summary>
    public void MoveSE()
    {
        if (ribo.velocity != Vector2.zero)                       //移動ベクトルが(0, 0)以外なら
        {
            var c = AudioScript.I.TankAudio.Dictionary[TankClip.Move];//移動SEのクリップ

            seInterval ??= new(c.Clip.length / speed, true);     //インターバルがnullならインスタンス化

            if (seInterval) source.PlayOneShot(c.Clip, c.Volume);//SE再生間隔を越えていたら移動SEを再生
        }
    }
}
