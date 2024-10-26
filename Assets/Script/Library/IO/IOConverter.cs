public static class IOConverter
{
    /// <summary>ObjectをJsonに変換</summary>
    public static string ToJson(this object data, bool prettyPrint = false) => UnityEngine.JsonUtility.ToJson(data, prettyPrint);

    /// <summary>JsonをObjectに変換</summary>
    public static T FormJson<T>(this string json)
    {
        try
        {
            return UnityEngine.JsonUtility.FromJson<T>(json);//変換し返す
        }
        catch (System.ArgumentException)                     //例外が発生したら
        {
            return default;                                  //defaultを返す
        }
    }

    /// <summary>JsonをObjectに変換</summary>
    public static bool TryFormJson<T>(this string json, out T obj) => !Equals(obj = FormJson<T>(json), default);

    /// <summary>文字列をbyte配列に変換</summary>
    public static byte[] StringToBytes(this string text) => System.Text.Encoding.UTF8.GetBytes(text);

    /// <summary>byte配列を文字列に変換</summary>
    public static string BytesToString(this byte[] bytes) => System.Text.Encoding.UTF8.GetString(bytes);

    /// <summary>文字列をbyte文字列に変換</summary>
    public static string StringToByteString(this string text) => StringToBytes(text).JoinComma();

    /// <summary>byte文字列を文字列に変換</summary>
    public static bool TryByteStringToString(this string byteText, out string r)
    {
        r = string.Empty;                                  //返すテキストにEmptyを代入

        var byteTexts = byteText.SplitComma();             //byte文字列配列

        byte[] bytes = new byte[byteTexts.Length];         //byte配列を作成

        for (int i = default; i < byteTexts.Length; i++)   //byte文字列配列の要素分繰り返す
        {
            if (byte.TryParse(byteTexts[i], out var value))//文字列からbyteに変換出来たら
            {
                bytes[i] = value;                          //byteを代入
            }
            else return false;                             //それ以外なら/falseを返す
        }

        r = BytesToString(bytes);                          //byte配列を文字列に変換

        return true;                                       //trueを返す
    }

    /// <summary>ObjectをJsonに変換しbyte文字列に変換</summary>
    public static string ObjectToByteString(this object data, bool prettyPrint = false) => StringToByteString(ToJson(data, prettyPrint));

    /// <summary>byte文字列をJsonに変換しObjectに変換</summary>
    public static bool TryByteStringFormJson<T>(this string byteText, out T r)
    {
        if (!TryByteStringToString(byteText, out var v))//変換出来たか
        {
            r = default;                                //defaultを代入

            return false;                               //falseを返す
        }

        return TryFormJson(v, out r);                   //変換出来たかを返す
    }
}

