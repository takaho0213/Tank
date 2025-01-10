using System;
using UnityEngine;

/// <summary>�^���N�̃L���m��</summary>
public class TankCannonScript : MonoBehaviour
{
    /// <summary>�e������SE���Đ�����AudioSource</summary>
    [SerializeField, LightColor] private AudioSource shootSource;

    /// <summary>�e�̏��</summary>
    [SerializeField] private BulletInfo bulletInfo;

    /// <summary>���˂���Ԋu</summary>
    [SerializeField] private float shootIntervalTime;

    /// <summary>���˂���C���^�[�o��</summary>
    private Interval shootInterval;

    /// <summary>�v�[�����Ă���e���擾����֐�</summary>
    public Func<BulletScript> BulletPool { get; set; }

    /// <summary>�e��</summary>
    public float BulletSpeed => bulletInfo.Speed;

    /// <summary>���˂���Ԋu</summary>
    public float ShootIntervalTime { set => shootInterval.Time = value; }

    /// <summary>���ˊԊu�����Ă��邩</summary>
    public bool IsShoot => shootInterval;

    /// <summary>������</summary>
    public void Init()
    {
        shootInterval = new Interval(shootIntervalTime, true);
    }

    /// <summary>�G�l�~�[�̏����Z�b�g����֐�</summary>
    /// <param name="i">�G�l�~�[�̏��</param>
    public void SetInfo(TankEnemyInfoScript i)
    {
        bulletInfo.Init(i.BulletSpeed, i.BulletReflectionCount, i.FillColor);//�e�̏����Z�b�g����֐����Ăяo��
    }

    /// <summary>�e�𔭎�</summary>
    public void Shoot()
    {
        BulletPool.Invoke().Shoot(bulletInfo);                                 //�v�[������Ă���e���擾���A���˂���֐��̈����ɒe�̏������Ăяo��

        AudioScript.I.TankAudio[TankClip.Shoot].PlayOneShot(shootSource);//���˂���ۂ�SE���Đ�����
    }

    /// <summary>���ˊԊu�����Z�b�g</summary>
    public void ReSetShootInterval() => shootInterval.ReSet();
}
