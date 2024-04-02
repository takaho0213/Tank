using UnityEngine;

namespace Tank
{
    /// <summary>�^�C�g��</summary>
    public class TitleScript : ImageFaderScript
    {
        /// <summary>�X�e�[�W�}�l�[�W���[</summary>
        [SerializeField, LightColor] private StageManagerScript StageManager;

        /// <summary>�^�C�g���I�u�W�F�N�g</summary>
        [SerializeField, LightColor] private GameObject TitleObj;

        private void Start()
        {
            ReStart();                         //���X�^�[�g

            StageManager.OnGameClear = ReStart;//�Q�[���N���A�����s����֐�����
        }

        /// <summary>���X�^�[�g�����s</summary>
        private void ReStart()
        {
            Run(() => TitleObj.SetActive(true), () => AudioScript.I.StageAudio.Play(StageClip.BGM));//�t�F�[�h���J�n �t�F�[�h�C�����X�e�[�W���A�N�e�B�u �t�F�[�h�A�E�g��BGM���Đ�
        }

        /// <summary>�t�F�[�h�C�������s����֐�</summary>
        private void OnFadeIn()
        {
            TitleObj.SetActive(false);                                   //�^�C�g�����A�N�e�B�u

            AudioScript.I.StageAudio.Audios[StageClip.BGM].Source.Stop();//BGM���~

            StageManager.OnReStartFadeIn();                              //���X�^�[�g�̃t�F�[�h�A�E�g�����s����֐������s
        }

        /// <summary>�Q�[�����J�n</summary>
        private void GameStart()
        {
            if (IsRun) return;                           //�t�F�[�h���Ȃ�/�I��
    
            Run(OnFadeIn, StageManager.OnReStartFadeOut);//�t�F�[�h�J�n �t�F�[�h�C����OnFadeIn�֐������s �t�F�[�h�A�E�g��OnReStartFadeIn�֐������s
        }

        protected override void Update()
        {
            base.Update();                                           //���N���X��Update����т���

            if (Input.GetMouseButton(default) && TitleObj.activeSelf)//�E�N���b�N ���� �^�C�g���I�u�W�F�N�g���A�N�e�B�u�Ȃ�
            {
                GameStart();                                         //�E�N���b�N�������ꂽ��/�Q�[�����J�n
            }
        }
    }
}