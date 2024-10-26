using UnityEngine;
using UnityEngine.EventSystems;

public class TankInputMoveScript : MonoBehaviour
{
    /// <summary>inputModule</summary>
    [SerializeField, LightColor] private StandaloneInputModule inputModule;

    /// <summary>�ړ��Ɏg��Rigidbody</summary>
    [SerializeField, LightColor] private Rigidbody2D ribo;

    /// <summary>�ړ�SE���Đ�����AudioSource</summary>
    [SerializeField, LightColor] private AudioSource moveSource;

    /// <summary>�ړ����x</summary>
    [SerializeField] private float moveSpeed;

    /// <summary>SE���Đ�����C���^�[�o��</summary>
    private Interval seInterval;

    /// <summary>���̓x�N�g��</summary>
    private Vector2 inputVector;

    /// <summary>�ꏊ</summary>
    public Vector2 Pos => ribo.position;

    /// <summary>�ړ��x�N�g��</summary>
    public Vector3 Velocity => ribo.velocity;

    /// <summary>�ړ���~</summary>
    public void Stop() => ribo.velocity = Vector2.zero;

    /// <summary>���͈ړ�</summary>
    public void InputMove()
    {
        inputVector.x = Input.GetAxisRaw(inputModule.horizontalAxis);//���̓x�N�g��X����
        inputVector.y = Input.GetAxisRaw(inputModule.verticalAxis);  //���̓x�N�g��Y����

        ribo.velocity = inputVector * moveSpeed;                     //(���̓x�N�g�� * �ړ����x)���ړ��x�N�g���ɑ��

        if (ribo.velocity != Vector2.zero)                           //�ړ��x�N�g����(0, 0)�ȊO�Ȃ�
        {
            var c = AudioScript.I.TankAudio.Dictionary[TankClip.Move];    //�ړ�SE�N���b�v

            seInterval ??= new(c.Clip.length / moveSpeed, true);     //null�Ȃ���

            if (seInterval) moveSource.PlayOneShot(c.Clip, c.Volume);//SE�Đ��Ԋu���z���Ă�����/SE���Đ�
        }
    }
}
