using UnityEngine;

public static class VectorEx
{
    /// <summary>1280, 720</summary>
    public readonly static Vector2 HD = new(1280f, 720f);
    /// <summary>1920, 1080</summary>
    public readonly static Vector2 FHD = new(1920f, 1080f);
    /// <summary>2560, 1440</summary>
    public readonly static Vector2 WQHD = new(2560f, 1440f);
    /// <summary>3840, 2160</summary>
    public readonly static Vector2 UHD = new(3840f, 2160f);
    /// <summary>7680, 4320</summary>
    public readonly static Vector2 SHV = new(7680f, 4320f);

    /// <summary>Xをセット</summary>
    public static Vector2 SetX(this Vector2 v, float x) => new(x, v.y);
    /// <summary>Yをセット</summary>
    public static Vector2 SetY(this Vector2 v, float y) => new(v.x, y);

    /// <summary>Xをセット</summary>
    public static Vector3 SetX(this Vector3 v, float x) => new(x, v.y, v.z);
    /// <summary>Yをセット</summary>
    public static Vector3 SetY(this Vector3 v, float y) => new(v.x, y, v.z);
    public static Vector3 SetZ(this Vector3 v, float z) => new(v.x, v.y, z);

    /// <summary>Xをセット</summary>
    public static Vector4 SetX(this Vector4 v, float x) => new(x, v.y, v.z, v.w);
    /// <summary>Yをセット</summary>
    public static Vector4 SetY(this Vector4 v, float y) => new(v.x, y, v.z, v.w);
    /// <summary>Zをセット</summary>
    public static Vector4 SetZ(this Vector4 v, float z) => new(v.x, v.y, z, v.w);
    /// <summary>Wをセット</summary>
    public static Vector4 SetW(this Vector4 v, float w) => new(v.x, v.y, v.z, w);

    /// <summary>絶対値</summary>
    public static Vector2 Abs(this Vector2 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y));
    /// <summary>絶対値</summary>
    public static Vector3 Abs(this Vector3 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    /// <summary>絶対値</summary>
    public static Vector4 Abs(this Vector4 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z), Mathf.Abs(v.w));

    /// <summary>切り上げ</summary>
    public static Vector2 Ceil(this Vector2 v) => new(Mathf.Ceil(v.x), Mathf.Ceil(v.y));
    /// <summary>切り上げ</summary>
    public static Vector3 Ceil(this Vector3 v) => new(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z));
    /// <summary>切り上げ</summary>
    public static Vector4 Ceil(this Vector4 v) => new(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z), Mathf.Ceil(v.w));
    /// <summary>切り上げ</summary>
    public static Vector2Int CeilToInt(this Vector2 v) => new(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
    /// <summary>切り上げ</summary>
    public static Vector3Int CeilToInt(this Vector3 v) => new(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));

    /// <summary>切り捨て</summary>
    public static Vector2 Floor(this Vector2 v) => new(Mathf.Floor(v.x), Mathf.Floor(v.y));
    /// <summary>切り捨て</summary>
    public static Vector3 Floor(this Vector3 v) => new(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z));
    /// <summary>切り捨て</summary>
    public static Vector4 Floor(this Vector4 v) => new(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z), Mathf.Floor(v.w));
    /// <summary>切り捨て</summary>
    public static Vector2Int FloorToInt(this Vector2 v) => new(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
    /// <summary>切り捨て</summary>
    public static Vector3Int FloorToInt(this Vector3 v) => new(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));

    /// <summary>四捨五入</summary>
    public static Vector2 Round(this Vector2 v) => new(Mathf.Round(v.x), Mathf.Round(v.y));
    /// <summary>四捨五入</summary>
    public static Vector3 Round(this Vector3 v) => new(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    /// <summary>四捨五入</summary>
    public static Vector4 Round(this Vector4 v) => new(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z), Mathf.Round(v.w));
    /// <summary>四捨五入</summary>
    public static Vector2Int RoundToInt(this Vector2 v) => new(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    /// <summary>四捨五入</summary>
    public static Vector3Int RoundToInt(this Vector3 v) => new(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));

    /// <summary>0～1内に抑える</summary>
    public static Vector2 Clamp01(this Vector2 v) => new(Mathf.Clamp01(v.x), Mathf.Clamp01(v.y));
    /// <summary>0～1内に抑える</summary>
    public static Vector3 Clamp01(this Vector3 v) => new(Mathf.Clamp01(v.x), Mathf.Clamp01(v.y), Mathf.Clamp01(v.z));
    /// <summary>0～1内に抑える</summary>
    public static Vector4 Clamp01(this Vector4 v) => new(Mathf.Clamp01(v.x), Mathf.Clamp01(v.y), Mathf.Clamp01(v.z), Mathf.Clamp01(v.w));

    /// <summary>Min～Max内に抑える</summary>
    public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max) => new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));
    /// <summary>Min～Max内に抑える</summary>
    public static Vector3 Clamp(this Vector3 v, Vector3 min, Vector3 max) => new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z));
    /// <summary>Min～Max内に抑える</summary>
    public static Vector4 Clamp(this Vector4 v, Vector4 min, Vector4 max) => new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z), Mathf.Clamp(v.w, min.w, max.w));
    /// <summary>Min～Max内に抑える</summary>
    public static Vector2Int Clamp(this Vector2Int v, Vector2Int min, Vector2Int max) => new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));
    /// <summary>Min～Max内に抑える</summary>
    public static Vector3Int Clamp(this Vector3Int v, Vector3Int min, Vector3Int max) => new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z));

    /// <summary>ほぼ等しいか</summary>
    public static bool Approximately(this Vector2 v1, Vector2 v2) => Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.x, v2.x);
    /// <summary>ほぼ等しいか</summary>
    public static bool Approximately(this Vector3 v1, Vector3 v2) => Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y) && Mathf.Approximately(v1.z, v2.z);
    /// <summary>ほぼ等しいか</summary>
    public static bool Approximately(this Vector4 v1, Vector4 v2) => Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y) && Mathf.Approximately(v1.z, v2.z) && Mathf.Approximately(v1.w, v2.w);
}
