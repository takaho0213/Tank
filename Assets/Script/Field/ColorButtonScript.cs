using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary><see cref="ColorFieldScript"/>�̃{�^��</summary>
public class ColorButtonScript : MonoBehaviour
{
    /// <summary>�{�^��</summary>
    [SerializeField] private Button button;

    /// <summary>�J���[�ƃJ���[����\������TMP</summary>
    [SerializeField] private TextMeshProUGUI colorTMP;

    /// <summary>�J���[</summary>
    [SerializeField] private Color color;

    /// <summary>�J���[��</summary>
    [SerializeField] private string colorName;

    /// <summary>�{�^�����N���b�N���ꂽ�ہA���s����֐�</summary>
    private UnityAction<Color> onClick;

    /// <summary>������</summary>
    /// <param name="onClick">�{�^�����N���b�N���ꂽ�ہA���s����֐�</param>
    public void Init(UnityAction<Color> onClick)
    {
        colorTMP.text = colorName;          //�J���[������
        colorTMP.color = color;             //�J���[����

        this.onClick = onClick;             //�{�^�����N���b�N���ꂽ�ہA���s����֐�����

        button.onClick.AddListener(OnClick);//�{�^�����N���b�N���ꂽ�ہA���s����֐���ǉ�
    }

    /// <summary>�{�^�����N���b�N���ꂽ�ہA���s����֐�</summary>
    public void OnClick()
    {
        onClick.Invoke(color);//�N���b�N���ꂽ�ێ��s����֐������s
    }
}
