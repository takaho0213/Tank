using System.Collections.Generic;

public class ObjectPoolQueue<T> : Queue<T>, IPool<T>
{
    /// <summary>オブジェクトを生成する関数の参照</summary>
    public System.Func<T> Instantiate { get; private set; }

    public ObjectPoolQueue(System.Func<T> instantiate) => Instantiate = instantiate;

    public T GetObject() => TryDequeue(out var result) ? result : Instantiate();
}