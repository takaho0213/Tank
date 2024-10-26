using UnityEngine;

[System.Serializable]
public class AudioMixerInfoScript : MonoBehaviour
{
    [SerializeField] private AudioIOScript AudioIO;

    [SerializeField] protected KeyValue<string, AudioMixerGroupInfo>[] Groups;

    private void Awake()
    {
        foreach (var item in Groups)
        {
            AudioIO.Value.TryGetValue(item.Key, out var data);

            item.Value.Init(data ??= new());
        }
    }

    private void OnDisable()
    {
        foreach (var item in Groups)
        {
            AudioIO.Value.TryGetValue(item.Key, out var data);

            item.Value.Save(data ??= new());
        }
    }
}
