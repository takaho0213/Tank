using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>�X�R�AUI</summary>
public class ScoreUIScript : MonoBehaviour
{
    /// <summary>�ϊ�����ۂ̃t�H�[�}�b�g</summary>
    private const string ScoreFormat = @"hh\:mm\:ss\.ff";

    /// <summary>�X�R�A�̃Z�[�u</summary>
    [SerializeField, LightColor] private CrearTimeIOScript scoreIO;

    /// <summary>�X�R�A�C���[�W</summary>
    [SerializeField, LightColor] private Image scoreImage;

    /// <summary>�X�R�ATMP</summary>
    [SerializeField, LightColor] private TextMeshProUGUI scoreTMP;

    /// <summary>�X�R�A�e�L�X�g</summary>
    [SerializeField] private StringReplace2 scoreTextR2;
    /// <summary>�S�N���e�L�X�g</summary>
    [SerializeField] private StringReplace2 allClearTextR2;

    /// <summary>�ڕW�̐F</summary>
    [SerializeField] private Color scoreImageColor;

    /// <summary>�X�R�A�C���[�W�̕�Ԓl</summary>
    [SerializeField, Range01] private float scoreImageLerp;

    /// <summary>�X�R�A�e�L�X�g���Z�C���^�[�o��</summary>
    [SerializeField] private float scoreTextInterval;

    /// <summary>�\������X�R�A��</summary>
    [SerializeField] private int displayScoreCount;

    /// <summary>�����񌋍�</summary>
    private StringUnion scoreUnion;

    /// <summary>SE�ҋ@</summary>
    private WaitForSeconds waitScoreSE;

    /// <summary>�X�R�A���X�g</summary>
    private SerializeList<float> scoreList;

    /// <summary>�X�R�A�e�L�X�g</summary>
    public static string ToScoreText(float time) => System.TimeSpan.FromSeconds(time).ToString(ScoreFormat);

    public void Start()
    {
        scoreUnion ??= new(scoreTextInterval);                   //�C���X�^���X���쐬

        scoreList = scoreIO.Load<SerializeList<float>>();//�X�R�A���X�g
    }

    /// <summary>�X�R�A���X�g���\�[�g���Z�[�u</summary>
    /// <param name="current">���݂̃X�R�A</param>
    public void ScoreListSort(float current)
    {
        scoreList.Add(current);                         //�X�R�A��ǉ�

        scoreList.Sort();                               //�X�R�A���X�g���\�[�g

        int count = scoreList.Count - displayScoreCount;//�폜��

        for (int i = default; i < count; i++)               //�폜�����J��Ԃ�
        {
            scoreList.Remove(scoreList.Last());     //�Ō�̗v�f���폜
        }

        scoreIO.Save(scoreList, true);              //�X�R�A���Z�[�u
    }

    /// <summary>�X�R�A�e�L�X�g�𐶐�</summary>
    /// <param name="currentText">���݂̃X�R�A�e�L�X�g</param>
    /// <returns>�\���e�L�X�g</returns>
    private string ScoreText(string currentText)
    {
        string text = string.Empty;                                       //�\���e�L�X�g

        string number = default;                                          //���ʃe�L�X�g
        string time = default;                                            //�^�C���e�L�X�g

        for (int i = default; i < scoreList.Count; i++)               //�X�R�A���X�g���J��Ԃ�
        {
            number = (i + 1).ToString();                                  //���ʃe�L�X�g����
            time = ToScoreText(scoreList[i]);                     //�^�C���e�L�X�g����

            text += scoreTextR2.Replace2(number, time) + StringEx.NewLine;//�\���e�L�X�g�����Z���
        }

        return allClearTextR2.Replace2(currentText, text);                //�\���e�L�X�g��u��
    }

    /// <summary>�X�R�A��\��</summary>
    /// <param name="text">�\���e�L�X�g</param>
    /// <returns>�������S�Č������I������I��</returns>
    private IEnumerator ScoreDisplay(string text)
    {
        do                                                        //�Œ���͏����������̂�do while���g�p
        {
            scoreImage.LerpColor(scoreImageColor, scoreImageLerp);//��Ԓl����

            scoreTMP.text = scoreUnion.Union(text);               //�����������

            if (scoreUnion.IsAll) yield break;                    //�S�Č������I������I��

            yield return null;                                    //1�t���[����~
        }
        while (!scoreUnion.IsAll);                                //�����񂪂��ׂČ�������Ă��Ȃ�����J��Ԃ�
    }

    /// <summary>UI�����Z�b�g</summary>
    /// <returns>SE���I��莟��I��</returns>
    private IEnumerator ReSetUI()
    {
        var audio = AudioScript.I.StageAudio[StageClip.Score];//�X�R�A�I�[�f�B�I

        audio.Play();                                         //SE���Đ�

        yield return waitScoreSE ??= new(audio.Clip.Length);  //�ҋ@

        scoreImage.color = Color.clear;                       //�F���N���A�ɂ���

        scoreTMP.text = string.Empty;                         //TMP�̃e�L�X�g��Empty�ɂ���
    }

    /// <summary>�X�R�AUI��\��</summary>
    /// <param name="time">�N���A�^�C��</param>
    /// <returns>�X�R�A��\�����I���ASE�̍Đ����I��������I��</returns>
    public IEnumerator Display(float time)
    {
        ScoreListSort(time);                        //�X�R�A���X�g���\�[�g�����݂̃X�R�A���擾

        var text = ScoreText(ToScoreText(time));//�X�R�A�e�L�X�g���擾

        yield return ScoreDisplay(text);            //�X�R�A��\��

        yield return ReSetUI();                     //UI�����Z�b�g
    }
}
