using System;
using UnityEngine;

public class TankCannonScript : MonoBehaviour
{
    /// <summary>�e������SE���Đ�����AudioSource</summary>
    [SerializeField, LightColor] private AudioSource shootSource;

    /// <summary>�e�̏��</summary>
    [SerializeField] private BulletInfo bulletInfo;

    /// <summary>�v�[�����Ă���e���擾����֐�</summary>
    public Func<BulletScript> BulletPool { get; set; }

    /// <summary>�e��</summary>
    public float BulletSpeed => bulletInfo.Speed;

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

        AudioScript.I.TankAudio.Dictionary[TankClip.Shoot].PlayOneShot(shootSource);//���˂���ۂ�SE���Đ�����
    }
}
