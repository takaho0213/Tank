/// <summary>�����̃X�R�A��Json�`���ŃZ�[�u/���[�h����</summary>
public class ScoreIOScript : GenericJsonIOScript<SerializeList<Score>>
{
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(ScoreIOScript), true)]
    public class ScoreIOScriptEditor : GenericJsonIOScriptEditor { }
#endif
}
