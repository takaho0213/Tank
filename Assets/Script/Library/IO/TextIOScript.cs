/// <summary>Text形式のファイルへのI/Oを行う</summary>
public class TextIOScript : BaseIOScript
{
    /// <summary>拡張子</summary>
    public const string txt = nameof(txt);

    /// <summary>拡張子</summary>
    public override string Extension => txt;
}