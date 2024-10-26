using UnityEngine;

public class TankMovingPartsScript : MonoBehaviour
{
    /// <summary>�^���b�g��Transform</summary>
    [SerializeField, LightColor] private Transform turret;

    /// <summary>�L���^�s����Tran</summary>
    [SerializeField, LightColor] private Transform caterpillar;

    /// <summary>�^���b�g��]���̕�Ԓl</summary>
    [SerializeField, Range01] float turretLerp;

    /// <summary>�L���^�s����]���̕�Ԓl</summary>
    [SerializeField, Range01] private float caterpillarLerp;

    /// <summary>�^���b�g�ƃL���^�s����Rotation���Z�b�g</summary>
    public Quaternion SetRotation { set => caterpillar.rotation = turret.rotation = value; }

    public Vector2 TurretForward => turret.forward;

    /// <summary>�G�l�~�[�̏����Z�b�g</summary>
    /// <param name="i">�G�l�~�[�̏��</param>
    public void SetInfo(TankEnemyInfoScript i) => turretLerp = i.TurretLerp;

    /// <summary>�^���b�g���^�[�Q�b�g�̕����֌�����</summary>
    /// <param name="target">�^�[�Q�b�g�̕���</param>
    public void TargetLookAt(Vector3 target)
    {
        turret.LerpRotation(target, turretLerp, Vector3.forward);//�^���b�g���^�[�Q�b�g�̕����ւ̕�Ԓl����
    }

    /// <summary>�L���^�s���[���ړ������Ɍ�����</summary>
    /// <param name="velocity">�ړ��x�N�g��</param>
    public void CaterpillarLookAt(Vector2 velocity)
    {
        if (velocity != Vector2.zero)                                            //�ړ��x�N�g����(0, 0)�ȊO�Ȃ�
        {
            caterpillar.LerpRotation(velocity, caterpillarLerp, Vector3.forward);//�L���^�s���[���ړ������ւ̕�Ԓl����
        }
    }
}
