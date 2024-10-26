[System.Serializable]
public class SerializableReference<T>
{
    public T Value;

    public SerializableReference() { }

    public SerializableReference(T v) => Value = v;

    public static SerializableReference<T> Serializable(T value) => value;

    public override string ToString() => Value.ToString();

    public static implicit operator SerializableReference<T>(T value) => new(value);

    public static implicit operator T(SerializableReference<T> s) => s.Value;
}

public static class SerializableEx
{
    public static SerializableReference<T> ToSerialize<T>(this T value) => new(value);
}
