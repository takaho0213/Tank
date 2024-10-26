[System.Serializable]
public abstract class BaseCsvArrayImporter<TResult> : BaseCsvImporter
{
    /// <summary>配列のサイズを取得</summary>
    public System.Func<string[], int> GetArraySize { get; set; }

    /// <summary>string配列を変換</summary>
    /// <param name="texts">変換するstring配列</param>
    /// <param name="i">変換する行</param>
    /// <param name="result">変換した値</param>
    /// <returns>正常に変換できたか</returns>
    protected abstract bool TryParse(string[] texts, int i, out TResult result);

    /// <summary>一行分のテキストを変換</summary>
    /// <param name="i">変換する行</param>
    /// <param name="results">変換した値</param>
    /// <returns>正常に変換できたか</returns>
    public bool TryParse(int i, out TResult[] results) => TryParse(CsvAsset.Text[i], out results);

    /// <summary>string配列を変換</summary>
    /// <param name="texts">変換するstring配列</param>
    /// <param name="results">変換した値</param>
    /// <returns>正常に変換できたか</returns>
    protected bool TryParse(string[] texts, out TResult[] results)
    {
        GetArraySize ??= DefaultGetArraySize;                   //nullなら関数を代入

        int length = GetArraySize(texts);           //配列の長さを取得

        results = new TResult[length];              //配列を作成

        for (int i = default; i < length; i++)      //配列の要素数分繰り返す
        {
            if (!TryParse(texts, i, out var result))//変換出来なければ
            {
                return false;                       //falseを返す
            }

            results[i] = result;                    //変換した値を代入
        }

        return true;                                //trueを返す
    }

    /// <summary>全行分のテキストを変換</summary>
    /// <param name="results">変換した値配列</param>
    /// <returns>正常に変換できたか</returns>
    public bool TryParse(out TResult[][] results)
    {
        string[][] allText = CsvAsset.Text;           //全てのテキスト

        int length = allText.Length;                  //行数

        results = new TResult[length][];              //配列を作成

        for (int i = default; i < length; i++)        //配列の要素数分繰り返す
        {
            if (!TryParse(allText[i], out var result))//変換できなかったら
            {
                return false;                         //falseを返す
            }

            results[i] = result;                      //変換した値を代入
        }

        return true;                                  //trueを返す
    }

    /// <summary>列を取得</summary>
    /// <param name="i">作成する配列のインデックス</param>
    /// <param name="row">列</param>
    /// <returns>列</returns>
    protected int GetRow(int i, int row) => (i * Length) + FirstRow + row;//(インデックス * 読み取る要素数) + 最初の列 + 列

    /// <summary>デフォルトの配列のサイズを取得する関数</summary>
    /// <param name="texts">一行分のテキスト</param>
    /// <returns>配列のサイズ</returns>
    public int DefaultGetArraySize(string[] texts) => (texts.Length - FirstRow) / Length;//(string配列のサイズ - 最初の列数) / 読み取る要素の数
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;

    private System.Func<T1, TResult> Generate;

    public override int Length => 1;

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        bool isGet = TryParse1(texts[GetRow(i, default)], out var value0);

        result = Generate(value0);

