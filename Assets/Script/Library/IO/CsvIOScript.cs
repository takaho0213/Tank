/// <summary>Csv�`���̃t�@�C���ւ�I/O���s��</summary>
public class CsvIOScript : BaseIOScript
{
    /// <summary>�g���q</summary>
    private const string csv = nameof(csv);

    public override string Extension => csv;

    protected override string FileText
    {
        get => File.Text.TryByteStringToString(out var text) ? text : string.Empty;

        set => File.Text = value.StringToByteString();
    }
}
