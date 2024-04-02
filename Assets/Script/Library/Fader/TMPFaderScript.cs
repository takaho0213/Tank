using UnityEngine;

/// <summary>TMP�̃t�F�[�_�[�N���X</summary>
public class TMPFaderScript : BaseFaderScript
{
    /// <summary>�t�F�[�h������TMP�z��</summary>
    [SerializeField] protected TMPro.TextMeshProUGUI[] TMPs;

    public override Color FadeColor
    {
        get => TMPs[default].color;
        set { foreach (var t in TMPs) t.color = value; }
    }
}
