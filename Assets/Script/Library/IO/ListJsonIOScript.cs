using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListJsonIOScript<T> : JsonIOScript, ICollection<T>, IEnumerable<T>, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>
{
    [SerializeField] private SerializeList<T> List;

    public T this[int index] { get => List[index]; set => List[index] = value; }

    public int Count => List.Count;
    public void Add(T item) => List.Add(item);
    public void Clear() => List.Clear();
    public bool Contains(T item) => List.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);
    public int IndexOf(T item) => List.IndexOf(item);
    public void Insert(int index, T item) => List.Insert(index, item);
    public bool Remove(T item) => List.Remove(item);
    public void RemoveAt(int index) => List.RemoveAt(index);
    public IEnumerator<T> GetEnumerator() => List.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => (List as IEnumerable).GetEnumerator();
    bool ICollection<T>.IsReadOnly => (List as ICollection<T>).IsReadOnly;

    protected override void Awake()
    {
        base.Awake();

        List = Load<SerializeList<T>>();
    }
}
