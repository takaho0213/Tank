using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public static class GUIL
{
    public static T GenericField<T>(string text, T value)
    {
        switch (value)
        {
            case int v:

                return EditorGUILayout.IntField(text, v) is T vi ? vi : default;

            case float v:

                return EditorGUILayout.FloatField(text, v) is T vf ? vf : default;

            case double v:

                return EditorGUILayout.DoubleField(text, v) is T vd ? vd : default;

            case bool v:

                return EditorGUILayout.Toggle(text, v) is T vb ? vb : default;

            case string v:

                return EditorGUILayout.TextField(text, v) is T vs ? vs : default;

            case Color v:

                return EditorGUILayout.ColorField(text, v) is T vc ? vc : default;

            case Vector2 v:

                return EditorGUILayout.Vector2Field(text, v) is T v2 ? v2 : default;

            case Vector2Int v:

                return EditorGUILayout.Vector2IntField(text, v) is T v3 ? v3 : default;

            case Vector3 v:

                return EditorGUILayout.Vector3Field(text, v) is T v4 ? v4 : default;

            case Vector3Int v:

                return EditorGUILayout.Vector3IntField(text, v) is T v5 ? v5 : default;

            case Vector4 v:

                return EditorGUILayout.Vector4Field(text, v) is T v6 ? v6 : default;

            case LayerMask v:

                return EditorGUILayout.LayerField(text, v) is T vl ? vl : default;

            case Object v:

                return EditorGUILayout.ObjectField(text, v, typeof(T), true) is T vo ? vo : default;

            case System.Enum v:
                
                return EditorGUILayout.EnumPopup(text, v) is T ve ? ve : default;

            default:
                Label($"{text} : {value}");

                return value;
        }
    }

    public static T TryParseField<T>(string text, T value, TryParse<T> tryParse)
    {
        string oldText = value.ToString();

        string newText = EditorGUILayout.DelayedTextField(text, oldText);

        return oldText != newText && tryParse(newText, out var newValue) ? newValue : value;
    }

    public static T TryParseField<T>(T value, TryParse<T> tryParse) => TryParseField(string.Empty, value, tryParse);

    public static int Field(string text, int value) => EditorGUILayout.IntField(text, value);
    public static float Field(string text, float value) => EditorGUILayout.FloatField(text, value);
    public static double Field(string text, double value) => EditorGUILayout.DoubleField(text, value);
    public static bool Field(string text, bool value) => EditorGUILayout.Toggle(text, value);
    public static string Field(string text, string value) => EditorGUILayout.TextField(text, value);
    public static Color Field(string text, Color value) => EditorGUILayout.ColorField(text, value);
    public static Vector2 Field(string text, Vector2 value) => EditorGUILayout.Vector2Field(text, value);
    public static Vector2Int Field(string text, Vector2Int value) => EditorGUILayout.Vector2IntField(text, value);
    public static Vector3 Field(string text, Vector3 value) => EditorGUILayout.Vector3Field(text, value);
    public static Vector3Int Field(string text, Vector3Int value) => EditorGUILayout.Vector3IntField(text, value);
    public static Vector4 Field(string text, Vector4 value) => EditorGUILayout.Vector4Field(text, value);
    public static LayerMask Field(string text, LayerMask value) => EditorGUILayout.LayerField(text, value);
    public static T Field<T>(string text, T value) where T : Object => (T)EditorGUILayout.ObjectField(text, value, typeof(T), true);
    public static T EnumField<T>(string text, T value) where T : System.Enum => (T)EditorGUILayout.EnumPopup(text, value);
    public static T EnumFlagField<T>(string text, T value) where T : System.Enum => (T)EditorGUILayout.EnumFlagsField(text, value);
    public static void Field(SerializedProperty p) => EditorGUILayout.PropertyField(p, true);

    public static int DelayedField(string text, int value) => EditorGUILayout.DelayedIntField(text, value);
    public static float DelayedField(string text, float value) => EditorGUILayout.DelayedFloatField(text, value);
    public static double DelayedField(string text, double value) => EditorGUILayout.DelayedDoubleField(text, value);
    public static string DelayedField(string text, string value) => EditorGUILayout.DelayedTextField(text, value);

    public static float Field(string text, float num, float min, float max) => EditorGUILayout.Slider(text, num, min, max);
    public static float Field(string text, int num, int min, int max) => EditorGUILayout.IntSlider(text, num, min, max);
    public static string TagField(string text, string value) => EditorGUILayout.TagField(text, value);
    public static bool ToggleLeft(string text, bool value) => EditorGUILayout.ToggleLeft(text, value);
    public static bool Foldout(string text, bool foldout) => EditorGUILayout.Foldout(foldout, text);

    public static int Field(int value) => EditorGUILayout.IntField(string.Empty, value);
    public static float Field(float value) => EditorGUILayout.FloatField(string.Empty, value);
    public static double Field(double value) => EditorGUILayout.DoubleField(string.Empty, value);
    public static bool Field(bool value) => EditorGUILayout.Toggle(string.Empty, value);
    public static string Field(string value) => EditorGUILayout.TextField(string.Empty, value);
    public static Color Field(Color value) => EditorGUILayout.ColorField(string.Empty, value);
    public static Vector2 Field(Vector2 value) => EditorGUILayout.Vector2Field(string.Empty, value);
    public static Vector3 Field(Vector3 value) => EditorGUILayout.Vector3Field(string.Empty, value);
    public static Vector4 Field(Vector4 value) => EditorGUILayout.Vector4Field(string.Empty, value);
    public static LayerMask Field(LayerMask value) => EditorGUILayout.LayerField(string.Empty, value);
    public static T Field<T>(T value) where T : Object => (T)EditorGUILayout.ObjectField(string.Empty, value, typeof(T), true);
    public static T EnumField<T>(T value) where T : System.Enum => (T)EditorGUILayout.EnumPopup(string.Empty, value);

    public static int DelayedField(int value) => EditorGUILayout.DelayedIntField(string.Empty, value);
    public static float DelayedField(float value) => EditorGUILayout.DelayedFloatField(string.Empty, value);
    public static double DelayedField(double value) => EditorGUILayout.DelayedDoubleField(string.Empty, value);
    public static string DelayedField(string value) => EditorGUILayout.DelayedTextField(string.Empty, value);

    public static float Field(float num, float min, float max) => EditorGUILayout.Slider(string.Empty, num, min, max);
    public static float Field(int num, int min, int max) => EditorGUILayout.IntSlider(string.Empty, num, min, max);
    public static string TagField(string value) => EditorGUILayout.TagField(string.Empty, value);
    public static bool ToggleLeft(bool value) => EditorGUILayout.ToggleLeft(string.Empty, value);

    public static bool Foldout(bool foldout) => EditorGUILayout.Foldout(foldout, string.Empty);

    public static void SetColorWhite() => GUI.color = Color.white;

    public static void ColorChange(bool iswhite) => ColorChange(iswhite, Color.gray);

    public static void ColorChange(bool iswhite, Color color) => GUI.color = iswhite ? Color.white : color;

    public static bool Button(string text) => GUILayout.Button(text);

    public static bool ButtonSpace(string text)
    {
        EditorGUILayout.Space();

        bool isOn = GUILayout.Button(text);

        EditorGUILayout.Space();

        return isOn;
    }

    public static void Button(string text, UnityAction action)
    {
        EditorGUILayout.Space();

        if (UnityEngine.GUILayout.Button(text))
        {
            action?.Invoke();
        }

        EditorGUILayout.Space();
    }

    /// <summary>中央にテキストを表示する関数</summary>
    /// <param name="text">表示するテキスト</param>
    public static void FlexibleLabel(string text)
    {
        EditorGUILayout.BeginHorizontal();//開始

        UnityEngine.GUILayout.FlexibleSpace();

        UnityEngine.GUILayout.Label(text);

        UnityEngine.GUILayout.FlexibleSpace();

        EditorGUILayout.EndHorizontal();//終了
    }

    public static void FlexibleLabel(string[] texts)
    {
        EditorGUILayout.BeginHorizontal();//開始

        UnityEngine.GUILayout.FlexibleSpace();

        foreach (var text in texts)
        {
            UnityEngine.GUILayout.Label(text);

            UnityEngine.GUILayout.FlexibleSpace();
        }

        EditorGUILayout.EndHorizontal();//終了
    }

    public static void FlexibleLabelSpace(string text)
    {
        EditorGUILayout.Space();

        FlexibleLabel(text);

        EditorGUILayout.Space();
    }

    public static void FlexibleLabelSpace(string[] texts)
    {
        EditorGUILayout.BeginHorizontal();//開始

        GUILayout.FlexibleSpace();

        foreach(var text in texts)
        {
            GUILayout.Label(text);

            GUILayout.FlexibleSpace();
        }

        EditorGUILayout.EndHorizontal();//終了
    }

    /// <summary>色付きテキストを表示する関数</summary>
    public static void ColorLabel(string text, Color color)
    {
        var Style = new GUIStyle(EditorStyles.label)
        {
            richText = true
        };

        EditorGUILayout.LabelField(text.ToRichColorText(color), Style, UnityEngine.GUILayout.ExpandHeight(true));
    }

    public static bool Titlebar<T>(bool isOpen ,T num) where T : Object => EditorGUILayout.InspectorTitlebar(isOpen, num);

    public static void Label(string text) => EditorGUILayout.LabelField(text);

    public static void Space() => EditorGUILayout.Space();
    public static void Space(int count) => EditorGUILayout.Space(count);
    public static void FlexibleSpace() => UnityEngine.GUILayout.FlexibleSpace();

    /// <summary>例)using (VerticalText) { 縦に列挙したい項目名 } </summary>
    public static EditorGUILayout.VerticalScope Vertical => new();

    /// <summary>例)using (VerticalText) { 縦に列挙したい項目名 } </summary>
    public static EditorGUILayout.VerticalScope VerticalText(string text) => new(text);

    /// <summary>例)using (Horizontal) { 横に列挙したい項目名 } </summary>
    public static EditorGUILayout.HorizontalScope Horizontal => new();

    /// <summary>例)using (Horizontal) { 横に列挙したい項目名 } </summary>
    public static EditorGUILayout.HorizontalScope HorizontalText(string text) => new(text);

    /// <summary>縦列挙</summary>
    public static void VerticalScope(UnityAction action)
    {
        EditorGUILayout.BeginVertical();

        action?.Invoke();

        EditorGUILayout.EndVertical();
    }

    /// <summary>横列挙</summary>
    public static void HorizontalScope(UnityAction action)
    {
        EditorGUILayout.BeginHorizontal();

        action?.Invoke();

        EditorGUILayout.EndHorizontal();
    }
}
