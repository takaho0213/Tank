/// <summary>�����̃X�R�A��Json�`���ŃZ�[�u/���[�h����</summary>
public class CrearTimeIOScript : GenericJsonIOScript<SerializeList<float>>
{
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(CrearTimeIOScript), true)]
    public class CrearTimeIOScriptEditor : GenericJsonIOScriptEditor { }
#endif
}
