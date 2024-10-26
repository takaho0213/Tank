using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteFaderScript : FaderScript
{
    /// <summary>フェードさせるSpriteRenderer</summary>
    [SerializeField] protected SpriteRenderer spriteRenderer;

    public override float FadeValue
    {
        get => spriteRenderer.color.a;
        set => spriteRenderer.color = spriteRenderer.color.SetAlpha(value);
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SpriteFaderScript), true)]
    public class SpriteRendererEditor : FaderEditor
    {
        protected SpriteFaderScript spriteScript;

        protected override void OnEnable()
        {
            base.OnEnable();

            spriteScript = (SpriteFaderScript)target;
        }

        protected override void Field()
        {
            spriteScript.spriteRenderer = GUIL.Field(nameof(SpriteRenderer), spriteScript.spriteRenderer);

            base.Field();

            if (GUIL.ButtonSpace($"{nameof(VectorEx.FHD)} {nameof(SpriteRenderer)}"))
            {
                spriteScript.spriteRenderer ??= spriteScript.TryGetComponent<SpriteRenderer>(out var renderer) ? renderer : spriteScript.gameObject.AddComponent<SpriteRenderer>();

                renderer = spriteScript.spriteRenderer;

                renderer.color = Color.black;

                var trafo = renderer.transform;

                trafo.position = default;

                trafo.rotation = default;

                trafo.localScale = (Vector2)MathEx.VectorRatio(VectorEx.FHD.RoundToInt());
            }
        }
    }
#endif
}
