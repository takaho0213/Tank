public static class IOConverter
{
    /// <summary>Object��Json�ɕϊ�</summary>
    public static string ToJson(this object data, bool prettyPrint = false) => UnityEngine.JsonUtility.ToJson(data, prettyPrint);

    /// <summary>Json��Object�ɕϊ�</summary>
    public static T FormJson<T>(this string json)
    {
        try
        {
            return UnityEngine.JsonUtility.FromJson<T>(json);//�ϊ����Ԃ�
        }
        catch (System.ArgumentException)                     //��O������������
        {
            return default;                                  //default��Ԃ�
        }
    }

    /// <summary>Json��Object�ɕϊ�</summary>
    public static bool TryFormJson<T>(this string json, out T obj) => !Equals(obj = FormJson<T>(json), default);

    /// <summary>�������byte�z��ɕϊ�</summary>
    public static byte[] StringToBytes(this string text) => System.Text.Encoding.UTF8.GetBytes(text);

    /// <summary>byte�z��𕶎���ɕϊ�</summary>
    public static string BytesToString(this byte[] bytes) => System.Text.Encoding.UTF8.GetString(bytes);

    /// <summary>�������byte������ɕϊ�</summary>
    public static string StringToByteString(this string text) => StringToBytes(text).JoinComma();

    /// <summary>byte������𕶎���ɕϊ�</summary>
    public static bool TryByteStringToString(this string byteText, out string r)
    {
        r = string.Empty;                                  //�Ԃ��e�L�X�g��Empty����

        var byteTexts = byteText.SplitComma();             //byte������z��

        byte[] bytes = new byte[byteTexts.Length];         //byte�z����쐬

        for (int i = default; i < byteTexts.Length; i++)   //byte������z��̗v�f���J��Ԃ�
        {
            if (byte.TryParse(byteTexts[i], out var value))//�����񂩂�byte�ɕϊ��o������
            {
                bytes[i] = value;                          //byte����
            }
            else return false;                             //����ȊO�Ȃ�/false��Ԃ�
        }

        r = BytesToString(bytes);                          //byte�z��𕶎���ɕϊ�

        return true;                                       //true��Ԃ�
    }

    /// <summary>Object��Json�ɕϊ���byte������ɕϊ�</summary>
    public static string ObjectToByteString(this object data, bool prettyPrint = false) => StringToByteString(ToJson(data, prettyPrint));

    /// <summary>byte�������Json�ɕϊ���Object�ɕϊ�</summary>
    public static bool TryByteStringFormJson<T>(this string byteText, out T r)
    {
        if (!TryByteStringToString(byteText, out var v))//�ϊ��o������
        {
            r = default;                                //default����

            return false;                               //false��Ԃ�
        }

        return TryFormJson(v, out r);                   //�ϊ��o��������Ԃ�
    }
}

