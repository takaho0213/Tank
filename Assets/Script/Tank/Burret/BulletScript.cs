using UnityEngine;
using System.Collections;

/// <summary>�^���N�̒e</summary>
public class BulletScript : MonoBehaviour
{
    [SerializeField, LightColor] private BulletEffectScript effect;

    [SerializeField, LightColor] private BulletReflectScript reflect;

    /// <summary>�S�̃I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject obj;

    /// <summary>�e�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject bodyObj;

    /// <summary>�S�̃g�����X�t�H�[��</summary>
    [SerializeField, LightColor] private Transform trafo;

    /// <summary>Rigidbody</summary>
    [SerializeField, LightColor] private Rigidbody2D ribo;

    /// <summary>�h��Ԃ��X�v���C�g�����_���[�z��</summary>
    [SerializeField, LightColor] private SpriteRenderer[] fillSprites;

    /// <summary>�e�̏��</summary>
    private BulletInfo Info;

    /// <summary>�A�N�e�B�u��</summary>
    public bool IsActive
    {
        get => obj.activeSelf;
        private set => obj.SetActive(value);
    }

    /// <summary>��A�N�e�B�u</summary>
    public void Inactive()
    {
        IsActive = false;              //��A�N�e�B�u

        effect.IsActive = false;       //�����G�t�F�N�g�I�u�W�F�N�g���A�N�e�B�u

        bodyObj.SetActive(true);       //�e�I�u�W�F�N�g���A�N�e�B�u

        reflect.ReSetReflectionCount();//
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (!Info.IsDamage(hit)) return;             //�_���[�W���󂯂Ȃ����/�I��

        if (reflect.IsReflection(hit))               //���˂ł���Ȃ�
        {
            ribo.velocity = reflect.Reflection(ribo);//

            trafo.up = ribo.velocity.normalized;     //�ړ��x�N�g������

            return;                                  //�I��
        }

        StartCoroutine(ExplosionEffect());           //�����G�t�F�N�g���J�n
    }
    /// <summary>��A�N�e�B�u�ɂ��邷��ۂ̃G�t�F�N�g</summary>
    private IEnumerator ExplosionEffect()
    {
        ribo.velocity = Vector2.zero;         //�ړ��x�N�g����(0, 0)�ɂ���

        bodyObj.SetActive(false);             //�e�I�u�W�F�N�g���A�N�e�B�u

        yield return effect.ExplosionEffect();//�����G�t�F�N�g��\��

        Inactive();                           //��A�N�e�B�u
    }

    /// <summary>�e�𔭎�</summary>
    /// <param name="info">���˂���ۂ̏��</param>
    public void Shoot(BulletInfo info)
    {
        Init(info);                           //

        IsActive = true;                      //�A�N�e�B�u

        ribo.velocity = trafo.up * info.Speed;//���� * �e������
    }

    /// <summary></summary>
    /// <param name="info"></param>
    private void Init(BulletInfo info)
    {
        Info = info;                    //������

        bodyObj.tag = info.Tag;         //�^�O����

        effect.JetSetActive(info.Speed);//�W�F�b�g�G�t�F�N�g�̃A�N�e�B�u��Ԃ��Z�b�g

        reflect.Init(info);             //

        var t = info.GenerateInfo;      //�����ʒu�Ɗp�x

        trafo.position = t.position;    //�ʒu����
        trafo.rotation = t.rotation;    //�p�x����

        foreach (var s in fillSprites)  //FillSprites���J��Ԃ�
        {
            s.color = info.Color;       //�e�̐F����
        }
    }
}
