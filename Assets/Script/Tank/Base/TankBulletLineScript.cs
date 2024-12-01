using UnityEngine;

public class TankBulletLineScript : MonoBehaviour
{
    /// <summary></summary>
    [SerializeField, LightColor] private LineRenderer lineRenderer;

    /// <summary>探す際回転させるトランスフォーム</summary>
    [SerializeField, LightColor] private Transform originTrafo;

    /// <summary>レイが検知するレイヤー</summary>
    [SerializeField] private LayerMask rayLayer;

    [SerializeField] private float lineWidth;

    /// <summary></summary>
    /// <param name="color"></param>
    public void Init(Color color)
    {
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;

        SetLineColor(color);
    }

    /// <summary></summary>
    public void Crear()
    {
        for (int i = default; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, default);
        }
    }

    /// <summary></summary>
    /// <param name="color"></param>
    public void SetLineColor(Color color)
    {
        lineRenderer.startColor = lineRenderer.endColor = color;
    }

    /// <summary>カプセル型のRayを発射</summary>
    /// <returns>ヒットしたか</returns>
    public RaycastHit2D CreateLine()
    {
        var pos = originTrafo.position;                            //自身の位置

        var hit = Physics2D.Raycast(pos, originTrafo.up, rayLayer);//

        int lineCount = default;                                   //

        lineRenderer.SetPosition(lineCount++, pos);                //

        lineRenderer.SetPosition(lineCount++, hit.centroid);       //

        return hit;                                                //ヒットしたか
    }
}
