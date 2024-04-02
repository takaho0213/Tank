using UnityEngine;
using System.Collections.Generic;

/// <summary>�v�f���V���A���C�Y�����郊�X�g</summary>
[System.Serializable]
public class SerializList<T> : List<T>
{
    [SerializeField] private List<T> List;

    public SerializList() => List = this;
}
