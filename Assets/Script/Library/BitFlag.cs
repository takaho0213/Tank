/// <summary>ビットフラグに関する処理を行うクラス</summary>
public static class BitFlag
{
    /// <summary>第二引数の項目が1か</summary>
    /// <param name="flags">調べたいフラグ</param>
    /// <param name="target">調べたい項目</param>
    public static bool IsFlag(this int flags, int target) => (flags & target) == target;

    /// <summary>第二引数の項目を1にする</summary>
    /// <param name="flags">1にするフラグ</param>
    /// <param name="target">1にしたい項目</param>
    public static int True(this int flags, int target) => flags | target;

    /// <summary>引数の項目を0にする</summary>
    /// <param name="flags">0にするフラグ</param>
    /// <param name="num">0にしたい項目</param>
    public static int False(this int flags, int num) => flags & ~num;

    /// <summary>すべて0</summary>
    public static int None(this int flags) => flags ^ flags;

    /// <summary>すべて1</summary>
    public static int Everything(this int flags) => flags | ~flags;

    /// <summary>すべて反転</summary>
    public static int Reverse(this int flags) => ~flags;
}
//【& : AND演算子】どちらも1なら1
//【| : OR演算子 】どちらかが1なら1
//【^ : XOR演算子】片方だけ1なら1
//【~ : NOT演算子】1を0, 0をに  1反転
