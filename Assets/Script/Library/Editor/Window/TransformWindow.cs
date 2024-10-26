#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class TransformWindow : BaseObjectsWindow<Transform>
{
    private bool IsBranch;

    private void Field(Transform parent, int count)
    {
        count++;

        using (new EditorGUI.IndentLevelScope(count))
        {
            GUIL.Field(parent);

            for (int i = default; i < parent.childCount; i++)
            {
                Field(parent.GetChild(i), count);
            }
        }
    }

    protected override void Selector(Transform obj)
    {
        if (!IsBranch)
        {
            GUIL.Field(obj);
        }
        else if (!obj.parent)
        {
            Field(obj, default);

            GUIL.Space();
            GUIL.Space();
            GUIL.Space();
        }
    }

    protected override int Sort(Transform a, Transform b) => a.name.CompareTo(b.name);

    protected override void CreateWindow()
    {
        IsBranch = GUIL.Field(nameof(IsBranch), IsBranch);

        base.CreateWindow();
    }
}
#endif