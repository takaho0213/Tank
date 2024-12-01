using TMPro;
using UnityEngine;

/// <summary>移動するテキスト</summary>
[System.Serializable]
public class MoveText
{
    /// <summary>テキストを表示するTextMeshProUGUI</summary>
    [field: SerializeField, LightColor] public TextMeshProUGUI TMP { get; private set; }

    /// <summary>動かすTransform</summary>
    public Transform Trafo { get; private set; }

    /// <summary>初期位置</summary>
    private Vector3 initPos;

    /// <summary>初期位置セット</summary>
    public void Init()
    {
        Trafo = TMP.transform;

        initPos = Trafo.position;
    }

    /// <summary>リセット</summary>
    public void ReSet()
    {
        TMP.color = Color.clear; //(0, 0, 0, 0)を代入

        TMP.text = string.Empty; //テキストにEmptyを代入

        Trafo.position = initPos;//初期位置を代入
    }
}
