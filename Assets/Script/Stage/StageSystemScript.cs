using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>�X�e�[�W�V�X�e��</summary>
public class StageSystemScript : MonoBehaviour
{
    /// <summary>�^�C�g��</summary>
    [SerializeField, LightColor] private TitleScript title;

    /// <summary>�X�e�[�W�I��UI</summary>
    [SerializeField, LightColor] private StageEndUIScript stageEndUI;

    /// <summary>�X�R�AUI</summary>
    [SerializeField, LightColor] private ScoreUIScript scoreUI;

    /// <summary>�A�C�L���b�`</summary>
    [SerializeField, LightColor] private StageEyeCatchScript eyecatch;

    /// <summary>�^���N�̊Ǘ���</summary>
    [SerializeField, LightColor] private StageTankManagerScript tankManager;

    /// <summary>�X�e�[�W�̊Ǘ���</summary>
    [SerializeField, LightColor] private StageManagerScript stageManager;

    /// <summary>���j���[</summary>
    [SerializeField, LightColor] private MenuScript menu;

    /// <summary>�`���[�g���A��</summary>
    [SerializeField, LightColor] private TutorialScript tutorial;

    /// <summary>�t���[�����[�g</summary>
    [SerializeField, ReadOnly] private int frameRate;

    /// <summary>�I������</summary>
    private System.Func<bool> isBreak;

    /// <summary>�^���N�������Ȃ���Ԃ�</summary>
    private bool isNotMove;

    /// <summary>�X�e�[�W��Audio</summary>
    private AudioInfoDictionary<StageClip> stageAudio;

    /// <summary>�`���[�g���A������</summary>
    public bool IsTutorial => tutorial.IsActive;

    /// <summary>�^���N�������Ȃ���Ԃ�</summary>
    public bool IsNotMove => title.IsRun || eyecatch.IsNotMove || isNotMove;

    private void Start()
    {
        ApplicationEx.SetFrameRate(frameRate);                 //�t���[�����[�g���Œ�

        stageAudio = AudioScript.I.StageAudio;                 //�X�e�[�W�̃I�[�f�B�I

        isBreak = () => !stageAudio[StageClip.Clear].IsPlaying;//�I����������

        title.Init(OnReStartFadeIn, () => Fade(ActiveStage));  //�^�C�g����������

        tankManager.Init(OnPlayerDeath);                       //�v���C���[�����S�����ێ��s����֐����Z�b�g

        tutorial.SetOnStartAction(() => isNotMove = false);    //�`���[�g���A����������

        menu.AddOnClickBackTitleButton(() =>                   //�^�C�g���ɖ߂�{�^���������ꂽ�ێ��s����֐���ǉ�
        {
            isNotMove = stageManager.ClearTime.IsStop = true;  //�ړ��o���Ȃ���Ԃɂ���

            title.FadeActive(() =>                             //�t�F�[�h���A�N�e�B�u�ɂ���ێ��s����֐���ǉ�
            {
                InActiveStage();                               //�X�e�[�W���A�N�e�B�u

                tutorial.OnQuit();                             //�`���[�g���A�����I��
            });
        });

        tankManager.Player.ReSetTank();                        //�v���C���[�^���N�����Z�b�g

        isNotMove = true;                                      //�ړ��ł��Ȃ���Ԃɂ���
    }

    /// <summary>���X�^�[�g�̃t�F�[�h�C�������s</summary>
    private void OnReStartFadeIn()
    {
        eyecatch.DisplayUI();        //�A�C�L���b�`��\��

        tankManager.Player.ReStart();//���X�^�[�g

        InActiveStage();             //�X�e�[�W���Z�b�g
    }

    /// <summary>�v���C���[���S���Ɏ��s</summary>
    /// <param name="isNoLife">���C�t���c���Ă��Ȃ���</param>
    private IEnumerator OnPlayerDeath(bool isNoLife)
    {
        if (isNotMove) yield break;                                   //�������Ȃ���ԂȂ�/�I��

        isNotMove = stageManager.ClearTime.IsStop = true;             //�����Ȃ���Ԃ���true

        var audio = stageAudio[StageClip.PlayerDeath];                //�v���C���[���SAudio

        audio.Play();                                                 //SE���Đ�

        var type = isNoLife ? UI.GameOver : UI.Retry;                 //�\��UI

        yield return stageEndUI.Display(type, () => !audio.IsPlaying);//UI��\��

        if (isNoLife) OnPlayerGameOver();                             //���C�t���������/OnPlayerGameOver()�����s
        else Fade(OnPlayerRetry);                                     //����ȊO�Ȃ�/�A�C�L���b�`���t�F�[�h���A�t�F�[�h�C����OnPlayerRetry()�����s

        isNotMove = false;                                            //�����Ȃ���Ԃ���false
    }

    /// <summary>�A�C�L���b�`���t�F�[�h����֐�</summary>
    /// <param name="c">�t�F�[�h�C�������s����֐�</param>
    private void Fade(UnityAction c)
    {
        eyecatch.Fade(c, () => isNotMove = stageManager.ClearTime.IsStop = false);//�A�C�L���b�`���t�F�[�h������
    }

    /// <summary>Player���Q�[���I�[�o�[�����ێ��s</summary>
    private void OnPlayerGameOver()
    {
        stageManager.ReSetStageCount();    //�X�e�[�W��0�ɂ���

        title.FadeActive(() =>             //�t�F�[�h���A�N�e�B�u�ɂ���ۂ̊֐����Z�b�g
        {
            InActiveStage();               //�X�e�[�W���A�N�e�B�u

            tankManager.Player.ReSetTank();//�v���C���[�^���N�����Z�b�g
        });
    }

    /// <summary>Player�����g���C�����ێ��s</summary>
    private void OnPlayerRetry()
    {
        tankManager.Player.OnRetry();//�v���C���[�����g���C

        tankManager.InActive();      //���݂̃X�e�[�W�����Z�b�g

        ActiveStage();               //���݂̃X�e�[�W�𐶐�

        stageManager.Retry();        //�G�^���N�̓|���ꂽ����0�ɂ���
    }

    /// <summary>���݂̃X�e�[�W�𐶐�</summary>
    private void ActiveStage()
    {
        stageManager.ActiveStage(tankManager, OnAllEnemysDeath);                         //�X�e�[�W���A�N�e�B�u

        eyecatch.SetText(stageManager.NextStageCount, tankManager.Player.Life.LifeCount);//�X�e�[�W�e�L�X�g���Z�b�g
    }

    /// <summary>�X�e�[�W���A�N�e�B�u</summary>
    private void InActiveStage()
    {
        tankManager.InActive();      //�^���N���A�N�e�B�u

        stageManager.InActiveStage();//�X�e�[�W���A�N�e�B�u
    }

    /// <summary>Enemy�����ׂē|�����ێ��s</summary>
    private IEnumerator OnAllEnemysDeath()
    {
        if (isNotMove) yield break;                                          //�����Ȃ���ԂȂ�/�I��

        isNotMove = stageManager.ClearTime.IsStop = true;                    //�ړ��o���Ȃ���Ԃɂ���

        stageAudio[StageClip.BGM].Stop();                                    //BGM���~

        bool isAllClear = stageManager.IsLastStage;                          //���̃X�e�[�W�����S�X�e�[�W���ȏ�Ȃ�

        stageAudio.Play(isAllClear ? StageClip.AllClear : StageClip.Clear);  //�I�[���N���ASE���Đ�

        var type = isAllClear ? UI.AllClear : UI.Clear;                      //�\��UI

        yield return stageEndUI.Display(type, isBreak);                      //UI��\��

        if (isAllClear)                                                      //�S�N���Ȃ�
        {
            yield return scoreUI.Display(stageManager.ClearTime.ElapsedTime);//�X�R�AUI��\��

            stageManager.ClearTime.ReSetTime();                              //�N���A�^�C�������Z�b�g

            OnPlayerGameOver();                                              //Player���Q�[���I�[�o�[�����ێ��s
        }
        else Fade(NextStage);                                                //�A�C�L���b�`���t�F�[�h���A�t�F�[�h�C����NextStage�֐������s
    }

    /// <summary>���̃X�e�[�W�ֈڍs</summary>
    private void NextStage()
    {
        InActiveStage();                                   //�X�e�[�W���A�N�e�B�u

        stageManager.AddStageCount();                      //�X�e�[�W�̐��� + 1

        tankManager.PlayerAddLife(stageManager.StageCount);//�v���C���[�^���N�̃��C�t�𑝂₷

        ActiveStage();                                     //���݂̃X�e�[�W�𐶐�
    }

    private void Update()
    {
        if (!isNotMove && Input.GetKeyDown(KeyCode.Space))//�������� ���� �X�y�[�X�������ꂽ��
        {
            menu.OpenOrClose();                           //���j���[���I�[�v���܂��̓N���[�Y
        }
    }
}
