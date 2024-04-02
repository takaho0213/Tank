using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Tank
{
    /// <summary>�X�e�[�W�̊Ǘ�</summary>
    public class StageManagerScript : MonoBehaviour
    {
        /// <summary>�X�e�[�W�I��UI</summary>
        [SerializeField, LightColor] private StageEndUIScript StageEndUI;

        /// <summary>�X�R�AUI</summary>
        [SerializeField, LightColor] private ScoreUIScript ScoreUI;

        /// <summary>�A�C�L���b�`</summary>
        [SerializeField, LightColor] private StageEyeCatchScript EyeCatch;

        /// <summary>�v���C���[�^���N</summary>
        [SerializeField, LightColor] private TankPlayerScript Player;

        /// <summary>�G�l�~�[�̃v�[�����X�g</summary>
        [SerializeField] private PoolList<TankEnemyScript> EnemyList;

        /// <summary>�e�̃v�[�����X�g</summary>
        [SerializeField] private PoolList<BulletScript> BulletList;

        /// <summary>�S�X�e�[�W</summary>
        [SerializeField] private StageScript[] Stages;

        /// <summary>�X�e�[�W�J�n���̑ҋ@����</summary>
        [SerializeField] private float StartWaitTime;

        /// <summary>�t���[�����[�g</summary>
        [SerializeField, ReadOnly] private int FrameRate;

        /// <summary>���݂̃X�e�[�W</summary>
        private StageScript CurrentStage;

        /// <summary>�X�e�[�W�J�n���̑ҋ@����</summary>
        private Interval StageStartWaitInterval;

        /// <summary>�O�X�e�[�W�N���A�^�C���v���p</summary>
        private Interval AllStageClearTime;

        /// <summary>�I������</summary>
        private System.Func<bool> IsBreak;

        /// <summary>���݂̃X�e�[�W��</summary>
        private int StageNumber;

        /// <summary>�^���N�������Ȃ���Ԃ�</summary>
        private bool isNotMove;

        /// <summary>�X�e�[�W��Audio</summary>
        private AudioInfo<StageClip> Audio => AudioScript.I.StageAudio;

        /// <summary>�^���N�������Ȃ���Ԃ�</summary>
        public bool IsNotMove => EyeCatch.IsRun || !StageStartWaitInterval.IsOver || isNotMove;

        /// <summary>�Q�[���N���A�����s</summary>
        public UnityAction OnGameClear { get; set; }

        private void Start()
        {
            Application.targetFrameRate = FrameRate;                        //�t���[�����[�g���Œ�

            IsBreak = () => !Audio.Audios[StageClip.Clear].Source.isPlaying;//�I����������

            AllStageClearTime = new Interval();                             //Interval���C���X�^���X��

            StageStartWaitInterval = new(Time : StartWaitTime);             //Interval���C���X�^���X��

            Player.Init(BulletList.Pool, PlayerDeath);                      //�v���C���[�̊֐����Z�b�g

            CurrentStage = Stages[default];                                 //���݂̃X�e�[�W����
        }

        /// <summary>���X�^�[�g�̃t�F�[�h�C�������s</summary>
        public void OnReStartFadeIn()
        {
            AllStageClearTime.ReSet();//�X�R�A�����Z�b�g

            EyeCatch.Display();       //�A�C�L���b�`��\��

            Player.ReStart();         //���X�^�[�g

            StageReSet();             //�X�e�[�W���Z�b�g

            StageNumber = default;    //���݂̃X�e�[�W����0�ɂ���
        }

        /// <summary>���X�^�[�g�̃t�F�[�h�A�E�g�����s</summary>
        public void OnReStartFadeOut() => EyeCatchFade(Generate);//�A�C�L���b�`���t�F�[�h���A�t�F�[�h�C����Generate�֐������s

        /// <summary>�v���C���[���S���Ɏ��s</summary>
        /// <param name="isNoLife">���C�t���c���Ă��Ȃ���</param>
        private IEnumerator PlayerDeath(bool isNoLife)
        {
            if (isNotMove) yield break;                                          //�������Ȃ���ԂȂ�/�I��

            var audio = AudioScript.I.StageAudio.Audios[StageClip.PlayerDeath];  //�v���C���[���SAudio

            audio.Play();                                                        //SE���Đ�

            isNotMove = true;                                                    //�����Ȃ���Ԃ���true

            var type = isNoLife ? UI.GameOver : UI.Retry;                        //�\��UI

            yield return StageEndUI.Display(type, () => !audio.Source.isPlaying);//UI��\��

            EyeCatchFade(isNoLife ? PlayerGameOver : PlayerRetry);               //�A�C�L���b�`���t�F�[�h���A�t�F�[�h�C����(���C�t��������΃Q�[���I�[�o�[ : ����΃��g���C)�֐������s

            isNotMove = false;                                                   //�����Ȃ���Ԃ���false
        }

        /// <summary>Player�����g���C�����ێ��s</summary>
        private void PlayerRetry()
        {
            Player.Retry();//�v���C���[�����g���C

            StageReSet();  //���݂̃X�e�[�W�����Z�b�g

            Generate();    //���݂̃X�e�[�W�𐶐�
        }

        /// <summary>Player���Q�[���I�[�o�[�����ێ��s</summary>
        private void PlayerGameOver()
        {
            StageNumber = default;    //���݂̃X�e�[�W����0�ɂ���

            PlayerRetry();            //�v���C���[�����g���C
        }

        /// <summary>�A�C�L���b�`���t�F�[�h����֐�</summary>
        /// <param name="c">�t�F�[�h�C�������s����֐�</param>
        private void EyeCatchFade(UnityAction c)
        {
            Audio.Audios[StageClip.BGM].Source.Stop();         //BGM���~

            c += () => Audio.Audios[StageClip.EyeCatch].Play();//�A�C�L���b�`SE���Đ�����֐������Z���

            EyeCatch.Run(c, StageStart);                       //�t�F�[�h�J�n �t�F�[�h�C�������ۈ����̊֐������s �t�F�[�h�A�E�g������StageStart�֐������s
        }

        /// <summary>�X�e�[�W���J�n�����ێ��s</summary>
        private void StageStart()
        {
            StageStartWaitInterval.ReSet();    //�C���^�[�o�������Z�b�g

            Audio.Audios[StageClip.BGM].Play();//BGM���Đ�

            isNotMove = false;                 //�����Ȃ���Ԃ���false
        }

        /// <summary>���̃X�e�[�W�ֈڍs</summary>
        private void NextStage()
        {
            if (Player.IsAddLife(++StageNumber))//���C�t�𑝂₷�X�e�[�W���Ȃ�
            {
                Player.AddLife();               //�v���C���[�̃��C�t�𑝂₷
            }

            Player.HealthRecovery();            //�̗͂���

            StageReSet();                       //���݂̃X�e�[�W�����Z�b�g

            Generate();                         //���݂̃X�e�[�W�𐶐�
        }

        /// <summary>Enemy�����ׂē|�����ێ��s</summary>
        private IEnumerator OnAllEnemysDeath()
        {
            if (isNotMove) yield break;                                     //�����Ȃ���ԂȂ�/�I��

            isNotMove = true;                                               //�����Ȃ���Ԃ���true

            Audio.Audios[StageClip.BGM].Source.Stop();                      //BGM���~

            int number = StageNumber + 1;                                   //�\���X�e�[�W��

            bool isAllClear = number >= Stages.Length;                      //���݂̃X�e�[�W�� + 1���S�X�e�[�W���ȏ�Ȃ�

            if (isAllClear) Audio.Play(StageClip.AllClear);                 //�I�[���N���ASE���Đ�
            else Audio.Play(StageClip.Clear);                               //�N���ASE���Đ�

            var type = isAllClear ? UI.AllClear : UI.Clear;                 //�\��UI

            yield return StageEndUI.Display(type, IsBreak);                 //UI��\��

            if (isAllClear)                                                 //�S�N���Ȃ�
            {
                yield return ScoreUI.Display(AllStageClearTime.ElapsedTime);//�X�R�AUI��\��

                OnGameClear.Invoke();                                       //�Q�[���N���A�����s����֐������s
            }
            else EyeCatchFade(NextStage);                                   //�A�C�L���b�`���t�F�[�h���A�t�F�[�h�C����NextStage�֐������s
        }

        /// <summary>���݂̃X�e�[�W�𐶐�</summary>
        private void Generate()
        {
            CurrentStage = Stages[StageNumber];                                              //���݂̃X�e�[�W����

            CurrentStage.Generate(EnemyList.Pool, BulletList.Pool, Player, OnAllEnemysDeath);//���݂̃X�e�[�W�𐶐�

            EyeCatch.StageText = (StageNumber + 1).ToString();                               //�X�e�[�W�e�L�X�g����

            EyeCatch.PlayerLifeText = Player.Life.ToString();                                //�v���C���[�̃��C�t�e�L�X�g����
        }

        /// <summary>���݂̃X�e�[�W�����Z�b�g</summary>
        private void StageReSet()
        {
            Player.IsActive = false;                        //�v���C���[���A�N�e�B�u

            CurrentStage.Clear(EnemyList.List);             //���݂̃X�e�[�W���N���A

            foreach (var b in BulletList.List) b.Inactive();//�e�̃v�[�����X�g���J��Ԃ�/�e���A�N�e�B�u
        }
    }
}
