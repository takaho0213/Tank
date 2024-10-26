using UnityEngine;

public static class TransformEx
{
    /// <summary>Position��Lerp</summary>
    public static void LerpPosition(this Transform a, Vector3 b, float t) => a.position = Vector3.Lerp(a.position, b, t);
    /// <summary>Position��Lerp</summary>
    public static void LerpPosition(this Transform a, Transform b, float t) => a.LerpPosition(b.position, t);

    /// <summary>Rotation��Lerp</summary>
    public static void LerpRotation(this Transform a, Quaternion b, float t) => a.rotation = Quaternion.Lerp(a.rotation, b, t);
    /// <summary>Rotation��Lerp</summary>
    public static void LerpRotation(this Transform a, Transform b, float t) => a.LerpRotation(b.rotation, t);

    /// <summary>Rotation��Lerp</summary>
    public static void LerpRotation(this Transform a, Vector3 b, float t, Vector3 f) => a.LerpRotation(Quaternion.LookRotation(f, b), t);

    /// <summary>Scale��Lerp</summary>
    public static void LerpScale(this Transform a, Vector3 b, float t) => a.localScale = Vector3.Lerp(a.localScale, b, t);
    /// <summary>Scale��Lerp</summary>
    public static void LerpScale(this Transform a, Transform b, float t) => a.LerpScale(b.localScale, t);

    /// <summary>Angles��Lerp</summary>
    public static void LerpAngles(this Transform a, Vector3 b, float t) => a.eulerAngles = Vector3.Lerp(a.eulerAngles, b, t);
    /// <summary>Angles��Lerp</summary>
    public static void LerpAngles(this Transform a, Transform b, float t) => a.LerpAngles(b.eulerAngles, t);

    /// <summary>Forward��Lerp</summary>
    public static void LerpForward(this Transform a, Vector3 b, float t) => a.forward = Vector3.Lerp(a.forward, b, t);
    /// <summary>Forward��Lerp</summary>
    public static void LerpForward(this Transform a, Transform b, float t) => a.LerpForward(b.forward, t);

    /// <summary>Up��Lerp</summary>
    public static void LerpUp(this Transform a, Vector3 b, float t) => a.up = Vector3.Lerp(a.up, b, t);
    /// <summary>Up��Lerp</summary>
    public static void LerpUp(this Transform a, Transform b, float t) => a.LerpUp(b.up, t);

    /// <summary>Right��Lerp</summary>
    public static void LerpRight(this Transform a, Vector3 b, float t) => a.right = Vector3.Lerp(a.right, b, t);
    /// <summary>Right��Lerp</summary>
    public static void LerpRight(this Transform a, Transform b, float t) => a.LerpRight(b.right, t);

    /// <summary>Pivot��Lerp</summary>
    public static void LerpPivot(this RectTransform a, Vector2 b, float t) => a.pivot = Vector2.Lerp(a.pivot, b, t);
    /// <summary>Pivot��Lerp</summary>
    public static void LerpPivot(this RectTransform a, RectTransform b, float t) => a.LerpPivot(b.pivot, t);

    /// <summary>SizeDelta��Lerp</summary>
    public static void LerpSizeDelta(this RectTransform a, Vector2 b, float t) => a.sizeDelta = Vector2.Lerp(a.sizeDelta, b, t);
    /// <summary>SizeDelta��Lerp</summary>
    public static void LerpSizeDelta(this RectTransform a, RectTransform b, float t) => a.LerpSizeDelta(b.sizeDelta, t);

    /// <summary>AnchoredPosition��Lerp</summary>
    public static void LerpAnchoredPosition(this RectTransform a, Vector2 b, float t) => a.anchoredPosition = Vector2.Lerp(a.anchoredPosition, b, t);
    /// <summary>AnchoredPosition��Lerp</summary>
    public static void LerpAnchoredPosition(this RectTransform a, RectTransform b, float t) => a.LerpAnchoredPosition(b.anchoredPosition, t);

    /// <summary>AnchoredPositionX��Lerp</summary>
    public static void LerpAnchoredPositionX(this RectTransform a, float b, float t) => a.LerpAnchoredPosition(a.anchoredPosition.SetX(b), t);
    /// <summary>AnchoredPositionX��Lerp</summary>
    public static void LerpAnchoredPositionY(this RectTransform a, float b, float t) => a.LerpAnchoredPosition(a.anchoredPosition.SetY(b), t);

    /// <summary>Position��MoveTowards</summary>
    public static void MoveTowardsPosition(this Transform a, Vector3 b, float t) => a.position = Vector3.MoveTowards(a.position, b, t);
    /// <summary>Position��MoveTowards</summary>
    public static void MoveTowardsPosition(this Transform a, Transform b, float t) => a.MoveTowardsPosition(b.position, t);

