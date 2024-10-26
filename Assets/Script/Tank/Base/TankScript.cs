using UnityEngine;
using UnityEngine.Events;

/// <summary>�x�[�X�̃^���N</summary>
public abstract class TankScript : MonoBehaviour
{
    /// <summary>�}�l�[�W���[</summary>
    [SerializeField, LightColor] protected StageManagerScript stageManager;

    /// <summary>���g��Object</summary>
    [SerializeField, LightColor] protected GameObject obj;

    /// <summary>���g��Transform</summary>
    [SerializeField, LightColor] protected Transform trafo;

    /// <summary>����</summary>
    [SerializeField, LightColor] protected TankCannonScript cannon;

    /// <summary>�^���b�g �L���^�s���[</summary>
    [SerializeField, LightColor] protected TankMovingPartsScript parts;

    /// <summary>�_���[�W����</summary>
    [SerializeField, LightColor] protected TankDamageScript damage;

    /// <summary>�F</summary>
    [SerializeField, LightColor] protected TankFillColorScript fillColor;

    [SerializeField, LightColor] protected TankBulletLineScript line;

    /// <summary>����Interval</summary>
    [SerializeField] protected Interval shootInterval;

    /// <summary>���S���o���I������ێ��s</summary>
    protected UnityAction onDeath;

    /// <summary>�ړ��ł��Ȃ���Ԃ�</summary>
    protected bool IsNotMove => stageManager.IsNotMove || damage.IsDeath;

    /// <summary>�A�N�e�B�u��</summary>
    public bool IsActive
    {
        get => obj.activeSelf;
        set => obj.SetActive(value);
    }

    protected virtual void OnEnable() => fillColor.SetColor();//�A�N�e�B�u�ɂȂ�����/�F���Z�b�g

    protected virtual void Awake()
    {
        fillColor.SetColor();              //�F���Z�b�g

        line.Init(fillColor.Color);        //

        damage.OnHealthGone = OnHealthGone;//�̗͂�0�ɂȂ����ێ��s����֐����Z�b�g
    }

    /// <summary>position��rotation���Z�b�g</summary>
    /// <param name="trafo">�Z�b�g����position��rotation</param>
    public void SetPosAndRot(Transform trafo)
    {
        this.trafo.position = trafo.position;   //�ʒu����
        parts.SetRotation = trafo.rotation;//�p�x����

        line.Crear();
    }

    /// <summary>Health��0�ɂȂ����ێ��s</summary>
    protected virtual void OnHealthGone()
    {
        fillColor.SetGray();                        //�F���O���[�ɂ���

        line.Crear();

        StartCoroutine(damage.DeathEffect(onDeath));//���S���o���J�n
    }

    protected virtual void OnTriggerEnter2D(Collider2D hit)
    {
        if (IsNotMove) return;                                 //�ړ��ł��Ȃ���ԂȂ�

        StartCoroutine(damage.Hit(hit, fillColor.DamageColor));//�_���[�W�G�t�F�N�g���J�n
    }
}
