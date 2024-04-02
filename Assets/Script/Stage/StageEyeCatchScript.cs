using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tank
{
    /// <summary>ステージアイキャッチ</summary>
    public class StageEyeCatchScript : UIFaderScript
    {
        /// <summary>アイキャッチInage</summary>
        [SerializeField, LightColor] private Image Image;
        /// <summary>アイキャッチTMP</summary>
        [SerializeField, LightColor] private TextMeshProUGUI TMP;
        /// <summary>プレイヤーライフTMP</summary>
        [SerializeField, LightColor] private TextMeshProUGUI PlayerLifeTMP;

        /// <summary>ステージテキスト置換</summary>
        [SerializeField] private StringReplace StageTextReplace;
        /// <summary>プレイヤーライフテキスト置換</summary>
        [SerializeField] private StringReplace PlayerLifeTextReplace;

        /// <summary>ステージテキスト</summary>
        public string StageText { set => TMP.text = StageTextReplace.Replace(value); }

        /// <summary>プレイヤーライフテキスト</summary>
        public string PlayerLifeText { set => PlayerLifeTMP.text = PlayerLifeTextReplace.Replace(value); }

        /// <summary>表示</summary>
        public void Display() => Image.color = TMP.color = PlayerLifeTMP.color += Color.black;
    }
}