    /// <summary>Rotation��MoveTowards</summary>
    public static void MoveTowardsRotation(this Transform a, Quaternion b, float t) => a.rotation = Quaternion.RotateTowards(a.rotation, b, t);
    /// <summary>Rotation��MoveTowards</summary>
    public static void MoveTowardsRotation(this Transform a, Transform b, float t) => a.MoveTowardsRotation(b.rotation, t);

    /// <summary>Rotation��MoveTowards</summary>
    public static void MoveTowardsRotation(this Transform a, Vector3 b, float t, Vector3 f) => a.MoveTowardsRotation(Quaternion.LookRotation(f, b), t);

    /// <summary>Scale��MoveTowards</summary>
    public static void MoveTowardsScale(this Transform a, Vector3 b, float t) => a.localScale = Vector3.MoveTowards(a.localScale, b, t);
    /// <summary>Scale��MoveTowards</summary>
    public static void MoveTowardsScale(this Transform a, Transform b, float t) => a.MoveTowardsScale(b.localScale, t);

    /// <summary>Angles��MoveTowards</summary>
    public static void MoveTowardsAngles(this Transform a, Vector3 b, float t) => a.eulerAngles = Vector3.MoveTowards(a.eulerAngles, b, t);
    /// <summary>Angles��MoveTowards</summary>
    public static void MoveTowardsAngles(this Transform a, Transform b, float t) => a.MoveTowardsAngles(b.eulerAngles, t);

    /// <summary>Forward��MoveTowards</summary>
    public static void MoveTowardsForward(this Transform a, Vector3 b, float t) => a.forward = Vector3.MoveTowards(a.forward, b, t);
    /// <summary>Forward��MoveTowards</summary>
    public static void MoveTowardsForward(this Transform a, Transform b, float t) => a.MoveTowardsForward(b.forward, t);

    /// <summary>Up��MoveTowards</summary>
    public static void MoveTowardsUp(this Transform a, Vector3 b, float t) => a.up = Vector3.MoveTowards(a.up, b, t);
    /// <summary>Up��MoveTowards</summary>
    public static void MoveTowardsUp(this Transform a, Transform b, float t) => a.MoveTowardsUp(b.up, t);

    /// <summary>Right��MoveTowards</summary>
    public static void MoveTowardsRight(this Transform a, Vector3 b, float t) => a.right = Vector3.MoveTowards(a.right, b, t);
    /// <summary>Right��MoveTowards</summary>
    public static void MoveTowardsRight(this Transform a, Transform b, float t) => a.MoveTowardsRight(b.right, t);

    /// <summary>Pivot��MoveTowards</summary>
    public static void MoveTowardsPivot(this RectTransform a, Vector2 b, float t) => a.pivot = Vector2.MoveTowards(a.pivot, b, t);
    /// <summary>Pivot��MoveTowards</summary>
    public static void MoveTowardsPivot(this RectTransform a, RectTransform b, float t) => a.MoveTowardsPivot(b.pivot, t);

    /// <summary>SizeDelta��MoveTowards</summary>
    public static void MoveTowardsSizeDelta(this RectTransform a, Vector2 b, float t) => a.sizeDelta = Vector2.MoveTowards(a.sizeDelta, b, t);
    /// <summary>SizeDelta��MoveTowards</summary>
    public static void MoveTowardsSizeDelta(this RectTransform a, RectTransform b, float t) => a.MoveTowardsSizeDelta(b.sizeDelta, t);

    /// <summary>AnchoredPosition��MoveTowards</summary>
    public static void MoveTowardsAnchoredPosition(this RectTransform a, Vector2 b, float t) => a.anchoredPosition = Vector2.MoveTowards(a.anchoredPosition, b, t);
    /// <summary>AnchoredPosition��MoveTowards</summary>
    public static void MoveTowardsAnchoredPosition(this RectTransform a, RectTransform b, float t) => a.MoveTowardsAnchoredPosition(b.anchoredPosition, t);

    /// <summary>AnchoredPositionX��MoveTowards</summary>
    public static void MoveTowardsAnchoredPositionX(this RectTransform a, float b, float t) => a.MoveTowardsAnchoredPosition(a.anchoredPosition.SetX(b), t);
    /// <summary>AnchoredPositionY��MoveTowards</summary>
    public static void MoveTowardsAnchoredPositionY(this RectTransform a, float b, float t) => a.MoveTowardsAnchoredPosition(a.anchoredPosition.SetY(b), t);
}
