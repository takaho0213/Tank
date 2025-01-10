using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>�I��������t�B�[���h</summary>
public class SelectFieldScript<T> : MonoBehaviour
{
    /// <summary>�{�^���z��</summary>
    [SerializeField] protected SelectButtonScript<T>[] buttons;

    /// <summary>�����l�̃{�^��</summary>
    [SerializeField] protected SelectButtonScript<T> initButton;

    /// <summary>�t�B�[���h����\������TMP</summary>
    [SerializeField] protected TextMeshProUGUI fieldTMP;

    /// <summary>�t�B�[���h��</summary>
    [SerializeField] protected string fieldName;

    /// <summary>�I���̍ۂ�Color</summary>
    [SerializeField] protected Color onColor;
    /// <summary>�I�t�̍ۂ�Color</summary>
    [SerializeField] protected Color offColor;

    /// <summary>���ݑI������Ă���{�^��</summary>
    protected SelectButtonScript<T> current;

    /// <summary>�l</summary>
    public T Value => current.Value;

    protected void Start()
    {
        fieldTMP.text = fieldName;                     //�t�B�[���h������

        UnityAction<SelectButtonScript<T>> c = OnClick;//�N���b�N���ꂽ�ێ��s����֐�

        foreach (var b in buttons)                     //�{�^���z��̗v�f�����J��Ԃ�
        {
            b.SetOnClick(c);                           //�N���b�N���ꂽ�ێ��s����֐����Z�b�g

            b.SetTextColor = offColor;                 //�I�t�̍ۂ�Color����
        }

        OnClick(current = initButton);                 //���݂̃{�^�����Z�b�g
    }

    /// <summary>�N���b�N���ꂽ�ۂ̏���</summary>
    /// <param name="button">�{�^��</param>
    protected void OnClick(SelectButtonScript<T> button)
    {
        current.SetTextColor = offColor;//�I�t��Color����

        current = button;               //���݂̃{�^�����Z�b�g

        current.SetTextColor = onColor; //�I����Color���Z�b�g
    }
}
