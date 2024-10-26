using UnityEditor;
using UnityEngine;

public class ScoreIOScript : GenericJsonIOScript<SerializeList<Score>>
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ScoreIOScript), true)]
    public class ScoreIOScriptEditor : GenericJsonIOScriptEditor { }
#endif
}
