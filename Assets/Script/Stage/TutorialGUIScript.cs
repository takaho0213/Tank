using UnityEngine;

public class TutorialGUIScript : MonoBehaviour
{
    [SerializeField] private string text;

    [SerializeField] private Vector2 pos;

    [SerializeField] private Vector2 size;

    private Rect windowRect;

    private void Start()
    {
        var s = size / MathEx.TwoI;

        var fhd = VectorEx.FHD / MathEx.TwoI;

        windowRect.x = fhd.x + pos.x - s.y;
        windowRect.y = fhd.y + pos.y - s.x;

        windowRect.width = size.x;
        windowRect.height = size.y;
    }

    private void Window(int id)
    {
        if (GUILayout.Button("Press me"))
        {

        }
    }

    private void OnGUI()
    {
        GUI.Window(default, windowRect, Window, text);
    }
}
