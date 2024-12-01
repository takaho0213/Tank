using System;
using UnityEngine;
using System.Collections;

/// <summary>�X�e�[�W�̊Ǘ�</summary>
public class StageManagerScript : MonoBehaviour
{
    /// <summary>�S�X�e�[�W</summary>
    [SerializeField] private StageScript[] stages;

    /// <summary>���݂̃X�e�[�W</summary>
    private StageScript currentStage;

    /// <summary>�O�X�e�[�W�N���A�^�C���v���p</summary>
    private Interval allStageClearTime;

    /// <summary>���݂̃X�e�[�W��</summary>
    public int StageCount { get; private set; }

    /// <summary>�X�e�[�W��Audio</summary>
    private AudioInfoDictionary<StageClip> stageAudio;

    /// <summary>���̃X�e�[�W��</summary>
    public int NextStageCount
    {
        get
        {
            const int AddCount = 1;

            return StageCount + AddCount;
        }
    }

    public float ElapsedTime => allStageClearTime.ElapsedTime;

    public bool IsMaxStage => NextStageCount >= stages.Length;

    private void Start()
    {
        stageAudio = AudioScript.I.StageAudio;

        allStageClearTime = new Interval();                                      //Interval���C���X�^���X��

        currentStage = stages[default];                                          //�ŏ��̃X�e�[�W����
    }

    /// <summary></summary>
    public void ReSetTime() => allStageClearTime.ReSet();//�X�R�A�����Z�b�g

    /// <summary></summary>
    public void ReSetStageCount() => StageCount = default;//���݂̃X�e�[�W����0�ɂ���

    public void Retry() => currentStage.ReSetEnemyDeathCount();

    /// <summary>���݂̃X�e�[�W�𐶐�</summary>
    public void ActiveStage(StageTankManagerScript tankManager, Func<IEnumerator> onAllEnemysDeath)
    {
        currentStage = stages[StageCount];                 //���݂̃X�e�[�W����

        currentStage.Active(tankManager, onAllEnemysDeath);//���݂̃X�e�[�W�𐶐�
    }

    public void InActiveStage() => currentStage.InActive();

    /// <summary>���̃X�e�[�W�ֈڍs</summary>
    public void NextStage() => StageCount++;//�X�e�[�W���𑝂₷
}
