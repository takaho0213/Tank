using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PoolList<T> : List<T>, IPool<T> where T : MonoBehaviour
{
    /// <summary>プールするオブジェクトのベース</summary>
    [SerializeField] private T original;

    /// <summary>プールするオブジェクトのベース</summary>
    public T Original { get => original; set => original = value; }

    /// <summary>プールされているオブジェクトを返すか？を返す関数の参照</summary>
    public System.Func<T, bool> IsReturn { get; private set; }

    /// <summary>オブジェクトを生成する関数の参照</summary>
    public System.Func<T, T> Instantiate { get; private set; }

    /// <summary>デフォルトのプールされているオブジェクトを返す条件</summary>
    private bool DefaultIsReturn(T v) => !v.gameObject.activeSelf;

    /// <summary>デフォルトのオブジェクトを生成する関数</summary>
    private T DefaultInstantiate(T original) => Object.Instantiate(original);

    /// <summary>初期化</summary>
    /// <param name="isReturn">プールされているオブジェクトを返すか？</param>
    /// <param name="instantiate">オブジェクトを生成する関数の参照</param>
    public void Initialize(System.Func<T, bool> isReturn, System.Func<T, T> instantiate)
    {
        IsReturn = isReturn;
        Instantiate = instantiate;
    }

    /// <summary>オブジェクトを取得</summary>
    public T GetObject()
    {
        IsReturn ??= DefaultIsReturn;           //nullならデフォルトの関数を代入

        T result;                               //返すオブジェクト

        for (int i = default; i < Count; i++)   //要素数分繰り返す
        {
            result = this[i];                   //i番目の要素を代入

            if (IsReturn(result)) return result;//返す条件のものがあれば返す
        }

        Instantiate ??= DefaultInstantiate;     //nullならデフォルトの関数を代入

        result = Instantiate.Invoke(original);  //新たにインスタンスを生成

        Add(result);                            //リストに追加

        return result;                          //インスタンスを返す
    }
}
