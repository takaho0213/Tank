using System;
using UnityEngine;

/// <summary>�N���A�^�C���̃X�R�A</summary>
[Serializable]
public class Score
{
    /// <summary>�ϊ�����ۂ̃t�H�[�}�b�g</summary>
    private const string Format = @"hh\:mm\:ss\.ff";

    /// <summary>�X�R�A�e�L�X�g</summary>
    [SerializeField] private string text;

    /// <summary>�N���A����</summary>
    [SerializeField] private float time;

    /// <summary>�X�R�A�e�L�X�g</summary>
    public string Text => text;

    /// <summary>�N���A����</summary>
    public float Time => time;

    /// <param name="time">�N���A����</param>
    public Score(float time) => text = TimeSpan.FromSeconds(this.time = time).ToString(Format);//�X�R�A�e�L�X�g���쐬

    public Score() { }
}