        return isGet;
    }

    public void Initialize(TryParse<T1> try1, System.Func<T1, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        Generate = generate;
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, T2, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;
    private TryParse<T2> TryParse2;

    private System.Func<T1, T2, TResult> Generate;

    public override int Length => 2;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, System.Func<T1, T2, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[GetRow(i, row++)], out var value1);
        bool isGet2 = TryParse2(texts[GetRow(i, row++)], out var value2);

        result = Generate(value1, value2);

        return isGet1 && isGet2;
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, T2, T3, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;
    private TryParse<T2> TryParse2;
    private TryParse<T3> TryParse3;

    private System.Func<T1, T2, T3, TResult> Generate;

    public override int Length => 3;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, System.Func<T1, T2, T3, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        TryParse3 = isAddException ? try3.AddException() : try3;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[GetRow(i, row++)], out var value1);
        bool isGet2 = TryParse2(texts[GetRow(i, row++)], out var value2);
        bool isGet3 = TryParse3(texts[GetRow(i, row++)], out var value3);

        result = Generate(value1, value2, value3);

        return isGet1 && isGet2 && isGet3;
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="T4">四列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, T2, T3, T4, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;
    private TryParse<T2> TryParse2;
    private TryParse<T3> TryParse3;
    private TryParse<T4> TryParse4;

    private System.Func<T1, T2, T3, T4, TResult> Generate;

    public override int Length => 4;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, TryParse<T4> try4, System.Func<T1, T2, T3, T4, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        TryParse3 = isAddException ? try3.AddException() : try3;
        TryParse4 = isAddException ? try4.AddException() : try4;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[GetRow(i, row++)], out var value1);
        bool isGet2 = TryParse2(texts[GetRow(i, row++)], out var value2);
        bool isGet3 = TryParse3(texts[GetRow(i, row++)], out var value3);
        bool isGet4 = TryParse4(texts[GetRow(i, row++)], out var value4);

        result = Generate(value1, value2, value3, value4);

        return isGet1 && isGet2 && isGet3 && isGet4;
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="T4">四列目のデータ型</typeparam>
/// <typeparam name="T5">五列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, T2, T3, T4, T5, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;
    private TryParse<T2> TryParse2;
    private TryParse<T3> TryParse3;
    private TryParse<T4> TryParse4;
    private TryParse<T5> TryParse5;

    private System.Func<T1, T2, T3, T4, T5, TResult> Generate;

    public override int Length => 5;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, TryParse<T4> try4, TryParse<T5> try5, System.Func<T1, T2, T3, T4, T5, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        TryParse3 = isAddException ? try3.AddException() : try3;
        TryParse4 = isAddException ? try4.AddException() : try4;
        TryParse5 = isAddException ? try5.AddException() : try5;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[GetRow(i, row++)], out var value1);
        bool isGet2 = TryParse2(texts[GetRow(i, row++)], out var value2);
        bool isGet3 = TryParse3(texts[GetRow(i, row++)], out var value3);
        bool isGet4 = TryParse4(texts[GetRow(i, row++)], out var value4);
        bool isGet5 = TryParse5(texts[GetRow(i, row++)], out var value5);

        result = Generate(value1, value2, value3, value4, value5);

        return isGet1 && isGet2 && isGet3 && isGet4 && isGet5;
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="T4">四列目のデータ型</typeparam>
/// <typeparam name="T5">五列目のデータ型</typeparam>
/// <typeparam name="T6">六列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, T2, T3, T4, T5, T6, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;
    private TryParse<T2> TryParse2;
    private TryParse<T3> TryParse3;
    private TryParse<T4> TryParse4;
    private TryParse<T5> TryParse5;
    private TryParse<T6> TryParse6;

    private System.Func<T1, T2, T3, T4, T5, T6, TResult> Generate;

    public override int Length => 6;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, TryParse<T4> try4, TryParse<T5> try5, TryParse<T6> try6, System.Func<T1, T2, T3, T4, T5, T6, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        TryParse3 = isAddException ? try3.AddException() : try3;
        TryParse4 = isAddException ? try4.AddException() : try4;
        TryParse5 = isAddException ? try5.AddException() : try5;
        TryParse6 = isAddException ? try6.AddException() : try6;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[GetRow(i, row++)], out var value1);
        bool isGet2 = TryParse2(texts[GetRow(i, row++)], out var value2);
        bool isGet3 = TryParse3(texts[GetRow(i, row++)], out var value3);
        bool isGet4 = TryParse4(texts[GetRow(i, row++)], out var value4);
        bool isGet5 = TryParse5(texts[GetRow(i, row++)], out var value5);
        bool isGet6 = TryParse6(texts[GetRow(i, row++)], out var value6);

        result = Generate(value1, value2, value3, value4, value5, value6);

        return isGet1 && isGet2 && isGet3 && isGet4 && isGet5 && isGet6;
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="T4">四列目のデータ型</typeparam>
/// <typeparam name="T5">五列目のデータ型</typeparam>
/// <typeparam name="T6">六列目のデータ型</typeparam>
/// <typeparam name="T7">七列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, T2, T3, T4, T5, T6, T7, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;
    private TryParse<T2> TryParse2;
    private TryParse<T3> TryParse3;
    private TryParse<T4> TryParse4;
    private TryParse<T5> TryParse5;
    private TryParse<T6> TryParse6;
    private TryParse<T7> TryParse7;

    private System.Func<T1, T2, T3, T4, T5, T6, T7, TResult> Generate;

    public override int Length => 7;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, TryParse<T4> try4, TryParse<T5> try5, TryParse<T6> try6, TryParse<T7> try7, System.Func<T1, T2, T3, T4, T5, T6, T7, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        TryParse3 = isAddException ? try3.AddException() : try3;
        TryParse4 = isAddException ? try4.AddException() : try4;
        TryParse5 = isAddException ? try5.AddException() : try5;
        TryParse6 = isAddException ? try6.AddException() : try6;
        TryParse7 = isAddException ? try7.AddException() : try7;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[GetRow(i, row++)], out var value1);
        bool isGet2 = TryParse2(texts[GetRow(i, row++)], out var value2);
        bool isGet3 = TryParse3(texts[GetRow(i, row++)], out var value3);
        bool isGet4 = TryParse4(texts[GetRow(i, row++)], out var value4);
        bool isGet5 = TryParse5(texts[GetRow(i, row++)], out var value5);
        bool isGet6 = TryParse6(texts[GetRow(i, row++)], out var value6);
        bool isGet7 = TryParse7(texts[GetRow(i, row++)], out var value7);

        result = Generate(value1, value2, value3, value4, value5, value6, value7);

        return isGet1 && isGet2 && isGet3 && isGet4 && isGet5 && isGet6 && isGet7;
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="T4">四列目のデータ型</typeparam>
/// <typeparam name="T5">五列目のデータ型</typeparam>
/// <typeparam name="T6">六列目のデータ型</typeparam>
/// <typeparam name="T7">七列目のデータ型</typeparam>
/// <typeparam name="T8">八列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;
    private TryParse<T2> TryParse2;
    private TryParse<T3> TryParse3;
    private TryParse<T4> TryParse4;
    private TryParse<T5> TryParse5;
    private TryParse<T6> TryParse6;
    private TryParse<T7> TryParse7;
    private TryParse<T8> TryParse8;

    private System.Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Generate;

    public override int Length => 8;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, TryParse<T4> try4, TryParse<T5> try5, TryParse<T6> try6, TryParse<T7> try7, TryParse<T8> try8, System.Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        TryParse3 = isAddException ? try3.AddException() : try3;
        TryParse4 = isAddException ? try4.AddException() : try4;
        TryParse5 = isAddException ? try5.AddException() : try5;
        TryParse6 = isAddException ? try6.AddException() : try6;
        TryParse7 = isAddException ? try7.AddException() : try7;
        TryParse8 = isAddException ? try8.AddException() : try8;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[GetRow(i, row++)], out var value1);
        bool isGet2 = TryParse2(texts[GetRow(i, row++)], out var value2);
        bool isGet3 = TryParse3(texts[GetRow(i, row++)], out var value3);
        bool isGet4 = TryParse4(texts[GetRow(i, row++)], out var value4);
        bool isGet5 = TryParse5(texts[GetRow(i, row++)], out var value5);
        bool isGet6 = TryParse6(texts[GetRow(i, row++)], out var value6);
        bool isGet7 = TryParse7(texts[GetRow(i, row++)], out var value7);
        bool isGet8 = TryParse8(texts[GetRow(i, row++)], out var value8);

        result = Generate(value1, value2, value3, value4, value5, value6, value7, value8);

        return isGet1 && isGet2 && isGet3 && isGet4 && isGet5 && isGet6 && isGet7 && isGet8;
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="T4">四列目のデータ型</typeparam>
/// <typeparam name="T5">五列目のデータ型</typeparam>
/// <typeparam name="T6">六列目のデータ型</typeparam>
/// <typeparam name="T7">七列目のデータ型</typeparam>
/// <typeparam name="T8">八列目のデータ型</typeparam>
/// <typeparam name="T9">九列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvArrayImporter<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : BaseCsvArrayImporter<TResult>
{
    private TryParse<T1> TryParse1;
    private TryParse<T2> TryParse2;
    private TryParse<T3> TryParse3;
    private TryParse<T4> TryParse4;
    private TryParse<T5> TryParse5;
    private TryParse<T6> TryParse6;
    private TryParse<T7> TryParse7;
    private TryParse<T8> TryParse8;
    private TryParse<T9> TryParse9;

    private System.Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Generate;

    public override int Length => 9;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, TryParse<T4> try4, TryParse<T5> try5, TryParse<T6> try6, TryParse<T7> try7, TryParse<T8> try8, TryParse<T9> try9, System.Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        TryParse3 = isAddException ? try3.AddException() : try3;
        TryParse4 = isAddException ? try4.AddException() : try4;
        TryParse5 = isAddException ? try5.AddException() : try5;
        TryParse6 = isAddException ? try6.AddException() : try6;
        TryParse7 = isAddException ? try7.AddException() : try7;
        TryParse8 = isAddException ? try8.AddException() : try8;
        TryParse9 = isAddException ? try9.AddException() : try9;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, int i, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[GetRow(i, row++)], out var value1);
        bool isGet2 = TryParse2(texts[GetRow(i, row++)], out var value2);
        bool isGet3 = TryParse3(texts[GetRow(i, row++)], out var value3);
        bool isGet4 = TryParse4(texts[GetRow(i, row++)], out var value4);
        bool isGet5 = TryParse5(texts[GetRow(i, row++)], out var value5);
        bool isGet6 = TryParse6(texts[GetRow(i, row++)], out var value6);
        bool isGet7 = TryParse7(texts[GetRow(i, row++)], out var value7);
        bool isGet8 = TryParse8(texts[GetRow(i, row++)], out var value8);
        bool isGet9 = TryParse9(texts[GetRow(i, row++)], out var value9);

        result = Generate(value1, value2, value3, value4, value5, value6, value7, value8, value9);

        return isGet1 && isGet2 && isGet3 && isGet4 && isGet5 && isGet6 && isGet7 && isGet8 && isGet9;
    }
}
