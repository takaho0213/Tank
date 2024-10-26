using UnityEngine;

/// <summary>Audio���Ǘ�����N���X</summary>
public class AudioScript : MonoBehaviour
{
    /// <summary>�V���O���g���C���X�^���X</summary>
    public static AudioScript I { get; private set; }

    private void Awake() => I = this;//�C���X�^���X����

    /// <summary>�X�e�[�W�I�[�f�B�I</summary>
    [field: SerializeField] public AudioInfoDictionary<StageClip> StageAudio { get; private set; }

    /// <summary>�^���N�I�[�f�B�I</summary>
    [field: SerializeField] public ClipInfoDictionary<TankClip> TankAudio { get; private set; }

    /// <summary>�e�I�[�f�B�I</summary>
    [field: SerializeField] public ClipInfoDictionary<BulletClip> BulletAudio { get; private set; }
}

/// <summary>�X�e�[�W�̃N���b�v</summary>
public enum StageClip
{
    /// <summary>�J�n</summary>
    Start,
    /// <summary>�N���A</summary>
    Clear,
    /// <summary>�S�N��</summary>
    AllClear,
    /// <summary>�A�C�L���b�`</summary>
    EyeCatch,
    /// <summary>���C�t�ǉ�</summary>
    LifeAdd,
    /// <summary>���C�t����</summary>
    LifeReMove,
    /// <summary>�v���C���[���S</summary>
    PlayerDeath,
    /// <summary>�X�R�A</summary>
    Score,
    /// <summary>BGM</summary>
    BGM,
}

/// <summary>�^���N�̃N���b�v</summary>
public enum TankClip
{
    /// <summary>�ړ�</summary>
    Move,
    /// <summary>�ˌ�</summary>
    Shoot,
    /// <summary>�_���[�W</summary>
    Damage,
    /// <summary>����</summary>
    Explosion,
}

/// <summary>�e�̃N���b�v</summary>
public enum BulletClip
{
    /// <summary>����</summary>
    Reflection,
    /// <summary>����</summary>
    Explosion,
}
