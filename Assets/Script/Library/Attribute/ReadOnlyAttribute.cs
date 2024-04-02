using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>読み取り専用にする属性(Ctrlを押している間は編集可能)</summary>
[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute { }
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    protected bool isControlKey => !Event.current.control;

    /// <summary>読み取り専用か？</summary>
    protected virtual bool IsReadOnly(Rect r, SerializedProperty p, GUIContent l)
    {
        return isControlKey && p.propertyType switch
        {
            SerializedPropertyType.String          => !string.IsNullOrEmpty(p.stringValue),
            SerializedPropertyType.ObjectReference => p.objectReferenceValue != null,
            SerializedPropertyType.Integer         => p.intValue != default,
            SerializedPropertyType.Float           => p.floatValue != default,
            SerializedPropertyType.Color           => p.colorValue != default,
            SerializedPropertyType.Vector2         => p.vector2Value != default,
            SerializedPropertyType.Vector3         => p.vector3Value != default,
            SerializedPropertyType.Vector4         => p.vector4Value != default,
            SerializedPropertyType.Vector2Int      => p.vector2IntValue != default,
            SerializedPropertyType.Vector3Int      => p.vector3IntValue != default,
            SerializedPropertyType.Quaternion      => p.quaternionValue != default,
            SerializedPropertyType.Rect            => p.rectValue != default,
            SerializedPropertyType.AnimationCurve  => p.animationCurveValue != default,
            _                                      => isControlKey,
        };
    }

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        l.tooltip = "Ctrlを押している間は編集可能";

        EditorGUI.BeginDisabledGroup(IsReadOnly(r, p, l));
        EditorGUI.PropertyField(r, p, l);
        EditorGUI.EndDisabledGroup();
    }
}
#endif
