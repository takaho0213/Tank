using UnityEngine;

public class TankBulletLineScript : MonoBehaviour
{
    [SerializeField, LightColor] private LineRenderer lineRenderer;

    /// <summary>�T���ۉ�]������g�����X�t�H�[��</summary>
    [SerializeField, LightColor] private Transform originTrafo;

    /// <summary>���C�����m���郌�C���[</summary>
    [SerializeField] private LayerMask rayLayer;

    [SerializeField] private float lineWidth;

    public void Init(Color color)
    {
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;

        SetLineColor(color);
    }

    public void Crear()
    {
        for (int i = default; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, default);
        }
    }

    public void SetLineColor(Color color)
    {
        lineRenderer.startColor = lineRenderer.endColor = color;
    }

    /// <summary>�J�v�Z���^��Ray�𔭎�</summary>
    /// <returns>�q�b�g������</returns>
    public RaycastHit2D CreateLine()
    {
        var pos = originTrafo.position;                         //���g�̈ʒu

        var hit = Physics2D.Raycast(pos, originTrafo.up, rayLayer);//

        int lineCount = default;                                //

        lineRenderer.SetPosition(lineCount++, pos);             //

        lineRenderer.SetPosition(lineCount++, hit.centroid);    //

        return hit;                                             //�q�b�g������
    }
}
