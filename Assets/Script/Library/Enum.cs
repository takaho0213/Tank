using System.Linq;

/// <summary>Enumの情報クラス</summary>
public class Enum<T> where T : System.Enum
{
    /// <summary>タイプ</summary>
    public readonly System.Type Type;

    /// <summary>項目数</summary>
    public readonly int Length;

    /// <summary>項目配列</summary>
    public readonly T[] Values;

    /// <summary>intに変換した項目配列</summary>
    public readonly int[] IntValues;

    /// <summary>stringに変換した項目配列</summary>
    public readonly string[] StringValues;

    /// <summary>ランダムに項目を返す</summary>
    public T Random => RandomEx.Element(Values);

    /// <summary>キャストできるか？</summary>
    public bool IsCast(object num) => System.Enum.IsDefined(Type, num);

    public Enum()
    {
        Type = typeof(T);
        Values = (T[])System.Enum.GetValues(Type);
        StringValues = System.Enum.GetNames(Type);
        IntValues = Values.Select(value => System.Convert.ToInt32(value)).ToArray();
        Length = Values.Length;
    }
}
