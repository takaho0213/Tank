#if UNITY_EDITOR

using TMPro;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class Window : BaseEditorWindow
{
    [MenuItem(nameof(Window) + "/^p^")]
    public static void ShowWindow() => GetWindow<Window>(nameof(Window)).Show();

    public void WindowButton<T>() where T : BaseEditorWindow
    {
        GUIL.Space();

        var text = typeof(T).ToString();

        if (GUIL.Button(text))
        {
            GetWindow<T>(text).Show();
        }
    }

    protected override void CreateWindow()
    {
        using (new EditorGUI.IndentLevelScope())
        {
            GUIL.Space();

            GUIL.FlexibleLabelSpace("Editor起動時間 : " + EditorApplication.timeSinceStartup.ToTime());

            WindowButton<FontWindow>();
            WindowButton<SpriteRendererLayerWindow>();
            WindowButton<LayerWindow>();
            WindowButton<CameraDepthWindow>();
            WindowButton<CanvasLayerWindow>();
            WindowButton<StaticWindow>();
            WindowButton<TagWindow>();
            WindowButton<GameObjectWindow>();
            WindowButton<TransformWindow>();
            WindowButton<GraphicColorWindow>();
            WindowButton<RaycastTargetWindow>();

            GUIL.Space();

            Button(VectorEx.FHD);

            GUIL.FlexibleLabel(Screen.currentResolution.ToString());
        }
    }

    private void Button(Vector2 vector)
    {
        Vector2Int v = vector.RoundToInt();

        if (GUIL.Button(v.ToString()))
        {
            Screen.SetResolution(v.x, v.y, true);
        }
    }
}

public abstract class BaseEditorWindow : EditorWindow
{
    protected Vector2 ScrollPos;

    protected abstract void CreateWindow();

    public virtual void OnGUI()
    {
        using (new EditorGUI.IndentLevelScope())
        {
            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos);

            CreateWindow();

            EditorGUILayout.EndScrollView();
        }
    }
}

public abstract class BaseIteratorWindow<T> : BaseEditorWindow
{
    protected T[] objects;

    protected bool IsOpen;

    protected abstract T[] Find { get; }

    protected abstract int Sort(T a, T b);

    protected abstract void Selector(T obj);

    protected abstract Object Titlebar { get; }

    protected virtual void OnEnable()
    {
        IsOpen = true;

        objects = Find;

        System.Array.Sort(objects, Sort);
    }

    protected override void CreateWindow()
    {
        if (objects.Length == default)
        {
            GUIL.FlexibleLabel($"{typeof(T)}がヒエラルキー上に存在しません");

            return;
        }

        if (IsOpen = EditorGUILayout.InspectorTitlebar(IsOpen, Titlebar))
        {
            foreach (var obj in objects)
            {
                Selector(obj);
            }
        }
    }
}

public abstract class BaseObjectsWindow<T> : BaseIteratorWindow<T> where T : UnityEngine.Object
{
    protected override T[] Find => Resources.FindObjectsOfTypeAll<T>();

    protected override Object Titlebar => objects.FirstOrDefault();
}

public class FontWindow : BaseIteratorWindow<Group<TMP_FontAsset, TextMeshProUGUI>>
{
    private TMP_FontAsset FontAsset;

    private TextMeshProUGUI[] TMPs => Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();

    protected override Group<TMP_FontAsset, TextMeshProUGUI>[] Find
    {
        get => TMPs.GroupByToGroup((v) => v.font);
    }

