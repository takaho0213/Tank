#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Interval))]
public class IntervalDrawer : PropertyDrawer
{
    private float Height;
    private const int Count = 3;

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var Time = p.FindPropertyRelative(nameof(Interval.Time));
        var IsAutoReSet = p.FindPropertyRelative(nameof(Interval.IsAutoReSet));

        r.height = Height;

        EditorGUI.LabelField(r, l);

        r.y += Height;
        r.x += Height;

        EditorGUI.PropertyField(r, Time);

        r.y += Height;

        EditorGUI.PropertyField(r, IsAutoReSet);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Height = EditorGUI.GetPropertyHeight(property, label);

        return Height * Count;
    }
}
#endif