#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public static class Drawer
{
    public const float Height = 18f;
    public const float Gap = 2f;

    public const float HeightAndGap = 20f;

    public const string IndentSpace = "    ";

    /// <summary>コントロールが押されているか</summary>
    public static bool IsControlKey => Event.current.control;

    public static float GetPropertyHeight(int index) => (HeightAndGap * index) - Gap;

    public static string Indent(string name) => $"    {name}";

    /// <summary>int型のフィールドを表示</summary>
    public static void IntField(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Integer)
        {
            p.intValue = EditorGUI.IntField(r, l, p.intValue);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>float型のフィールドを表示</summary>
    public static void FloatField(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Float)
        {
            p.floatValue = EditorGUI.FloatField(r, l, p.floatValue);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>bool型のフィールドを表示</summary>
    public static void BooleanField(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Boolean)
        {
            p.boolValue = EditorGUI.Toggle(r, l, p.boolValue);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>string型のフィールドを表示</summary>
    public static void StringField(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.String)
        {
            p.stringValue = EditorGUI.TextField(r, l, p.stringValue);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Color型のフィールドを表示</summary>
    public static void ColorField(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Color)
        {
            p.colorValue = EditorGUI.ColorField(r, l, p.colorValue);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Vector2型のフィールドを表示</summary>
    public static void Vector2Field(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Vector2)
        {
            p.vector2Value = EditorGUI.Vector2Field(r, l, p.vector2Value);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Vector2Int型のフィールドを表示</summary>
    public static void Vector2IntField(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Vector2Int)
        {
            p.vector2IntValue = EditorGUI.Vector2IntField(r, l, p.vector2IntValue);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Vector3型のフィールドを表示</summary>
    public static void Vector3Field(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Vector3)
        {
            p.vector3Value = EditorGUI.Vector3Field(r, l, p.vector3Value);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Vector3Int型のフィールドを表示</summary>
    public static void Vector3IntField(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Vector3Int)
        {
            p.vector3IntValue = EditorGUI.Vector3IntField(r, l, p.vector3IntValue);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Vector4型のフィールドを表示</summary>
    public static void Vector4Field(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.Vector4)
        {
            p.vector4Value = EditorGUI.Vector4Field(r, l, p.vector4Value);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Object派生型のフィールドを表示</summary>
    public static void ObjectField(Rect r, SerializedProperty p, GUIContent l)
    {
        if (p.propertyType == SerializedPropertyType.ObjectReference)
        {
            var value = p.objectReferenceValue;

            EditorGUI.ObjectField(r, l, value, value.GetType(), true);
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Object派生型のフィールドを表示</summary>
    public static void ObjectField<T>(Rect r, SerializedProperty p, GUIContent l, T value) where T : Object
    {
        if (p.propertyType == SerializedPropertyType.ObjectReference)
        {
            var obj = EditorGUI.ObjectField(r, l, value, typeof(T), true);

            p.objectReferenceValue = value = obj as T; ;
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>Object派生型のフィールドを表示</summary>
    public static void ObjectField<T>(Rect r, SerializedProperty p, GUIContent l, ref T value) where T : Object
    {
        if (p.propertyType == SerializedPropertyType.ObjectReference)
        {
            var obj = EditorGUI.ObjectField(r, l, value, typeof(T), true);

            p.objectReferenceValue = value = obj as T; ;
        }
        else EditorGUI.PropertyField(r, p, l);
    }

    /// <summary>名前からアセットを探す</summary>
    public static bool TryFindAsset<T>(string name, out T value) where T : Object => value = Find<T>(name);

    /// <summary>名前からアセットのパスを取得</summary>
    public static bool FindPath(string name, out string path)
    {
        var assets = AssetDatabase.FindAssets(name);

        foreach (var asset in assets)
        {
            path = AssetDatabase.GUIDToAssetPath(asset);

            return true;
        }

        path = null;

        return false;
    }

    /// <summary>名前からアセットを探す</summary>
    public static T Find<T>(string name) where T : Object
    {
        var assets = AssetDatabase.FindAssets(name);

        foreach (var asset in assets)
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);

            return AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
        }

        return null;
    }

    /// <summary>読み取り専用フィールド</summary>
    public static void ReadOnlyField(Rect r, SerializedProperty p, GUIContent l) => ReadOnlyField(r, p, l, true);

    /// <summary>読み取り専用フィールド</summary>
    public static void ReadOnlyField(Rect r, SerializedProperty p, GUIContent l, bool isReadOnly)
    {
        EditorGUI.BeginDisabledGroup(isReadOnly);
        EditorGUI.PropertyField(r, p, l);
        EditorGUI.EndDisabledGroup();
    }

    /// <summary>Defaultの値か？</summary>
    public static bool IsDefault(SerializedProperty p) => p.propertyType switch
    {
        SerializedPropertyType.String => string.IsNullOrEmpty(p.stringValue),
        SerializedPropertyType.ObjectReference => p.objectReferenceValue == null,
        SerializedPropertyType.Integer => p.intValue == default,
        SerializedPropertyType.Float => p.floatValue == default,
        SerializedPropertyType.Color => p.colorValue == default,
        SerializedPropertyType.Vector2 => p.vector2Value == default,
        SerializedPropertyType.Vector3 => p.vector3Value == default,
        SerializedPropertyType.Vector4 => p.vector4Value == default,
        SerializedPropertyType.Vector2Int => p.vector2IntValue == default,
        SerializedPropertyType.Vector3Int => p.vector3IntValue == default,
        SerializedPropertyType.Quaternion => p.quaternionValue == default,
        SerializedPropertyType.Rect => p.rectValue == default,
        SerializedPropertyType.AnimationCurve => p.animationCurveValue == default,
        _ => default,
    };
}
#endif