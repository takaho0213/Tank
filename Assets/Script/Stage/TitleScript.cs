using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>�^�C�g��</summary>
public class TitleScript : GraphicFaderScript
{
    [SerializeField, LightColor] private TutorialScript tutorial;

    /// <summary>�^�C�g���I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject titleObj;

    [SerializeField, LightColor] private Button gameStartButton;
    [SerializeField, LightColor] private Button tutorialButton;
    [SerializeField, LightColor] private Button gameQuitButton;

    private AudioInfo StageBGM;

    private UnityAction onReStartFadeIn;
    private UnityAction onReStartFadeOut;

    private void Start()
    {
        StageBGM = AudioScript.I.StageAudio[StageClip.BGM];

        OnGameClear(null);                         //���X�^�[�g

        gameStartButton.onClick.AddListener(OnGameStart);

        tutorialButton.onClick.AddListener(OnTutorial);

        gameQuitButton.onClick.AddListener(OnGameQuit);

        tutorial.Init(OnGameClear);
    }

    public void Init(UnityAction onReStartFadeIn, UnityAction onReStartFadeOut)
    {
        this.onReStartFadeIn = onReStartFadeIn;
        this.onReStartFadeOut = onReStartFadeOut;
    }

    /// <summary>���X�^�[�g�����s</summary>
    public void OnGameClear(UnityAction c)
    {
        c += () => titleObj.SetActive(true);

        Run(c, StageBGM.Play);//�t�F�[�h���J�n �t�F�[�h�C�����X�e�[�W���A�N�e�B�u �t�F�[�h�A�E�g��BGM���Đ�
    }

    /// <summary>�t�F�[�h�C�������s����֐�</summary>
    private void OnFadeIn()
    {
        titleObj.SetActive(false);     //�^�C�g�����A�N�e�B�u

        StageBGM.Stop();        //BGM���~

        onReStartFadeIn?.Invoke();//���X�^�[�g�̃t�F�[�h�A�E�g�����s����֐������s
    }

    /// <summary>�Q�[�����J�n</summary>
    private void OnGameStart()
    {
        if (IsRun) return;                           //�t�F�[�h���Ȃ�/�I��

        Run(OnFadeIn, onReStartFadeOut);//�t�F�[�h�J�n �t�F�[�h�C����OnFadeIn�֐������s �t�F�[�h�A�E�g��OnReStartFadeIn�֐������s
    }

    private void OnTutorial()
    {
        if (IsRun) return;                           //�t�F�[�h���Ȃ�/�I��

        UnityAction i = () => titleObj.SetActive(false);

        Run(tutorial.OnFadeIn + i, tutorial.OnFadeOut);
    }

    private void OnGameQuit()
    {
        if (IsRun) return;

        Run(ApplicationEx.Quit);
    }
}
