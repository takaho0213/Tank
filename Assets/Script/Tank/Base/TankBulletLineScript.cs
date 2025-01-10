using UnityEngine;

/// <summary>タンクが発射する弾のライン</summary>
public class TankBulletLineScript : MonoBehaviour
{
    /// <summary>ラインを表示させるLineRenderer</summary>
    [SerializeField, LightColor] private LineRenderer lineRenderer;

    /// <summary>探す際回転させるトランスフォーム</summary>
    [SerializeField, LightColor] private Transform originTrafo;

    /// <summary>レイが検知するレイヤー</summary>
    [SerializeField] private LayerMask rayLayer;

    /// <summary>ラインの幅</summary>
    [SerializeField] private float lineWidth;

    /// <summary>初期化</summary>
    /// <param name="color">ラインのカラー</param>
    public void Init(Color color)
    {
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;//ラインの幅

        SetLineColor(color);                                        //色をセット
    }

    /// <summary>ラインを削除</summary>
    public void Crear()
    {
        for (int i = default; i < lineRenderer.positionCount; i++)//ラインの数分繰り返す
        {
            lineRenderer.SetPosition(i, default);                 //ラインの場所をセット
        }
    }

    /// <summary>ラインのカラーをセット</summary>
    /// <param name="color">カラー</param>
    public void SetLineColor(Color color)
    {
        lineRenderer.startColor = lineRenderer.endColor = color;//カラーを代入
    }

    /// <summary>カプセル型のRayを発射</summary>
    /// <returns>ヒットしたか</returns>
    public RaycastHit2D CreateLine()
    {
        var pos = originTrafo.position;                            //自身の位置

        var hit = Physics2D.Raycast(pos, originTrafo.up, rayLayer);//レイを発射

        int lineCount = default;                                   //ラインの数

        lineRenderer.SetPosition(lineCount++, pos);                //ラインの場所をセット

        lineRenderer.SetPosition(lineCount++, hit.centroid);       //ラインの場所をセット

        return hit;                                                //ヒットしたか
    }
}
