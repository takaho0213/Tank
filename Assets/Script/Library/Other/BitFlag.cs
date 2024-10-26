public static class BitFlag
{
    public const int Flag0 = 1 << 0;
    public const int Flag1 = 1 << 1;
    public const int Flag2 = 1 << 2;
    public const int Flag3 = 1 << 3;
    public const int Flag4 = 1 << 4;
    public const int Flag5 = 1 << 5;
    public const int Flag6 = 1 << 6;
    public const int Flag7 = 1 << 7;
    public const int Flag8 = 1 << 8;
    public const int Flag9 = 1 << 9;
    public const int Flag10 = 1 << 10;
    public const int Flag11 = 1 << 11;
    public const int Flag12 = 1 << 12;
    public const int Flag13 = 1 << 13;
    public const int Flag14 = 1 << 14;
    public const int Flag15 = 1 << 15;
    public const int Flag16 = 1 << 16;
    public const int Flag17 = 1 << 17;
    public const int Flag18 = 1 << 18;
    public const int Flag19 = 1 << 19;
    public const int Flag20 = 1 << 20;
    public const int Flag21 = 1 << 21;
    public const int Flag22 = 1 << 22;
    public const int Flag23 = 1 << 23;
    public const int Flag24 = 1 << 24;
    public const int Flag25 = 1 << 25;
    public const int Flag26 = 1 << 26;
    public const int Flag27 = 1 << 27;
    public const int Flag28 = 1 << 28;
    public const int Flag29 = 1 << 29;
    public const int Flag30 = 1 << 30;

    /// <summary>�������̍��ڂ�1��</summary>
    /// <param name="flags">���ׂ����t���O</param>
    /// <param name="target">���ׂ�������</param>
    public static bool IsFlag(this int flags, int target) => (flags & target) == target;

    /// <summary>�������̍��ڂ�1�ɂ���</summary>
    /// <param name="flags">1�ɂ���t���O</param>
    /// <param name="target">1�ɂ���������</param>
    public static int True(this int flags, int target) => flags | target;

    /// <summary>�����̍��ڂ�0�ɂ���</summary>
    /// <param name="flags">0�ɂ���t���O</param>
    /// <param name="num">0�ɂ���������</param>
    public static int False(this int flags, int num) => flags & ~num;

    /// <summary>���ׂ�0</summary>
    public static int None(this int flags) => flags ^ flags;

    /// <summary>���ׂ�1</summary>
    public static int Everything(this int flags) => flags | ~flags;

    /// <summary>���ׂĔ��]</summary>
    public static int Reverse(this int flags) => ~flags;
}
//�y& : AND���Z�q�z�ǂ����1�Ȃ�1
//�y| : OR���Z�q �z�ǂ��炩��1�Ȃ�1
//�y^ : XOR���Z�q�z�Е�����1�Ȃ�1
//�y~ : NOT���Z�q�z1��0, 0����  1���]
