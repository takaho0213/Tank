using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class ClampVector3Attribute : PropertyAttribute
{
    public readonly Vector3 Max;
    public readonly Vector3 Min;

    public ClampVector3Attribute(float maxX, float maxY, float maxZ, float minX, float minY, float minZ)
    {
        Max = new(maxX, maxY, maxZ);
        Min = new(minX, minY, minZ);
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class ClampVector2Attribute : PropertyAttribute
{
    public readonly Vector2 Max;
    public readonly Vector2 Min;

    public ClampVector2Attribute(float maxX, float maxY, float minX, float minY)
    {
        Max = new(maxX, maxY);
        Min = new(minX, minY);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ClampVector3Attribute))]
public class ClampVector3Drawer : PropertyDrawer
{
    private ClampVector3Attribute Attribute => attribute as ClampVector3Attribute;

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var max = Attribute.Max;
        var min = Attribute.Min;

        switch (p.propertyType)
        {
            case SerializedPropertyType.Vector3:

                var v = p.vector3Value;

                v = EditorGUI.Vector3Field(r, l, v);

                p.vector3Value = VectorEx.Clamp(v, min, max);

                break;

            case SerializedPropertyType.Vector3Int:

                var vi = p.vector3IntValue;

                vi = EditorGUI.Vector3IntField(r, l, vi);

                p.vector3IntValue = VectorEx.Clamp(vi, min.RoundToInt(), max.RoundToInt());

                break;

            default:

                EditorGUI.PropertyField(r, p, l);

                break;
        }
    }
}

[CustomPropertyDrawer(typeof(ClampVector2Attribute))]
public class ClampVector2Drawer : PropertyDrawer
{
    private ClampVector2Attribute Attribute => attribute as ClampVector2Attribute;

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var max = Attribute.Max;
        var min = Attribute.Min;

        switch (p.propertyType)
        {
            case SerializedPropertyType.Vector2:

                var v = p.vector2Value;

                v = EditorGUI.Vector2Field(r, l, v);

                p.vector2Value = VectorEx.Clamp(v, min, max);

                break;

            case SerializedPropertyType.Vector2Int:

                var vi = p.vector2IntValue;

                vi = EditorGUI.Vector2IntField(r, l, vi);

                p.vector2Value = VectorEx.Clamp(vi, Vector2Int.RoundToInt(min), max);

                break;

            default:

                EditorGUI.PropertyField(r, p, l);

                break;
        }
    }
}
#endif