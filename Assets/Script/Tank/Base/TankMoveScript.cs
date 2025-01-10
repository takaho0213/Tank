using UnityEngine;

/// <summary>�^���N�̈ړ�</summary>
public class TankMoveScript : MonoBehaviour
{
    /// <summary>�ړ��Ɏg��Rigidbody</summary>
    [SerializeField, LightColor] protected Rigidbody2D ribo;

    /// <summary>�ړ�SE���Đ�����AudioSource</summary>
    [SerializeField, LightColor] protected AudioSource moveSource;

    /// <summary>�ړ����x</summary>
    [SerializeField] protected float moveSpeed;

    /// <summary>SE���Đ�����C���^�[�o��</summary>
    protected Interval seInterval;

    /// <summary>�ړ�SE</summary>
    protected ClipInfo moveSE;

    /// <summary>�ꏊ</summary>
    public Vector2 Pos => ribo.position;

    /// <summary>�ړ��x�N�g��</summary>
    public Vector3 Velocity => ribo.velocity;

    /// <summary>������</summary>
    public virtual void Init()
    {
        moveSE ??= AudioScript.I.TankAudio[TankClip.Move];  //�ړ�SE�̃N���b�v

        seInterval ??= new(moveSE.Length / moveSpeed, true);//�C���^�[�o����null�Ȃ�C���X�^���X��
    }

    /// <summary>��~</summary>
    public void Stop() => ribo.velocity = Vector2.zero;//�ړ��x�N�g����(0, 0)����

    /// <summary>���Ԋu�ňړ�SE���Đ�</summary>
    public void MoveSE()
    {
        if (ribo.velocity != Vector2.zero && seInterval)//�ړ��x�N�g����(0, 0)�ȊO ���� SE�Đ��Ԋu���z���Ă�����
        {
            moveSE.PlayOneShot(moveSource);             //�ړ�SE���Đ�
        }
    }
}
