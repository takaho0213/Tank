using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ステージアイキャッチのUI</summary>
public class StageEyeCatchUIScript : MonoBehaviour
{
    /// <summary>フェーダー</summary>
    [SerializeField, LightColor] private GraphicsFaderScript fader;

    /// <summary>アイキャッチInage</summary>
    [SerializeField, LightColor] private Image image;
    /// <summary>アイキャッチTMP</summary>
    [SerializeField, LightColor] private TextMeshProUGUI stageTMP;
    /// <summary>プレイヤーライフTMP</summary>
    [SerializeField, LightColor] private TextMeshProUGUI playerLifeTMP;

    /// <summary>ステージテキスト置換</summary>
    [SerializeField] private StringReplace stageTextReplace;
    /// <summary>プレイヤーライフテキスト置換</summary>
    [SerializeField] private StringReplace playerLifeTextReplace;

    /// <summary>フェーダー</summary>
    public GraphicsFaderScript Fader => fader;

    /// <summary>表示</summary>
    public void Display() => fader.FadeValue = Color.black.a;

    /// <summary>ステージ数,ライフ数を表示するテキストをセット</summary>
    /// <param name="stage">ステージ数</param>
    /// <param name="life">ライフ数</param>
    public void SetText(int stage, int life)
    {
        stageTMP.text = stageTextReplace.Replace(stage.ToString());         //ステージテキストを代入

        playerLifeTMP.text = playerLifeTextReplace.Replace(life.ToString());//プレイヤーライフテキストを代入
    }

    /// <summary>ステージテキストをセット</summary>
    /// <param name="count">ステージテキスト</param>
    public void SetStageCount(string count) => stageTMP.text = stageTextReplace.Replace(count);
}
