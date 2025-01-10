using System;
using UnityEngine;
using System.Collections;

/// <summary>�X�e�[�W�̊Ǘ�</summary>
public class StageManagerScript : MonoBehaviour
{
    /// <summary>�S�X�e�[�W</summary>
    [SerializeField] private StageScript[] stages;

    /// <summary>�S�X�e�[�W�N���A�^�C���v���p</summary>
    [SerializeField] private IntervalScript allStageClearTime;

    /// <summary>���݂̃X�e�[�W</summary>
    private StageScript currentStage;

    /// <summary>���݂̃X�e�[�W��</summary>
    public int StageCount { get; private set; }

    /// <summary>���̃X�e�[�W��</summary>
    public int NextStageCount
    {
        get
        {
            const int AddCount = 1;      //�ǉ����鐔

            return StageCount + AddCount;//���݂̃X�e�[�W�� + �ǉ����鐔��Ԃ�
        }
    }

    /// <summary>���X�g�X�e�[�W��</summary>
    public bool IsLastStage => NextStageCount >= stages.Length;

    /// <summary>�S�X�e�[�W�̃N���A�^�C��</summary>
    public IntervalScript ClearTime => allStageClearTime;

    private void Start()
    {
        currentStage = stages[default];//�ŏ��̃X�e�[�W����
    }

    /// <summary>�X�e�[�W���� + 1</summary>
    public void AddStageCount() => StageCount++;//�X�e�[�W���𑝂₷

    /// <summary>�X�e�[�W����0�ɂ���</summary>
    public void ReSetStageCount() => StageCount = default;//���݂̃X�e�[�W����0�ɂ���

    /// <summary>�G�^���N�̓|���ꂽ����0�ɂ���</summary>
    public void Retry() => currentStage.ReSetEnemyDeathCount();//�G�^���N�̓|���ꂽ����0�ɂ���

    /// <summary>���݂̃X�e�[�W�𐶐�</summary>
    /// <param name="tankManager">�^���N�̊Ǘ���</param>
    /// <param name="onAllEnemysDeath">�S�Ă̓G�^���N��|�����ێ��s����֐�</param>
    public void ActiveStage(StageTankManagerScript tankManager, Func<IEnumerator> onAllEnemysDeath)
    {
        currentStage = stages[StageCount];                 //���݂̃X�e�[�W����

        currentStage.Active(tankManager, onAllEnemysDeath);//���݂̃X�e�[�W�𐶐�
    }

    /// <summary>�X�e�[�W���A�N�e�B�u</summary>
    public void InActiveStage() => currentStage.InActive();//���݂̃X�e�[�W���A�N�e�B�u
}
