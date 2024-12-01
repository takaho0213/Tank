using System.Linq;
using UnityEngine;

/// <summary>タンクの弾の情報</summary>
[System.Serializable]
public class BulletInfo
{
    /// <summary>初期位置と角度</summary>
    [field: SerializeField, LightColor] public Transform GenerateInfo { get; private set; }

    /// <summary>弾速</summary>
    [field: SerializeField] public float Speed { get; private set; }

    /// <summary>最大反射数</summary>
    [field: SerializeField] public int MaxReflectCount { get; private set; }

    /// <summary>色</summary>
    [field: SerializeField] public Color Color { get; private set; }

    /// <summary>タグ</summary>
    [field: SerializeField, Tag] public string Tag { get; private set; }

    /// <summary>反射しないタグ</summary>
    [field: SerializeField, Tag] public string[] NoReflectTags { get; private set; }

    /// <summary>ダメージを受けないタグ</summary>
    [SerializeField, Tag] private string[] NoDamageTags;

    /// <summary>弾速と最大反射数をセット</summary>
    /// <param name="speed">弾速</param>
    /// <param name="count">最大反射数</param>
    /// <param name="color">弾の色</param>
    public void Init(float speed, int count, Color color)
    {
        Speed = speed;
        MaxReflectCount = count;
        Color = color;
    }

    /// <summary>ダメージを受けるか</summary>
    /// <param name="hit">接触したコライダー</param>
    /// <returns>ダメージを受けるか</returns>
    public bool IsDamage(Collider2D hit) => !NoDamageTags.Any((v) => hit.CompareTag(v));//ダメージを受けないタグが含まれていないか
}
