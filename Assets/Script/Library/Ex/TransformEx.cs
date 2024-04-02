using UnityEngine;

/// <summary>Transform‚ÌŠg’£ƒNƒ‰ƒX</summary>
public static class TransformEx
{
    public static void LerpPosition(this Transform a, Vector3 b, float t) => a.position = Vector3.Lerp(a.position, b, t);
    public static void LerpPosition(this Transform a, Transform b, float t) => a.LerpPosition(b.position, t);

    public static void LerpRotation(this Transform a, Quaternion b, float t) => a.rotation = Quaternion.Lerp(a.rotation, b, t);
    public static void LerpRotation(this Transform a, Transform b, float t) => a.LerpRotation(b.rotation, t);

    public static void LerpRotation(this Transform a, Vector3 b, float t, Vector3 f) => a.LerpRotation(Quaternion.LookRotation(f, b), t);

    public static void LerpScale(this Transform a, Vector3 b, float t) => a.localScale = Vector3.Lerp(a.localScale, b, t);
    public static void LerpScale(this Transform a, Transform b, float t) => a.LerpScale(b.localScale, t);

    public static void LerpAngles(this Transform a, Vector3 b, float t) => a.eulerAngles = Vector3.Lerp(a.eulerAngles, b, t);
    public static void LerpAngles(this Transform a, Transform b, float t) => a.LerpAngles(b.eulerAngles, t);

    public static void LerpForward(this Transform a, Vector3 b, float t) => a.forward = Vector3.Lerp(a.forward, b, t);
    public static void LerpForward(this Transform a, Transform b, float t) => a.LerpForward(b.forward, t);

    public static void LerpUp(this Transform a, Vector3 b, float t) => a.up = Vector3.Lerp(a.up, b, t);
    public static void LerpUp(this Transform a, Transform b, float t) => a.LerpUp(b.up, t);

    public static void LerpRight(this Transform a, Vector3 b, float t) => a.right = Vector3.Lerp(a.right, b, t);
    public static void LerpRight(this Transform a, Transform b, float t) => a.LerpRight(b.right, t);

    public static void LerpPivot(this RectTransform a, Vector2 b, float t) => a.pivot = Vector2.Lerp(a.pivot, b, t);
    public static void LerpPivot(this RectTransform a, RectTransform b, float t) => a.LerpPivot(b.pivot, t);

    public static void LerpSizeDelta(this RectTransform a, Vector2 b, float t) => a.sizeDelta = Vector2.Lerp(a.sizeDelta, b, t);
    public static void LerpSizeDelta(this RectTransform a, RectTransform b, float t) => a.LerpSizeDelta(b.sizeDelta, t);

    public static void MoveTowardsPosition(this Transform a, Vector3 b, float t) => a.position = Vector3.MoveTowards(a.position, b, t);
    public static void MoveTowardsPosition(this Transform a, Transform b, float t) => a.MoveTowardsPosition(b.position, t);

    public static void MoveTowardsRotation(this Transform a, Quaternion b, float t) => a.rotation = Quaternion.RotateTowards(a.rotation, b, t);
    public static void MoveTowardsRotation(this Transform a, Transform b, float t) => a.MoveTowardsRotation(b.rotation, t);

    public static void MoveTowardsRotation(this Transform a, Vector3 b, float t, Vector3 f) => a.MoveTowardsRotation(Quaternion.LookRotation(f, b), t);

    public static void MoveTowardsScale(this Transform a, Vector3 b, float t) => a.localScale = Vector3.MoveTowards(a.localScale, b, t);
    public static void MoveTowardsScale(this Transform a, Transform b, float t) => a.MoveTowardsScale(b.localScale, t);

    public static void MoveTowardsAngles(this Transform a, Vector3 b, float t) => a.eulerAngles = Vector3.MoveTowards(a.eulerAngles, b, t);
    public static void MoveTowardsAngles(this Transform a, Transform b, float t) => a.MoveTowardsAngles(b.eulerAngles, t);

    public static void MoveTowardsForward(this Transform a, Vector3 b, float t) => a.forward = Vector3.MoveTowards(a.forward, b, t);
    public static void MoveTowardsForward(this Transform a, Transform b, float t) => a.MoveTowardsForward(b.forward, t);

    public static void MoveTowardsUp(this Transform a, Vector3 b, float t) => a.up = Vector3.MoveTowards(a.up, b, t);
    public static void MoveTowardsUp(this Transform a, Transform b, float t) => a.MoveTowardsUp(b.up, t);

    public static void MoveTowardsRight(this Transform a, Vector3 b, float t) => a.right = Vector3.MoveTowards(a.right, b, t);
    public static void MoveTowardsRight(this Transform a, Transform b, float t) => a.MoveTowardsRight(b.right, t);

    public static void MoveTowardsPivot(this RectTransform a, Vector2 b, float t) => a.pivot = Vector2.MoveTowards(a.pivot, b, t);
    public static void MoveTowardsPivot(this RectTransform a, RectTransform b, float t) => a.MoveTowardsPivot(b.pivot, t);

    public static void MoveTowardsSizeDelta(this RectTransform a, Vector2 b, float t) => a.sizeDelta = Vector2.MoveTowards(a.sizeDelta, b, t);
    public static void MoveTowardsSizeDelta(this RectTransform a, RectTransform b, float t) => a.MoveTowardsSizeDelta(b.sizeDelta, t);
}
