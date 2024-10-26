using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GraphicFaderScript : FaderScript
{
    /// <summary>フェードさせるGraphic</summary>
    [SerializeField] protected Graphic graphic;

    public override float FadeValue
    {
        get => graphic.color.a;
        set => graphic.color = graphic.color.SetAlpha(value);
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GraphicFaderScript), true)]
    public class GraphicFaderEditor : FaderEditor
    {
        protected GraphicFaderScript graphicScript;

        protected override void OnEnable()
        {
            base.OnEnable();

            graphicScript = (GraphicFaderScript)target;
        }

        protected override void Field()
        {
            graphicScript.graphic = GUIL.Field(nameof(Graphic), graphicScript.graphic);

            if (!graphicScript.graphic && GUIL.ButtonSpace($"Add {nameof(Image)}"))
            {
                graphicScript.graphic ??= graphicScript.TryGetComponent<Image>(out var image) ? image : graphicScript.gameObject.AddComponent<Image>();

                var graphic = graphicScript.graphic;

                graphic.color = Color.black;

                graphic.raycastTarget = false;

                var trafo = graphic.rectTransform;

                trafo.anchoredPosition = default;
                trafo.rotation = default;
                trafo.localScale = Vector3.one;

                trafo.sizeDelta = VectorEx.FHD;
            }

            base.Field();
        }
    }
#endif
}
