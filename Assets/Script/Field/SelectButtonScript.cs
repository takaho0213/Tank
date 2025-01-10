using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>�I���{�^��</summary>
[System.Serializable]
public class SelectButtonScript<T> : MonoBehaviour
{
    /// <summary>�{�^��</summary>
    [SerializeField] protected Button button;

    /// <summary>�{�^���̕������\������TMP</summary>
    [SerializeField] protected TextMeshProUGUI buttonTMP;

    /// <summary>�l</summary>
    [SerializeField] protected T value;

    /// <summary>�{�^���̖��O</summary>
    [SerializeField] protected string buttonName;

    /// <summary>�N���b�N���ꂽ�ێ��s����֐�</summary>
    private UnityAction<SelectButtonScript<T>> onClick;

    /// <summary>�l</summary>
    public T Value => value;

    /// <summary>�{�^���e�L�X�g��Color���Z�b�g</summary>
    public Color SetTextColor { set => buttonTMP.color = value; }

    /// <summary>�N���b�N���ꂽ�ێ��s����֐����Z�b�g</summary>
    /// <param name="onClick">�N���b�N���ꂽ�ێ��s����֐�</param>
    public void SetOnClick(UnityAction<SelectButtonScript<T>> onClick)
    {
        this.onClick = onClick;             //�N���b�N���ꂽ�ێ��s����֐�����

        button.onClick.AddListener(OnClick);//�N���b�N���ꂽ�ێ��s����֐���ǉ�

        buttonTMP.text = buttonName;        //�{�^��������
    }

    /// <summary>�N���b�N���ꂽ�ێ��s����֐�</summary>
    public void OnClick()
    {
        onClick.Invoke(this);//�N���b�N���ꂽ�ێ��s����֐������s
    }
}