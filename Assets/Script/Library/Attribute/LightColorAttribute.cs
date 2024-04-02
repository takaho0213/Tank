using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>インスペクターに表示する際灰色にする属性</summary>
[AttributeUsage(AttributeTargets.Field)]
public class LightColorAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LightColorAttribute))]
public class LightColorDrawer : PropertyDrawer
{
    private readonly Color LightColor = new(1f, 1f, 1f, 0.5f);

    private bool IsGray(SerializedProperty p) => p.propertyType switch
    {
        SerializedPropertyType.String          => p.stringValue          != default,
        SerializedPropertyType.ObjectReference => p.objectReferenceValue != default,
        SerializedPropertyType.Integer         => p.intValue             != default,
        SerializedPropertyType.Float           => p.floatValue           != default,
        SerializedPropertyType.Color           => p.colorValue           != default,
        SerializedPropertyType.Vector2         => p.vector2Value         != default,
        SerializedPropertyType.Vector3         => p.vector3Value         != default,
        SerializedPropertyType.Vector4         => p.vector4Value         != default,
        SerializedPropertyType.Quaternion      => p.quaternionValue      != default,
        _                                      => true,
    };

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var init = GUI.color;

        if (IsGray(p)) GUI.color = LightColor;

        EditorGUI.PropertyField(r, p, l);

        GUI.color = init;
    }
}
#endif
