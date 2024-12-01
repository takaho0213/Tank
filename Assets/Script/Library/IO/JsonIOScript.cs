/// <summary>Json形式のファイルへのI/Oを行う</summary>
public class JsonIOScript : BaseIOScript
{
    /// <summary>拡張子</summary>
    public const string json = nameof(json);

    public override string Extension => json;
}
