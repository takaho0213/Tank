[System.Serializable]
public class Reference<T>
{
    public T x;

    public Reference() { }

    public Reference(T x) => this.x = x;
}

[System.Serializable]
public class Reference2<T> : Reference<T>
{
    public T y;

    public Reference2() { }

    public Reference2(T x, T y) : base(x) => this.y = y;
}

[System.Serializable]
public class Reference3<T> : Reference2<T>
{
    public T z;

    public Reference3() { }

    public Reference3(T x, T y, T z) : base(x, y) => this.z = z;
}

[System.Serializable]
public class Reference4<T> : Reference3<T>
{
    public T w;

    public Reference4() { }

    public Reference4(T x, T y, T z, T w) : base(x, y, z) => this.w = w;
}
