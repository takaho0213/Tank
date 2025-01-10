using UnityEngine;

/// <summary>�^���N�̎����ړ�</summary>
public class TankAutoMoveScript : TankMoveScript
{
    /// <summary>�ړ��^�C�v</summary>
    public enum MoveType
    {
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

    /// <summary>���̃����_���x�N�g��</summary>
    private Vector2 Horizontal => new(RandomSpeed, default);//(�����_�����x, 0)

    /// <summary>�c�̃����_���x�N�g��</summary>
    private Vector2 Vertical => new(default, RandomSpeed);//(0, �����_�����x)

    /// <summary>�ړ��x�N�g��</summary>
    private Vector2 MoveVector
    {
        get
        {
            var type = this.type;                                                       //�^�C�v

            if (type == MoveType.Random) type = EnumEx<MoveType>.Values.Random();       //�^�C�v�������_���Ȃ烉���_���ȃ^�C�v����

            return type switch                                                          //�^�C�v�ŕ���
            {
                MoveType.Horizontal            => Horizontal,                           //���̃����_���x�N�g��
                MoveType.Vertical              => Vertical,                             //�c�̃����_���x�N�g��
                MoveType.HorizontalOrVertical  => RandomEx.Bool ? Horizontal : Vertical,//�� or �c�̃����_���x�N�g��
                MoveType.HorizontalAndVertical => Horizontal + Vertical,                //�� and �c�̃����_���x�N�g��
                MoveType.Random                => MoveVector,                           //�ċN
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

        seInterval.Time = moveSE.Length / moveSpeed;//SE�̃C���^�[�o������
    }

    /// <summary>�ړ��x�N�g����ύX</summary>
    public void VectorChange()
    {
        ribo.velocity = isNormalized ? MoveVector.normalized * moveSpeed : MoveVector;//�ړ��x�N�g����(�ړ��x�N�g���𐳋K�����邩 ? ���K�������ړ��x�N�g���̐��` * �ړ����x : �ړ��x�N�g���̐��`)����
    }
}
