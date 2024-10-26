using UnityEngine;
using System.Linq;
using System.Collections;

/// <summary>�^���N�̒e</summary>
public class BulletScript : MonoBehaviour
{
    /// <summary>�S�̃I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject Obj;

    /// <summary>�e�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject BodyObj;

    /// <summary>�����G�t�F�N�g�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject ExplosionObj;

    /// <summary>�W�F�b�g�G�t�F�N�g�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject JetObj;

    /// <summary>�S�̃g�����X�t�H�[��</summary>
    [SerializeField, LightColor] private Transform Trafo;

    /// <summary>Rigidbody</summary>
    [SerializeField, LightColor] private Rigidbody2D Ribo;

    /// <summary>�R���C�_�[</summary>
    [SerializeField, LightColor] private CapsuleCollider2D Coll;

    /// <summary>����AudioSource</summary>
    [SerializeField, LightColor] private AudioSource ReflectionSource;

    /// <summary>����AudioSource</summary>
    [SerializeField, LightColor] private AudioSource ExplosionSource;

    /// <summary>�h��Ԃ��X�v���C�g�����_���[�z��</summary>
    [SerializeField, LightColor] private SpriteRenderer[] FillSprites;

    /// <summary>�W�F�b�g�G�t�F�N�g���o�������鑬�x</summary>
    [SerializeField] private float JetEffectSpeed;

    /// <summary>���C�����m���郌�C���[</summary>
    [SerializeField] private LayerMask Layer;

    /// <summary>��~����</summary>
    private WaitForSeconds Wait;

    /// <summary>�e�̏��</summary>
    private BulletInfo Info;

    /// <summary>���݂̔��ː�</summary>
    private int ReflectionCount;

    /// <summary>�A�N�e�B�u��</summary>
    public bool IsActive
    {
        get => Obj.activeSelf;
        private set => Obj.SetActive(value);
    }

    /// <summary>��A�N�e�B�u</summary>
    public void Inactive()
    {
        ExplosionObj.SetActive(false);//�����G�t�F�N�g�I�u�W�F�N�g���A�N�e�B�u

        BodyObj.SetActive(true);      //�e�I�u�W�F�N�g���A�N�e�B�u

        IsActive = false;             //��A�N�e�B�u

        Coll.enabled = true;          //�R���C�_�[���I��

        ReflectionCount = default;    //���ː������Z�b�g
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (!Info.IsDamage(hit)) return;                                                         //�_���[�W���󂯂Ȃ����/�I��

        if (Info.IsReflection(hit, ReflectionCount))                                             //���˂ł���Ȃ�
        {
            Ribo.velocity = Vector2.Reflect(Ribo.velocity, RayNormalVector());                   //���ˊp����

            Trafo.up = Ribo.velocity.normalized;                                                 //�ړ��x�N�g������

            ReflectionCount++;                                                                   //���ː��� + 1

            AudioScript.I.BulletAudio.Dictionary[BulletClip.Reflection].PlayOneShot(ReflectionSource);//����SE���Đ�

            return;                                                                              //�I��
        }

        StartCoroutine(ExplosionEffect());                                                       //�����G�t�F�N�g���J�n
    }

    /// <summary>���C�𔭎˂��@���x�N�g����Ԃ�</summary>
    /// <returns>�@���x�N�g��</returns>
    private Vector2 RayNormalVector()
    {
        var size = Coll.size * Trafo.localScale;                                                    //�T�C�Y

        var type = CapsuleDirection2D.Vertical;                                                     //�R���C�_�[�̃^�C�v

        var angle = Coll.transform.eulerAngles.z;                                                   //�p�x

        var dis = Coll.size.y;                                                                      //����

        return Physics2D.CapsuleCast(Ribo.position, size, type, angle, Trafo.up, dis, Layer).normal;//Ray�𔭎˂��@���x�N�g����Ԃ�
    }

