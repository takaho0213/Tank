using UnityEngine;

/// <summary>�G�l�~�[�^���N�̏��</summary>
public class TankEnemyInfoScript : MonoBehaviour
{
    /// <summary>�����ʒu�Ɗp�x</summary>
    [field: AddLabel("�����ʒu�Ɗp�x")]
    [field: SerializeField, LightColor] public Transform PosAndRot { get; private set; }

    [field: Space]

    /// <summary>�̗�</summary>
    [field: AddLabel("�̗�")]
    [field: SerializeField] public int Health { get; private set; }

    [field: Space]

    /// <summary>�ړ��^�C�v</summary>
    [field: AddLabel("�ړ��^�C�v")]
    [field: SerializeField] public TankAutoMoveScript.MoveType MoveType { get; private set; }

    /// <summary>�ړ����x</summary>
    [field: AddLabel("�ړ����x")]
    [field: SerializeField] public float MoveSpeed { get; private set; }

    /// <summary>�ړ��x�N�g���𐳋K�����邩</summary>
    [field: AddLabel("�ړ��x�N�g���𐳋K�����邩")]
    [field: SerializeField] public bool IsMoveVectorNormalized { get; private set; }

    [field: Space]

    /// <summary>�^���b�g��]���̕�Ԓl</summary>
    [field: AddLabel("�^���b�g��]���̕�Ԓl")]
    [field: SerializeField, Range01()] public float TurretLerp { get; private set; }

    [field: Space]

    /// <summary>�U���^�C�v</summary>
    [field: AddLabel("�U���^�C�v")]
    [field: SerializeField] public TankEnemyScript.AttackType AttackType { get; private set; }

    /// <summary>���ˊԊu</summary>
    [field: AddLabel("���ˊԊu(�b)")]
    [field: SerializeField] public float ShootInterval { get; private set; }

    /// <summary>�e��</summary>
    [field: AddLabel("�e��")]
    [field: SerializeField] public float BulletSpeed { get; private set; }

    /// <summary>�e�̔��ː�</summary>
    [field: AddLabel("�e�̔��ː�")]
    [field: SerializeField] public int BulletReflectionCount { get; private set; }

    [field: Space]

    /// <summary>�F</summary>
    [field: AddLabel("�F")]
    [field: SerializeField] public Color FillColor { get; private set; }
}
