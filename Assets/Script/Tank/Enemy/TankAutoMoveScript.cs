using UnityEngine;

/// <summary>タンクの自動移動</summary>
public class TankAutoMoveScript : TankMoveScript
{
    /// <summary>移動タイプ</summary>
    public enum MoveType
    {
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

    /// <summary>横のランダムベクトル</summary>
    private Vector2 Horizontal => new(RandomSpeed, default);//(ランダム速度, 0)

    /// <summary>縦のランダムベクトル</summary>
    private Vector2 Vertical => new(default, RandomSpeed);//(0, ランダム速度)

    /// <summary>移動ベクトル</summary>
    private Vector2 MoveVector
    {
        get
        {
            var type = this.type;                                                       //タイプ

            if (type == MoveType.Random) type = EnumEx<MoveType>.Values.Random();       //タイプがランダムならランダムなタイプを代入

            return type switch                                                          //タイプで分岐
            {
                MoveType.Horizontal            => Horizontal,                           //横のランダムベクトル
                MoveType.Vertical              => Vertical,                             //縦のランダムベクトル
                MoveType.HorizontalOrVertical  => RandomEx.Bool ? Horizontal : Vertical,//横 or 縦のランダムベクトル
                MoveType.HorizontalAndVertical => Horizontal + Vertical,                //横 and 縦のランダムベクトル
                MoveType.Random                => MoveVector,                           //再起
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

        seInterval.Time = moveSE.Length / moveSpeed;//SEのインターバルを代入
    }

    /// <summary>移動ベクトルを変更</summary>
    public void VectorChange()
    {
        ribo.velocity = isNormalized ? MoveVector.normalized * moveSpeed : MoveVector;//移動ベクトルに(移動ベクトルを正規化するか ? 正規化した移動ベクトルの雛形 * 移動速度 : 移動ベクトルの雛形)を代入
    }
}
