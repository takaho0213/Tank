using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SerializeStack<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection
{
    [SerializeField] public T[] Array;

    private Stack<T> stack;

    public Stack<T> Stack => stack ??= new(Array);

    public int Count => Stack.Count;
    public bool IsSynchronized => (Stack as ICollection).IsSynchronized;
    public object SyncRoot => (Stack as ICollection).SyncRoot;
    public void Push(T item) => Stack.Push(item);
    public T Peek() => Stack.Peek();
    public bool TryPeek(out T result) => Stack.TryPeek(out result);
    public T Pop() => Stack.Pop();
    public bool TryPop(out T result) => Stack.TryPop(out result);
    public bool Contains(T item) => Stack.Contains(item);
    public void Clear() => Stack.Clear();
    public void TrimExcess() => Stack.TrimExcess();
    public T[] ToArray() => Array = Stack.ToArray();
    public void CopyTo(T[] array, int arrayIndex) => Stack.CopyTo(array, arrayIndex);
    public void CopyTo(System.Array array, int index) => (Stack as ICollection).CopyTo(array, index);
    public IEnumerator<T> GetEnumerator() => Stack.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => (Stack as IEnumerable).GetEnumerator();
}
