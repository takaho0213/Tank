using UnityEngine;

/// <summary>�C���[�W��TMP�̃t�F�[�_�[�N���X</summary>
public class UIFaderScript : BaseFaderScript
{
    /// <summary>�t�F�[�h������C���[�W�z��</summary>
    [SerializeField] protected UnityEngine.UI.Image[] images;
    /// <summary>�t�F�[�h������TMP�z��</summary>
    [SerializeField] protected TMPro.TextMeshProUGUI[] TMPs;

    public override Color FadeColor
    {
        get => images[default].color;
        set
        {
            foreach (var i in images) i.color = value;
            foreach (var t in TMPs) t.color = value;
        }
    }
}
