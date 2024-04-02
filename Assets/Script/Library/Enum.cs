using System.Linq;

/// <summary>Enum�̏��N���X</summary>
public class Enum<T> where T : System.Enum
{
    /// <summary>�^�C�v</summary>
    public readonly System.Type Type;

    /// <summary>���ڐ�</summary>
    public readonly int Length;

    /// <summary>���ڔz��</summary>
    public readonly T[] Values;

    /// <summary>int�ɕϊ��������ڔz��</summary>
    public readonly int[] IntValues;

    /// <summary>string�ɕϊ��������ڔz��</summary>
    public readonly string[] StringValues;

    /// <summary>�����_���ɍ��ڂ�Ԃ�</summary>
    public T Random => RandomEx.Element(Values);

    /// <summary>�L���X�g�ł��邩�H</summary>
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
