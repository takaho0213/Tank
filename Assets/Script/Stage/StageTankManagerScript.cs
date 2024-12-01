using UnityEngine;
using System.Collections;

public class StageTankManagerScript : MonoBehaviour
{
    /// <summary>�v���C���[�^���N</summary>
    [SerializeField, LightColor] private TankPlayerScript player;

    /// <summary>�G�l�~�[�̃v�[�����X�g</summary>
    [SerializeField] private PoolList<TankEnemyScript> enemyList;

    /// <summary>�e�̃v�[�����X�g</summary>
    [SerializeField] private PoolList<BulletScript> bulletList;

    public TankPlayerScript Player => player;

    public System.Func<TankEnemyScript> GetEnemy { get; private set; }

    public System.Func<BulletScript> GetBullet { get; private set; }

    public void Init(System.Func<bool, IEnumerator> onPlayerDeath)
    {
        GetEnemy = enemyList.GetObject;
        GetBullet = bulletList.GetObject;

        player.Init(GetBullet, onPlayerDeath);                        //�v���C���[�̊֐����Z�b�g
    }

    public void PlayerAddLife(int stageCount)
    {
        if (player.Life.IsAddLife(stageCount))//���C�t�𑝂₷�X�e�[�W���Ȃ�
        {
            player.Life.AddLife();            //�v���C���[�̃��C�t�𑝂₷
        }

        player.HealthRecovery();              //�̗͂���
    }

    /// <summary></summary>
    public void InActive()
    {
        player.IsActive = false;     //�v���C���[���A�N�e�B�u

        foreach (var e in enemyList)
        {
            e.IsActive = false;//�G�l�~�[���A�N�e�B�u
        }

        foreach (var b in bulletList)//�e�̃v�[�����X�g���J��Ԃ�
        {
            b.Inactive();            //�e���A�N�e�B�u
        }
    }
}
