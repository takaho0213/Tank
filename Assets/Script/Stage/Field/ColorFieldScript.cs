using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>Color��ݒ肷��t�B�[���h</summary>
public class ColorFieldScript : MonoBehaviour
{
    /// <summary>Color��\������Graphic</summary>
    [SerializeField] private Graphic graphic;

    /// <summary>Color��R�l��ݒ肷��X���C�_�[</summary>
    [SerializeField] private Slider rSlider;
    /// <summary>Color��G�l��ݒ肷��X���C�_�[</summary>
    [SerializeField] private Slider gSlider;
    /// <summary>Color��B�l��ݒ肷��X���C�_�[</summary>
    [SerializeField] private Slider bSlider;

    /// <summary>�t�B�[���h����\������TMP</summary>
    [SerializeField] private TextMeshProUGUI fieldTMP;

    /// <summary>�t�B�[���h��</summary>
    [SerializeField] private string fieldName;

    /// <summary>�{�^���z��</summary>
    [SerializeField] private ColorButtonScript[] buttons;

    [SerializeField] private ColorButtonScript initButton;

    /// <summary>�l</summary>
    public Color Value => graphic.color;

    private void Start()
    {
        fieldTMP.text = fieldName;                         //�t�B�[���h������

        rSlider.onValueChanged.AddListener(OnValueChanged);//�X���C�_�[�̒l���ύX���ꂽ�ہA���s����֐���ǉ�
        gSlider.onValueChanged.AddListener(OnValueChanged);
        bSlider.onValueChanged.AddListener(OnValueChanged);

        OnValueChanged(default);                           //Color��\������Graphic�ɃX���C�_�[�̒l���Z�b�g

        UnityAction<Color> c = OnClick;                    //�{�^�����N���b�N���ꂽ�ێ��s���邷��֐�

        foreach (var b in buttons)
        {
            b.Init(c);                                     //�{�^����������
        }

        initButton.OnClick();
    }

    /// <summary>Color��\������Graphic�ɃX���C�_�[�̒l���Z�b�g</summary>
    /// <param name="value">�X���C�_�[�̒l</param>
    private void OnValueChanged(float value)
    {
        var color = graphic.color;//���݂�Color����

        color.r = rSlider.value;  //R�l����
        color.g = gSlider.value;  //G�l����
        color.b = bSlider.value;  //B�l����

        graphic.color = color;    //�l����
    }

    /// <summary>�{�^�����N���b�N���ꂽ�ێ��s���邷��֐�</summary>
    /// <param name="color">�{�^����Color</param>
    private void OnClick(Color color)
    {
        rSlider.value = color.r;//R�l����
        gSlider.value = color.g;//G�l����
        bSlider.value = color.b;//B�l����

        OnValueChanged(default);//Color��\������Graphic�ɃX���C�_�[�̐F���Z�b�g
    }
}