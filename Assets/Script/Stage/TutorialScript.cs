using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>�`���[�g���A��</summary>
public class TutorialScript : MonoBehaviour
{
    /// <summary>�A�C�L���b�`</summary>
    [SerializeField, LightColor] private StageEyeCatchUIScript stageEyeCatchUI;

    /// <summary>�^���N�̊Ǘ���</summary>
    [SerializeField, LightColor] private StageTankManagerScript tankManager;

    /// <summary>�X�e�[�W</summary>
    [SerializeField, LightColor] private StageScript stage;

    /// <summary>�`���[�g���A���̃��j���[</summary>
    [SerializeField, LightColor] private TutorialMenuScript tutorialMenu;

    /// <summary>�X�e�[�W��</summary>
    [SerializeField] private string stageName;

    /// <summary>�X�^�[�g�����ێ��s����֐�</summary>
    private UnityAction onStart;

    /// <summary>�`���[�g���A�����A�N�e�B�u����</summary>
    public bool IsActive => tutorialMenu.IsActive;

    /// <summary>�X�^�[�g�����ێ��s����֐����Z�b�g</summary>
    /// <param name="onStart">�X�^�[�g�����ێ��s����֐�</param>
    public void SetOnStartAction(UnityAction onStart) => this.onStart = onStart;

    /// <summary>�t�F�[�h�C�������ۂ̏���</summary>
    public void OnFadeIn()
    {
        stageEyeCatchUI.Display();                  //UI��\��

        tankManager.Player.ReStart();               //�v���C���[�����X�^�[�g

        stageEyeCatchUI.SetStageCount(stageName);   //�X�e�[�W�����Z�b�g

        stage.Active(tankManager, OnAllEnemysDeath);//�X�e�[�W���A�N�e�B�u

        tutorialMenu.IsActive = true;               //�`���[�g���A�����j���[���A�N�e�B�u
    }

    /// <summary>�t�F�[�h�A�E�g�����ۂ̏���</summary>
    public void OnFadeOut()
    {
        stageEyeCatchUI.Fader.Run(null, onStart);//�A�C�L���b�`�̃t�F�[�h���J�n
    }

    /// <summary>���ׂĂ̓G�^���N��|�����ۂ̏���</summary>
    private IEnumerator OnAllEnemysDeath()
    {
        yield break;
    }

    /// <summary>�`���[�g���A���I�����̏���</summary>
    public void OnQuit()
    {
        tankManager.InActive();       //�^���N���A�N�e�B�u

        stage.InActive();             //�X�e�[�W���A�N�e�B�u

        tutorialMenu.IsActive = false;//�`���[�g���A�����j���[���A�N�e�B�u
    }
}
