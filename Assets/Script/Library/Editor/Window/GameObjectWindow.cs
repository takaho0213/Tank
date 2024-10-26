#if UNITY_EDITOR

using UnityEngine;

public class GameObjectWindow : BaseObjectsWindow<GameObject>
{
    private class Search<T>
    {
        public T Value;
        public bool IsOn;

        public string Label;

        public System.Func<GameObject, T, bool> Selector;

        public System.Func<T, T> ValueField;

        public void Field()
        {
            GUIL.Space();

            using (GUIL.Horizontal)
            {
                GUIL.Label(Label);

                Value = ValueField(Value);

                IsOn = GUIL.ToggleLeft(IsOn);
            }
        }

        public bool IsSearch(GameObject obj) => IsOn && Selector(obj, Value);
    }

    private Search<string> Name;
    private Search<string> Tag;
    private Search<LayerMask> Layer;
    private Search<bool> IsActive;
    private Search<bool> IsStatic;
    private Search<Component> ComponentType;
    private Search<string> ComponentString;

    private bool IsAll;

    protected override void OnEnable()
    {
        Name = new()
        {
            Label = nameof(Name),

            ValueField = GUIL.Field,

            Selector = (obj, v) => obj.name.Contains(v),

            Value = string.Empty
        };

        Tag = new()
        {
            Label = nameof(Tag),

            ValueField = GUIL.TagField,

            Selector = (obj, v) => !string.IsNullOrEmpty(v) && obj.CompareTag(v),

            Value = string.Empty
        };

        Layer = new()
        {
            Label = nameof(Layer),

            ValueField = GUIL.Field,

            Selector = (obj, v) => obj.layer == v
        };

        IsActive = new()
        {
            Label = nameof(IsActive),

            ValueField = GUIL.Field,

            Selector = (obj, v) => obj.activeInHierarchy == v
        };

        IsStatic = new()
        {
            Label = nameof(IsStatic),

            ValueField = GUIL.Field,

            Selector = (obj, v) => obj.isStatic == v
        };

        ComponentType = new()
        {
            Label = nameof(ComponentType),

            ValueField = GUIL.Field,

            Selector = (obj, v) => v && obj.GetComponent(v.GetType())
        };

        ComponentString = new()
        {
            Label = nameof(ComponentString),

            ValueField = GUIL.Field,

            Selector = (obj, v) => obj.GetComponent(v)
        };

        base.OnEnable();
    }

    protected override void Selector(GameObject obj)
    {
        bool isName = Name.IsSearch(obj);
        bool isTag = Tag.IsSearch(obj);
        bool isLayer = Layer.IsSearch(obj);
        bool isActive = IsActive.IsSearch(obj);
        bool isStatic = IsStatic.IsSearch(obj);
        bool componentT = ComponentType.IsSearch(obj);
        bool componentS = ComponentString.IsSearch(obj);

        bool isAll = isName && isTag && isLayer && isActive && isStatic && componentT && componentS;
        bool isAny = isName || isTag || isLayer || isActive || isStatic || componentT || componentS;

        if (IsAll ? isAll : isAny)
        {
            GUIL.Field(string.Empty, obj);
        }
    }

    protected override int Sort(GameObject a, GameObject b) => a.name.CompareTo(b.name);

    protected override void CreateWindow()
    {
        GUIL.Space();

        if (GUIL.Button(IsAll ? "All" : "Any")) IsAll = !IsAll;

        Name.Field();
        Tag.Field();
        Layer.Field();
        IsActive.Field();
        IsStatic.Field();
        ComponentType.Field();
        ComponentString.Field();

        GUIL.Space();

        base.CreateWindow();
    }
}
#endif