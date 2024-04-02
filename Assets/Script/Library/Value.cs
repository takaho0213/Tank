/// <summary>�l��1���N���X</summary>
[System.Serializable]
public struct Value<T>
{
    public T x;

    public Value(T x)
    {
        this.x = x;
    }

    public static implicit operator Value<T>(Value2<T> v) => new Value<T>(v.x);
    public static implicit operator Value<T>(Value3<T> v) => new Value<T>(v.x);
    public static implicit operator Value<T>(Value4<T> v) => new Value<T>(v.x);

}

/// <summary>�l��2���N���X</summary>
[System.Serializable]
public struct Value2<T>
{
    public T x;
    public T y;

    public Value2(T x, T y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Value2<T>(Value<T> v) => new Value2<T>(v.x ,default);
    public static implicit operator Value2<T>(Value3<T> v) => new Value2<T>(v.x, v.y);
    public static implicit operator Value2<T>(Value4<T> v) => new Value2<T>(v.x, v.y);
}

/// <summary>�l��3���N���X</summary>
[System.Serializable]
public struct Value3<T>
{
    public T x;
    public T y;
    public T z;

    public Value3(T x, T y, T z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static implicit operator Value3<T>(Value<T> v) => new Value3<T>(v.x, default, default);
    public static implicit operator Value3<T>(Value2<T> v) => new Value3<T>(v.x, v.y, default);
    public static implicit operator Value3<T>(Value4<T> v) => new Value3<T>(v.x, v.y, v.z);
}

/// <summary>�l��4���N���X</summary>
[System.Serializable]
public struct Value4<T>
{
    public T x;
    public T y;
    public T z;
    public T w;

    public Value4(T x, T y, T z, T w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public static implicit operator Value4<T>(Value<T> v) => new Value4<T>(v.x, default, default, default);
    public static implicit operator Value4<T>(Value2<T> v) => new Value4<T>(v.x, v.y, default, default);
    public static implicit operator Value4<T>(Value3<T> v) => new Value4<T>(v.x, v.y, v.z, default);
}

