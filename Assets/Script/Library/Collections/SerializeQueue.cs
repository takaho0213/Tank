using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SerializeQueue<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection
{
    [SerializeField] private T[] Array;

    private Queue<T> queue;

    public Queue<T> Queue => queue ??= new(Array);

    public int Count => Queue.Count;
    public bool IsSynchronized => (Queue as ICollection).IsSynchronized;
    public object SyncRoot => (Queue as ICollection).SyncRoot;
    public T Dequeue() => Queue.Dequeue();
    public void Enqueue(T item) => Queue.Enqueue(item);
    public bool TryDequeue(out T result) => Queue.TryDequeue(out result);
    public T Peek() => Queue.Peek();
    public bool TryPeek(out T result) => Queue.TryPeek(out result);
    public bool Contains(T item) => Queue.Contains(item);
    public void Clear() => Queue.Clear();
    public void CopyTo(System.Array array, int index) => (Queue as ICollection).CopyTo(array, index);
    public void CopyTo(T[] array, int arrayIndex) => Queue.CopyTo(array, arrayIndex);
    public void TrimExcess() => Queue.TrimExcess();
    public T[] ToArray() => Array = Queue.ToArray();
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => Queue.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => (Queue as IEnumerable).GetEnumerator();
}
