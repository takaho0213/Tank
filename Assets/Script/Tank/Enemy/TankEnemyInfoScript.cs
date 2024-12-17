using UnityEngine;
using static TankEnemyScript;
using static TankAutoMoveScript;

/// <summary>�G�l�~�[�^���N�̏��</summary>
public class TankEnemyInfoScript : MonoBehaviour
{
    /// <summary>�����ʒu�Ɗp�x</summary>
    [field: AddLabel("�����ʒu�Ɗp�x"), SerializeField, LightColor]
    public Transform PosAndRot { get; private set; }

    [field: Space]

    /// <summary>�̗�</summary>
    [field: AddLabel("�̗�"), SerializeField] 
    public int Health { get; private set; }

    [field: Space]

    /// <summary>�ړ��^�C�v</summary>
    [field: AddLabel("�ړ��^�C�v"), SerializeField] 
    public MoveType MoveType { get; private set; }

    /// <summary>�ړ����x</summary>
    [field: AddLabel("�ړ����x"), SerializeField] 
    public float MoveSpeed { get; private set; }

    /// <summary>�ړ��x�N�g���𐳋K�����邩</summary>
    [field: AddLabel("�ړ��x�N�g���𐳋K�����邩"), SerializeField] 
    public bool IsMoveVectorNormalized { get; private set; }

    [field: Space]

    /// <summary>�^���b�g��]���̕�Ԓl</summary>
    [field: AddLabel("�^���b�g��]���̕�Ԓl"), SerializeField, Range01()]
    public float TurretLerp { get; private set; }

    [field: Space]

    /// <summary>�U���^�C�v</summary>
    [field: AddLabel("�U���^�C�v"), SerializeField]
    public AttackType AttackType { get; private set; }

    /// <summary>���ˊԊu</summary>
    [field: AddLabel("���ˊԊu(�b)"), SerializeField]
    public float ShootInterval { get; private set; }

    /// <summary>�e��</summary>
    [field: AddLabel("�e��"), SerializeField]
    public float BulletSpeed { get; private set; }

    /// <summary>�e�̔��ː�</summary>
    [field: AddLabel("�e�̔��ː�"), SerializeField]
    public int BulletReflectionCount { get; private set; }

    [field: Space]

    /// <summary>�F</summary>
    [field: AddLabel("�F"), SerializeField]
    public Color FillColor { get; private set; }

    /// <summary>�l���Z�b�g</summary>
    /// <param name="p">�����ʒu�Ɗp�x</param>
    /// <param name="h">�̗�</param>
    /// <param name="m">�ړ��^�C�v</param>
    /// <param name="s">�ړ����x</param>
    /// <param name="n">�ړ��x�N�g���𐳋K�����邩</param>
    /// <param name="l">�^���b�g��]���̕�Ԓl</param>
    /// <param name="a">�U���^�C�v</param>
    /// <param name="i">���ˊԊu</param>
    /// <param name="b">�e��</param>
    /// <param name="r">�e�̔��ː�</param>
    /// <param name="c">�F</param>
    public void Set(int h, MoveType m, float s, bool n, float l,  AttackType a, float i, float b, int r, Color c)
    {
        Health = h;                     //�̗͂���
        MoveType = m;                   //�ړ��^�C�v����
        MoveSpeed = s;                  //�ړ����x����
        IsMoveVectorNormalized = n;     //�ړ��x�N�g���𐳋K�����邩����
        TurretLerp = l;                 //�^���b�g��]���̕�Ԓl����
        AttackType = a;                 //�U���^�C�v����
        ShootInterval = i;              //���ˊԊu����
        BulletSpeed = b;                //�e������
        BulletReflectionCount = r;      //�e�̔��ː�����
        FillColor = c;                  //�F����
    }
}
