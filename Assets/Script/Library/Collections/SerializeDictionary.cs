using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SerializeDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
{
    [SerializeField] protected KeyValue<TKey, TValue>[] Pairs;

    protected Dictionary<TKey, TValue> dictionary;

    public Dictionary<TKey, TValue> Dictionary => dictionary ??= Pairs.ToDictionary();

    public int Count => Dictionary.Count;

    public IEnumerable<TKey> Keys => Dictionary.Keys;

    public IEnumerable<TValue> Values => Dictionary.Values;

    public TValue this[TKey key] => Dictionary[key];

    public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

    public bool TryGetValue(TKey key, out TValue value) => Dictionary.TryGetValue(key, out value);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();
}
