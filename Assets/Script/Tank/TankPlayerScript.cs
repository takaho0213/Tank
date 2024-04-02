using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Tank
{
    /// <summary>�v���C���[�̃^���N</summary>
    public class TankPlayerScript : TankScript
    {
        /// <summary>���͈ړ�</summary>
        [SerializeField] private TankInputMove InputMove;

        /// <summary>���C���J����</summary>
        [SerializeField, LightColor] private Camera MainCamera;

        /// <summary>���X�e�[�W�N���A������c�@�𓾂邩</summary>
        [SerializeField] private int AddLifeCount;

        /// <summary>���C�t(�c�@)</summary>
        public int Life { get; private set; }

        /// <summary>�c�@�𑝂₷�����肷��</summary>
        /// <param name="count">���݃X�e�[�W��</param>
        /// <returns>�c�@�𑝂₷��</returns>
        public bool IsAddLife(int count) => count % AddLifeCount == default;

        /// <summary>�c�@��1���₷</summary>
        public void AddLife()
        {
            Life++;                                          //���C�t�� + 1

            AudioScript.I.StageAudio.Play(StageClip.LifeAdd);//���C�t�ǉ�SE���Đ�
        }

        /// <summary>�c�@��1���炷</summary>
        private void ReMoveLife()
        {
            if (Life > default(int))                                //���C�t��0����Ȃ�
            {
                Life--;                                             //���C�t�� - 1

                AudioScript.I.StageAudio.Play(StageClip.LifeReMove);//���C�t����SE���Đ�
            }
        }

        /// <summary>�̗͂�1��</summary>
        public void HealthRecovery() => Damage.HealthRecovery();//�̗͂�1��

        /// <summary>(�v�[���e, ���g���C, �Q�[���I�[�o�[)�֐����Z�b�g</summary>
        /// <param name="pool">�v�[�����Ă���e���擾����֐�</param>
        /// <param name="onRetry">���g���C�����ێ��s����֐�</param>
        /// <param name="onGameOver">�Q�[���I�[�o�[�ɂȂ����Վ��s����֐�</param>
        public void Init(System.Func<BulletScript> pool, System.Func<bool, IEnumerator> onDeath)
        {
            Weapon.BulletPool = pool;                                            //�v�[�����Ă���e���擾����֐����Z�b�g

            OnDeath = () => StartCoroutine(onDeath.Invoke(Life <= default(int)));//���S�����ێ��s����֐����Z�b�g
        }

        /// <summary>�������܂Ƃ߂��֐�</summary>
        private void Move()
        {
            Parts.TargetLookAt(Input.mousePosition - MainCamera.WorldToScreenPoint(InputMove.Pos));//�}�E�X�J�[�\���̕����փ^���b�g��������

            InputMove.InputMove();                                                                 //���͈ړ�

            if (Input.GetMouseButton(default) && ShootInterval) Weapon.Shoot();                    //�E�N���b�N ���� ���ˊԊu�Ȃ�/���˂���
        }

        /// <summary>���g���C</summary>
        public void Retry()
        {
            Damage.HealthReSet();                 //�̗͂����Z�b�g

            FillColor.SetColor();                 //�F���Z�b�g

            if (Life > default(int)) ReMoveLife();//���C�t��0�ȏ�Ȃ�/���C�t�� - 1
        }

        /// <summary>���X�^�[�g</summary>
        public void ReStart()
        {
            Damage.HealthReSet();                 //�̗͂����Z�b�g

            FillColor.SetColor();                 //�F���Z�b�g

            Life = default;                       //���C�t�����Z�b�g
        }

        private void FixedUpdate()
        {
            Damage.UpdateHealthGauge();                 //�̗̓o�[���X�V

            Parts.CaterpillarLookAt(InputMove.Velocity);//�L���^�s������]

            if (IsNotMove)                              //�ړ��ł��Ȃ���ԂȂ�
            {
                InputMove.Stop();                       //�ړ����~

                return;                                 //�I��
            }

            Move();                                     //�������܂Ƃ߂��֐����Ăяo��
        }
    }

    /// <summary>�^���N�̓��͈ړ�</summary>
    [System.Serializable]
    public class TankInputMove
    {
        /// <summary>InputModule</summary>
        [SerializeField, LightColor] private StandaloneInputModule InputModule;

        /// <summary>�ړ��Ɏg��Rigidbody</summary>
        [SerializeField, LightColor] private Rigidbody2D Ribo;

        /// <summary>�ړ�SE���Đ�����AudioSource</summary>
        [SerializeField, LightColor] private AudioSource MoveSource;

        /// <summary>�ړ����x</summary>
        [SerializeField] private float MoveSpeed;

        /// <summary>SE���Đ�����C���^�[�o��</summary>
        private Interval SEInterval;

        /// <summary>���̓x�N�g��</summary>
        private Vector2 InputVector;

        /// <summary>�ꏊ</summary>
        public Vector2 Pos => Ribo.position;

        /// <summary>�ړ��x�N�g��</summary>
        public Vector3 Velocity => Ribo.velocity;

        /// <summary>�ړ���~</summary>
        public void Stop() => Ribo.velocity = Vector2.zero;

        /// <summary>���͈ړ�</summary>
        public void InputMove()
        {
            InputVector.x = Input.GetAxisRaw(InputModule.horizontalAxis);//���̓x�N�g��X����
            InputVector.y = Input.GetAxisRaw(InputModule.verticalAxis);  //���̓x�N�g��Y����

            Ribo.velocity = InputVector * MoveSpeed;                     //(���̓x�N�g�� * �ړ����x)���ړ��x�N�g���ɑ��

            if (Ribo.velocity != Vector2.zero)                           //�ړ��x�N�g����(0, 0)�ȊO�Ȃ�
            {
                var c = AudioScript.I.TankAudio.Clips[TankClip.Move];    //�ړ�SE�N���b�v

                SEInterval ??= new(c.Clip.length / MoveSpeed, true);     //null�Ȃ���

                if (SEInterval) MoveSource.PlayOneShot(c.Clip, c.Volume);//SE�Đ��Ԋu���z���Ă�����/SE���Đ�
            }
        }
    }
}
