[System.Serializable]
public abstract class BaseCsvImporter<TResult> : BaseCsvImporter
{
    /// <summary>string�z���ϊ�</summary>
    /// <param name="texts">�ϊ�����string�z��</param>
    /// <param name="result">�ϊ������l</param>
    /// <returns>����ɕϊ��ł�����</returns>
    protected abstract bool TryParse(string[] texts, out TResult result);

    /// <summary>��s���̃e�L�X�g��ϊ�</summary>
    /// <param name="i">�ϊ�����s</param>
    /// <param name="result">�ϊ������l</param>
    /// <returns>����ɕϊ��ł�����</returns>
    public bool TryParse(int i, out TResult result) => TryParse(CsvAsset.Text[i], out result);

    /// <summary>�S�s���̃e�L�X�g��ϊ�</summary>
    /// <param name="results">�ϊ������l�z��</param>
    /// <returns>����ɕϊ��ł�����</returns>
    public bool TryParse(out TResult[] results)
    {
        var allTexts = CsvAsset.Text;                  //���ׂẴe�L�X�g

        results = new TResult[allTexts.Length];        //�z����쐬

        for (int i = default; i < allTexts.Length; i++)//�e�L�X�g�̍s�����J��Ԃ�
        {
            if (!TryParse(allTexts[i], out var result))//i�Ԗڂ̃e�L�X�g��ϊ������s������
            {
                return false;                          //false��Ԃ�
            }

            results[i] = result;                       //�ϊ������l����
        }

        return true;                                   //true��Ԃ�
    }
}

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
[System.Serializable]
public class CsvImporter<T1, TResult> : BaseCsvImporter<TResult>
{
    /// <summary>1��ڂ�ϊ�����֐�</summary>
    public TryParse<T1> TryParse1 { get; set; }

    /// <summary>�ϊ����ꂽ�l��Ԃ��l�ɕϊ�����֐�</summary>
    public System.Func<T1, TResult> Generate { get; set; }

    public override int Length => 1;

    /// <summary>������</summary>
    /// <param name="try1">1��ڂ�ϊ�����֐�</param>
    /// <param name="generate">�ϊ����ꂽ�l��Ԃ��l�ɕϊ�����֐�</param>
    /// <param name="isAddException">�ϊ�����֐��ɑ΂��āA�ϊ��Ɏ��s�����ۗ�O�𓊂��鏈����t�������邩</param>
    public void Initialize(TryParse<T1> try1, System.Func<T1, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;//��O��t��������Ȃ�/��O��t���������֐����Z�b�g
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, out TResult result)
    {
        var isGet = TryParse1(texts[FirstRow], out var value1);//[�ŏ��̍s��]�Ԗڂ̃e�L�X�g��ϊ�

        result = Generate(value1);                             //�Ԃ��l�ɕϊ�

        return isGet;                                          //�ϊ��ł�������Ԃ�
    }
}

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T2">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
[System.Serializable]
public class CsvImporter<T1, T2, TResult> : BaseCsvImporter<TResult>
{
    /// <summary></summary>
    public TryParse<T1> TryParse1 { get; set; }
    /// <summary></summary>
    public TryParse<T2> TryParse2 { get; set; }

    /// <summary>�ϊ����ꂽ�l��Ԃ��l�ɕϊ�����֐�</summary>
    public System.Func<T1, T2, TResult> Generate { get; set; }

    public override int Length => 2;

    /// <summary>������</summary>
    /// <param name="try1">1��ڂ�ϊ�����֐�</param>
    /// <param name="try2">2��ڂ�ϊ�����֐�</param>
    /// <param name="generate">�ϊ����ꂽ�l��Ԃ��l�ɕϊ�����֐�</param>
    /// <param name="isAddException">�ϊ�����֐��ɑ΂��āA�ϊ��Ɏ��s�����ۗ�O�𓊂��鏈����t�������邩</param>
    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, System.Func<T1, T2, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;//��O��t��������Ȃ�/��O��t���������֐����Z�b�g
        TryParse2 = isAddException ? try2.AddException() : try2;
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;                                              //�s��

        var isGet1 = TryParse1(texts[FirstRow + row++], out var value1);//[�ŏ��̍s�� + row]�Ԗڂ̃e�L�X�g��ϊ�
        var isGet2 = TryParse2(texts[FirstRow + row++], out var value2);

        result = Generate(value1, value2);                              //�Ԃ��l�ɕϊ�

        return isGet1 && isGet2;                                        //�S�Đ���ɕϊ��ł�������Ԃ�
    }
}

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T2">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T3">�O��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
[System.Serializable]
public class CsvImporter<T1, T2, T3, TResult> : BaseCsvImporter<TResult>
{
    /// <summary>1��ڂ�ϊ�����֐�</summary>
    public TryParse<T1> TryParse1 { get; set; }
    /// <summary>2��ڂ�ϊ�����֐�</summary>
    public TryParse<T2> TryParse2 { get; set; }
    /// <summary>3��ڂ�ϊ�����֐�</summary>
    public TryParse<T3> TryParse3 { get; set; }

    /// <summary>�ϊ����ꂽ�l��Ԃ��l�ɕϊ�����֐�</summary>
    private System.Func<T1, T2, T3, TResult> Generate;

    public override int Length => 3;

    public void Initialize(TryParse<T1> try1, TryParse<T2> try2, TryParse<T3> try3, System.Func<T1, T2, T3, TResult> generate, bool isAddException = true)
    {
        TryParse1 = isAddException ? try1.AddException() : try1;//��O��t��������Ȃ�/��O��t���������֐����Z�b�g
        TryParse2 = isAddException ? try2.AddException() : try2;//                  �V
        TryParse3 = isAddException ? try3.AddException() : try3;//                  �V
        Generate = generate;
    }

    protected override bool TryParse(string[] texts, out TResult result)
    {
        int row = default;                                              //�s��

        bool isGet1 = TryParse1(texts[FirstRow + row++], out var value1);//[�ŏ��̍s�� + row]�Ԗڂ̃e�L�X�g��ϊ�
        bool isGet2 = TryParse2(texts[FirstRow + row++], out var value2);//              �V
        bool isGet3 = TryParse3(texts[FirstRow + row++], out var value3);//              �V

        result = Generate(value1, value2, value3);                       //�Ԃ��l�ɕϊ�

        return isGet1 && isGet2 && isGet3;                               //�S�Đ���ɕϊ��ł�������Ԃ�
    }
}

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T2">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T3">�O��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T4">�l��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
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

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T2">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T3">�O��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T4">�l��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T5">�ܗ�ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
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

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T2">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T3">�O��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T4">�l��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T5">�ܗ�ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T6">�Z��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
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

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T2">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T3">�O��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T4">�l��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T5">�ܗ�ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T6">�Z��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T7">����ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
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

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T2">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T3">�O��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T4">�l��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T5">�ܗ�ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T6">�Z��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T7">����ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T8">����ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
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

/// <typeparam name="T1">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T2">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T3">�O��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T4">�l��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T5">�ܗ�ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T6">�Z��ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T7">����ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T8">����ڂ̃f�[�^�^</typeparam>
/// <typeparam name="T9">���ڂ̃f�[�^�^</typeparam>
/// <typeparam name="TResult">�Ԃ��f�[�^�^</typeparam>
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
