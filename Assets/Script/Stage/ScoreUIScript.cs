using TMPro;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Tank
{
    /// <summary>�X�R�AUI</summary>
    public class ScoreUIScript : MonoBehaviour
    {
        /// <summary>�X�R�A�̃Z�[�u</summary>
        [SerializeField, LightColor] private TextIOScript ClearTimeIO;

        /// <summary>�X�R�A�C���[�W</summary>
        [SerializeField, LightColor] private Image ScoreImage;

        /// <summary>�X�R�ATMP</summary>
        [SerializeField, LightColor] private TextMeshProUGUI ScoreTMP;

        /// <summary>�X�R�A�e�L�X�g</summary>
        [SerializeField] private StringReplace2 ScoreTextR2;
        /// <summary>�S�N���e�L�X�g</summary>
        [SerializeField] private StringReplace2 AllClearTextR2;

        /// <summary>�ڕW�̐F</summary>
        [SerializeField] private Color ScoreImageColor;

        /// <summary>�X�R�A�C���[�W�̕�Ԓl</summary>
        [SerializeField, Range01] private float ScoreImageLerp;

        /// <summary>�X�R�A�e�L�X�g���Z�C���^�[�o��</summary>
        [SerializeField] private float ScoreTextInterval;

        /// <summary>�\������X�R�A��</summary>
        [SerializeField] private int DisplayScoreCount;

        /// <summary>�����񌋍�</summary>
        private StringUnion ScoreUnion;

        /// <summary>SE�ҋ@</summary>
        private WaitForSeconds WaitScoreSE;

        /// <summary>�X�R�A���X�g</summary>
        private SerializList<Score> ScoreList;

        public void Start()
        {
            ScoreUnion ??= new(ScoreTextInterval);           //�C���X�^���X���쐬

            ScoreList = ClearTimeIO.Load<SerializList<Score>>();//�X�R�A���X�g
        }

        /// <summary>�X�R�A���X�g���\�[�g���Z�[�u</summary>
        /// <param name="current">���݂̃X�R�A</param>
        public void ScoreListSort(Score current)
        {
            ScoreList.Add(current);                            //�X�R�A��ǉ�

            ScoreList.Sort((a, b) => a.Time.CompareTo(b.Time));//�X�R�A���X�g���\�[�g

            int count = ScoreList.Count - DisplayScoreCount;   //�폜��

            for (int i = default; i < count; i++)              //�폜�����J��Ԃ�
            {
                ScoreList.Remove(ScoreList.Last());            //�Ō�̗v�f���폜
            }

            ClearTimeIO.Save(ScoreList, true);                      //�X�R�A���Z�[�u
        }

        /// <summary>�X�R�A�e�L�X�g�𐶐�</summary>
        /// <param name="currentText">���݂̃X�R�A�e�L�X�g</param>
        /// <returns>�\���e�L�X�g</returns>
        private string ScoreText(string currentText)
        {
            string text = string.Empty;                                             //�\���e�L�X�g

            for (int i = default; i < ScoreList.Count; i++)                         //�X�R�A���X�g���J��Ԃ�
            {
                text += ScoreTextR2.Replace2((i + 1).ToString(), ScoreList[i].Text);//�\���e�L�X�g�����Z���
            }

            return AllClearTextR2.Replace2(currentText, text);                      //�\���e�L�X�g��u��
        }

        /// <summary>�X�R�A��\��</summary>
        /// <param name="text">�\���e�L�X�g</param>
        /// <returns>�������S�Č������I������I��</returns>
        private IEnumerator ScoreDisplay(string text)
        {
            do                                                   //�Œ���͏����������̂�do while���g�p
            {
                ScoreImage.Lerp(ScoreImageColor, ScoreImageLerp);//��Ԓl����

                ScoreTMP.text = ScoreUnion.Union(text);          //�����������

                if (ScoreUnion.IsAll) yield break;               //�S�Č������I������I��

                yield return null;                               //1�t���[����~
            }
            while (!ScoreUnion.IsAll);                           //�����񂪂��ׂČ�������Ă��Ȃ�����J��Ԃ�
        }

        /// <summary>UI�����Z�b�g</summary>
        /// <returns>SE���I��莟��I��</returns>
        private IEnumerator UIReSet()
        {
            var audio = AudioScript.I.StageAudio.Audios[StageClip.Score];//�X�R�A�I�[�f�B�I

            audio.Play();                                                //SE���Đ�

            yield return WaitScoreSE ??= new(audio.Clip.Clip.length);    //�ҋ@

            ScoreImage.color = Color.clear;                              //�F���N���A�ɂ���

            ScoreTMP.text = string.Empty;                                //TMP�̃e�L�X�g��Empty�ɂ���
        }

        /// <summary>�X�R�AUI��\��</summary>
        /// <param name="time">�N���A�^�C��</param>
        /// <returns>�X�R�A��\�����I���ASE�̍Đ����I��������I��</returns>
        public IEnumerator Display(float time)
        {
            var current = new Score(time);     //���݂̃X�R�A

            ScoreListSort(current);            //�X�R�A���X�g���\�[�g�����݂̃X�R�A���擾

            var text = ScoreText(current.Text);//�X�R�A�e�L�X�g���擾

            yield return ScoreDisplay(text);   //�X�R�A��\��

            yield return UIReSet();            //UI�����Z�b�g
        }
    }

    [System.Serializable]
    public class Score
    {
        /// <summary>�ϊ�����ۂ̃t�H�[�}�b�g</summary>
        private const string Format = @"hh\:mm\:ss\.ff";

        /// <summary>�X�R�A�e�L�X�g</summary>
        public string Text;

        /// <summary>�N���A����</summary>
        public float Time;

        /// <param name="time">�N���A����</param>
        public Score(float time) => Text = TimeSpan.FromSeconds(Time = time).ToString(Format);//�X�R�A�e�L�X�g���쐬
    }
}