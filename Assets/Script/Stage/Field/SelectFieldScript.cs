using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>�I��������t�B�[���h</summary>
public class SelectFieldScript<T> : MonoBehaviour
{
    /// <summary>�{�^���z��</summary>
    [SerializeField] protected SelectButtonScript<T>[] buttons;

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
        fieldTMP.text = fieldName;               //�t�B�[���h������

        UnityAction<SelectButtonScript<T>> c = OnClick;//�N���b�N���ꂽ�ێ��s����֐�

        foreach (var b in buttons)               //�{�^���z��̗v�f�����J��Ԃ�
        {
            b.SetOnClick(c);                     //�N���b�N���ꂽ�ێ��s����֐����Z�b�g

            b.SetTextColor = offColor;           //�I�t�̍ۂ�Color����
        }

        OnClick(current = initButton);     //���݂̃{�^�����Z�b�g
    }

    /// <summary></summary>
    /// <param name="button"></param>
    protected void OnClick(SelectButtonScript<T> button)
    {
        current.SetTextColor = offColor;//�I�t��Color����

        current = button;               //���݂̃{�^�����Z�b�g

        current.SetTextColor = onColor; //�I����Color���Z�b�g
    }
}

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