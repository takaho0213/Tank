using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>UI�̃^�C�v</summary>
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
    [SerializeField, LightColor] private Transform tmpTarget;

    /// <summary>�X�e�[�W�I���C���[�W</summary>
    [SerializeField, LightColor] private Image image;

    /// <summary>�X�e�[�W�I�����ɓ������e�L�X�g�z��</summary>
    [SerializeField] private MoveText[] moveTexts;

    /// <summary>TMP�̖ړI�̐F</summary>
    [SerializeField] private Color tmpColor;

    /// <summary>�ڕW�̐F</summary>
    [SerializeField] private Color imageColor;

    /// <summary>UI�̕�Ԓl</summary>
    [SerializeField, Range01] private float uiLerp;

    /// <summary>�S�N���e�L�X�g</summary>
    [SerializeField] private string allClearText;
    /// <summary>�N���A�e�L�X�g</summary>
    [SerializeField] private string clearText;
    /// <summary>�Q�[���I�[�o�[�e�L�X�g</summary>
    [SerializeField] private string gameOverText;
    /// <summary>���g���C�e�L�X�g</summary>
    [SerializeField] private string retryText;

    public void Start()
    {
        foreach (var t in moveTexts)//StageEndTexts�̗v�f�����J��Ԃ�
        {
            t.Init();               //������
        }
    }

    /// <summary>UI��\��</summary>
    /// <param name="type">�\��UI</param>
    /// <param name="isBreak">�I������</param>
    /// <returns>�I�������𖞂�������I��</returns>
    public IEnumerator Display(UI type, System.Func<bool> isBreak)
    {
        var target = tmpTarget.position;               //�ړI���W

        string text = type switch                      //�������UI�̎��
        {
            UI.Clear => clearText,                     //�N���A        �Ȃ� �N���A�e�L�X�g
            UI.AllClear => allClearText,               //�S�N��        �Ȃ� �S�N���e�L�X�g
            UI.Retry => retryText,                     //���g���C      �Ȃ� ���g���C�e�L�X�g
            UI.GameOver => gameOverText,               //�Q�[���I�[�o�[�Ȃ� �Q�[���I�[�o�[�e�L�X�g
            _ => string.Empty                          //����ȊO      �Ȃ� Empty
        };

        foreach (var t in moveTexts) t.TMP.text = text;//�e�L�X�g����

        while (!isBreak.Invoke())                      //�I�������ɂȂ�Ȃ�����J��Ԃ�
        {
            image.LerpColor(imageColor, uiLerp);       //��Ԓl����

            foreach (var t in moveTexts)               //MoveTexts�̗v�f�����J��Ԃ�
            {
                t.TMP.LerpColor(tmpColor, uiLerp);     //��Ԓl����

                t.Trafo.LerpPosition(target, uiLerp);  //��Ԓl����
            }

            yield return null;                         //1�t���[���ҋ@
        }

        image.color = Color.clear;                     //Image��Color��(0, 0, 0, 0)����

        foreach (var t in moveTexts)                   //MoveTexts�̗v�f�����J��Ԃ�
        {
            t.ReSet();                                 //���Z�b�g
        }
    }
}
