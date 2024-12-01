/// <summary>Csv形式のファイルへのI/Oを行う</summary>
public class CsvIOScript : BaseIOScript
{
    /// <summary>拡張子</summary>
    private const string csv = nameof(csv);

    public override string Extension => csv;

    protected override string FileText
    {
        get => File.Text.TryByteStringToString(out var text) ? text : string.Empty;

        set => File.Text = value.StringToByteString();
    }
}
