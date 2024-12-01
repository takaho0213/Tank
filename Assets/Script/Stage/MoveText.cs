using TMPro;
using UnityEngine;

/// <summary>�ړ�����e�L�X�g</summary>
[System.Serializable]
public class MoveText
{
    /// <summary>�e�L�X�g��\������TextMeshProUGUI</summary>
    [field: SerializeField, LightColor] public TextMeshProUGUI TMP { get; private set; }

    /// <summary>������Transform</summary>
    public Transform Trafo { get; private set; }

    /// <summary>�����ʒu</summary>
    private Vector3 initPos;

    /// <summary>�����ʒu�Z�b�g</summary>
    public void Init()
    {
        Trafo = TMP.transform;

        initPos = Trafo.position;
    }

    /// <summary>���Z�b�g</summary>
    public void ReSet()
    {
        TMP.color = Color.clear; //(0, 0, 0, 0)����

        TMP.text = string.Empty; //�e�L�X�g��Empty����

        Trafo.position = initPos;//�����ʒu����
    }
}
