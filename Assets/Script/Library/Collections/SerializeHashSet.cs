using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

[System.Serializable]
public class SerializeHashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ISet<T>, IDeserializationCallback, ISerializable
{
    [SerializeField] private T[] Array;

    private HashSet<T> hashSet;

    public HashSet<T> HashSet => hashSet ??= new(Array);

    public int Count => HashSet.Count;
    public IEqualityComparer<T> Comparer => HashSet.Comparer;
    public bool IsReadOnly => (HashSet as ICollection<T>).IsReadOnly;
    public bool Add(T item) => HashSet.Add(item);
    public void Clear() => HashSet.Clear();
    public bool Contains(T item) => HashSet.Contains(item);
    public void CopyTo(T[] array) => HashSet.CopyTo(array);
    public void CopyTo(T[] array, int arrayIndex) => HashSet.CopyTo(array, arrayIndex);
    public void CopyTo(T[] array, int arrayIndex, int count) => HashSet.CopyTo(array, arrayIndex, count);
    public int EnsureCapacity(int capacity) => HashSet.EnsureCapacity(capacity);
    public void ExceptWith(IEnumerable<T> other) => HashSet.ExceptWith(other);
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context) => HashSet.GetObjectData(info, context);
    public void IntersectWith(IEnumerable<T> other) => HashSet.IntersectWith(other);
    public bool IsProperSubsetOf(IEnumerable<T> other) => HashSet.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<T> other) => HashSet.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<T> other) => HashSet.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<T> other) => HashSet.IsSupersetOf(other);
    public virtual void OnDeserialization(object sender) => HashSet.OnDeserialization(sender);
    public bool Overlaps(IEnumerable<T> other) => HashSet.Overlaps(other);
    public bool Remove(T item) => HashSet.Remove(item);
    public int RemoveWhere(System.Predicate<T> match) => HashSet.RemoveWhere(match);
    public bool SetEquals(IEnumerable<T> other) => HashSet.SetEquals(other);
    public void SymmetricExceptWith(IEnumerable<T> other) => HashSet.SymmetricExceptWith(other);
    public void TrimExcess() => HashSet.TrimExcess();
    public bool TryGetValue(T equalValue, out T actualValue) => HashSet.TryGetValue(equalValue, out actualValue);
    public void UnionWith(IEnumerable<T> other) => HashSet.UnionWith(other);
    void ICollection<T>.Add(T item) => (HashSet as ICollection<T>).Add(item);
    IEnumerator IEnumerable.GetEnumerator() => (HashSet as IEnumerable).GetEnumerator();
    public IEnumerator<T> GetEnumerator() => HashSet.GetEnumerator();
}
