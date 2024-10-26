using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]
public class AudioMixerGroupInfo
{
    [SerializeField] private AudioMixerGroup Group;

    [SerializeField] private Slider VolumeSlider;

    [SerializeField] private Toggle MuteToggle;

    private AudioMixer Mixer;

    private string Name;

    private const float MinDecibel = -80f;

    public void Init(AudioSaveData data)
    {
        VolumeSlider.value = data.Volume;

        MuteToggle.isOn = data.IsMute;

        Mixer = Group.audioMixer;

        Name = Group.name;

        VolumeSlider.onValueChanged.AddListener(OnSliderValueChanged);

        MuteToggle.onValueChanged.AddListener(OnToggleValueChanged);

        OnToggleValueChanged(MuteToggle.isOn);

        OnSliderValueChanged(VolumeSlider.value);
    }

    private void SetVolume(float value)
    {
        Mixer.SetFloat(Name, Mathf.Lerp(MinDecibel, default, value));
    }

    private void OnSliderValueChanged(float value)
    {
        SetVolume(value);

        if (MuteToggle.isOn) MuteToggle.isOn = false;
    }

    private void OnToggleValueChanged(bool value)
    {
        if (value)
        {
            VolumeSlider.value = default;

            MuteToggle.isOn = value;
        }
    }

    public void Save(AudioSaveData data)
    {
        data.Volume = VolumeSlider.value;
        data.IsMute = MuteToggle.isOn;
    }
}
