using UnityEngine;

public class TankTargetPredictionScript : MonoBehaviour
{
    /// <summary>�^�[�Q�b�g�̃g�����X�t�H�[��</summary>
    [SerializeField, LightColor] private Transform target;

    /// <summary>�^�[�Q�b�g�̑O�t���[���̈ʒu</summary>
    private Vector2 previousTargetPos;

    /// <summary>�^�[�Q�b�g�̈ʒu</summary>
    public Vector2 TargetPos => target.position;

    /// <summary>�^�[�Q�b�g�̈ړ����\��</summary>
    /// <param name="pos">���g�̏ꏊ</param>
    /// <param name="speed">�e��</param>
    /// <returns>�^�[�Q�b�g�̈ړ���</returns>
    public Vector2 PredictionPos(Vector2 pos, float speed)
    {
        Vector2 targetPos = target.position;                       //�^�[�Q�b�g�̈ʒu

        Vector2 targetMoveDistance = targetPos - previousTargetPos;//�^�[�Q�b�g�̈ړ�����

        return targetPos + targetMoveDistance * Vector2.Distance(pos, targetPos) / speed / Time.fixedDeltaTime;//�^�Q�ʒu + �^�Q�ړ����� * (���g�̈ʒu�ƃ^�Q�ʒu�̋���) / �e�� / �f���^�^�C��
    }

    /// <summary>�^�[�Q�b�g�̑O�t���[���̈ʒu���X�V(PredictionPos()�̌�ɌĂяo��)</summary>
    public void UpdatePreviousPos() => previousTargetPos = target.position;//�^�[�Q�b�g�̈ʒu���X�V
}
