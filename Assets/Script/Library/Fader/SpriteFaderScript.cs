using UnityEngine;

/// <summary>�X�v���C�g�̃t�F�[�_�[�N���X</summary>
public class SpriteFaderScript : BaseFaderScript
{
    /// <summary>�t�F�[�h������X�v���C�g�����_���[�z��</summary>
    [SerializeField] protected SpriteRenderer[] renderers;

    public override Color FadeColor
    {
        get => renderers[default].color;
        set { foreach (var r in renderers) r.color = value; }
    }
}
