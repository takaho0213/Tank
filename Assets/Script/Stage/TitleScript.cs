using UnityEngine;
using UnityEngine.UI;

/// <summary>�^�C�g��</summary>
public class TitleScript : GraphicFaderScript
{
    [SerializeField] private GraphicFaderScript Fader;

    /// <summary>�X�e�[�W�}�l�[�W���[</summary>
    [SerializeField, LightColor] private StageManagerScript StageManager;

    //[SerializeField, LightColor] private TutorialScript Tutorial;

    /// <summary>�^�C�g���I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject TitleObj;

    [SerializeField] private Button GameStartButton;
    [SerializeField] private Button GameQuitButton;
    //[SerializeField] private Button TutorialButton;

    private void Start()
    {
        ReStart();                         //���X�^�[�g

        StageManager.OnGameClear = ReStart;//�Q�[���N���A�����s����֐�����

        GameStartButton.onClick.AddListener(GameStart);

        //TutorialButton.onClick.AddListener(Tutorial.Active);

        GameQuitButton.onClick.AddListener(() => Fader.Run(ApplicationEx.Quit));
    }

    /// <summary>���X�^�[�g�����s</summary>
    private void ReStart()
    {
        Run(() => TitleObj.SetActive(true), () => AudioScript.I.StageAudio.Play(StageClip.BGM));//�t�F�[�h���J�n �t�F�[�h�C�����X�e�[�W���A�N�e�B�u �t�F�[�h�A�E�g��BGM���Đ�
    }

    /// <summary>�t�F�[�h�C�������s����֐�</summary>
    private void OnFadeIn()
    {
        TitleObj.SetActive(false);                                   //�^�C�g�����A�N�e�B�u

        AudioScript.I.StageAudio.Dictionary[StageClip.BGM].Source.Stop();//BGM���~

        StageManager.OnReStartFadeIn();                              //���X�^�[�g�̃t�F�[�h�A�E�g�����s����֐������s
    }

    /// <summary>�Q�[�����J�n</summary>
    private void GameStart()
    {
        if (IsRun) return;                           //�t�F�[�h���Ȃ�/�I��

        Run(OnFadeIn, StageManager.OnReStartFadeOut);//�t�F�[�h�J�n �t�F�[�h�C����OnFadeIn�֐������s �t�F�[�h�A�E�g��OnReStartFadeIn�֐������s
    }

    private void OnTutorial()
    {

    }
}
