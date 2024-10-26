[System.Serializable]
public abstract class BaseCsvImporter<TResult> : BaseCsvImporter
{
    /// <summary>string配列を変換</summary>
    /// <param name="texts">変換するstring配列</param>
    /// <param name="result">変換した値</param>
    /// <returns>正常に変換できたか</returns>
    protected abstract bool TryParse(string[] texts, out TResult result);

    /// <summary>一行分のテキストを変換</summary>
    /// <param name="i">変換する行</param>
    /// <param name="result">変換した値</param>
    /// <returns>正常に変換できたか</returns>
    public bool TryParse(int i, out TResult result) => TryParse(CsvAsset.Text[i], out result);

    /// <summary>全行分のテキストを変換</summary>
    /// <param name="results">変換した値配列</param>
    /// <returns>正常に変換できたか</returns>
    public bool TryParse(out TResult[] results)
    {
        var allTexts = CsvAsset.Text;                  //すべてのテキスト

        results = new TResult[allTexts.Length];        //配列を作成

        for (int i = default; i < allTexts.Length; i++)//テキストの行数分繰り返す
        {
            if (!TryParse(allTexts[i], out var result))//i番目のテキストを変換し失敗したら
            {
                return false;                          //falseを返す
            }

            results[i] = result;                       //変換した値を代入
        }

        return true;                                   //trueを返す
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvImporter<T1, TResult> : BaseCsvImporter<TResult>
{
    /// <summary>1列目を変換する関数</summary>
    public TryParse<T1> TryParse1 { get; set; }

    /// <summary>変換された値を返す値に変換する関数</summary>
    public System.Func<T1, TResult> Generate { get; set; }

    public override int Length => 1;

    /// <summary>初期化</summary>
    /// <param name="try1">1列目を変換する関数</param>
    /// <param name="generate">変換された値を返す値に変換する関数</param>
    /// <param name="isAddException">変換する関数に対して、変換に失敗した際例外を投げる処理を付け加えるか</param>
    public void Initialize(TryParse<T1> try1, System.Func<T1, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;//例外を付け加えるなら/例外を付け加えた関数をセット
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, out TResult result)
    {
        var isGet = TryParse1(texts[FirstRow], out var value1);//[最初の行数]番目のテキストを変換

        result = Generate(value1);                             //返す値に変換

        return isGet;                                          //変換できたかを返す
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvImporter<T1, T2, TResult> : BaseCsvImporter<TResult>
{
    /// <summary></summary>
    public TryParse<T1> TryParse1 { get; set; }
    /// <summary></summary>
    public TryParse<T2> TryParse2 { get; set; }

    /// <summary>変換された値を返す値に変換する関数</summary>
    public System.Func<T1, T2, TResult> Generate { get; set; }

    public override int Length => 2;

    /// <summary>初期化</summary>
    /// <param name="try1">1列目を変換する関数</param>
    /// <param name="try2">2列目を変換する関数</param>
    /// <param name="generate">変換された値を返す値に変換する関数</param>
    /// <param name="isAddException">変換する関数に対して、変換に失敗した際例外を投げる処理を付け加えるか</param>
    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, System.Func<T1, T2, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;//例外を付け加えるなら/例外を付け加えた関数をセット
        TryParse2 = isAddException ? try2.AddException() : try2;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;                                              //行数

        var isGet1 = TryParse1(texts[FirstRow + row++], out var value1);//[最初の行数 + row]番目のテキストを変換
        var isGet2 = TryParse2(texts[FirstRow + row++], out var value2);

        result = Generate(value1, value2);                              //返す値に変換

        return isGet1 && isGet2;                                        //全て正常に変換できたかを返す
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvImporter<T1, T2, T3, TResult> : BaseCsvImporter<TResult>
{
    /// <summary>1列目を変換する関数</summary>
    public TryParse<T1> TryParse1 { get; set; }
    /// <summary>2列目を変換する関数</summary>
    public TryParse<T2> TryParse2 { get; set; }
    /// <summary>3列目を変換する関数</summary>
    public TryParse<T3> TryParse3 { get; set; }

    /// <summary>変換された値を返す値に変換する関数</summary>
    private System.Func<T1, T2, T3, TResult> Generate;

    public override int Length => 3;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, System.Func<T1, T2, T3, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;//例外を付け加えるなら/例外を付け加えた関数をセット
        TryParse2 = isAddException ? try2.AddException() : try2;//                  〃
        TryParse3 = isAddException ? try3.AddException() : try3;//                  〃
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;                                              //行数

        bool isGet1 = TryParse1(texts[FirstRow + row++], out var value1);//[最初の行数 + row]番目のテキストを変換
        bool isGet2 = TryParse2(texts[FirstRow + row++], out var value2);//              〃
        bool isGet3 = TryParse3(texts[FirstRow + row++], out var value3);//              〃

        result = Generate(value1, value2, value3);                       //返す値に変換

        return isGet1 && isGet2 && isGet3;                               //全て正常に変換できたかを返す
    }
}

/// <typeparam name="T1">一列目のデータ型</typeparam>
/// <typeparam name="T2">二列目のデータ型</typeparam>
/// <typeparam name="T3">三列目のデータ型</typeparam>
/// <typeparam name="T4">四列目のデータ型</typeparam>
/// <typeparam name="TResult">返すデータ型</typeparam>
[System.Serializable]
public class CsvImporter<T1, T2, T3, T4, TResult> : BaseCsvImporter<TResult>
{
    public TryParse<T1> TryParse1 { get; set; }
    public TryParse<T2> TryParse2 { get; set; }
    public TryParse<T3> TryParse3 { get; set; }
    public TryParse<T4> TryParse4 { get; set; }

    public System.Func<T1, T2, T3, T4, TResult> Generate { get; set; }

    public override int Length => 4;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, TryParse<T4> try4, System.Func<T1, T2, T3, T4, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;
        TryParse2 = isAddException ? try2.AddException() : try2;
        TryParse3 = isAddException ? try3.AddException() : try3;
        TryParse4 = isAddException ? try4.AddException() : try4;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[FirstRow + row++], out var value1);
        bool isGet2 = TryParse2(texts[FirstRow + row++], out var value2);
        bool isGet3 = TryParse3(texts[FirstRow + row++], out var value3);
        bool isGet4 = TryParse4(texts[FirstRow + row++], out var value4);

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
public class CsvImporter<T1, T2, T3, T4, T5, TResult> : BaseCsvImporter<TResult>
{
    public TryParse<T1> TryParse1 { get; set; }
    public TryParse<T2> TryParse2 { get; set; }
    public TryParse<T3> TryParse3 { get; set; }
    public TryParse<T4> TryParse4 { get; set; }
    public TryParse<T5> TryParse5 { get; set; }

    public System.Func<T1, T2, T3, T4, T5, TResult> Generate { get; set; }

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

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[FirstRow + row++], out var value1);
        bool isGet2 = TryParse2(texts[FirstRow + row++], out var value2);
        bool isGet3 = TryParse3(texts[FirstRow + row++], out var value3);
        bool isGet4 = TryParse4(texts[FirstRow + row++], out var value4);
        bool isGet5 = TryParse5(texts[FirstRow + row++], out var value5);

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
public class CsvImporter<T1, T2, T3, T4, T5, T6, TResult> : BaseCsvImporter<TResult>
{
    public TryParse<T1> TryParse1 { get; set; }
    public TryParse<T2> TryParse2 { get; set; }
    public TryParse<T3> TryParse3 { get; set; }
    public TryParse<T4> TryParse4 { get; set; }
    public TryParse<T5> TryParse5 { get; set; }
    public TryParse<T6> TryParse6 { get; set; }

    public System.Func<T1, T2, T3, T4, T5, T6, TResult> Generate { get; set; }

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

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[FirstRow + row++], out var value1);
        bool isGet2 = TryParse2(texts[FirstRow + row++], out var value2);
        bool isGet3 = TryParse3(texts[FirstRow + row++], out var value3);
        bool isGet4 = TryParse4(texts[FirstRow + row++], out var value4);
        bool isGet5 = TryParse5(texts[FirstRow + row++], out var value5);
        bool isGet6 = TryParse6(texts[FirstRow + row++], out var value6);

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
public class CsvImporter<T1, T2, T3, T4, T5, T6, T7, TResult> : BaseCsvImporter<TResult>
{
    public TryParse<T1> TryParse1 { get; set; }
    public TryParse<T2> TryParse2 { get; set; }
    public TryParse<T3> TryParse3 { get; set; }
    public TryParse<T4> TryParse4 { get; set; }
    public TryParse<T5> TryParse5 { get; set; }
    public TryParse<T6> TryParse6 { get; set; }
    public TryParse<T7> TryParse7 { get; set; }

    public System.Func<T1, T2, T3, T4, T5, T6, T7, TResult> Generate { get; set; }

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

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[FirstRow + row++], out var value1);
        bool isGet2 = TryParse2(texts[FirstRow + row++], out var value2);
        bool isGet3 = TryParse3(texts[FirstRow + row++], out var value3);
        bool isGet4 = TryParse4(texts[FirstRow + row++], out var value4);
        bool isGet5 = TryParse5(texts[FirstRow + row++], out var value5);
        bool isGet6 = TryParse6(texts[FirstRow + row++], out var value6);
        bool isGet7 = TryParse7(texts[FirstRow + row++], out var value7);

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
public class CsvImporter<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : BaseCsvImporter<TResult>
{
    public TryParse<T1> TryParse1 { get; set; }
    public TryParse<T2> TryParse2 { get; set; }
    public TryParse<T3> TryParse3 { get; set; }
    public TryParse<T4> TryParse4 { get; set; }
    public TryParse<T5> TryParse5 { get; set; }
    public TryParse<T6> TryParse6 { get; set; }
    public TryParse<T7> TryParse7 { get; set; }
    public TryParse<T8> TryParse8 { get; set; }

    public System.Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Generate { get; set; }

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

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[FirstRow + row++], out var value1);
        bool isGet2 = TryParse2(texts[FirstRow + row++], out var value2);
        bool isGet3 = TryParse3(texts[FirstRow + row++], out var value3);
        bool isGet4 = TryParse4(texts[FirstRow + row++], out var value4);
        bool isGet5 = TryParse5(texts[FirstRow + row++], out var value5);
        bool isGet6 = TryParse6(texts[FirstRow + row++], out var value6);
        bool isGet7 = TryParse7(texts[FirstRow + row++], out var value7);
        bool isGet8 = TryParse8(texts[FirstRow + row++], out var value8);

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
public class CsvImporter<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : BaseCsvImporter<TResult>
{
    public TryParse<T1> TryParse1 { get; set; }
    public TryParse<T2> TryParse2 { get; set; }
    public TryParse<T3> TryParse3 { get; set; }
    public TryParse<T4> TryParse4 { get; set; }
    public TryParse<T5> TryParse5 { get; set; }
    public TryParse<T6> TryParse6 { get; set; }
    public TryParse<T7> TryParse7 { get; set; }
    public TryParse<T8> TryParse8 { get; set; }
    public TryParse<T9> TryParse9 { get; set; }

    public System.Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Generate { get; set; }

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

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;

        bool isGet1 = TryParse1(texts[FirstRow + row++], out var value1);
        bool isGet2 = TryParse2(texts[FirstRow + row++], out var value2);
        bool isGet3 = TryParse3(texts[FirstRow + row++], out var value3);
        bool isGet4 = TryParse4(texts[FirstRow + row++], out var value4);
        bool isGet5 = TryParse5(texts[FirstRow + row++], out var value5);
        bool isGet6 = TryParse6(texts[FirstRow + row++], out var value6);
        bool isGet7 = TryParse7(texts[FirstRow + row++], out var value7);
        bool isGet8 = TryParse8(texts[FirstRow + row++], out var value8);
        bool isGet9 = TryParse9(texts[FirstRow + row++], out var value9);

        result = Generate(value1, value2, value3, value4, value5, value6, value7, value8, value9);

        return isGet1 && isGet2 && isGet3 && isGet4 && isGet5 && isGet6 && isGet7 && isGet8 && isGet9;
    }
}
