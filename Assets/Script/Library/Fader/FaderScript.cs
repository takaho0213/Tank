using UnityEngine;
using UnityEngine.Events;

public class FaderScript : MonoBehaviour
{
    /// <summary>�t�F�[�h�C�����̑ҋ@����</summary>
    public float WaitTime;

    /// <summary>�t�F�[�h���x</summary>
    public float Speed;

    /// <summary>�t�F�[�h������J���[����������C���^�[�t�F�C�X</summary>
    protected IFade fade;

    /// <summary>�ҋ@����</summary>
    protected System.Func<bool> isWait;

    /// <summary>�t�F�[�h�C�������s����֐�</summary>
    protected UnityAction onFadeIn;
    /// <summary>�t�F�[�h�A�E�g�����s����֐�</summary>
    protected UnityAction onFadeOut;

    /// <summary>�t�F�[�h�I�������s����֐�</summary>
    protected UnityAction onFadeEnd;

    /// <summary>�ҋ@�C���^�[�o��</summary>
    protected Interval waitInterval;

    /// <summary>�ڕW�̃A���t�@�l</summary>
    protected float target;

    /// <summary>�t�F�[�h�C�����̃A���t�@�l</summary>
    protected readonly float inAlpha = Color.black.a;
    /// <summary>�t�F�[�h�A�E�g���̃A���t�@�l</summary>
    protected readonly float outAlpha = Color.clear.a;

    /// <summary>�t�F�[�h�A�E�g����</summary>
    public bool IsFadeOut { get; protected set; }

    /// <summary>�t�F�[�h����</summary>
    public bool IsRun { get; protected set; }

    protected virtual void Awake() => waitInterval ??= new(Time: WaitTime);//�C���X�^���X���쐬

    /// <summary>�t�F�[�h���J�n</summary>
    /// <param name="f">�t�F�[�h������Color</param>
    /// <param name="i">�t�F�[�h�C�������s</param>
    /// <param name="o">�t�F�[�h�A�E�g�����s</param>
    /// <param name="w">�ҋ@����</param>
    public virtual void Run(IFade f, UnityAction i, UnityAction o, System.Func<bool> w)
    {
        if (IsRun) return;                 //�t�F�[�h���Ȃ�/�I��

        onFadeEnd ??= () => IsRun = false; //�t�F�[�h�I�����̊֐������s

        waitInterval.Time = WaitTime;      //�ҋ@���Ԃ���

        IsRun = true;                      //�t�F�[�h������true

        fade = f;                          //�t�F�[�h������Color����

        onFadeIn = i + waitInterval.ReSet; //(�t�F�[�h�C�������s + �C���^�[�o�������Z�b�g)�֐�����
        onFadeOut = o + onFadeEnd;         //(�t�F�[�h�A�E�g�����s + �t�F�[�h�I�������s)�֐�����

        isWait = w ??= () => !waitInterval;//�ҋ@����/null�Ȃ�C���^�[�o�����z���Ă��Ȃ�������

        target = inAlpha;                  //�A���t�@�l����
    }

    /// <summary>�t�F�[�h</summary>
    protected virtual void Fade()
    {
        var color = fade.FadeColor;

        if (Mathf.Approximately(color.a, target))        //�ڕW�̐F�ɂȂ�����
        {
            target = IsFadeOut ? inAlpha : outAlpha;     //�ڕW�̐F�̃A���t�@�l��

            (IsFadeOut ? onFadeOut : onFadeIn)?.Invoke();//(�t�F�[�h�A�E�g�Ȃ�t�F�[�h�A�E�g�����s/����ȊO�Ȃ�t�F�[�h�C�������s)����֐������s

            IsFadeOut = !IsFadeOut;                      //���]

            return;                                      //�I��
        }

        if (!isWait.Invoke())                            //�ҋ@���łȂ����
        {
            color.a = Mathf.MoveTowards(color.a, target, Speed * Time.deltaTime);//�A���t�@�l���t�F�[�h

            fade.FadeColor = color;                                              //�t�F�[�h�������l����
        }
    }

    protected virtual void Update() { if (IsRun) Fade(); }//�t�F�[�h���Ȃ�/�t�F�[�h
}

public interface IFade
{
    /// <summary>�t�F�[�h������J���[</summary>
    public Color FadeColor { get; set; }
}

//�t�F�[�h�̒�`

//0 => 1 : �t�F�[�h�C��
//1 => 0 : �t�F�[�h�A�E�g