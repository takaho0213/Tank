using UnityEngine;

/// <summary>AudioClip�̏��</summary>
[System.Serializable]
public class ClipInfo
{
    /// <summary>AudioClip</summary>
    [field: SerializeField] public AudioClip Clip { get; private set; }

    /// <summary>�{�����[��</summary>
    [SerializeField, Range01] private float volume;

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
public class ClipInfoDictionary<T> : SerializeDictionary<T, ClipInfo>
{
    /// <summary>�Đ�����AudioSource</summary>
    [field: SerializeField] public AudioSource Source { get; private set; }

    /// <summary>Audio���Đ�</summary>
    /// <param name="type">�Đ�����Clip�̎��</param>
    public void Play(T type)
    {
        if (TryGetValue(type, out var clip))
        {
            clip.Play(Source);
        }
    }

    /// <summary>Audio�����Đ�</summary>
    /// <param name="type">�Đ�����Clip�̎��</param>
    public void PlayOneShot(T type)
    {
        if (TryGetValue(type, out var clip))
        {
            clip.PlayOneShot(Source);
        }
    }
}
