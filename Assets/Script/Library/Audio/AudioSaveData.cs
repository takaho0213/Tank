[System.Serializable]
public class AudioSaveData
{
    [Range01] public float Volume;
    public bool IsMute;

    public AudioSaveData() { }

    public AudioSaveData(float volume, bool isMute)
    {
        Volume = volume;
        IsMute = isMute;
    }
}
