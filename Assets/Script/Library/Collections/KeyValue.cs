using System.Collections.Generic;

[System.Serializable]
public class KeyValue<TKey, TValue>
{
    public TKey Key;
    public TValue Value;

    public KeyValue(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public KeyValuePair<TKey, TValue> ToKeyValuePair() => new(Key, Value);
}

/// <summary>KeyValue‚ÌŠg’£</summary>
public static class KeyValuEx
{
    /// <summary>DictionaryŒ^‚É•ÏŠ·</summary>
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this KeyValue<TKey, TValue>[] array)
    {
        Dictionary<TKey, TValue> d = new();

        foreach (var pair in array)
        {
            if (!d.ContainsKey(pair.Key))
            {
                d.Add(pair.Key, pair.Value);
            }
        }

        return d;
    }

    public static KeyValuePair<TKey, TValue>[] ToKeyValuePairs<TKey, TValue>(this KeyValue<TKey, TValue>[] array)
    {
        var pairs = new KeyValuePair<TKey, TValue>[array.Length];

        for (int i = default; i < array.Length; i++)
        {
            pairs[i] = array[i].ToKeyValuePair();
        }

        return pairs;
    }
}
