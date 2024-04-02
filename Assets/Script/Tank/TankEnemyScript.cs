using UnityEngine;
using System.Collections;

namespace Tank
{
    /// <summary>�G�l�~�[�̃^���N</summary>
    public class TankEnemyScript : TankScript
    {
        /// <summary>�U���^�C�v</summary>
        public enum AttackType
        {
            /// <summary>�m�[�}��</summary>
            Normal,
            /// <summary>�΍�����</summary>
            Deviation,
            /// <summary>�ː��Ƀ^�[�Q�b�g������Ȃ�</summary>
            Target,
            /// <summary>�ː����ː�Ƀ^�[�Q�b�g������Ȃ�</summary>
            TargetReflection,
            /// <summary>�����_��</summary>
            Random,
        }

        /// <summary>�����ړ�</summary>
        [SerializeField] private TankAutoMove AutoMove;

        /// <summary>�^�[�Q�b�g</summary>
        [SerializeField] private TankChasingTarget Target;

        /// <summary>�T�[�`</summary>
        [SerializeField] private TankSearch Search;

        /// <summary>�U���^�C�v</summary>
        [SerializeField] private AttackType Type;

        /// <summary>AttackType�z��</summary>
        private readonly AttackType[] AttackTypes = (AttackType[])System.Enum.GetValues(typeof(AttackType));

        /// <summary>�̒l���Z�b�g</summary>
        /// <param name="info">�̏��</param>
        /// <param name="onDeath">���S���Ɏ��s����R�[���o�b�N</param>
        public void SetInfo(TankEnemyInfoScript info, System.Func<BulletScript> pool, System.Func<IEnumerator> onDeath)
        {
            AutoMove.Stop();                                  //�ړ����~

            Weapon.BulletPool = pool;                         //�e�v�[���֐����Z�b�g

            Parts.SetInfo(info);                              //�G�l�~�[�̏����Z�b�g

            Damage.SetInfo(info);                             //�G�l�~�[�̏����Z�b�g

            FillColor.SetInfo(info);                          //�G�l�~�[�̏����Z�b�g

            Weapon.SetInfo(info);                             //�G�l�~�[�̏����Z�b�g

            AutoMove.SetInfo(info);                           //�G�l�~�[�̏����Z�b�g

            SetPosAndRot(info.PosAndRot);                     //�ꏊ�Ɗp�x���Z�b�g

            ShootInterval.Time = info.ShootInterval;          //���ˊԊu���Z�b�g

            Type = info.AttackType;                           //�U���^�C�v���Z�b�g

            OnDeath = () =>                                   //���S�����Ď��s����֐����Z�b�g
            {
                IsActive = false;                             //��A�N�e�B�u

                StageManager.StartCoroutine(onDeath.Invoke());//���S���̏������J�n
            };

            IsActive = true;                                  //�A�N�e�B�u

            while (Type == AttackType.Random)                                 //�U���^�C�v�������_���Ȍ���J��Ԃ�
            {
                Type = AttackTypes[Random.Range(default, AttackTypes.Length)];//�����_���ɃZ�b�g
            }
        }

        /// <summary>�^���b�g���^�[�Q�b�g�̕����Ɍ�����</summary>
        private void LookAt()
        {
            Vector3 pos = Type != AttackType.Deviation ? Target.Pos : Target.PredictionPos(AutoMove.Pos, Weapon.BulletSpeed);//�U���^�C�v���΍���������Ȃ���� ? ���݂̃^�[�Q�b�g�̈ʒu : �^�[�Q�b�g�̈ړ��\����

            Parts.TargetLookAt(pos - AutoMove.Pos);                                                                          //�^���b�g��(�^�[�Q�b�g�̈ʒu - ���g�̌��݈ʒu)�̕����Ɍ�����
        }

        /// <summary>���C���q�b�g ���� �^�[�Q�b�g�ƈ�v��</summary>
        private bool IsSearchRayHit() => Search.Ray(Target.Pos, out var r) && r.transform.position == Target.Pos;//���C���q�b�g ���� �^�[�Q�b�g�ƈ�v�Ȃ�

