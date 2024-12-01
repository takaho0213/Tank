using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GraphicsFaderScript : FaderScript
{
    /// <summary>フェードさせるGraphic配列</summary>
    [SerializeField] protected Graphic[] graphics;

    public override float FadeValue
    {
        get => graphics[default].color.a;
        set
        {
            foreach (var g in graphics) g.color = g.color.SetAlpha(value);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GraphicsFaderScript), true)]
    public class UIFaderEditor : FaderEditor
    {
        protected SerializedProperty graphicsProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            graphicsProperty = serializedObject.FindProperty(nameof(graphics));
        }

        protected override void Field()
        {
            GUIL.Field(graphicsProperty);

            base.Field();
        }
    }
#endif
}
