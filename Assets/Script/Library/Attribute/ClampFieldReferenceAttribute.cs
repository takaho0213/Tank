using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>フィールドを参照し値を制限する属性(int, float)</summary>
public class ClampFieldReferenceAttribute : PropertyAttribute
{
    /// <summary>Max値の変数名</summary>
    public readonly string Max;
    /// <summary>Min値の変数名</summary>
    public readonly string Min;

    /// <param name="min">Min値の変数名</param>
    /// <param name="max">Max値の変数名</param>
    public ClampFieldReferenceAttribute(string min, string max)
    {
        Max = max;
        Min = min;
    }
}

/// <summary>値のMaxを制限する属性(int, float)</summary>
public class ClampMaxFieldReferenceAttribute : PropertyAttribute
{
    /// <summary>Max値の変数名</summary>
    public readonly string Max;

    /// <param name="max">Max値の変数名</param>
    public ClampMaxFieldReferenceAttribute(string max) => Max = max;
}

/// <summary>値のMinを制限する属性(int, float)</summary>
public class ClampMinFieldReferenceAttribute : PropertyAttribute
{
    /// <summary>Min値の変数名</summary>
    public readonly string Min;

    /// <param name="min">Min値の変数名</param>
    public ClampMinFieldReferenceAttribute(string min) => Min = min;
}

# if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ClampFieldReferenceAttribute))]
public class ClampFieldReferenceDrawer : ClampDrawer
{
    private SerializedProperty MinProperty;
    private SerializedProperty MaxProperty;

    private float Min;
    private float Max;

    private ClampFieldReferenceAttribute A => attribute as ClampFieldReferenceAttribute;

    protected override int IntClamp(int value) => Mathf.RoundToInt(MathEx.Clamp(value, Min, Max));

    protected override float FloatClamp(float value) => MathEx.Clamp(value, Min, Max);

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        MinProperty ??= p.serializedObject.FindProperty(A.Min);
        MaxProperty ??= p.serializedObject.FindProperty(A.Max);

        if (MinProperty == null)
        {
            Debug.LogError($"プロパティ名\"{A.Min}\"は見つかりませんでした");
        }
        else
        {
            Min = MinProperty.propertyType switch
            {
                SerializedPropertyType.Integer => MinProperty.intValue,
                SerializedPropertyType.Float => MinProperty.floatValue,
                _ => default
            };
        }

        if (MaxProperty == null)
        {
            Debug.LogError($"プロパティ名\"{A.Max}\"は見つかりませんでした");
        }
        else
        {
            Max = MaxProperty.propertyType switch
            {
                SerializedPropertyType.Integer => MaxProperty.intValue,
                SerializedPropertyType.Float => MaxProperty.floatValue,
                _ => default
            };

        }

        base.OnGUI(r, p, l);
    }
}

[CustomPropertyDrawer(typeof(ClampMaxFieldReferenceAttribute))]
public class ClampMaxFieldReferenceDrawer : ClampMaxDrawer
{
    private ClampMaxFieldReferenceAttribute A => attribute as ClampMaxFieldReferenceAttribute;

    private SerializedProperty MaxProperty;

    protected override float Max
    {
        get
        {
            if (MaxProperty == null)
            {
                Debug.LogError($"プロパティ名\"{A.Max}\"は見つかりませんでした");

                return default;
            }

            return MaxProperty.propertyType switch
            {
                SerializedPropertyType.Integer => MaxProperty.intValue,
                SerializedPropertyType.Float => MaxProperty.floatValue,
                _ => default
            };
        }
    }

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        MaxProperty ??= p.serializedObject.FindProperty(A.Max);

        base.OnGUI(r, p, l);
    }
}

[CustomPropertyDrawer(typeof(ClampMinFieldReferenceAttribute))]
public class ClampMinFieldReferenceDrawer : ClampMinDrawer
{
    private ClampMinFieldReferenceAttribute A => attribute as ClampMinFieldReferenceAttribute;

    private SerializedProperty MinProperty;

    protected override float Min
    {
        get
        {
            if (MinProperty == null)
            {
                Debug.LogError($"プロパティ名\"{A.Min}\"は見つかりませんでした");

                return default;
            }

            return MinProperty.propertyType switch
            {
                SerializedPropertyType.Integer => MinProperty.intValue,
                SerializedPropertyType.Float => MinProperty.floatValue,
                _ => default
            };
        }
    }

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        MinProperty ??= p.serializedObject.FindProperty(A.Min);

        base.OnGUI(r, p, l);
    }
}
#endif