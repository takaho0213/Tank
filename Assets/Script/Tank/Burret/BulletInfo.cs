using System.Linq;
using UnityEngine;

/// <summary>�^���N�̒e�̏��</summary>
[System.Serializable]
public class BulletInfo
{
    /// <summary>�����ʒu�Ɗp�x</summary>
    [field: SerializeField, LightColor] public Transform GenerateInfo { get; private set; }

    /// <summary>�e��</summary>
    [field: SerializeField] public float Speed { get; private set; }

    /// <summary>�ő唽�ː�</summary>
    [field: SerializeField] public int MaxReflectCount { get; private set; }

    /// <summary>�F</summary>
    [field: SerializeField] public Color Color { get; private set; }

    /// <summary>�^�O</summary>
    [field: SerializeField, Tag] public string Tag { get; private set; }

    /// <summary>���˂��Ȃ��^�O</summary>
    [field: SerializeField, Tag] public string[] NoReflectTags { get; private set; }

    /// <summary>�_���[�W���󂯂Ȃ��^�O</summary>
    [SerializeField, Tag] private string[] NoDamageTags;

    /// <summary>�e���ƍő唽�ː����Z�b�g</summary>
    /// <param name="speed">�e��</param>
    /// <param name="count">�ő唽�ː�</param>
    /// <param name="color">�e�̐F</param>
    public void Init(float speed, int count, Color color)
    {
        Speed = speed;
        MaxReflectCount = count;
        Color = color;
    }

    /// <summary>�_���[�W���󂯂邩</summary>
    /// <param name="hit">�ڐG�����R���C�_�[</param>
    /// <returns>�_���[�W���󂯂邩</returns>
    public bool IsDamage(Collider2D hit) => !NoDamageTags.Any((v) => hit.CompareTag(v));//�_���[�W���󂯂Ȃ��^�O���܂܂�Ă��Ȃ���
}
