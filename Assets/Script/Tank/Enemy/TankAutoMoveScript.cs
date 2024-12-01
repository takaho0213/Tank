using UnityEngine;

public class TankAutoMoveScript : TankMoveScript
{
    /// <summary></summary>
    public enum MoveType
    {
        /// <summary>����</summary>
        None,
        /// <summary>��</summary>
        Horizontal,
        /// <summary>�c</summary>
        Vertical,
        /// <summary>�� or �c</summary>
        HorizontalOrVertical,
        /// <summary>�� and �c</summary>
        HorizontalAndVertical,
        /// <summary>�����_��</summary>
        Random,
    }

    /// <summary>�ړ��^�C�v</summary>
    [SerializeField] private MoveType type;

    /// <summary>�ړ��x�N�g���𐳋K�����邩</summary>
    [SerializeField] private bool isNormalized;

    /// <summary>�����_���ɑ��x��Ԃ�</summary>
    private float RandomSpeed => Random.Range(-moveSpeed, moveSpeed);

    private Vector2 Horizontal => new(RandomSpeed, default);//(�����_�����x, 0)

    private Vector2 Vertical => new(default, RandomSpeed);//(0, �����_�����x)

    /// <summary>�ړ��x�N�g���̐��`</summary>
    private Vector2 MoveVector
    {
        get
        {
            var type = this.type;

            if (type == MoveType.Random) type = EnumEx<MoveType>.Values.Random();

            return type switch                                                          //�^�C�v�ŕ���
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

    /// <summary>�G�l�~�[�̏����Z�b�g</summary>
    /// <param name="i">�G�l�~�[�̏��</param>
    public void SetInfo(TankEnemyInfoScript i)
    {
        type = i.MoveType;                          //�ړ��̎d�����Z�b�g
        moveSpeed = i.MoveSpeed;                    //�ړ����x���Z�b�g
        isNormalized = i.IsMoveVectorNormalized;    //�ړ��x�N�g���𐳋K�����邩���Z�b�g

        seInterval.Time = moveSE.Length / moveSpeed;//
    }

    /// <summary>�ړ��x�N�g����ύX</summary>
    public void VectorChange()
    {
        ribo.velocity = isNormalized ? MoveVector.normalized * moveSpeed : MoveVector;//�ړ��x�N�g����(�ړ��x�N�g���𐳋K�����邩 ? ���K�������ړ��x�N�g���̐��` * �ړ����x : �ړ��x�N�g���̐��`)����
    }
}
