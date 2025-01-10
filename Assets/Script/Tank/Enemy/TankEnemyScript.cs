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
        UpdateInfo(info);                                 //�����X�V

        cannon.BulletPool = pool;                         //�e�v�[���֐����Z�b�g

        base.onDeath = () =>                              //���S�����Ď��s����֐����Z�b�g
        {
            IsActive = false;                             //��A�N�e�B�u

            stageSystem.StartCoroutine(onDeath.Invoke());//���S���̏������J�n
        };
    }

    /// <summary>�����X�V</summary>
    /// <param name="info">���</param>
    public void UpdateInfo(TankEnemyInfoScript info)
    {
        IsActive = true;                                    //�A�N�e�B�u

        line.Crear();                                       //�e�������Z�b�g

        autoMove.Stop();                                    //�ړ����~

        SetPosAndRot(info.PosAndRot);                       //�ꏊ�Ɗp�x���Z�b�g

        parts.SetInfo(info);                                //�G�l�~�[�̏����Z�b�g

        damage.SetInfo(info);                               //�G�l�~�[�̏����Z�b�g

        fillColor.SetInfo(info);                            //�G�l�~�[�̏����Z�b�g

        line.SetLineColor(info.FillColor);                  //�G�l�~�[�̏����Z�b�g

        cannon.SetInfo(info);                               //�G�l�~�[�̏����Z�b�g

        autoMove.SetInfo(info);                             //�G�l�~�[�̏����Z�b�g

        cannon.ShootIntervalTime = info.ShootInterval;      //���ˊԊu���Z�b�g

        attackType = info.AttackType;                       //�U���^�C�v���Z�b�g

        while (attackType == AttackType.Random)             //�U���^�C�v�������_���Ȍ���J��Ԃ�
        {
            attackType = EnumEx<AttackType>.Values.Random();//�����_���ȍU���^�C�v����
        }
    }

    /// <summary>�^���b�g���^�[�Q�b�g�̕����Ɍ�����</summary>
    private void LookAt()
    {
        parts.TargetLookAt(prediction.TargetPos - autoMove.Pos);//�^���b�g��(�^�[�Q�b�g�̈ʒu - ���g�̌��݈ʒu)�̕����Ɍ�����
    }

    /// <summary>���C���q�b�g ���� �^�[�Q�b�g�ƈ�v��</summary>
    private bool Ray() => search.Ray(prediction.TargetPos);//���C���q�b�g ���� �^�[�Q�b�g�ƈ�v�Ȃ�

    /// <summary>�ʏ�U��</summary>
    private bool AttackNormal()
    {
        LookAt();   //�^���b�g���^�[�Q�b�g�̕����Ɍ�����

        return true;//true��Ԃ�
    }

    /// <summary>�΍��U��</summary>
    private bool AttackDeviation()
    {
        Vector2 pos = prediction.PredictionPos(autoMove.Pos, cannon.BulletSpeed);//�ړ��\�������

        parts.TargetLookAt(pos - autoMove.Pos);                                  //�^���b�g���ړ��\����Ɍ�����

        return true;                                                             //true��Ԃ�
    }

    /// <summary>�^�[�Q�b�g�U��</summary>
    /// <returns>���C���q�b�g������</returns>
    private bool AttackTarget()
    {
        LookAt();    //�^���b�g���^�[�Q�b�g�̕����Ɍ�����

        return Ray();//���C���q�b�g������
    }

    /// <summary>���ˍU��</summary>
    /// <returns>�ː��� or ���˂���ː���Ƀ^�[�Q�b�g�����邩</returns>
    private bool AttackReflection()
    {
        if (Ray())                          //���C���q�b�g������
        {
            LookAt();                       //�^���b�g���^�[�Q�b�g�̕����Ɍ�����

            return true;                    //true��Ԃ�
        }

        parts.TargetLookAt(search.Rotate());//�^���b�g���T�[�`��������Ɍ�����

        return search.ReflectionRay();      //���˂��郌�C���q�b�g������
    }

    /// <summary>�U������ۂ̏���</summary>
    /// <returns>�U�����邩�H</returns>
    private bool AttackMove() => attackType switch  //�U���^�C�v�ŕ���
    {
        AttackType.Normal     => AttackNormal(),    //�^�C�v���ʏ�U��      �Ȃ�/�ʏ�U��
        AttackType.Deviation  => AttackDeviation(), //�^�C�v���΍��U��      �Ȃ�/�΍��U��
        AttackType.Target     => AttackTarget(),    //�^�C�v���^�[�Q�b�g�U���Ȃ�/�^�[�Q�b�g�U��
        AttackType.Reflection => AttackReflection(),//�^�C�v�����ˍU��      �Ȃ�/���ˍU��
        _ => default,                               //����ȊO              �Ȃ�/default�l
    };

    protected override void Move()
    {
        if (AttackMove() && cannon.IsShoot)//���ˊԊu���z���Ă�����
        {
            cannon.Shoot();                //�e�𔭎�

            autoMove.VectorChange();       //�ړ��x�N�g����ύX
        }

        prediction.UpdatePreviousPos();    //�^�[�Q�b�g�ʒu���X�V

        base.Move();                       //�x�[�X���Ăяo��
    }

    protected override void NotMove()
    {
        cannon.ReSetShootInterval();//�C���^�[�o�������Z�b�g

        base.NotMove();             //�x�[�X���Ăяo��
    }
}
