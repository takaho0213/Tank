using UnityEngine;

/// <summary>�C���[�W�̃t�F�[�_�[�N���X</summary>
public class ImageFaderScript : BaseFaderScript
{
    /// <summary>�t�F�[�h������C���[�W�z��</summary>
    [SerializeField] protected UnityEngine.UI.Image[] images;

    public override Color FadeColor
    {
        get => images[default].color;
        set { foreach (var i in images) i.color = value; }
    }
}