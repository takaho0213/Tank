using UnityEngine;
using System.Collections.Generic;

/// <summary>要素をシリアライズ化するリスト</summary>
[System.Serializable]
public class SerializList<T> : List<T>
{
    [SerializeField] private List<T> List;

    public SerializList() => List = this;
}
