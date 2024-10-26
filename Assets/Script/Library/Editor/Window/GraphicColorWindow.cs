using UnityEngine;
using UnityEngine.UI;

public class GraphicColorWindow : BaseObjectsWindow<Graphic>
{
    protected override void Selector(Graphic obj)
    {
        using (GUIL.Horizontal)
        {
            GUIL.Field(string.Empty, obj);

            obj.color = GUIL.Field(obj.color);
        }
    }

    protected override int Sort(Graphic a, Graphic b) => a.name.CompareTo(b.name);
}
