using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaycastTargetWindow : BaseIteratorWindow<Group<Type, Graphic>>
{
    private Dictionary<Graphic, Button> dictionary;

    protected override Group<Type, Graphic>[] Find
    {
        get
        {
            dictionary = new();

            var groups = Resources.FindObjectsOfTypeAll<Graphic>().GroupByToGroup((v) => v.GetType());

            foreach (var group in groups)
            {
                foreach (var graphic in group)
                {
                    if (graphic.TryGetComponent<Button>(out var button))
                    {
                        dictionary.Add(graphic, button);
                    }
                }
            }

            return groups;
        }
    }


    protected override UnityEngine.Object Titlebar => objects.First().First();

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var group in objects)
        {
            group.Array.Sort((v) => v.name);
        }
    }

    protected override void Selector(Group<Type, Graphic> objs)
    {
        GUIL.Space();
        GUIL.Space();

        var type = objs.Key;

        GUIL.FlexibleLabel($"-------------------------------------------------- Åy{type}Åz --------------------------------------------------");

        GUIL.Space();
        GUIL.Space();

        int length = objs.Array.Length;

        for (int i = default; i < length; i++)
        {
            var obj = objs.Array[i];

            using (GUIL.Horizontal)
            {
                GUIL.Field(obj);

                obj.raycastTarget = GUIL.ToggleLeft(dictionary.ContainsKey(obj) ? nameof(Button) : string.Empty, obj.raycastTarget);
            }
        }
    }

    protected override int Sort(Group<Type, Graphic> a, Group<Type, Graphic> b)
    {
        return a.Key.ToString().CompareTo(b.Key.ToString());
    }
}