    /// <summary>��A�N�e�B�u�ɂ��邷��ۂ̃G�t�F�N�g</summary>
    private IEnumerator ExplosionEffect()
    {
        Ribo.velocity = Vector2.zero;                                 //�ړ��x�N�g����(0, 0)�ɂ���

        Coll.enabled = false;                                         //�R���C�_�[���I�t

        BodyObj.SetActive(false);                                   //�e�I�u�W�F�N�g���A�N�e�B�u

        ExplosionObj.SetActive(true);                                 //�����G�t�F�N�g�I�u�W�F�N�g���A�N�e�B�u

        var c = AudioScript.I.BulletAudio.Dictionary[BulletClip.Explosion];//����SE�N���b�v

        c.PlayOneShot(ExplosionSource);                               //����SE���Đ�

        yield return Wait ??= new WaitForSeconds(c.Clip.length);      //SE�̕b������~

        Inactive();                                                   //��A�N�e�B�u
    }

    /// <summary>�e�𔭎�</summary>
    /// <param name="info">���˂���ۂ̏��</param>
    public void Shoot(BulletInfo info)
    {
        IsActive = true;                               //�A�N�e�B�u

        Info = info;                                   //������

        tag = info.Tag;                                //�^�O����

        JetObj.SetActive(info.Speed >= JetEffectSpeed);//�e�����W�F�b�g�G�t�F�N�g���o�������鑬�x�ȏ�Ȃ�W�F�b�g�G�t�F�N�g���A�N�e�B�u

        var trafo = info.GenerateInfo;                 //�����ʒu�Ɗp�x

        Trafo.position = trafo.position;               //�ʒu����
        Trafo.rotation = trafo.rotation;               //�p�x����

        Ribo.velocity = Trafo.up * info.Speed;         //���� * �e������

        foreach (var s in FillSprites)                 //FillSprites���J��Ԃ�
        {
            s.color = info.Color;                      //�e�̐F����
        }
    }
}

/// <summary>�^���N�̒e�̏��</summary>
[System.Serializable]
public class BulletInfo
{
    /// <summary>�����ʒu�Ɗp�x</summary>
    [field: SerializeField, LightColor] public Transform GenerateInfo { get; private set; }

    /// <summary>�e��</summary>
    [field: SerializeField] public float Speed { get; private set; }

    /// <summary>�ő唽�ː�</summary>
    [SerializeField] private int MaxReflectionCount;

    /// <summary>�F</summary>
    [field: SerializeField] public Color Color { get; private set; }

    /// <summary>�^�O</summary>
    [field: SerializeField, Tag] public string Tag { get; private set; }

    /// <summary>���˂��Ȃ��^�O</summary>
    [SerializeField, Tag] private string[] NoReflectionTags;
    /// <summary>�_���[�W���󂯂Ȃ��^�O</summary>
    [SerializeField, Tag] private string[] NoDamageTags;

    /// <summary>�e���ƍő唽�ː����Z�b�g</summary>
    /// <param name="speed">�e��</param>
    /// <param name="count">�ő唽�ː�</param>
    /// <param name="color">�e�̐F</param>
    public void Init(float speed, int count, Color color)
    {
        Speed = speed;
        MaxReflectionCount = count;
        Color = color;
    }

    /// <summary>�_���[�W���󂯂邩</summary>
    /// <param name="hit">�ڐG�����R���C�_�[</param>
    /// <returns>�_���[�W���󂯂邩</returns>
    public bool IsDamage(Collider2D hit) => !NoDamageTags.Any((v) => hit.CompareTag(v));//�_���[�W���󂯂Ȃ��^�O���܂܂�Ă��Ȃ���

    /// <summary>���˂ł��邩</summary>
    /// <param name="hit">�ڐG�����R���C�_�[</param>
    /// <param name="count">���݂̔��ː�</param>
    /// <returns>���˂ł��邩</returns>
    public bool IsReflection(Collider2D hit, int count)
    {
        bool isReflection = MaxReflectionCount > count;                      //���˂ł��邩

        bool isNoReflection = NoReflectionTags.Any((v) => hit.CompareTag(v));//���˂ł��Ȃ���

        return isReflection && !isNoReflection;                              //(���˂ł��� ���� ���˂ł���)��Ԃ�
    }
}
