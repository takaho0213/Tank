using UnityEditor;
using UnityEngine;

public class ColorHtmlAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ColorHtmlAttribute))]
public class ColorHtmlPropertyDrawer : PropertyDrawer
{
    private const int Width = 100;

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType != SerializedPropertyType.Color)
        {
            EditorGUI.PropertyField(r, p, l);

            return;
        }

        var width = r.width - Width;

        var htmlRect = new Rect(r.x, r.y, width, r.height);

        var colorRect = new Rect(r.x + width, r.y, r.width - width, r.height);

        var html = EditorGUI.TextField(htmlRect, l, $"{ColorEx.Html}{p.colorValue.ToHtml()}");

        if (html.TryToColor(out var color)) p.colorValue = color;

        p.colorValue = EditorGUI.ColorField(colorRect, p.colorValue);
    }
}
#endif