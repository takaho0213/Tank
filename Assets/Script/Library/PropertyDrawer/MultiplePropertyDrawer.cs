#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public abstract class MultiplePropertyDrawer : PropertyDrawer
{
    protected class PropertyInfo
    {
        public SerializedProperty Property;

        public float Height;
    }

    protected bool isInit;

    protected PropertyInfo[] infos;

    protected abstract string[] PropertyNames { get; }

    protected virtual void Initialize(SerializedProperty p)
    {
        string[] names = PropertyNames;

        int length = names.Length;

        infos = new PropertyInfo[length];

        for (int i = default; i < length; i++)
        {
            infos[i] = new()
            {
                Property = p.FindPropertyRelative(names[i]),
            };
        }
    }

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        r.height = Drawer.Height;

        EditorGUI.LabelField(r, l);

        for (int i = default; i < infos.Length; i++)
        {
            var info = infos[i];

            var property = info.Property;

            r.height = info.Height;

            r.y += r.height + Drawer.Gap;

            l.text = $"    {property.name}";

            EditorGUI.PropertyField(r, property, l);
        }
    }

    public override float GetPropertyHeight(SerializedProperty p, GUIContent l)
    {
        if (!isInit)
        {
            Initialize(p);

            isInit = true;
        }

        return infos.Total((v) => v.Height = EditorGUI.GetPropertyHeight(v.Property)) + Drawer.HeightAndGap + Drawer.Gap;
    }
}
#endif