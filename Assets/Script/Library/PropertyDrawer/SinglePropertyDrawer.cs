#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public abstract class SinglePropertyDrawer : PropertyDrawer
{
    protected abstract string PropertyName { get; }

    protected SerializedProperty Property;

    protected bool TryProperty(SerializedProperty p, out SerializedProperty property)
    {
        Property ??= p.FindPropertyRelative(PropertyName);

        return (property = Property) != null;
    }

    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        EditorGUI.PropertyField(r, TryProperty(p, out var value) ? value : p, l);
    }

    public override float GetPropertyHeight(SerializedProperty p, GUIContent l)
    {
        return TryProperty(p, out var value) ? EditorGUI.GetPropertyHeight(value, l) : base.GetPropertyHeight(p, l);
    }
}

[CustomPropertyDrawer(typeof(SerializeQueue<>), true)]
[CustomPropertyDrawer(typeof(SerializeStack<>), true)]
[CustomPropertyDrawer(typeof(SerializeHashSet<>), true)]
public class SerializeCollectionDrawer : SinglePropertyDrawer
{
    protected override string PropertyName => "Array";
}

[CustomPropertyDrawer(typeof(SerializeDictionary<,>), true)]
public class SerializeDictionaryDrawer : SinglePropertyDrawer
{
    protected override string PropertyName => "Pairs";
}

[CustomPropertyDrawer(typeof(SerializeList<>), true)]
public class SerializeListDrawer : SinglePropertyDrawer
{
    protected override string PropertyName => "List";
}

[CustomPropertyDrawer(typeof(PoolList<>))]
public class PoolListDrawer : SinglePropertyDrawer
{
    protected override string PropertyName => "original";
}

[CustomPropertyDrawer(typeof(BaseCsvImporter), true)]
public class CsvImporterDrawer : SinglePropertyDrawer
{
    protected override string PropertyName => "CsvAsset";
}

[CustomPropertyDrawer(typeof(CsvAssetReader), true)]
public class TextAssetReaderDrawer : SinglePropertyDrawer
{
    protected override string PropertyName => "file";
}
#endif
