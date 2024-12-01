using System;
using UnityEngine;

/// <summary>クリアタイムのスコア</summary>
[Serializable]
public class Score
{
    /// <summary>変換する際のフォーマット</summary>
    private const string Format = @"hh\:mm\:ss\.ff";

    /// <summary>スコアテキスト</summary>
    [SerializeField] private string text;

    /// <summary>クリア時間</summary>
    [SerializeField] private float time;

    /// <summary>スコアテキスト</summary>
    public string Text => text;

    /// <summary>クリア時間</summary>
    public float Time => time;

    /// <param name="time">クリア時間</param>
    public Score(float time) => text = TimeSpan.FromSeconds(this.time = time).ToString(Format);//スコアテキストを作成

    public Score() { }
}