        /// <summary>�������܂Ƃ߂��֐�</summary>
        private void Move()
        {
            var isTargetType = Type == AttackType.Target;                       //�U���^�C�v���^�[�Q�b�g��

            var isTargetRType = Type == AttackType.TargetReflection;            //�U���^�C�v���^�[�Q�b�g���˂�

            var isTarget = (isTargetType || isTargetRType) && !IsSearchRayHit();//(�U���^�C�v���^�[�Q�b�g or ����) ���� ���C���q�b�g ���� �^�[�Q�b�g�ƈ�v�Ȃ�

            if (!isTargetRType) LookAt();                                       //�U���^�C�v���^�[�Q�b�g���˂���Ȃ����/�^���b�g���^�[�Q�b�g�̕����Ɍ�����

            if (isTarget)                                                       //���C�Ƀ^�[�Q�b�g���q�b�g���Ă�����
            {
                if (isTargetRType && Search.ReflectionRay(out var hit))         //�U���^�C�v���^�[�Q�b�g���� ���� ���ː�Ń^�[�Q�b�g���q�b�g������
                {
                    Parts.TargetLookAt(hit);                                    //�^���b�g�𔽎ː�Ƀq�b�g�����ۂ̕����Ɍ�����
                }
                else return;                                                    //����ȊO�Ȃ�/�I��
            }
            else if (isTargetRType) LookAt();                                   //����ȊO�ōU���^�C�v���^�[�Q�b�g���˂Ȃ�/�^���b�g���^�[�Q�b�g�̕����֌�����

            if (ShootInterval)                                                  //���ˊԊu���z���Ă�����
            {
                Weapon.Shoot();                                                 //�e�𔭎�

                AutoMove.VectorChange();                                        //�ړ��x�N�g����ύX
            }

            Target.LastUpdate();                                                //�^�[�Q�b�g�ʒu���X�V
        }

        private void FixedUpdate()
        {
            AutoMove.MoveSE();                         //�ړ�SE���Đ�

            Damage.UpdateHealthGauge();                //�̗�UI���X�V

            Parts.CaterpillarLookAt(AutoMove.Velocity);//�L���^�s������]

            if (IsNotMove)                             //�ړ��ł��Ȃ���ԂȂ�
            {
                ShootInterval.ReSet();                 //�C���^�[�o�������Z�b�g

                AutoMove.Stop();                       //�ړ����~

                return;                                //�I��
            }

            Move();                                    //�������܂Ƃ߂��֐����Ăяo��
        }
    }

    /// <summary>�^���N�̎����ړ�</summary>
    [System.Serializable]
    public class TankAutoMove
    {
        /// <summary></summary>
        public enum MoveType
        {
            /// <summary>����</summary>
            None,
            /// <summary>��</summary>
            Horizontal,
            /// <summary>�c</summary>
            Vertical,
            /// <summary>�� or �c</summary>
            HorizontalOrVertical,
            /// <summary>�� and �c</summary>
            HorizontalAndVertical,
            /// <summary>�����_��</summary>
            Random,
        }

        /// <summary>�ړ��pRigidbody</summary>
        [SerializeField, LightColor] private Rigidbody2D Ribo;

        /// <summary>�ړ�AudioSource</summary>
        [SerializeField, LightColor] private AudioSource Source;

        /// <summary>�ړ��^�C�v</summary>
        [SerializeField] private MoveType Type;

        /// <summary>�ړ����x</summary>
        [SerializeField] private float Speed;

        /// <summary>�ړ��x�N�g���𐳋K�����邩</summary>
        [SerializeField] private bool IsNormalized;

        /// <summary>�ړ�SE�̍Đ��Ԋu</summary>
        private Interval SEInterval;

        /// <summary>�ړ��^�C�v�z��</summary>
        private readonly MoveType[] MoveTypes = (MoveType[])System.Enum.GetValues(typeof(MoveType));

        /// <summary>�����_���ɑ��x��Ԃ�</summary>
        private float RandomSpeed => Random.Range(-Speed, Speed);

        /// <summary>�����_����true : false��Ԃ�</summary>
        private bool RandomBool => Random.Range(default, 2) == default;

        /// <summary>���g�̈ʒu</summary>
        public Vector3 Pos => Ribo.position;

        /// <summary>�ړ��x�N�g��</summary>
        public Vector3 Velocity => Ribo.velocity;

