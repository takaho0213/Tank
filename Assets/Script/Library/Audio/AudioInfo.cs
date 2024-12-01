using UnityEngine;

/// <summary>Audioの情報</summary>
[System.Serializable]
public class AudioInfo
{
    /// <summary>再生するAudioSource</summary>
    [field: SerializeField] public AudioSource Source { get; private set; }

    /// <summary>再生するClipInfo</summary>
    [field: SerializeField] public ClipInfo Clip { get; private set; }

    public bool IsPlaying => Source.isPlaying;

    /// <summary>再生</summary>
    public void Play()
    {
        Source.volume = Clip.Volume;
        Source.clip = Clip.Clip;
        Source.Play();
    }

    /// <summary>一回再生</summary>
    public void PlayOneShot() => Source.PlayOneShot(Clip.Clip, Clip.Volume);

    public void Stop() => Source.Stop();
}

/// <summary>Audioの情報Dictionary</summary>
[System.Serializable]
public class AudioInfoDictionary<T> : SerializeDictionary<T, AudioInfo>
{
    /// <summary>再生</summary>
    /// <param name="type">再生するAudioの種類</param>
    public void Play(T type)
    {
        if (TryGetValue(type, out var audio))
        {
            audio.Play();
        }
    }

    /// <summary>一回再生</summary>
    /// <param name="type">再生するAudioの種類</param>
    public void PlayOneShot(T type)
    {
        if (TryGetValue(type, out var audio))
        {
            audio.Play();
        }
    }
}
