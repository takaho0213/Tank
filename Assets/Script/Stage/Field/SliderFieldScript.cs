using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>int or float�̒l��ݒ肷��t�B�[���h</summary>
public class SliderFieldScript : MonoBehaviour
{
    /// <summary>�X���C�_�[</summary>
    [SerializeField] private Slider slider;

    /// <summary>�l��\������TMP</summary>
    [SerializeField] private TextMeshProUGUI valueTMP;
    /// <summary>�t�B�[���h����\������TMP</summary>
    [SerializeField] private TextMeshProUGUI fieldTMP;

    /// <summary>�t�B�[���h��</summary>
    [SerializeField] private string fieldName;

    /// <summary>�ŏ��l</summary>
    [SerializeField] private float min;
    /// <summary>�ő�l</summary>
    [SerializeField] private float max;

    [SerializeField] private float initValue;

    /// <summary>�\������</summary>
    [SerializeField] private int digits;

    /// <summary>int�^�ɂ��邩�H</summary>
    [SerializeField] private bool isInt;

    /// <summary>�l</summary>
    public float Value => slider.value;

    /// <summary>int�^�̒l</summary>
    public int IntValue => slider.value.RoundToInt();

    private void Start()
    {
        fieldTMP.text = fieldName;                        //�t�B�[���h������

        slider.minValue = isInt ? min.RoundToInt() : min; //�X���C�_�[�̍ŏ��l����
        slider.maxValue = isInt ? max.RoundToInt() : max; //�X���C�_�[�̍ő�l����

        slider.onValueChanged.AddListener(OnValueChanged);//�X���C�_�[�̒l���ύX���ꂽ�ہA���s����֐���ǉ�

        slider.value = Mathf.Clamp(initValue, min, max); //�X���C�_�[�̒l��ݒ�
    }

    /// <summary>�X���C�_�[�̒l���ύX���ꂽ�ہA���s����</summary>
    /// <param name="value">�X���C�_�[�̒l</param>
    private void OnValueChanged(float value)
    {
        if (isInt) slider.value = value.RoundToInt();                                 //int�^�Ȃ�/�X���C�_�[�̒l��int�ɕϊ�

        valueTMP.text = (isInt ? value.RoundToInt() : value.Round(digits)).ToString();//�l��\��
    }
}
