using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>�^�C�g��</summary>
public class TitleScript : GraphicFaderScript
{
    /// <summary>�`���[�g���A��</summary>
    [SerializeField, LightColor] private TutorialScript tutorial;

    /// <summary>���j���[</summary>
    [SerializeField, LightColor] private MenuScript menu;

    /// <summary>�^�C�g���I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject titleObj;

    /// <summary>�Q�[���X�^�[�g�{�^��</summary>
    [SerializeField, LightColor] private Button gameStartButton;
    /// <summary>�`���[�g���A���{�^��</summary>
    [SerializeField, LightColor] private Button tutorialButton;
    /// <summary>�Q�[���I���{�^��</summary>
    [SerializeField, LightColor] private Button gameQuitButton;

    /// <summary>�X�e�[�WBGM</summary>
    private AudioInfo StageBGM;

    /// <summary>�t�F�[�h�C������ێ��s����ւ���֐�</summary>
    private UnityAction onStartFadeIn;
    /// <summary>�t�F�[�h�A�E�g����ێ��s����ւ���֐�</summary>
    private UnityAction onStartFadeOut;

    private void Start()
    {
        StageBGM = AudioScript.I.StageAudio[StageClip.BGM];//�X�e�[�WBGM

        FadeActive(null);                                  //�X�e�[�W���A�N�e�B�u

        gameStartButton.onClick.AddListener(OnGameStart);  //�Q�[���J�n�{�^���������ꂽ�ێ��s����֐���ǉ�

        tutorialButton.onClick.AddListener(OnTutorial);    //�`���[�g���A���{�^���������ꂽ�ێ��s����֐���ǉ�

        gameQuitButton.onClick.AddListener(OnGameQuit);    //�Q�[���I���{�^���������ꂽ�ێ��s����ւ����ǉ�

        menu.AddOnClickGameQuitButton(OnGameQuit);         //���j���[�̃Q�[���I���{�^���������ꂽ�ێ��s����֐���ǉ�
    }

    /// <summary>������</summary>
    /// <param name="onStartFadeIn">�t�F�[�h�C������ێ��s����ւ���֐�</param>
    /// <param name="onStartFadeOut">�t�F�[�h�A�E�g����ێ��s����ւ���֐�</param>
    public void Init(UnityAction onStartFadeIn, UnityAction onStartFadeOut)
    {
        this.onStartFadeIn = onStartFadeIn;  //�t�F�[�h�C������ێ��s����ւ���֐�����
        this.onStartFadeOut = onStartFadeOut;//�t�F�[�h�A�E�g����ێ��s����ւ���֐�����
    }

    /// <summary>���X�^�[�g�����s</summary>
    /// <param name="c">�t�F�[�h�C�������s����֐�</param>
    public void FadeActive(UnityAction c)
    {
        c += () => titleObj.SetActive(true);//�^�C�g�����A�N�e�B�u�ɂ���֐������Z���

        StageBGM.Stop();                    //BGM���X�g�b�v

        Run(c, StageBGM.Play);              //�t�F�[�h���J�n �t�F�[�h�C�����X�e�[�W���A�N�e�B�u �t�F�[�h�A�E�g��BGM���Đ�
    }

    /// <summary>�t�F�[�h�C�������s����֐�</summary>
    private void OnFadeIn()
    {
        titleObj.SetActive(false);//�^�C�g�����A�N�e�B�u

        StageBGM.Stop();          //BGM���~

        onStartFadeIn?.Invoke();  //���X�^�[�g�̃t�F�[�h�A�E�g�����s����֐������s
    }

    /// <summary>�Q�[�����J�n</summary>
    private void OnGameStart()
    {
        if (IsRun) return;            //�t�F�[�h���Ȃ�/�I��

        Run(OnFadeIn, onStartFadeOut);//�t�F�[�h�J�n �t�F�[�h�C����OnFadeIn�֐������s �t�F�[�h�A�E�g��OnReStartFadeIn�֐������s
    }

    /// <summary>�`���[�g���A���{�^���������ꂽ�ێ��s����֐���ǉ�</summary>
    private void OnTutorial()
    {
        if (IsRun) return;                              //�t�F�[�h���Ȃ�/�I��

        UnityAction i = () => titleObj.SetActive(false);//�^�C�g�����A�N�e�B�u

        Run(tutorial.OnFadeIn + i, tutorial.OnFadeOut); //�t�F�[�h
    }

    /// <summary>�Q�[���I���{�^���������ꂽ�ێ��s����֐���ǉ�</summary>
    private void OnGameQuit()
    {
        if (IsRun) return;      //�t�F�[�h���Ȃ�/�I��

        Run(ApplicationEx.Quit);//�t�F�[�h���Q�[�����I��
    }
}
