using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>�t�B�[���h���Q�Ƃ��l�𐧌����鑮��(int, float)</summary>
public class ClampFieldReferenceAttribute : PropertyAttribute
{
    /// <summary>Max�l�̕ϐ���</summary>
    public readonly string Max;
    /// <summary>Min�l�̕ϐ���</summary>
    public readonly string Min;

    /// <param name="min">Min�l�̕ϐ���</param>
    /// <param name="max">Max�l�̕ϐ���</param>
    public ClampFieldReferenceAttribute(string min, string max)
    {
        Max = max;
        Min = min;
    }
}

/// <summary>�l��Max�𐧌����鑮��(int, float)</summary>
public class ClampMaxFieldReferenceAttribute : PropertyAttribute
{
    /// <summary>Max�l�̕ϐ���</summary>
    public readonly string Max;

    /// <param name="max">Max�l�̕ϐ���</param>
    public ClampMaxFieldReferenceAttribute(string max) => Max = max;
}

/// <summary>�l��Min�𐧌����鑮��(int, float)</summary>
public class ClampMinFieldReferenceAttribute : PropertyAttribute
{
    /// <summary>Min�l�̕ϐ���</summary>
    public readonly string Min;

    /// <param name="min">Min�l�̕ϐ���</param>
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
            Debug.LogError($"�v���p�e�B��\"{A.Min}\"�͌�����܂���ł���");
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
            Debug.LogError($"�v���p�e�B��\"{A.Max}\"�͌�����܂���ł���");
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
                Debug.LogError($"�v���p�e�B��\"{A.Max}\"�͌�����܂���ł���");

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
                Debug.LogError($"�v���p�e�B��\"{A.Min}\"�͌�����܂���ł���");

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