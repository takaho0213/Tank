/// <summary>�Q�Ƃ�1���N���X</summary>
[System.Serializable]
public class Reference<T>
{
    public T x;

    public Reference(T x) => this.x = x;
}

/// <summary>�Q�Ƃ�2���N���X</summary>
[System.Serializable]
public class Reference2<T> : Reference<T>
{
    public T y;

    public Reference2(T x, T y) : base(x) => this.y = y;
}

/// <summary�Q�Ƃ�3���N���X></summary>
[System.Serializable]
public class Reference3<T> : Reference2<T>
{
    public T z;

    public Reference3(T x, T y, T z) : base(x, y) => this.z = z;
}

/// <summary>�Q�Ƃ�4���N���X</summary>
[System.Serializable]
public class Reference4<T> : Reference3<T>
{
    public T w;

    public Reference4(T x, T y, T z, T w) : base(x, y, z) => this.w = w;
}
/// <summary>�Q�Ƃ�2���N���X</summary>
[System.Serializable]
public class Reference2<T, TY> : Reference<T>
{
    public TY y;

    public Reference2(T x, TY y) : base(x) => this.y = y;
}

/// <summary�Q�Ƃ�3���N���X></summary>
[System.Serializable]
public class Reference3<T, TY, TZ> : Reference2<T, TY>
{
    public TZ z;

    public Reference3(T x, TY y, TZ z) : base(x, y) => this.z = z;
}

/// <summary>�Q�Ƃ�4���N���X</summary>
[System.Serializable]
public class Reference4<T, TY, TZ, TW> : Reference3<T, TY, TZ>
{
    public TW w;

    public Reference4(T x, TY y, TZ z, TW w) : base(x, y, z) => this.w = w;
}
