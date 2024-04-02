using System;
using System.Linq;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Tank
{
    /// <summary>�x�[�X�̃^���N</summary>
    public abstract class TankScript : MonoBehaviour
    {
        /// <summary>�}�l�[�W���[</summary>
        [SerializeField, LightColor] protected StageManagerScript StageManager;

        /// <summary>���g��Object</summary>
        [SerializeField, LightColor] protected GameObject Obj;

        /// <summary>���g��Transform</summary>
        [SerializeField, LightColor] protected Transform Trafo;

        /// <summary>����</summary>
        [SerializeField] protected TankCannon Weapon;

        /// <summary>�^���b�g �L���^�s���[</summary>
        [SerializeField] protected TankMovingParts Parts;

        /// <summary>�_���[�W����</summary>
        [SerializeField] protected TankDamage Damage;

        /// <summary>�F</summary>
        [SerializeField] protected TankFillColor FillColor;

        /// <summary>����Interval</summary>
        [SerializeField] protected Interval ShootInterval;

        /// <summary>���S���o���I������ێ��s</summary>
        protected UnityAction OnDeath;

        /// <summary>�ړ��ł��Ȃ���Ԃ�</summary>
        protected bool IsNotMove => StageManager.IsNotMove || Damage.IsDeath;

        /// <summary>�A�N�e�B�u��</summary>
        public bool IsActive
        {
            get => Obj.activeSelf;
            set => Obj.SetActive(value);
        }

        protected virtual void OnEnable() => FillColor.SetColor();//�A�N�e�B�u�ɂȂ�����/�F���Z�b�g

        protected virtual void Awake()
        {
            FillColor.SetColor();              //�F���Z�b�g

            Damage.OnHealthGone = OnHealthGone;//�̗͂�0�ɂȂ����ێ��s����֐����Z�b�g
        }

        /// <summary>position��rotation���Z�b�g</summary>
        /// <param name="trafo">�Z�b�g����position��rotation</param>
        public void SetPosAndRot(Transform trafo)
        {
            Trafo.position = trafo.position;   //�ʒu����
            Parts.SetRotation = trafo.rotation;//�p�x����
        }

        /// <summary>Health��0�ɂȂ����ێ��s</summary>
        protected virtual void OnHealthGone()
        {
            FillColor.SetGray();                        //�F���O���[�ɂ���

            StartCoroutine(Damage.DeathEffect(OnDeath));//���S���o���J�n
        }

        protected virtual void OnTriggerEnter2D(Collider2D hit)
        {
            if (IsNotMove) return;                                 //�ړ��ł��Ȃ���ԂȂ�

            StartCoroutine(Damage.Hit(hit, FillColor.DamageColor));//�_���[�W�G�t�F�N�g���J�n
        }
    }

    /// <summary>�^���N�̑�C</summary>
    [System.Serializable]
    public class TankCannon
    {
        /// <summary>�e������SE���Đ�����AudioSource</summary>
        [SerializeField, LightColor] private AudioSource ShootSource;

        /// <summary>�e�̏��</summary>
        [SerializeField] private BulletInfo BulletInfo;

        /// <summary>�v�[�����Ă���e���擾����֐�</summary>
        public Func<BulletScript> BulletPool { get; set; }

        /// <summary>�e��</summary>
        public float BulletSpeed => BulletInfo.Speed;

        /// <summary>�G�l�~�[�̏����Z�b�g����֐�</summary>
        /// <param name="i">�G�l�~�[�̏��</param>
        public void SetInfo(TankEnemyInfoScript i)
        {
            BulletInfo.Init(i.BulletSpeed, i.BulletReflectionCount, i.FillColor);//�e�̏����Z�b�g����֐����Ăяo��
        }

        /// <summary>�e�𔭎�</summary>
        public void Shoot()
        {
            BulletPool.Invoke().Shoot(BulletInfo);                                 //�v�[������Ă���e���擾���A���˂���֐��̈����ɒe�̏������Ăяo��

            AudioScript.I.TankAudio.Clips[TankClip.Shoot].PlayOneShot(ShootSource);//���˂���ۂ�SE���Đ�����
        }
    }

    /// <summary>�^���N�̈ړ�����(�^���b�g : �L���^�s���[)</summary>
    [System.Serializable]
    public class TankMovingParts
    {
        /// <summary>�^���b�g��Transform</summary>
        [SerializeField, LightColor] private Transform Turret;

        /// <summary>�L���^�s����Tran</summary>
        [SerializeField, LightColor] private Transform Caterpillar;

        /// <summary>�^���b�g��]���̕�Ԓl</summary>
        [SerializeField, Range01] float TurretLerp;

        /// <summary>�L���^�s����]���̕�Ԓl</summary>
        [SerializeField, Range01] private float CaterpillarLerp;

        /// <summary>�^���b�g�ƃL���^�s����Rotation���Z�b�g</summary>
        public Quaternion SetRotation { set => Caterpillar.rotation = Turret.rotation = value; }

        /// <summary>�G�l�~�[�̏����Z�b�g</summary>
        /// <param name="i">�G�l�~�[�̏��</param>
        public void SetInfo(TankEnemyInfoScript i) => TurretLerp = i.TurretLerp;

        /// <summary>�^���b�g���^�[�Q�b�g�̕����֌�����</summary>
        /// <param name="target">�^�[�Q�b�g�̕���</param>
        public void TargetLookAt(Vector3 target)
        {
            Turret.LerpRotation(target, TurretLerp, Vector3.forward);//�^���b�g���^�[�Q�b�g�̕����ւ̕�Ԓl����
        }

        /// <summary>�L���^�s���[���ړ������Ɍ�����</summary>
        /// <param name="velocity">�ړ��x�N�g��</param>
        public void CaterpillarLookAt(Vector2 velocity)
        {
            if (velocity != Vector2.zero)                                            //�ړ��x�N�g����(0, 0)�ȊO�Ȃ�
            {
                Caterpillar.LerpRotation(velocity, CaterpillarLerp, Vector3.forward);//�L���^�s���[���ړ������ւ̕�Ԓl����
            }
        }
    }

    /// <summary>�^���N�̃_���[�W����</summary>
    [System.Serializable]
    public class TankDamage
    {
        /// <summary>�Ԃ̗̑̓Q�[�W</summary>
        [SerializeField, LightColor] private Image RedHealthGauge;

        /// <summary>�΂̗̑̓Q�[�W</summary>
        [SerializeField, LightColor] private Image GreenHealthGauge;

        /// <summary>���S�����ۂ̃G�t�F�N�g�I�u�W�F�N�g</summary>
        [SerializeField, LightColor] private GameObject DeathEffectObj;

        /// <summary>��eSE���Đ�����AudioSource</summary>
        [SerializeField, LightColor] private AudioSource DamageSource;

        /// <summary>�̗�</summary>
        [SerializeField] private int Health;

        /// <summary>�ԑ̗̓Q�[�W�̕�Ԓl</summary>
        [SerializeField, Range01] private float RedHealthBarLerp;

        /// <summary>�_���[�W���󂯂�^�O</summary>
        [SerializeField, Tag] private string[] DamageTags;

        /// <summary>�ő�̗�</summary>
        private int MaxHealth;

        /// <summary>�G�t�F�N�g�\������</summary>
        private WaitForSeconds EffectWait;

        /// <summary>�_���[�W���󂯂Ȃ���Ԃ�</summary>
        private bool IsNoDamage;

        /// <summary>�̗͂�0�ɂȂ����ێ��s</summary>
        public UnityAction OnHealthGone { get; set; }

        /// <summary>���S���Ă��邩</summary>
        public bool IsDeath => Health <= default(int);

        /// <summary>�G�l�~�[�̏����Z�b�g</summary>
        /// <param name="i">�G�l�~�[�̏��</param>
        public void SetInfo(TankEnemyInfoScript i)
        {
            MaxHealth = Health = i.Health;  //�̗͂��Z�b�g

            DeathEffectObj.SetActive(false);//���S�G�t�F�N�g���A�N�e�B�u
        }

        /// <summary>�̗͂����Z�b�g</summary>
        public void HealthReSet()
        {
            if (MaxHealth == default) MaxHealth = Health;//�ő�̗͂�0�Ȃ�/�ő�̗͂��X�V

            Health = MaxHealth;                          //�ő�̗͂���
        }

        /// <summary>Health��1��</summary>
        public void HealthRecovery()
        {
            if (Health < MaxHealth) Health++;//���݂̗̑͂��ő�̗͂�艺�Ȃ�̗͂� + 1
        }

        /// <summary>���S���o</summary>
        /// <param name="c">���o�I�������s����R�[���o�b�N</param>
        public IEnumerator DeathEffect(UnityAction c)
        {
            DeathEffectObj.SetActive(true);                                                                //���S�G�t�F�N�g���A�N�e�B�u

            yield return EffectWait ??= new(AudioScript.I.TankAudio.Clips[TankClip.Explosion].Clip.length);//�ҋ@

            DeathEffectObj.SetActive(false);                                                               //���S�G�t�F�N�g���A�N�e�B�u

            c?.Invoke();                                                                                   //�R�[���o�b�N�����s
        }

        /// <summary>�_���[�W����</summary>
        /// <param name="hit">�ڐG�����R���C�_�[</param>
        /// <param name="damage">�_���[�W���o</param>
        public IEnumerator Hit(Collider2D hit, Func<float, IEnumerator> damage)
        {
            if (!IsNoDamage && DamageTags.Any((v) => hit.CompareTag(v)))      //�m�[�_���[�W����Ȃ� ���� �_���[�W���󂯂�^�O��
            {
                Health--;                                                     //�̗� - 1

                var audio = AudioScript.I.TankAudio;                          //�^���NAudio

                if (Health == default)                                        //�̗͂�0�Ȃ�
                {
                    OnHealthGone?.Invoke();                                   //�̗͂�0�ɂȂ����ێ��s����֐������s

                    audio.Clips[TankClip.Explosion].PlayOneShot(DamageSource);//����SE���Đ�
                }
                else if (Health > default(int))                               //�̗͂�0����Ȃ�
                {
                    IsNoDamage = true;                                        //�_���[�W���󂯂Ȃ���Ԃ���true

                    var clip = audio.Clips[TankClip.Damage];                  //�_���[�W�N���b�v

                    clip.PlayOneShot(DamageSource);                           //�_���[�WSE���Đ�

                    yield return damage.Invoke(clip.Clip.length);           �@ //�_���[�W���o

                    IsNoDamage = false;                                       //�_���[�W���󂯂Ȃ���Ԃ���false
                }
            }
        }

        /// <summary>�̗̓o�[���X�V</summary>
        public void UpdateHealthGauge()
        {
            if (MaxHealth == default) MaxHealth = Health;                                          //�ő�̗͂�0�Ȃ�/�ő�̗͂��X�V

            float v = Health / (float)MaxHealth;                                                   //�̗̓Q�[�W�̒l

            GreenHealthGauge.fillAmount = v;                                                       //�΃Q�[�W���X�V

            RedHealthGauge.fillAmount = Mathf.Lerp(RedHealthGauge.fillAmount, v, RedHealthBarLerp);//�ԃQ�[�W���Ԓl�ōX�V
        }
    }

    /// <summary>�^���N�̐F</summary>
    [System.Serializable]
    public class TankFillColor
    {
        /// <summary>�F��ς���X�v���C�g�z��</summary>
        [SerializeField, LightColor] private SpriteRenderer[] FillSprites;

        /// <summary>�^���N�̐F</summary>
        [SerializeField] private Color FillColor;

        /// <summary>�ҋ@����</summary>
        private WaitForSeconds Wait;

        /// <summary>�F</summary>
        public Color Color => FillColor;

        /// <summary>�G�l�~�[�̏����Z�b�g</summary>
        /// <param name="i">�G�l�~�[�̏��</param>
        public void SetInfo(TankEnemyInfoScript i) => SetColor(FillColor = i.FillColor);

        /// <summary>�F���O���[�ɃZ�b�g</summary>
        public void SetGray() => SetColor(Color.gray);

        /// <summary>�F���Z�b�g</summary>
        public void SetColor() => SetColor(FillColor);

        /// <summary>�F���Z�b�g</summary>
        /// <param name="color">�Z�b�g����F</param>
        public void SetColor(Color color)
        {
            foreach (var s in FillSprites)//FillSprites�̗v�f�����J��Ԃ�
            {
                s.color = color;          //�����̒l����
            }
        }

        /// <summary>�_���[�W���o</summary>
        /// <param name="waitTime">�ҋ@����</param>
        public IEnumerator DamageColor(float waitTime)
        {
            SetGray();                                         //�F���O���[�ɂ���

            yield return Wait ??= new WaitForSeconds(waitTime);//�ҋ@

            SetColor();                                        //�F���Z�b�g
        }
    }
}
