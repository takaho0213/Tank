using UnityEngine;

public class GUIWindowScript : MonoBehaviour
{
    [SerializeField] private RectTransform windowRectTrafo;

    private Rect windowRect;

    public GUI.WindowFunction Window { get; set; }

    private void Awake()
    {
        var size = windowRectTrafo.sizeDelta;

        windowRect.size = size;

        var fhd = VectorEx.FHD / MathEx.TwoI;

        var pos = windowRectTrafo.anchoredPosition;

        size /= MathEx.TwoI;

        windowRect.position = fhd + pos - size;
    }

    private void OnGUI()
    {
        if (Window == null) return;

        GUI.Window(default, windowRect, Window, string.Empty);
    }
}
