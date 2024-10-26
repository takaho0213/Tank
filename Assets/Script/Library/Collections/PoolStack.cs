using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PoolStack<T> : Stack<T>, IPool<T> where T : MonoBehaviour
{
    /// <summary>プールするオブジェクトのベース</summary>
    [SerializeField] private T original;

    /// <summary>プールするオブジェクトのベース</summary>
    public T Original { get => original; set => original = value; }

    /// <summary>オブジェクトを生成する関数の参照</summary>
    public System.Func<T, T> Instantiate { get; private set; }

    /// <summary>デフォルトのオブジェクトを生成する関数</summary>
    private T DefaultInstantiate(T original) => Object.Instantiate(original);

    /// <summary>初期化</summary>
    /// <param name="instantiate">オブジェクトを生成する関数の参照</param>
    public void Initialize(System.Func<T, T> instantiate)
    {
        Instantiate = instantiate;
    }

    /// <summary>オブジェクトを取得</summary>
    public T GetObject()
    {
        Instantiate ??= DefaultInstantiate;//nullならデフォルトの関数を代入

        return TryPop(out var value) ? value : Instantiate.Invoke(original);//要素があれば ? : オブジェクト : 新規のオブジェクトを返す
    }
}
