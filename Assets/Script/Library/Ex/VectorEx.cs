using UnityEngine;

public static class VectorEx
{
    public static Vector2 SetX(this Vector2 v, float x) => new(x, v.y);
    public static Vector2 SetY(this Vector2 v, float y) => new(v.x, y);

    public static Vector3 SetX(this Vector3 v, float x) => new(x, v.y, v.z);
    public static Vector3 SetY(this Vector3 v, float y) => new(v.x, y, v.z);
    public static Vector3 SetZ(this Vector3 v, float z) => new(v.x, v.y, z);

    public static Vector4 SetX(this Vector4 v, float x) => new(x, v.y, v.z, v.w);
    public static Vector4 SetY(this Vector4 v, float y) => new(v.x, y, v.z, v.w);
    public static Vector4 SetZ(this Vector4 v, float z) => new(v.x, v.y, z, v.w);
    public static Vector4 SetW(this Vector4 v, float w) => new(v.x, v.y, v.z, w);
    //â‘Î’l
    public static Vector2 Abs(this Vector2 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y));
    public static Vector3 Abs(this Vector3 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    public static Vector4 Abs(this Vector4 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z), Mathf.Abs(v.w));
    //Ø‚èã‚°
    public static Vector2 Ceil(this Vector2 v) => new(Mathf.Ceil(v.x), Mathf.Ceil(v.y));
    public static Vector3 Ceil(this Vector3 v) => new(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z));
    public static Vector4 Ceil(this Vector4 v) => new(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z), Mathf.Ceil(v.w));
    //Ø‚èŽÌ‚Ä
    public static Vector2 Floor(this Vector2 v) => new(Mathf.Floor(v.x), Mathf.Floor(v.y));
    public static Vector3 Floor(this Vector3 v) => new(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z));
    public static Vector4 Floor(this Vector4 v) => new(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z), Mathf.Floor(v.w));
    //ŽlŽÌŒÜ“ü
    public static Vector2 Round(this Vector2 v) => new(Mathf.Round(v.x), Mathf.Round(v.y));
    public static Vector3 Round(this Vector3 v) => new(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    public static Vector4 Round(this Vector4 v) => new(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z), Mathf.Round(v.w));
    //0`1“à‚É—}‚¦‚é
    public static Vector2 Clamp01(this Vector2 v) => new(Mathf.Clamp01(v.x), Mathf.Clamp01(v.y));
    public static Vector3 Clamp01(this Vector3 v) => new(Mathf.Clamp01(v.x), Mathf.Clamp01(v.y), Mathf.Clamp01(v.z));
    public static Vector4 Clamp01(this Vector4 v) => new(Mathf.Clamp01(v.x), Mathf.Clamp01(v.y), Mathf.Clamp01(v.z), Mathf.Clamp01(v.w));
    //Min`Max“à‚É—}‚¦‚é
    public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max) => new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));
    public static Vector3 Clamp(this Vector3 v, Vector3 min, Vector3 max) => new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z));
    public static Vector4 Clamp(this Vector4 v, Vector4 min, Vector4 max) => new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z), Mathf.Clamp(v.w, min.w, max.w));
    //‚Ù‚Ú“™‚µ‚¢‚©
    public static bool Approximately(this Vector2 v1, Vector2 v2) => Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.x, v2.x);
    public static bool Approximately(this Vector3 v1, Vector3 v2) => Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y) && Mathf.Approximately(v1.z, v2.z);
    public static bool Approximately(this Vector4 v1, Vector4 v2) => Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y) && Mathf.Approximately(v1.z, v2.z) && Mathf.Approximately(v1.w, v2.w);
}
