using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>�ǂݎ���p�ɂ��鑮��(Ctrl�������Ă���Ԃ͕ҏW�\)</summary>
[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute { }
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    protected bool isControlKey => !Event.current.control;

    /// <summary>�ǂݎ���p���H</summary>
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
        l.tooltip = "Ctrl�������Ă���Ԃ͕ҏW�\";

        EditorGUI.BeginDisabledGroup(IsReadOnly(r, p, l));
        EditorGUI.PropertyField(r, p, l);
        EditorGUI.EndDisabledGroup();
    }
}
#endif
