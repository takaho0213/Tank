using System.Linq;

public static class CharEx
{
    /// <summary>改行コード</summary>
    public const char NewLine = '\n';
    /// <summary>コンマ</summary>
    public const char Comma = ',';
    /// <summary>null終端文字</summary>
    public const char Final = '\0';

    /// <summary>最初の数</summary>
    public const char FirstNumber = '0';
    /// <summary>最後の数</summary>
    public const char LastNumber = '9';

    /// <summary>最初の小文字アルファベット</summary>
    public const char FirstAlphabetLower = 'a';
    /// <summary>最後の小文字アルファベット</summary>
    public const char LastAlphabetLower = 'z';

    /// <summary>最初の大文字アルファベット</summary>
    public const char FirstAlphabetUpper = 'A';
    /// <summary>最後の大文字アルファベット</summary>
    public const char LastAlphabetUpper = 'Z';

    /// <summary>1</summary>
    public const char One = '1';
    /// <summary>2</summary>
    public const char Two = '2';

    /// <summary>Stringにおけるキャラの長さ</summary>
    public const int Length = 1;

    /// <summary>ToString()の最初の要素</summary>
    public static char ToChar<T>(this T value) => value.ToString().FirstOrDefault();

    /// <summary>ランダムな数</summary>
    public static char RandomNumber() => (char)RandomEx.Range(FirstNumber, LastNumber);

    /// <summary>ランダムな大文字アルファベット</summary>
    public static char RandomAlphabetUpper() => (char)RandomEx.Range(FirstAlphabetUpper, LastAlphabetUpper);

    /// <summary>ランダムな小文字アルファベット</summary>
    public static char RandomAlphabetLower() => (char)RandomEx.Range(FirstAlphabetLower, LastAlphabetLower);

    /// <summary>ランダムな英数字/summary>
    public static char RandomAlphanumeric()
    {
        char value = (char)RandomEx.Range(FirstNumber, LastAlphabetLower);

        return value.IsAlphanumeric() ? value : RandomAlphanumeric();
    }

    /// <summary>インデックス指定の置換</summary>
    /// <param name="text">置換する文字列</param>
    /// <param name="index">置換する位置</param>
    /// <param name="replace">置換する文字</param>
    public static string ReplaceIndex(this string text, int index, char replace)
    {
        char[] chars = text.ToCharArray();

        chars[index] = replace;

        return new(chars);
    }

    /// <summary>アルファベットか？</summary>
    public static bool IsAlphabet(this char value) => value.IsAlphabetLower() || value.IsAlphabetUpper();
    /// <summary>大文字のアルファベットか？</summary>
    public static bool IsAlphabetUpper(this char value) => value.IsInOfRange(FirstAlphabetUpper, LastAlphabetUpper);
    /// <summary>小文字のアルファベットか？</summary>
    public static bool IsAlphabetLower(this char value) => value.IsInOfRange(FirstAlphabetLower, LastAlphabetLower);

    /// <summary>英数字か？</summary>
    public static bool IsAlphanumeric(this char value) => char.IsNumber(value) || value.IsAlphabet();
}

//【000】  =>  【NULL(null文字)】
//【001】  =>  【SOH(ヘッダ開始)】
//【002】  =>  【STX(テキスト開始)】
//【003】  =>  【ETX(テキスト終了)】
//【004】  =>  【EOT(転送終了)】
//【005】  =>  【ENQ(照会)】
//【006】  =>  【ACK(受信確認)】
//【007】  =>  【BEL(警告)】
//【008】  =>  【BS(後退)】
//【009】  =>  【HT(水平タブ)】
//【010】  =>  【LF(改行)】
//【011】  =>  【VT(垂直タブ)】
//【012】  =>  【FF(改頁)】
//【013】  =>  【CR(復帰)】
//【014】  =>  【SO(シフトアウト)】
//【015】  =>  【SI(シフトイン)】
//【016】  =>  【DLE(データリンクエスケープ)】
//【017】  =>  【DC1(装置制御1)】
//【018】  =>  【DC2(装置制御2)】
//【019】  =>  【DC3(装置制御3)】
//【020】  =>  【DC4(装置制御4)】
//【021】  =>  【NAK(受信失敗)】
//【022】  =>  【SYN(同期)】
//【023】  =>  【ETB(転送ブロック終了)】
//【024】  =>  【CAN(キャンセル)】
//【025】  =>  【EM(メディア終了)】
//【026】  =>  【SUB(置換)】
//【027】  =>  【ESC(エスケープ)】
//【028】  =>  【FS(フォーム区切り)】
//【029】  =>  【GS(グループ区切り)】
//【030】  =>  【RS(レコード区切り)】
//【031】  =>  【US(ユニット区切り)】
//【032】  =>  【SPC(空白文字)】
//【033】  =>  【!】
//【034】  =>  【"】
//【035】  =>  【#】
//【036】  =>  【$】
//【037】  =>  【%】
//【038】  =>  【&】
//【039】  =>  【'】
//【040】  =>  【(】
//【041】  =>  【)】
//【042】  =>  【*】
//【043】  =>  【+】
//【044】  =>  【,】
//【045】  =>  【-】
//【046】  =>  【.】
//【047】  =>  【/】
//【048】  =>  【0】
//【049】  =>  【1】
//【050】  =>  【2】
//【051】  =>  【3】
//【052】  =>  【4】
//【053】  =>  【5】
//【054】  =>  【6】
//【055】  =>  【7】
//【056】  =>  【8】
//【057】  =>  【9】
//【058】  =>  【:】
//【059】  =>  【;】
//【060】  =>  【<】
//【061】  =>  【=】
//【062】  =>  【>】
//【063】  =>  【?】
//【064】  =>  【@】
//【065】  =>  【A】
//【066】  =>  【B】
//【067】  =>  【C】
//【068】  =>  【D】
//【069】  =>  【E】
//【070】  =>  【F】
//【071】  =>  【G】
//【072】  =>  【H】
//【073】  =>  【SetFirst】
//【074】  =>  【J】
//【075】  =>  【K】
//【076】  =>  【L】
//【077】  =>  【M】
//【078】  =>  【N】
//【079】  =>  【O】
//【080】  =>  【P】
//【081】  =>  【Q】
//【082】  =>  【R】
//【083】  =>  【S】
//【084】  =>  【T】
//【085】  =>  【U】
//【086】  =>  【V】
//【087】  =>  【W】
//【088】  =>  【X】
//【089】  =>  【Y】
//【090】  =>  【Z】
//【091】  =>  【[】
//【092】  =>  【\】
//【093】  =>  【]】
//【094】  =>  【^】
//【095】  =>  【_】
//【096】  =>  【`】
//【097】  =>  【a】
//【098】  =>  【b】
//【099】  =>  【c】
//【100】  =>  【d】
//【101】  =>  【e】
//【102】  =>  【f】
//【103】  =>  【g】
//【104】  =>  【h】
//【105】  =>  【i】
//【106】  =>  【j】
//【107】  =>  【k】
//【108】  =>  【l】
//【109】  =>  【m】
//【110】  =>  【n】
//【111】  =>  【o】
//【112】  =>  【p】
//【113】  =>  【q】
//【114】  =>  【r】
//【115】  =>  【s】
//【116】  =>  【t】
//【117】  =>  【u】
//【118】  =>  【v】
//【119】  =>  【w】
//【120】  =>  【x】
//【121】  =>  【y】
//【122】  =>  【z】
//【123】  =>  【{】
//【124】  =>  【|】
//【125】  =>  【}】
//【126】  =>  【~】
//【127】  =>  【DEL(削除)】