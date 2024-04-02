using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>非表示にする属性(Ctrlを押している間は表示)</summary>
[AttributeUsage(AttributeTargets.Field)]
public class HideAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(HideAttribute))]
public class HideDrawer : PropertyDrawer
{
    /// <summary>非表示か？</summary>
    protected bool isHide;

    protected bool isControlKey => !Event.current.control;

    /// <summary>非表示か？</summary>
    protected virtual bool IsHide(Rect r, SerializedProperty p, GUIContent l) //!Event.current.control;
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
            _                                      => isControlKey,
        };
    }

    public override float GetPropertyHeight(SerializedProperty p, GUIContent l) => isHide ? default : base.GetPropertyHeight(p, l);

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        if (!(isHide = IsHide(r, p, l))) EditorGUI.PropertyField(r, p, l);
    }
}
#endif
