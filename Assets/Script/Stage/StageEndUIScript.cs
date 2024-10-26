using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary></summary>
public enum UI
{
    /// <summary>�N���A</summary>
    Clear,
    /// <summary>�S�N��</summary>
    AllClear,
    /// <summary>���g���C</summary>
    Retry,
    /// <summary>�Q�[���I�[�o�[</summary>
    GameOver,
}

/// <summary>�X�e�[�W�I������UI</summary>
public class StageEndUIScript : MonoBehaviour
{
    /// <summary>�ړI�n</summary>
    [SerializeField, LightColor] private Transform StageEndTMPTarget;

    /// <summary>�X�e�[�W�I���C���[�W</summary>
    [SerializeField, LightColor] private Image StageEndImage;

    /// <summary>�X�e�[�W�I�����ɓ������e�L�X�g�z��</summary>
    [SerializeField] private MoveText[] StageEndTexts;

    /// <summary>TMP�̖ړI�̐F</summary>
    [SerializeField] private Color TMPColor;

    /// <summary>�ڕW�̐F</summary>
    [SerializeField] private Color ImageColor;

    /// <summary>UI�̕�Ԓl</summary>
    [SerializeField, Range01] private float UILerp;

    /// <summary>�S�N���e�L�X�g</summary>
    [SerializeField] private string AllClearText;
    /// <summary>�N���A�e�L�X�g</summary>
    [SerializeField] private string ClearText;
    /// <summary>�Q�[���I�[�o�[�e�L�X�g</summary>
    [SerializeField] private string GameOverText;
    /// <summary>���g���C�e�L�X�g</summary>
    [SerializeField] private string RetryText;

    public void Start()
    {
        foreach (var t in StageEndTexts)//StageEndTexts�̗v�f�����J��Ԃ�
        {
            t.Init();                   //������
        }
    }

    /// <summary>UI��\��</summary>
    /// <param name="type">�\��UI</param>
    /// <param name="isBreak">�I������</param>
    /// <returns>�I�������𖞂�������I��</returns>
    public IEnumerator Display(UI type, System.Func<bool> isBreak)
    {
        var target = StageEndTMPTarget.position;           //�ړI���W

        string text = type switch                          //�������UI�̎��
        {
            UI.Clear => ClearText,                      //�N���A        �Ȃ� �N���A�e�L�X�g
            UI.AllClear => AllClearText,                   //�S�N��        �Ȃ� �S�N���e�L�X�g
            UI.Retry => RetryText,                      //���g���C      �Ȃ� ���g���C�e�L�X�g
            UI.GameOver => GameOverText,                   //�Q�[���I�[�o�[�Ȃ� �Q�[���I�[�o�[�e�L�X�g
            _ => string.Empty                    //����ȊO      �Ȃ� Empty
        };

        foreach (var t in StageEndTexts) t.TMP.text = text;//�e�L�X�g����

        while (!isBreak.Invoke())                          //�I�������ɂȂ�Ȃ�����J��Ԃ�
        {
            StageEndImage.LerpColor(ImageColor, UILerp);        //��Ԓl����

            foreach (var t in StageEndTexts)               //MoveTexts�̗v�f�����J��Ԃ�
            {
                t.TMP.LerpColor(TMPColor, UILerp);              //��Ԓl����

                t.Trafo.LerpPosition(target, UILerp);      //��Ԓl����
            }

            yield return null;                             //1�t���[���ҋ@
        }

        StageEndImage.color = Color.clear;                 //Image��Color��(0, 0, 0, 0)����

        foreach (var t in StageEndTexts)                   //MoveTexts�̗v�f�����J��Ԃ�
        {
            t.ReSet();                                     //���Z�b�g
        }
    }
}

/// <summary>�ړ�����e�L�X�g</summary>
[System.Serializable]
public class MoveText
{
    /// <summary>Transform</summary>
    [field: SerializeField, LightColor] public Transform Trafo { get; private set; }

    /// <summary>TextMeshProUGUI</summary>
    [field: SerializeField, LightColor] public TextMeshProUGUI TMP { get; private set; }

    /// <summary>�����ʒu</summary>
    private Vector3 InitPos;

    /// <summary>�����ʒu�Z�b�g</summary>
    public void Init() => InitPos = Trafo.position;

    /// <summary>���Z�b�g</summary>
    public void ReSet()
    {
        TMP.color = Color.clear; //(0, 0, 0, 0)����

        TMP.text = string.Empty; //�e�L�X�g��Empty����

        Trafo.position = InitPos;//�����ʒu����
    }
}
