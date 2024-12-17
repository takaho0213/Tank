/// <summary>複数のスコアをJson形式でセーブ/ロードする</summary>
public class CrearTimeIOScript : GenericJsonIOScript<SerializeList<float>>
{
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(CrearTimeIOScript), true)]
    public class CrearTimeIOScriptEditor : GenericJsonIOScriptEditor { }
#endif
}
