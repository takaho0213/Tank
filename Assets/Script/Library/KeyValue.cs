using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>キーと値のペアセットクラス</summary>
[System.Serializable]
public class KeyValue<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public KeyValue(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

/// <summary>KeyValueの拡張</summary>
public static class KeyValuEx
{
    /// <summary>Dictionary型に変換</summary>
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this KeyValue<TKey, TValue>[] array) => array.ToDictionary((v) => v.key, (v) => v.value);
}
