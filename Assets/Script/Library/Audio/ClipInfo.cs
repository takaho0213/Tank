using UnityEngine;

/// <summary>AudioClipの情報</summary>
[System.Serializable]
public class ClipInfo
{
    /// <summary>AudioClip</summary>
    [field: SerializeField] public AudioClip Clip { get; private set; }

    /// <summary>ボリューム</summary>
    [SerializeField, Range01] private float volume;

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
public class ClipInfoDictionary<T> : SerializeDictionary<T, ClipInfo>
{
    /// <summary>再生するAudioSource</summary>
    [field: SerializeField] public AudioSource Source { get; private set; }

    /// <summary>Audioを再生</summary>
    /// <param name="type">再生するClipの種類</param>
    public void Play(T type)
    {
        if (TryGetValue(type, out var clip))
        {
            clip.Play(Source);
        }
    }

    /// <summary>Audioを一回再生</summary>
    /// <param name="type">再生するClipの種類</param>
    public void PlayOneShot(T type)
    {
        if (TryGetValue(type, out var clip))
        {
            clip.PlayOneShot(Source);
        }
    }
}
