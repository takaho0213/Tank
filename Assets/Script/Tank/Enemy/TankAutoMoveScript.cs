using UnityEngine;

public class TankAutoMoveScript : TankMoveScript
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

    /// <summary>移動タイプ</summary>
    [SerializeField] private MoveType type;

    /// <summary>移動ベクトルを正規化するか</summary>
    [SerializeField] private bool isNormalized;

    /// <summary>ランダムに速度を返す</summary>
    private float RandomSpeed => Random.Range(-moveSpeed, moveSpeed);

    private Vector2 Horizontal => new(RandomSpeed, default);//(ランダム速度, 0)

    private Vector2 Vertical => new(default, RandomSpeed);//(0, ランダム速度)

    /// <summary>移動ベクトルの雛形</summary>
    private Vector2 MoveVector
    {
        get
        {
            var type = this.type;

            if (type == MoveType.Random) type = EnumEx<MoveType>.Values.Random();

            return type switch                                                          //タイプで分岐
            {
                MoveType.Horizontal            => Horizontal,                           //
                MoveType.Vertical              => Vertical,                             //
                MoveType.HorizontalOrVertical  => RandomEx.Bool ? Horizontal : Vertical,//
                MoveType.HorizontalAndVertical => Horizontal + Vertical,                //
                MoveType.Random                => MoveVector,                           //
                _                              => default,                              //(0, 0)
            };
        }
    }

    /// <summary>エネミーの情報をセット</summary>
    /// <param name="i">エネミーの情報</param>
    public void SetInfo(TankEnemyInfoScript i)
    {
        type = i.MoveType;                          //移動の仕方をセット
        moveSpeed = i.MoveSpeed;                    //移動速度をセット
        isNormalized = i.IsMoveVectorNormalized;    //移動ベクトルを正規化するかをセット

        seInterval.Time = moveSE.Length / moveSpeed;//
    }

    /// <summary>移動ベクトルを変更</summary>
    public void VectorChange()
    {
        ribo.velocity = isNormalized ? MoveVector.normalized * moveSpeed : MoveVector;//移動ベクトルに(移動ベクトルを正規化するか ? 正規化した移動ベクトルの雛形 * 移動速度 : 移動ベクトルの雛形)を代入
    }
}