        /// <summary>�ړ��x�N�g���̐��`</summary>
        private Vector2 MoveVector
        {
            get
            {
                var type = Type == MoveType.Random ? MoveTypes[Random.Range(default, MoveTypes.Length)] : Type;          //�ړ��^�C�v�������_���Ȃ�

                return type switch                                                                                       //�^�C�v�ŕ���
                {
                    MoveType.Horizontal            => new(RandomSpeed, default),                                         //(�����_�����x, 0)
                    MoveType.Vertical              => new(default, RandomSpeed),                                         //(0, �����_�����x)
                    MoveType.HorizontalOrVertical  => RandomBool ? new(RandomSpeed, default) : new(default, RandomSpeed),//�����_��bool ? (�����_�����x, 0) : (0, �����_�����x)
                    MoveType.HorizontalAndVertical => new(RandomSpeed, RandomSpeed),                                     //(�����_�����x, �����_�����x)
                    MoveType.Random                => MoveVector,                                                        //�ړ��x�N�g���̐��`
                    _                              => default,                                                           //(0, 0)
                };
            }
        }

        /// <summary>�G�l�~�[�̏����Z�b�g</summary>
        /// <param name="i">�G�l�~�[�̏��</param>
        public void SetInfo(TankEnemyInfoScript i)
        {
            Type = i.MoveType;                      //�ړ��̎d�����Z�b�g
            Speed = i.MoveSpeed;                    //�ړ����x���Z�b�g
            IsNormalized = i.IsMoveVectorNormalized;//�ړ��x�N�g���𐳋K�����邩���Z�b�g
        }

        /// <summary>��~</summary>
        public void Stop() => Ribo.velocity = Vector2.zero;//�ړ��x�N�g����(0, 0)����

        /// <summary>�ړ��x�N�g����ύX</summary>
        public void VectorChange()
        {
            Ribo.velocity = IsNormalized ? MoveVector.normalized * Speed : MoveVector;//�ړ��x�N�g����(�ړ��x�N�g���𐳋K�����邩 ? ���K�������ړ��x�N�g���̐��` * �ړ����x : �ړ��x�N�g���̐��`)����
        }

        /// <summary></summary>
        public void MoveSE()
        {
            if (Ribo.velocity != Vector2.zero)                       //�ړ��x�N�g����(0, 0)�ȊO�Ȃ�
            {
                var c = AudioScript.I.TankAudio.Clips[TankClip.Move];//�ړ�SE�̃N���b�v

                SEInterval ??= new(c.Clip.length / Speed, true);     //�C���^�[�o����null�Ȃ�C���X�^���X��

                if (SEInterval) Source.PlayOneShot(c.Clip, c.Volume);//SE�Đ��Ԋu���z���Ă�����ړ�SE���Đ�
            }
        }
    }

    /// <summary>�^���N�̃^�[�Q�b�g��ǂ�������N���X</summary>
    [System.Serializable]
    public class TankChasingTarget
    {
        /// <summary>�^�[�Q�b�g�̃g�����X�t�H�[��</summary>
        [SerializeField, LightColor] private Transform Target;

        /// <summary>�^�[�Q�b�g�̑O�t���[���̈ʒu</summary>
        private Vector3 TargetPosPreframe;

        /// <summary>�^�[�Q�b�g�̈ʒu</summary>
        public Vector3 Pos => Target.position;

        /// <summary>�^�[�Q�b�g�̈ړ����\��</summary>
        /// <param name="pos">���g�̏ꏊ</param>
        /// <param name="speed">�e��</param>
        /// <returns>�^�[�Q�b�g�̈ړ���</returns>
        public Vector3 PredictionPos(Vector3 pos, float speed)
        {
            Vector3 TargetPos = Target.position;                       //�^�[�Q�b�g�̈ʒu

            Vector3 TargetMoveDistance = TargetPos - TargetPosPreframe;//�^�[�Q�b�g�̈ړ�����

            return TargetPos + TargetMoveDistance * Vector3.Distance(pos, TargetPos) / speed / Time.fixedDeltaTime;//�^�Q�ʒu + �^�Q�ړ����� * (���g�̈ʒu�ƃ^�Q�ʒu�̋���) / �e�� / �f���^�^�C��
        }

