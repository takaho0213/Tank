using UnityEngine;
using System.Collections;

/// <summary>�G�l�~�[�̃^���N</summary>
public class TankEnemyScript : TankScript
{
    /// <summary>�U���^�C�v</summary>
    public enum AttackType
    {
        /// <summary>�m�[�}��</summary>
        Normal,
        /// <summary>�΍�����</summary>
        Deviation,
        /// <summary>�ː��Ƀ^�[�Q�b�g������Ȃ�</summary>
        Target,
        /// <summary>�ː����ː�Ƀ^�[�Q�b�g������Ȃ�</summary>
        Reflection,
        /// <summary>�����_��</summary>
        Random,
    }

    /// <summary>�����ړ�</summary>
    [SerializeField, LightColor] private TankAutoMoveScript autoMove;

    /// <summary>�^�[�Q�b�g</summary>
    [SerializeField, LightColor] private TankTargetPredictionScript prediction;

    /// <summary>�T�[�`</summary>
    [SerializeField, LightColor] private TankTargetSearchScript search;

    /// <summary>�U���^�C�v</summary>
    [SerializeField] private AttackType attackType;

    protected override TankMoveScript BaseMove => autoMove;

    /// <summary>�̒l���Z�b�g</summary>
    /// <param name="info">�̏��</param>
    /// <param name="onDeath">���S���Ɏ��s����R�[���o�b�N</param>
    public void SetInfo(TankEnemyInfoScript info, System.Func<BulletScript> pool, System.Func<IEnumerator> onDeath)
    {
        IsActive = true;                                  //�A�N�e�B�u

        autoMove.Stop();                                  //�ړ����~

        cannon.BulletPool = pool;                         //�e�v�[���֐����Z�b�g

        parts.SetInfo(info);                              //�G�l�~�[�̏����Z�b�g

        damage.SetInfo(info);                             //�G�l�~�[�̏����Z�b�g

        fillColor.SetInfo(info);                          //�G�l�~�[�̏����Z�b�g

        line.SetLineColor(info.FillColor);                //�G�l�~�[�̏����Z�b�g

        cannon.SetInfo(info);                             //�G�l�~�[�̏����Z�b�g

        autoMove.SetInfo(info);                           //�G�l�~�[�̏����Z�b�g

        line.Crear();                                     //�e�������Z�b�g

        SetPosAndRot(info.PosAndRot);                     //�ꏊ�Ɗp�x���Z�b�g

        cannon.ShootIntervalTime = info.ShootInterval;          //���ˊԊu���Z�b�g

        attackType = info.AttackType;                           //�U���^�C�v���Z�b�g

        base.onDeath = () =>                                   //���S�����Ď��s����֐����Z�b�g
        {
            IsActive = false;                             //��A�N�e�B�u

            stageSystem.StartCoroutine(onDeath.Invoke());//���S���̏������J�n
        };

        while (attackType == AttackType.Random)                                 //�U���^�C�v�������_���Ȍ���J��Ԃ�
        {
            attackType = EnumEx<AttackType>.Values.Random();
        }
    }

    /// <summary>�^���b�g���^�[�Q�b�g�̕����Ɍ�����</summary>
    private void LookAt()
    {
        parts.TargetLookAt(prediction.TargetPos - autoMove.Pos);//�^���b�g��(�^�[�Q�b�g�̈ʒu - ���g�̌��݈ʒu)�̕����Ɍ�����
    }

    /// <summary>���C���q�b�g ���� �^�[�Q�b�g�ƈ�v��</summary>
    private bool Ray() => search.Ray(prediction.TargetPos);//���C���q�b�g ���� �^�[�Q�b�g�ƈ�v�Ȃ�

    private bool AttackNormal()
    {
        LookAt();

        return true;
    }

    private bool AttackDeviation()
    {
        Vector2 pos = prediction.PredictionPos(autoMove.Pos, cannon.BulletSpeed);//

        parts.TargetLookAt(pos - autoMove.Pos);                                  //

        return true;
    }

    private bool AttackTarget()
    {
        LookAt();    //

        return Ray();//
    }

    private bool AttackReflection()
    {
        if (Ray())                          //
        {
            LookAt();                       //

            return true;                    //
        }

        parts.TargetLookAt(search.Rotate());//

        return search.ReflectionRay();      //
    }

    private bool AttackMove() => attackType switch
    {
        AttackType.Normal     => AttackNormal(),
        AttackType.Deviation  => AttackDeviation(),
        AttackType.Target     => AttackTarget(),
        AttackType.Reflection => AttackReflection(),
        _ => default,
    };

    /// <summary>�������܂Ƃ߂��֐�</summary>
    protected override void Move()
    {
        if (AttackMove() && cannon.IsShoot)//���ˊԊu���z���Ă�����
        {
            cannon.Shoot();                //�e�𔭎�

            autoMove.VectorChange();       //�ړ��x�N�g����ύX
        }

        prediction.UpdatePreviousPos();    //�^�[�Q�b�g�ʒu���X�V

        base.Move();                       //
    }

    protected override void NotMove()
    {
        cannon.ReSetShootInterval();//�C���^�[�o�������Z�b�g

        base.NotMove();
    }
}
