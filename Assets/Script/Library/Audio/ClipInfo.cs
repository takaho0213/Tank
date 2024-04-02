using UnityEngine;
using System.Collections.Generic;

/// <summary>AudioClipの情報</summary>
[System.Serializable]
public class ClipInfo
{
    /// <summary>最少ボリューム</summary>
    public const float MinVolume = 0.0f;
    /// <summary>最大ボリューム</summary>
    public const float MaxVolume = 1.0f;

    /// <summary>AudioClip</summary>
    [field: SerializeField] public AudioClip Clip { get; private set; }

    /// <summary>ボリューム</summary>
    [SerializeField, Range(MinVolume, MaxVolume)] private float volume;

    /// <summary>ボリューム</summary>
    public float Volume { get => volume; set => volume = Mathf.Clamp01(value); }

    /// <summary>再生</summary>
    /// <param name="s">再生するAudioSource</param>
    public void Play(AudioSource s)
    {
        s.volume = volume;
        s.clip = Clip;
        s.Play();
    }

    /// <summary>一回再生</summary>
    /// <param name="s">再生するAudioSource</param>
    public void PlayOneShot(AudioSource s)
    {
        s.PlayOneShot(Clip, volume);
    }
}

/// <summary>AudioClipの情報Dictionary</summary>
[System.Serializable]
public class ClipInfo<T>
{
    /// <summary>再生するAudioSource</summary>
    [field: SerializeField] public AudioSource Source { get; private set; }

    /// <summary>ClipInfoペア配列</summary>
    [SerializeField] private KeyValue<T, ClipInfo>[] clips;

    /// <summary>ClipInfoペアDictionary</summary>
    private Dictionary<T, ClipInfo> dic;

    /// <summary>ClipInfoペア読み取り専用Dictionary</summary>
    public IReadOnlyDictionary<T, ClipInfo> Clips => dic ??= clips.ToDictionary();

    /// <summary>Audioを再生</summary>
    /// <param name="type">再生するClipの種類</param>
    public void Play(T type)
    {
        if (Clips.TryGetValue(type, out var clip))
        {
            clip.Play(Source);
        }
    }

    /// <summary>Audioを一回再生</summary>
    /// <param name="type">再生するClipの種類</param>
    public void PlayOneShot(T type)
    {
        if (Clips.TryGetValue(type, out var clip))
        {
            clip.PlayOneShot(Source);
        }
    }
}
