/// <summary>�r�b�g�t���O�Ɋւ��鏈�����s���N���X</summary>
public static class BitFlag
{
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