        /// <summary>�^�[�Q�b�g�̑O�t���[���̈ʒu���X�V(PredictionPos()�̌�ɌĂяo��)</summary>
        public void LastUpdate() => TargetPosPreframe = Target.position;//�^�[�Q�b�g�̈ʒu���X�V
    }

    /// <summary>�^���N�̃^�[�Q�b�g��T���N���X</summary>
    [System.Serializable]
    public class TankSearch
    {
        /// <summary>���˂��郌�C�̌`</summary>
        [SerializeField, LightColor] private CapsuleCollider2D RayColl;

        /// <summary>�T���ۉ�]������g�����X�t�H�[��</summary>
        [SerializeField, LightColor] private Transform Trafo;

        /// <summary>��]���������</summary>
        [SerializeField] private Vector3 Angle;

        /// <summary>���C�����m���郌�C���[</summary>
        [SerializeField] private LayerMask RayLayer;

        /// <summary>�v���C���[�̃��C���[</summary>
        [SerializeField] private LayerMask PlayerLayer;

        /// <summary>���˂��郌�C�̌`�̃g�����X�t�H�[��</summary>
        private Transform RayCollTrafo;

        /// <summary>�J�v�Z���^��Ray�𔭎�</summary>
        /// <param name="o">���˂�����W</param>
        /// <param name="d">���˂���x�N�g��</param>
        /// <param name="l">���C���[</param>
        /// <returns>���C�̏��</returns>
        private RaycastHit2D CapsuleRayCast(Vector2 o, Vector2 d, LayerMask l)
        {
            RayCollTrafo ??= RayColl.transform;                                          //null�Ȃ���

            var size = RayColl.size * RayCollTrafo.localScale;                           //�T�C�Y

            var type = CapsuleDirection2D.Vertical;                                      //����

            var angle = RayCollTrafo.eulerAngles.z;                                      //�p�x

            var ray = Physics2D.CapsuleCast(o, size, type, angle, d, Mathf.Infinity, l); //���C

            if (ray) Debug.DrawRay(o, ray.centroid - o, Color.green);                    //���C���q�b�g�����烌�C������

            return ray;                                                                  //���C�̏���Ԃ�
        }

        /// <summary>�J�v�Z���^��Ray�𔭎�</summary>
        /// <param name="target">�^�[�Q�b�g���W�܂��͕���</param>
        /// <param name="hit">�ڐG�����I�u�W�F�N�g�̏��</param>
        /// <returns>�q�b�g������</returns>
        public bool Ray(Vector3 target, out RaycastHit2D hit)
        {
            var pos = Trafo.position;                             //���g�̈ʒu

            var direction = target - Trafo.position;              //���C�̕���

            Debug.DrawRay(pos, direction, Color.blue);            //���C������

            return hit = CapsuleRayCast(pos, direction, RayLayer);//�q�b�g������
        }

        /// <summary>���˂���J�v�Z���^��Ray�𔭎�</summary>
        /// <returns>���ː�Ƀq�b�g������ ? ��x�ڂ̃��C�̕��� : (0, 0)</returns>
        private Vector3 ReflectionCapsuleRay()
        {
            var d = Trafo.up;                                      //���C�̕���

            var ray1 = CapsuleRayCast(Trafo.position, d, RayLayer);//���C�̏��

            bool isRay2Hit = CapsuleRayCast(ray1.point, Vector2.Reflect(d, ray1.normal), PlayerLayer);//�q�b�g�����ꏊ���烌�C�𔭎˂��q�b�g������

            return isRay2Hit ? d : Vector3.zero;                   //�q�b�g������ ? ��x�ڂ̃��C�̕��� : (0, 0)
        }

        /// <summary>�p�x��ς��Ȃ��甽�˂���J�v�Z���^��Ray�𔭎�</summary>
        /// <param name="hit">�q�b�g�����ۂ̕���</param>
        /// <returns>���ː�ɃI�u�W�F�N�g�����邩</returns>
        public bool ReflectionRay(out Vector3 hit)
        {
            Trafo.Rotate(Angle);         //��]

            hit = ReflectionCapsuleRay();//���˃��C�𔭎�

            return hit != Vector3.zero;  //���ː�ɃI�u�W�F�N�g�����邩��Ԃ�
        }
    }
}
