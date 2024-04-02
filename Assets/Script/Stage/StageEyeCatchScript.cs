using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tank
{
    /// <summary>�X�e�[�W�A�C�L���b�`</summary>
    public class StageEyeCatchScript : UIFaderScript
    {
        /// <summary>�A�C�L���b�`Inage</summary>
        [SerializeField, LightColor] private Image Image;
        /// <summary>�A�C�L���b�`TMP</summary>
        [SerializeField, LightColor] private TextMeshProUGUI TMP;
        /// <summary>�v���C���[���C�tTMP</summary>
        [SerializeField, LightColor] private TextMeshProUGUI PlayerLifeTMP;

        /// <summary>�X�e�[�W�e�L�X�g�u��</summary>
        [SerializeField] private StringReplace StageTextReplace;
        /// <summary>�v���C���[���C�t�e�L�X�g�u��</summary>
        [SerializeField] private StringReplace PlayerLifeTextReplace;

        /// <summary>�X�e�[�W�e�L�X�g</summary>
        public string StageText { set => TMP.text = StageTextReplace.Replace(value); }

        /// <summary>�v���C���[���C�t�e�L�X�g</summary>
        public string PlayerLifeText { set => PlayerLifeTMP.text = PlayerLifeTextReplace.Replace(value); }

        /// <summary>�\��</summary>
        public void Display() => Image.color = TMP.color = PlayerLifeTMP.color += Color.black;
    }
}