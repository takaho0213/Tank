using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>���j���[</summary>
public class MenuScript : MonoBehaviour
{
    /// <summary>�`���[�g���A���̍ە\�����郁�j���[</summary>
    [SerializeField, LightColor] private TutorialMenuScript tutorialMenu;

    /// <summary>�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject obj;

    /// <summary>�^�C�g���ɖ߂�{�^��</summary>
    [SerializeField, LightColor] private Button backTitleButton;
    /// <summary>�Q�[���I���{�^��</summary>
    [SerializeField, LightColor] private Button gameQuitButton;

    /// <summary>�^�C�g���ɖ߂�{�^���������ꂽ�ێ��s����֐�</summary>
    private UnityAction onBackTitle;
    /// <summary>�Q�[���I���{�^���������ꂽ�ێ��s����֐�</summary>
    private UnityAction onGameQuit;

    /// <summary>�^�C�g���ɖ߂�{�^���������ꂽ�ێ��s����֐���ǉ�</summary>
    /// <param name="c">�ǉ�����֐�</param>
    public void AddOnClickBackTitleButton(UnityAction c) => onBackTitle += c;

    /// <summary>�Q�[���I���{�^���������ꂽ�ێ��s����֐���ǉ�</summary>
    /// <param name="c">�ǉ�����֐�</param>
    public void AddOnClickGameQuitButton(UnityAction c) => onGameQuit += c;

    private void Start()
    {
        backTitleButton.onClick.AddListener(OnBackTitle);//�^�C�g���ɖ߂�{�^���������ꂽ�ێ��s����֐���ǉ�

        gameQuitButton.onClick.AddListener(OnGameQuit);  //�Q�[���I���{�^���������ꂽ�ێ��s����֐���ǉ�
    }

    /// <summary>�^�C�g���֖߂�ۂ̏���</summary>
    private void OnBackTitle()
    {
        OpenOrClose();        //�I�[�v���܂��̓N���[�Y������

        onBackTitle?.Invoke();//�^�C�g���ɖ߂�{�^���������ꂽ�ۂ̃R�[���o�b�N�����s
    }

    /// <summary>�Q�[�����I������ۂ̏���</summary>
    private void OnGameQuit()
    {
        OpenOrClose();       //�I�[�v���܂��̓N���[�Y������

        onGameQuit?.Invoke();//�Q�[���I���{�^���������ꂽ�ۂ̃R�[���o�b�N�����s
    }

    /// <summary>�I�[�v���܂��̓N���[�Y������</summary>
    public void OpenOrClose()
    {
        bool isActive = !obj.activeSelf;                       //�A�N�e�B�u���

        obj.SetActive(isActive);                               //�A�N�e�B�u��Ԃ�ݒ�

        Time.timeScale = isActive ? MathEx.ZeroF : MathEx.OneF;//���Ԃ̐i�ݕ���ݒ�

        tutorialMenu.OpenOrClose();                            //�`���[�g���A�����j���[�̃I�[�v���܂��̓N���[�Y�����ۂ̊֐����Ăяo��
    }
}