    protected override Object Titlebar => objects.First().First();

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var group in objects)
        {
            group.Array.Sort((v) => v.name);
        }
    }

    protected override void CreateWindow()
    {
        GUIL.Space();

        FontAsset = GUIL.Field("一括で置き換えるFont", FontAsset);

        GUIL.Space();

        if (FontAsset && GUIL.Button($"全ての{nameof(TextMeshProUGUI)}のFontに \"{FontAsset.name}\" をセット"))
        {
            foreach (var tmp in TMPs)
            {
                tmp.font = FontAsset;
            }

            OnEnable();
        }

        base.CreateWindow();
    }

    protected override void Selector(Group<TMP_FontAsset, TextMeshProUGUI> objs)
    {
        GUIL.Space();
        GUIL.Space();

        var font = objs.Key;

        GUIL.FlexibleLabel($"-------------------------------------------------- 【{font.name}】 --------------------------------------------------");

        GUIL.Space();
        GUIL.Space();

        foreach (var obj in objs.Array)
        {
            using (GUIL.Horizontal)
            {
                GUIL.Field(obj);

                obj.font = GUIL.Field(font);

                if (font != obj.font) OnEnable();
            }
        }
    }

    protected int Sort(TextMeshProUGUI a, TextMeshProUGUI b) => a.name.CompareTo(b.name);

    protected override int Sort(Group<TMP_FontAsset, TextMeshProUGUI> a, Group<TMP_FontAsset, TextMeshProUGUI> b)
    {
        return a.Key.name.CompareTo(b.Key.name);
    }
}

public class TagWindow : BaseObjectsWindow<GameObject>
{
    private string TargetTag;

    protected override void CreateWindow()
    {
        GUIL.Space();

        TargetTag = GUIL.TagField("表示するタグ", TargetTag);

        GUIL.Space();

        if (string.IsNullOrEmpty(TargetTag)) return;

        base.CreateWindow();
    }

    protected override void Selector(GameObject obj)
    {
        if (obj.CompareTag(TargetTag))
        {
            obj.tag = GUIL.TagField(obj.name, obj.tag);
        }
    }

    protected override int Sort(GameObject a, GameObject b) => a.name.CompareTo(b.name);
}

public class LayerWindow : BaseObjectsWindow<GameObject>
{
    private int Target;

    protected override void CreateWindow()
    {
        GUIL.Space();

        Target = EditorGUILayout.LayerField("探すレイヤー", Target);

        GUIL.Space();

        base.CreateWindow();
    }

    protected override void Selector(GameObject obj)
    {
        LayerMask layer = obj.layer;

        if (layer == Target)
        {
            using (GUIL.Horizontal)
            {
                GUIL.Field(string.Empty, obj);

                obj.layer = GUIL.Field(string.Empty, layer);
            }
        }
    }

    protected override int Sort(GameObject a, GameObject b) => a.name.CompareTo(b.name);
}

public class StaticWindow : BaseObjectsWindow<GameObject>
{
    private bool isStatic;

    protected override void CreateWindow()
    {
        GUIL.Space();

        isStatic = GUIL.Field("IsStatic", isStatic);

        GUIL.Space();

        base.CreateWindow();
    }

    protected override void Selector(GameObject obj)
    {
        if (isStatic != obj.isStatic) return;

        using (GUIL.Horizontal)
        {
            GUIL.Field(string.Empty, obj);

            obj.isStatic = GUIL.Field(nameof(obj.isStatic), obj.isStatic);
        }
    }

    protected override int Sort(GameObject a, GameObject b) => a.name.CompareTo(b.name);
}

public class SpriteRendererLayerWindow : BaseObjectsWindow<SpriteRenderer>
{
    protected override void Selector(SpriteRenderer obj)
    {
        obj.sortingOrder = EditorGUILayout.DelayedIntField(obj.name, obj.sortingOrder);
    }

    protected override int Sort(SpriteRenderer a, SpriteRenderer b) => a.sortingOrder.CompareTo(b.sortingOrder);
}

public class CameraDepthWindow : BaseObjectsWindow<Camera>
{
    protected override void Selector(Camera obj)
    {
        using (GUIL.Horizontal)
        {
            GUIL.Field(string.Empty, obj);

            obj.depth = GUIL.DelayedField(obj.depth);

            GUIL.FlexibleSpace();
        }
    }

    protected override int Sort(Camera a, Camera b) => a.depth.CompareTo(b.depth);
}

public class CanvasLayerWindow : BaseObjectsWindow<Canvas>
{
    protected override void Selector(Canvas obj)
    {
        using (GUIL.Horizontal)
        {
            GUIL.Field(string.Empty, obj);

            obj.sortingOrder = GUIL.Field(nameof(obj.sortingOrder), obj.sortingOrder);
        }
    }

    protected override int Sort(Canvas a, Canvas b) => a.sortingOrder.CompareTo(b.sortingOrder);
}
#endif