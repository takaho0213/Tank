using System.Linq;
using UnityEngine;

public class BulletReflectScript : MonoBehaviour
{
    /// <summary>�R���C�_�[</summary>
    [SerializeField, LightColor] private CapsuleCollider2D Coll;

    /// <summary>����AudioSource</summary>
    [SerializeField, LightColor] private AudioSource reflectSource;

    /// <summary>���C�����m���郌�C���[</summary>
    [SerializeField] private LayerMask reflectLayer;

    /// <summary></summary>
    private string[] noReflectTags;

    /// <summary>�ő唽�ː�</summary>
    private int maxReflectCount;

    /// <summary>���݂̔��ː�</summary>
    private int reflectCount;

    private ClipInfo reflectSE;

    public void Init(BulletInfo info)
    {
        noReflectTags = info.NoReflectTags;

        maxReflectCount = info.MaxReflectCount;
    }

    public bool IsReflection(Collider2D hit)
    {
        bool isReflection = maxReflectCount > reflectCount;            //���˂ł��邩

        bool isNoReflection = noReflectTags.Any((v) => hit.CompareTag(v));//���˂ł��Ȃ���

        return isReflection && !isNoReflection;                              //���˂ł��邩��Ԃ�
    }

    public Vector2 Reflection(Vector2 velocity, Vector2 normal)
    {
        reflectSE ??= AudioScript.I.BulletAudio[BulletClip.Reflection];     //����SE�N���b�v����

        reflectSE.PlayOneShot(reflectSource);                            //����SE���Đ�

        reflectCount++;                                                     //���ː��� + 1

        return Vector2.Reflect(velocity, normal);//���ˊp����
    }

    public Vector2 Reflection(Rigidbody2D ribo)
    {
        return Reflection(ribo.velocity, RayNormalVector(ribo.transform));//���ˊp����
    }

    /// <summary>���C�𔭎˂��@���x�N�g����Ԃ�</summary>
    /// <returns>�@���x�N�g��</returns>
    private Vector2 RayNormalVector(Transform trafo)
    {
        var size = Coll.size * trafo.localScale;                                                    //�T�C�Y

        var type = CapsuleDirection2D.Vertical;                                                     //�R���C�_�[�̃^�C�v

        var angle = Coll.transform.eulerAngles.z;                                                   //�p�x

        var dis = Coll.size.y;                                                                      //����

        return Physics2D.CapsuleCast(trafo.position, size, type, angle, trafo.up, dis, reflectLayer).normal;//Ray�𔭎˂��@���x�N�g����Ԃ�
    }

    public void ReSetReflectionCount()
    {
        reflectCount = default;
    }
}
