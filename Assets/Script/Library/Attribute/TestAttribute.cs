using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
#endif

public class TestAttribute : PropertyAttribute
{
    public readonly string Name;

    public TestAttribute(string name) => Name = name;
}

# if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TestAttribute))]
public class TestDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        base.OnGUI(r, p, l);
    }
}
# endif