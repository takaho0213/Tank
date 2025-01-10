using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>�X�e�[�W��̃^���N�̊Ǘ���</summary>
public class StageTankManagerScript : MonoBehaviour
{
    /// <summary>�v���C���[�^���N</summary>
    [SerializeField, LightColor] private TankPlayerScript player;

    /// <summary>�G�l�~�[�̃v�[�����X�g</summary>
    [SerializeField] private PoolList<TankEnemyScript> enemyList;

    /// <summary>�e�̃v�[�����X�g</summary>
    [SerializeField] private PoolList<BulletScript> bulletList;

    /// <summary>�v���C���[�^���N</summary>
    public TankPlayerScript Player => player;

    /// <summary>�v�[������Ă����A�N�e�B�u�ȓG�^���N���擾����֐�</summary>
    public System.Func<TankEnemyScript> GetPoolEnemy { get; private set; }

    /// <summary>�v�[������Ă����A�N�e�B�u�Ȓe���擾����֐�</summary>
    public System.Func<BulletScript> GetPoolBullet { get; private set; }

    /// <summary>�v�[������Ă���G�^���N�̃��X�g</summary>
    public IReadOnlyList<TankEnemyScript> PoolEnemyList => enemyList;

    /// <summary>������</summary>
    /// <param name="onPlayerDeath">�v���C���[�����S�����Ď��s����֐�</param>
    public void Init(System.Func<bool, IEnumerator> onPlayerDeath)
    {
        GetPoolEnemy = enemyList.GetObject;       //�v�[������Ă����A�N�e�B�u�ȓG�^���N���擾����֐�����
        GetPoolBullet = bulletList.GetObject;     //�v�[������Ă����A�N�e�B�u�Ȓe���擾����֐�����

        player.Init(GetPoolBullet, onPlayerDeath);//�v���C���[�̊֐����Z�b�g
    }

    /// <summary>�v���C���[�̃��C�t��ǉ�</summary>
    /// <param name="stageCount">���݂̃X�e�[�W��</param>
    public void PlayerAddLife(int stageCount)
    {
        if (player.Life.IsAddLife(stageCount))//���C�t�𑝂₷�X�e�[�W���Ȃ�
        {
            player.Life.AddLife();            //�v���C���[�̃��C�t�𑝂₷
        }

        player.HealthRecovery();              //�̗͂���
    }

    /// <summary>��A�N�e�B�u</summary>
    public void InActive()
    {
        player.IsActive = false;     //�v���C���[���A�N�e�B�u

        foreach (var e in enemyList) //�G�^���N�𐔕��J��Ԃ�
        {
            e.IsActive = false;      //�G�l�~�[���A�N�e�B�u
        }

        foreach (var b in bulletList)//�e�̃v�[�����X�g���J��Ԃ�
        {
            b.Inactive();            //�e���A�N�e�B�u
        }
    }
}
