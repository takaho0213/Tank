using UnityEngine;

public class TankAutoMoveScript : MonoBehaviour
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

    /// <summary>�ړ��pRigidbody</summary>
    [SerializeField, LightColor] private Rigidbody2D ribo;

    /// <summary>�ړ�AudioSource</summary>
    [SerializeField, LightColor] private AudioSource source;

    /// <summary>�ړ��^�C�v</summary>
    [SerializeField] private MoveType type;

    /// <summary>�ړ����x</summary>
    [SerializeField] private float speed;

    /// <summary>�ړ��x�N�g���𐳋K�����邩</summary>
    [SerializeField] private bool isNormalized;

    /// <summary>�ړ�SE�̍Đ��Ԋu</summary>
    private Interval seInterval;

    /// <summary>�ړ��^�C�v�z��</summary>
    private readonly MoveType[] MoveTypes = (MoveType[])System.Enum.GetValues(typeof(MoveType));

    /// <summary>�����_���ɑ��x��Ԃ�</summary>
    private float RandomSpeed => Random.Range(-speed, speed);

    /// <summary>�����_����true : false��Ԃ�</summary>
    private bool RandomBool => Random.Range(default, 2) == default;

    /// <summary>���g�̈ʒu</summary>
    public Vector3 Pos => ribo.position;

    /// <summary>�ړ��x�N�g��</summary>
    public Vector3 Velocity => ribo.velocity;

    /// <summary>�ړ��x�N�g���̐��`</summary>
    private Vector2 MoveVector
    {
        get
        {
            var type = this.type == MoveType.Random ? MoveTypes[Random.Range(default, MoveTypes.Length)] : this.type;          //�ړ��^�C�v�������_���Ȃ�

            return type switch                                                                                       //�^�C�v�ŕ���
            {
                MoveType.Horizontal => new(RandomSpeed, default),                                         //(�����_�����x, 0)
                MoveType.Vertical => new(default, RandomSpeed),                                         //(0, �����_�����x)
                MoveType.HorizontalOrVertical => RandomBool ? new(RandomSpeed, default) : new(default, RandomSpeed),//�����_��bool ? (�����_�����x, 0) : (0, �����_�����x)
                MoveType.HorizontalAndVertical => new(RandomSpeed, RandomSpeed),                                     //(�����_�����x, �����_�����x)
                MoveType.Random => MoveVector,                                                        //�ړ��x�N�g���̐��`
                _ => default,                                                           //(0, 0)
            };
        }
    }

    /// <summary>�G�l�~�[�̏����Z�b�g</summary>
    /// <param name="i">�G�l�~�[�̏��</param>
    public void SetInfo(TankEnemyInfoScript i)
    {
        type = i.MoveType;                      //�ړ��̎d�����Z�b�g
        speed = i.MoveSpeed;                    //�ړ����x���Z�b�g
        isNormalized = i.IsMoveVectorNormalized;//�ړ��x�N�g���𐳋K�����邩���Z�b�g
    }

    /// <summary>��~</summary>
    public void Stop() => ribo.velocity = Vector2.zero;//�ړ��x�N�g����(0, 0)����

    /// <summary>�ړ��x�N�g����ύX</summary>
    public void VectorChange()
    {
        ribo.velocity = isNormalized ? MoveVector.normalized * speed : MoveVector;//�ړ��x�N�g����(�ړ��x�N�g���𐳋K�����邩 ? ���K�������ړ��x�N�g���̐��` * �ړ����x : �ړ��x�N�g���̐��`)����
    }

    /// <summary></summary>
    public void MoveSE()
    {
        if (ribo.velocity != Vector2.zero)                       //�ړ��x�N�g����(0, 0)�ȊO�Ȃ�
        {
            var c = AudioScript.I.TankAudio.Dictionary[TankClip.Move];//�ړ�SE�̃N���b�v

            seInterval ??= new(c.Clip.length / speed, true);     //�C���^�[�o����null�Ȃ�C���X�^���X��

            if (seInterval) source.PlayOneShot(c.Clip, c.Volume);//SE�Đ��Ԋu���z���Ă�����ړ�SE���Đ�
        }
    }
}
