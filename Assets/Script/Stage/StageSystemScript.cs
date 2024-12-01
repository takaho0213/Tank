using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StageSystemScript : MonoBehaviour
{
    [SerializeField, LightColor] private TitleScript title;

    /// <summary>�X�e�[�W�I��UI</summary>
    [SerializeField, LightColor] private StageEndUIScript stageEndUI;

    /// <summary>�X�R�AUI</summary>
    [SerializeField, LightColor] private ScoreUIScript scoreUI;

    /// <summary>�A�C�L���b�`</summary>
    [SerializeField, LightColor] private StageEyeCatchScript eyecatch;

    [SerializeField, LightColor] private StageTankManagerScript tankManager;

    [SerializeField, LightColor] private StageManagerScript stageManager;

    [SerializeField, LightColor] private TutorialScript tutorial;

    /// <summary>�t���[�����[�g</summary>
    [SerializeField, ReadOnly] private int frameRate;

    /// <summary>�I������</summary>
    private System.Func<bool> isBreak;

    /// <summary>�^���N�������Ȃ���Ԃ�</summary>
    private bool isNotMove;

    /// <summary>�X�e�[�W��Audio</summary>
    private AudioInfoDictionary<StageClip> stageAudio;

    /// <summary>�^���N�������Ȃ���Ԃ�</summary>
    public bool IsNotMove => eyecatch.IsNotMove�@|| tutorial.IsNotMove || isNotMove;

    private void Start()
    {
        ApplicationEx.SetFrameRate(frameRate);                              //�t���[�����[�g���Œ�

        stageAudio = AudioScript.I.StageAudio;

        isBreak = () => !stageAudio[StageClip.Clear].IsPlaying;//�I����������

        title.Init(OnReStartFadeIn, () => Fade(ActiveStage));

        tankManager.Init(OnPlayerDeath);                        //
    }

    /// <summary>���X�^�[�g�̃t�F�[�h�C�������s</summary>
    private void OnReStartFadeIn()
    {
        stageManager.ReSetTime();    //�X�R�A�����Z�b�g

        eyecatch.DisplayUI();        //�A�C�L���b�`��\��

        tankManager.Player.ReStart();//���X�^�[�g

        InActiveStage();      //�X�e�[�W���Z�b�g
    }

    /// <summary>�v���C���[���S���Ɏ��s</summary>
    /// <param name="isNoLife">���C�t���c���Ă��Ȃ���</param>
    private IEnumerator OnPlayerDeath(bool isNoLife)
    {
        if (isNotMove) yield break;                                          //�������Ȃ���ԂȂ�/�I��

        isNotMove = true;                                                    //�����Ȃ���Ԃ���true

        var audio = stageAudio[StageClip.PlayerDeath];            //�v���C���[���SAudio

        audio.Play();                                                        //SE���Đ�

        var type = isNoLife ? UI.GameOver : UI.Retry;                        //�\��UI

        yield return stageEndUI.Display(type, () => !audio.IsPlaying);//UI��\��

        if (isNoLife) OnPlayerGameOver();
        else Fade(OnPlayerRetry);//�A�C�L���b�`���t�F�[�h���A�t�F�[�h�C�������g���C�֐������s

        isNotMove = false;                                                   //�����Ȃ���Ԃ���false
    }

    /// <summary>�A�C�L���b�`���t�F�[�h����֐�</summary>
    /// <param name="c">�t�F�[�h�C�������s����֐�</param>
    private void Fade(UnityAction c) => eyecatch.Fade(c, () => isNotMove = false);

    /// <summary>Player���Q�[���I�[�o�[�����ێ��s</summary>
    private void OnPlayerGameOver()
    {
        stageManager.ReSetStageCount();

        stageAudio[StageClip.BGM].Stop();

        title.OnGameClear(InActiveStage);
    }

    /// <summary>Player�����g���C�����ێ��s</summary>
    private void OnPlayerRetry()
    {
        tankManager.Player.OnRetry();//�v���C���[�����g���C

        tankManager.InActive();//���݂̃X�e�[�W�����Z�b�g

        ActiveStage();    //���݂̃X�e�[�W�𐶐�

        stageManager.Retry();
    }

    /// <summary>���݂̃X�e�[�W�𐶐�</summary>
    private void ActiveStage()
    {
        stageManager.ActiveStage(tankManager, OnAllEnemysDeath);

        eyecatch.SetText(stageManager.NextStageCount, tankManager.Player.Life.LifeCount);
    }

    private void InActiveStage()
    {
        tankManager.InActive();

        stageManager.InActiveStage();
    }

    /// <summary>Enemy�����ׂē|�����ێ��s</summary>
    private IEnumerator OnAllEnemysDeath()
    {
        if (isNotMove) yield break;                                        //�����Ȃ���ԂȂ�/�I��

        isNotMove = true;                                                  //�����Ȃ���Ԃ���true

        stageAudio[StageClip.BGM].Stop();                //BGM���~

        bool isAllClear = stageManager.IsMaxStage;                 //���̃X�e�[�W�����S�X�e�[�W���ȏ�Ȃ�

        stageAudio.Play(isAllClear ? StageClip.AllClear : StageClip.Clear);//�I�[���N���ASE���Đ�

        var type = isAllClear ? UI.AllClear : UI.Clear;                    //�\��UI

        yield return stageEndUI.Display(type, isBreak);                    //UI��\��

        if (isAllClear)                                                    //�S�N���Ȃ�
        {
            yield return scoreUI.Display(stageManager.ElapsedTime);   //�X�R�AUI��\��

            OnPlayerGameOver();
        }
        else Fade(NextStage);                                              //�A�C�L���b�`���t�F�[�h���A�t�F�[�h�C����NextStage�֐������s
    }

    /// <summary>���̃X�e�[�W�ֈڍs</summary>
    private void NextStage()
    {
        InActiveStage();

        stageManager.NextStage();

        tankManager.PlayerAddLife(stageManager.StageCount);//

        ActiveStage();                        //���݂̃X�e�[�W�𐶐�
    }
}
