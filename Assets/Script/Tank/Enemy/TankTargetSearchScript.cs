using UnityEngine;

public class TankTargetSearchScript : MonoBehaviour
{
    [SerializeField, LightColor] private Collider2D targetColl;

    /// <summary>���˂��郌�C�̌`</summary>
    [SerializeField, LightColor] private CapsuleCollider2D rayColl;

    /// <summary>�T���ۉ�]������g�����X�t�H�[��</summary>
    [SerializeField, LightColor] private Transform trafo;

    /// <summary>��]���������</summary>
    [SerializeField] private Vector3 angle;

    /// <summary>���C�����m���郌�C���[</summary>
    [SerializeField] private LayerMask rayLayer;

    /// <summary>�v���C���[�̃��C���[</summary>
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float lineWidth;

    /// <summary>���˂��郌�C�̌`�̃g�����X�t�H�[��</summary>
    private Transform rayCollTrafo;

    /// <summary>�J�v�Z���^��Ray�𔭎�</summary>
    /// <param name="o">���˂�����W</param>
    /// <param name="d">���˂���x�N�g��</param>
    /// <param name="l">���C���[</param>
    /// <returns>���C�̏��</returns>
    private RaycastHit2D CapsuleRayCast(Vector2 o, Vector2 d, LayerMask l)
    {
        rayCollTrafo ??= rayColl.transform;                                          //null�Ȃ���

        var size = rayColl.size * rayCollTrafo.localScale;                           //�T�C�Y

        var type = CapsuleDirection2D.Vertical;                                      //����

        var angle = rayCollTrafo.eulerAngles.z;                                      //�p�x

        var ray = Physics2D.CapsuleCast(o, size, type, angle, d, Mathf.Infinity, l); //���C

        return ray;                                                                  //���C�̏���Ԃ�
    }

    /// <summary>�J�v�Z���^��Ray�𔭎�</summary>
    /// <param name="target">�^�[�Q�b�g���W�܂��͕���</param>
    /// <param name="hit">�ڐG�����I�u�W�F�N�g�̏��</param>
    /// <returns>�q�b�g������</returns>
    public bool Ray(Vector3 target)
    {
        var pos = trafo.position;                         //���g�̈ʒu

        var hit = CapsuleRayCast(pos, target - pos, rayLayer);//

        return hit && hit.collider == targetColl;//�q�b�g������
    }

    public Vector2 Rotate()
    {
        trafo.Rotate(angle);//��]

        return trafo.up;
    }

    /// <summary>���˂���J�v�Z���^��Ray�𔭎�</summary>
    /// <returns>���ː�Ƀq�b�g������ ? ��x�ڂ̃��C�̕��� : (0, 0)</returns>
    public bool ReflectionRay()
    {
        var pos = trafo.position;                                                              //���g�̈ʒu

        var d = trafo.up;                                                                      //���C�̕���

        var ray1 = CapsuleRayCast(pos, d, rayLayer);                                           //���C�̏��

        var ray2 = CapsuleRayCast(ray1.centroid, Vector2.Reflect(d, ray1.normal), playerLayer);//�q�b�g�����ꏊ���烌�C�𔭎˂��q�b�g������

        return ray2 && ray2.collider == targetColl;                                                                           //�q�b�g������ ? ��x�ڂ̃��C�̕��� : (0, 0)
    }
}
