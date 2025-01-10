using UnityEngine;

/// <summary>EnemyTank�̏�����͂���t�B�[���h</summary>
public class EnemyInfoFieldScript : MonoBehaviour
{
    /// <summary>�̗͂̃t�B�[���h</summary>
    [SerializeField, LightColor] private SliderFieldScript health;

    /// <summary>�ړ��^�C�v�̃t�B�[���h</summary>
    [SerializeField, LightColor] private MoveTypeFieldScript moveType;

    /// <summary>�ړ����x�̃t�B�[���h</summary>
    [SerializeField, LightColor] private SliderFieldScript moveSpeed;

    /// <summary>�ړ����x�Œ�̃t�B�[���h</summary>
    [SerializeField, LightColor] private IsNormalizedFieldScript isMoveVectorNormalized;

    /// <summary>�^���b�g��]���x�̃t�B�[���h</summary>
    [SerializeField, LightColor] private SliderFieldScript turretLerp;

    /// <summary>�U���^�C�v�̃t�B�[���h</summary>
    [SerializeField, LightColor] private AttackTypeFieldScript attackType;

    /// <summary>�ˌ��Ԋu�̃t�B�[���h</summary>
    [SerializeField, LightColor] private SliderFieldScript shootInterval;

    /// <summary>�e���̃t�B�[���h</summary>
    [SerializeField, LightColor] private SliderFieldScript bulletSpeed;

    /// <summary>�e�̔��ː��̃t�B�[���h</summary>
    [SerializeField, LightColor] private SliderFieldScript bulletReflectionCount;

    /// <summary>�J���[�̃t�B�[���h</summary>
    [SerializeField, LightColor] private ColorFieldScript fillColor;

    /// <summary>EnemyTank�̏��</summary>
    [SerializeField, LightColor] private TankEnemyInfoScript info;

    /// <summary>�t�B�[���h�̏����Z�b�g</summary>
    /// <returns>EnemyTank�̏��</returns>
    public TankEnemyInfoScript SetInfo()
    {
        var h = health.IntValue;               //�̗͂̒l
        var m = moveType.Value;                //�ړ��^�C�v�̒l
        var s = moveSpeed.Value;               //�ړ����x�̒l
        var n = isMoveVectorNormalized.Value;  //�ړ����x�Œ�̒l
        var l = turretLerp.Value;              //�^���b�g�̉�]���x�̒l
        var a = attackType.Value;              //�U���^�C�v�̒l
        var i = shootInterval.Value;           //�ˌ��Ԋu�̒l
        var b = bulletSpeed.Value;             //�e���̒l
        var r = bulletReflectionCount.IntValue;//�e�̔��ː��̒l
        var c = fillColor.Value;               //�J���[�̒l

        info.Set(h, m, s, n, l, a, i, b, r, c);//�����Z�b�g

        return info;                           //EnemyTank�̏���Ԃ�
    }
}
