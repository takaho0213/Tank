/// <summary>値を1つ持つクラス</summary>
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

/// <summary>値を2つ持つクラス</summary>
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

/// <summary>値を3つ持つクラス</summary>
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

/// <summary>値を4つ持つクラス</summary>
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

