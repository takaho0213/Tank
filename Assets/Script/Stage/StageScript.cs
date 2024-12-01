using System;
using UnityEngine;
using System.Collections;

/// <summary>�X�e�[�W</summary>
public class StageScript : MonoBehaviour
{
    /// <summary>�X�e�[�W�S�̂̃I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject stageObject;

    /// <summary>�v���C���[�̏����ʒu�Ɗp�x</summary>
    [SerializeField, LightColor] private Transform playerPosAndRot;

    /// <summary>�G�l�~�[�̌̒l</summary>
    [SerializeField, LightColor] private TankEnemyInfoScript[] enemyInfos;

    /// <summary>�G�l�~�[�����ׂē|���ꂽ�ێ��s</summary>
    private Func<IEnumerator> OnAllEnemysDeath;

    /// <summary>�G�l�~�[���|���ꂽ��</summary>
    private int enemysDeathCount;

    /// <summary>�X�e�[�W�𐶐�</summary>
    /// <param name="onAllEnemysDeath">�S�Ă�Enemy��|�����ۂ̏���</param>
    public void Active(StageTankManagerScript tankManager, Func<IEnumerator> onAllEnemysDeath)
    {
        stageObject.SetActive(true);                                   //�X�e�[�W���A�N�e�B�u

        OnAllEnemysDeath = onAllEnemysDeath;                           //�S�Ă�Enemy��|�����ۂ̏������Z�b�g

        tankManager.Player.IsActive = true;                                        //�v���C���[���A�N�e�B�u

        tankManager.Player.SetPosAndRot(playerPosAndRot);                          //�ꏊ�Ɗp�x���Z�b�g

        foreach (var info in enemyInfos)                               //EnemyInfos�̗v�f�����J��Ԃ�
        {
            tankManager.GetEnemy.Invoke().SetInfo(info, tankManager.GetBullet, OnEnemyDeath);//�G�l�~�[�̌̒l���Z�b�g���A�N�e�B�u
        }
    }

    /// <summary>Enemy�����S�����یĂяo��</summary>
    private IEnumerator OnEnemyDeath()
    {
        if (++enemysDeathCount >= enemyInfos.Length)//�G�l�~�[��|���������G�l�~�[�̐������ȏ�Ȃ�
        {
            yield return OnAllEnemysDeath.Invoke(); //�G�l�~�[�����ׂē|���ꂽ�ێ��s����֐����Ăяo��
        }
    }

    /// <summary>�X�e�[�W���N���A</summary>
    public void InActive()
    {
        enemysDeathCount = default;                  //�J�E���g�����Z�b�g

        stageObject.SetActive(false);                //�I�u�W�F�N�g���A�N�e�B�u
    }

    /// <summary>�G�l�~�[���|���ꂽ�������Z�b�g</summary>
    public void ReSetEnemyDeathCount() => enemysDeathCount = default;//�J�E���g�����Z�b�g
}
