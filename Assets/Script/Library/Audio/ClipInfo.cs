using UnityEngine;
using System.Collections.Generic;

/// <summary>AudioClip�̏��</summary>
[System.Serializable]
public class ClipInfo
{
    /// <summary>�ŏ��{�����[��</summary>
    public const float MinVolume = 0.0f;
    /// <summary>�ő�{�����[��</summary>
    public const float MaxVolume = 1.0f;

    /// <summary>AudioClip</summary>
    [field: SerializeField] public AudioClip Clip { get; private set; }

    /// <summary>�{�����[��</summary>
    [SerializeField, Range(MinVolume, MaxVolume)] private float volume;

    /// <summary>�{�����[��</summary>
    public float Volume { get => volume; set => volume = Mathf.Clamp01(value); }

    /// <summary>�Đ�</summary>
    /// <param name="s">�Đ�����AudioSource</param>
    public void Play(AudioSource s)
    {
        s.volume = volume;
        s.clip = Clip;
        s.Play();
    }

    /// <summary>���Đ�</summary>
    /// <param name="s">�Đ�����AudioSource</param>
    public void PlayOneShot(AudioSource s)
    {
        s.PlayOneShot(Clip, volume);
    }
}

/// <summary>AudioClip�̏��Dictionary</summary>
[System.Serializable]
public class ClipInfo<T>
{
    /// <summary>�Đ�����AudioSource</summary>
    [field: SerializeField] public AudioSource Source { get; private set; }

    /// <summary>ClipInfo�y�A�z��</summary>
    [SerializeField] private KeyValue<T, ClipInfo>[] clips;

    /// <summary>ClipInfo�y�ADictionary</summary>
    private Dictionary<T, ClipInfo> dic;

    /// <summary>ClipInfo�y�A�ǂݎ���pDictionary</summary>
    public IReadOnlyDictionary<T, ClipInfo> Clips => dic ??= clips.ToDictionary();

    /// <summary>Audio���Đ�</summary>
    /// <param name="type">�Đ�����Clip�̎��</param>
    public void Play(T type)
    {
        if (Clips.TryGetValue(type, out var clip))
        {
            clip.Play(Source);
        }
    }

    /// <summary>Audio�����Đ�</summary>
    /// <param name="type">�Đ�����Clip�̎��</param>
    public void PlayOneShot(T type)
    {
        if (Clips.TryGetValue(type, out var clip))
        {
            clip.PlayOneShot(Source);
        }
    }
}
