/// <summary>複数のスコアをJson形式でセーブ/ロードする</summary>
public class ScoreIOScript : GenericJsonIOScript<SerializeList<Score>>
{
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(ScoreIOScript), true)]
    public class ScoreIOScriptEditor : GenericJsonIOScriptEditor { }
#endif
}
