using UnityEngine;
using UnityEngine.Events;

/// <summary>�X�e�[�W�̃A�C�L���b�`</summary>
public class StageEyeCatchScript : MonoBehaviour
{
    /// <summary>�A�C�L���b�`</summary>
    [SerializeField, LightColor] private StageEyeCatchUIScript eyecatchUI;

    /// <summary>�X�e�[�W�J�n���̑ҋ@����</summary>
    [SerializeField] private float staegStartWaitTime;

    /// <summary>�X�e�[�W�J�n���̑ҋ@����</summary>
    private Interval stageStartWaitInterval;

    /// <summary>�A�C�L���b�`��SE���Đ�����֐�</summary>
    private UnityAction playEyeCatchSE;

    /// <summary>BGM��Info</summary>
    private AudioInfo audioBGM;

    /// <summary>�t�F�[�_�[</summary>
    public GraphicsFaderScript Fader => eyecatchUI.Fader;

    /// <summary>�^���N�������Ȃ���Ԃ�</summary>
    public bool IsNotMove => Fader.IsRun || !stageStartWaitInterval.IsOver;

    private void Start()
    {
        stageStartWaitInterval = new(time: staegStartWaitTime);            //�C���X�^���X��

        playEyeCatchSE = AudioScript.I.StageAudio[StageClip.EyeCatch].Play;//�A�C�L���b�`SE���Đ�����֐�����

        audioBGM = AudioScript.I.StageAudio[StageClip.BGM];                //BGM�I�[�f�B�I
    }

    /// <summary>�A�C�L���b�`���t�F�[�h����֐�</summary>
    /// <param name="i">�t�F�[�h�C�������s����֐�</param>
    /// <param name="o">�t�F�[�h�A�E�g�����s����֐�</param>
    public void Fade(UnityAction i, UnityAction o)
    {
        audioBGM.Stop();                                //BGM���~

        i += playEyeCatchSE;                            //�A�C�L���b�`SE���Đ�����֐������Z���

        Fader.Run(i + playEyeCatchSE, o + OnStageStart);//�t�F�[�h�J�n �t�F�[�h�C�������ۈ����̊֐������s
    }

    /// <summary>�X�e�[�W���J�n�����ێ��s</summary>
    private void OnStageStart()
    {
        stageStartWaitInterval.ReSet();//�C���^�[�o�������Z�b�g

        audioBGM.Play();               //BGM���Đ�
    }

    public void DisplayUI() => eyecatchUI.Display();

    /// <summary>�X�e�[�W��,���C�t����\������e�L�X�g���Z�b�g</summary>
    /// <param name="stage">�X�e�[�W��</param>
    /// <param name="life">���C�t��</param>
    public void SetText(int stage, int life) => eyecatchUI.SetText(stage, life);
}
