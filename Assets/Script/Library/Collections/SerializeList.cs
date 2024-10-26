using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SerializeList<T> : List<T>
{
    [SerializeField] private List<T> List;

    public SerializeList() => List = this;
}
