using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>�X�e�[�W</summary>
public class StageScript : MonoBehaviour
{
    /// <summary>�X�e�[�W�S�̂̃I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject StageObject;

    /// <summary>�v���C���[�̏����ʒu�Ɗp�x</summary>
    [SerializeField, LightColor] private Transform PlayerPosAndRot;

    /// <summary>�G�l�~�[�̌̒l</summary>
    [SerializeField, LightColor] private TankEnemyInfoScript[] EnemyInfos;

    /// <summary>�G�l�~�[�����ׂē|���ꂽ�ێ��s</summary>
    private Func<IEnumerator> OnAllEnemysDeath;

    /// <summary>�G�l�~�[���|���ꂽ��</summary>
    private int EnemysDeathCount;

    /// <summary>�X�e�[�W�𐶐�</summary>
    /// <param name="enemyPool">EnemyTank��Ԃ��֐�</param>
    /// <param name="bulletPool">�e��Ԃ��֐�</param>
    /// <param name="player">PLayerTank</param>
    /// <param name="onAllEnemysDeath">�S�Ă�Enemy��|�����ۂ̏���</param>
    public void Generate(Func<TankEnemyScript> enemyPool, Func<BulletScript> bulletPool, TankScript player, Func<IEnumerator> onAllEnemysDeath)
    {
        StageObject.SetActive(true);                                   //�X�e�[�W���A�N�e�B�u

        player.IsActive = true;                                        //�v���C���[���A�N�e�B�u

        player.SetPosAndRot(PlayerPosAndRot);                          //�ꏊ�Ɗp�x���Z�b�g

        OnAllEnemysDeath = onAllEnemysDeath;                           //�S�Ă�Enemy��|�����ۂ̏������Z�b�g

        foreach (var info in EnemyInfos)                               //EnemyInfos�̗v�f�����J��Ԃ�
        {
            enemyPool.Invoke().SetInfo(info, bulletPool, OnEnemyDeath);//�G�l�~�[�̌̒l���Z�b�g���A�N�e�B�u
        }
    }

    /// <summary>Enemy�����S�����یĂяo��</summary>
    private IEnumerator OnEnemyDeath()
    {
        if (++EnemysDeathCount >= EnemyInfos.Length)//�G�l�~�[��|���������G�l�~�[�̐������ȏ�Ȃ�
        {
            yield return OnAllEnemysDeath.Invoke(); //�G�l�~�[�����ׂē|���ꂽ�ێ��s����֐����Ăяo��
        }
    }

    /// <summary>�X�e�[�W���N���A</summary>
    /// <param name="enemys">�G�l�~�[�̃v�[�����X�g</param>
    public void Clear(IReadOnlyList<TankEnemyScript> enemys)
    {
        EnemysDeathCount = default;                  //�J�E���g�����Z�b�g

        StageObject.SetActive(false);                //�I�u�W�F�N�g���A�N�e�B�u

        foreach (var e in enemys) e.IsActive = false;//�G�l�~�[���A�N�e�B�u
    }
}
