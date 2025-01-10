using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�X�e�[�W�A�C�L���b�`��UI</summary>
public class StageEyeCatchUIScript : MonoBehaviour
{
    /// <summary>�t�F�[�_�[</summary>
    [SerializeField, LightColor] private GraphicsFaderScript fader;

    /// <summary>�A�C�L���b�`Inage</summary>
    [SerializeField, LightColor] private Image image;
    /// <summary>�A�C�L���b�`TMP</summary>
    [SerializeField, LightColor] private TextMeshProUGUI stageTMP;
    /// <summary>�v���C���[���C�tTMP</summary>
    [SerializeField, LightColor] private TextMeshProUGUI playerLifeTMP;

    /// <summary>�X�e�[�W�e�L�X�g�u��</summary>
    [SerializeField] private StringReplace stageTextReplace;
    /// <summary>�v���C���[���C�t�e�L�X�g�u��</summary>
    [SerializeField] private StringReplace playerLifeTextReplace;

    /// <summary>�t�F�[�_�[</summary>
    public GraphicsFaderScript Fader => fader;

    /// <summary>�\��</summary>
    public void Display() => fader.FadeValue = Color.black.a;

    /// <summary>�X�e�[�W��,���C�t����\������e�L�X�g���Z�b�g</summary>
    /// <param name="stage">�X�e�[�W��</param>
    /// <param name="life">���C�t��</param>
    public void SetText(int stage, int life)
    {
        stageTMP.text = stageTextReplace.Replace(stage.ToString());         //�X�e�[�W�e�L�X�g����

        playerLifeTMP.text = playerLifeTextReplace.Replace(life.ToString());//�v���C���[���C�t�e�L�X�g����
    }

    /// <summary>�X�e�[�W�e�L�X�g���Z�b�g</summary>
    /// <param name="count">�X�e�[�W�e�L�X�g</param>
    public void SetStageCount(string count) => stageTMP.text = stageTextReplace.Replace(count);
}
