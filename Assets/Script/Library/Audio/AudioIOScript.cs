#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioIOScript : GenericJsonIOScript<SerializeDictionary<string, AudioSaveData>>
{
#if UNITY_EDITOR
    [CustomEditor(typeof(AudioIOScript))]
    public class AudioIOScriptEditor : GenericJsonIOScriptEditor { }
#endif
}
