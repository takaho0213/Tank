using UnityEngine;

/// <summary>�^���N�����˂���e�̃��C��</summary>
public class TankBulletLineScript : MonoBehaviour
{
    /// <summary>���C����\��������LineRenderer</summary>
    [SerializeField, LightColor] private LineRenderer lineRenderer;

    /// <summary>�T���ۉ�]������g�����X�t�H�[��</summary>
    [SerializeField, LightColor] private Transform originTrafo;

    /// <summary>���C�����m���郌�C���[</summary>
    [SerializeField] private LayerMask rayLayer;

    /// <summary>���C���̕�</summary>
    [SerializeField] private float lineWidth;

    /// <summary>������</summary>
    /// <param name="color">���C���̃J���[</param>
    public void Init(Color color)
    {
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;//���C���̕�

        SetLineColor(color);                                        //�F���Z�b�g
    }

    /// <summary>���C�����폜</summary>
    public void Crear()
    {
        for (int i = default; i < lineRenderer.positionCount; i++)//���C���̐����J��Ԃ�
        {
            lineRenderer.SetPosition(i, default);                 //���C���̏ꏊ���Z�b�g
        }
    }

    /// <summary>���C���̃J���[���Z�b�g</summary>
    /// <param name="color">�J���[</param>
    public void SetLineColor(Color color)
    {
        lineRenderer.startColor = lineRenderer.endColor = color;//�J���[����
    }

    /// <summary>�J�v�Z���^��Ray�𔭎�</summary>
    /// <returns>�q�b�g������</returns>
    public RaycastHit2D CreateLine()
    {
        var pos = originTrafo.position;                            //���g�̈ʒu

        var hit = Physics2D.Raycast(pos, originTrafo.up, rayLayer);//���C�𔭎�

        int lineCount = default;                                   //���C���̐�

        lineRenderer.SetPosition(lineCount++, pos);                //���C���̏ꏊ���Z�b�g

        lineRenderer.SetPosition(lineCount++, hit.centroid);       //���C���̏ꏊ���Z�b�g

        return hit;                                                //�q�b�g������
    }
}
