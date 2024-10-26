using UnityEngine;
using System.Collections;

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
    [SerializeField, LightColor] private TankAutoMoveScript autoMove;

    /// <summary>�^�[�Q�b�g</summary>
    [SerializeField, LightColor] private TankTargetPredictionScript prediction;

    /// <summary>�T�[�`</summary>
    [SerializeField, LightColor] private TankTargetSearchScript search;

    /// <summary>�U���^�C�v</summary>
    [SerializeField] private AttackType attackType;

    /// <summary>AttackType�z��</summary>
    private readonly AttackType[] AttackTypes = (AttackType[])System.Enum.GetValues(typeof(AttackType));

    /// <summary>�̒l���Z�b�g</summary>
    /// <param name="info">�̏��</param>
    /// <param name="onDeath">���S���Ɏ��s����R�[���o�b�N</param>
    public void SetInfo(TankEnemyInfoScript info, System.Func<BulletScript> pool, System.Func<IEnumerator> onDeath)
    {
        autoMove.Stop();                                  //�ړ����~

        cannon.BulletPool = pool;                         //�e�v�[���֐����Z�b�g

        parts.SetInfo(info);                              //�G�l�~�[�̏����Z�b�g

        damage.SetInfo(info);                             //�G�l�~�[�̏����Z�b�g

        fillColor.SetInfo(info);                          //�G�l�~�[�̏����Z�b�g

        line.SetLineColor(info.FillColor);                //�G�l�~�[�̏����Z�b�g

        cannon.SetInfo(info);                             //�G�l�~�[�̏����Z�b�g

        autoMove.SetInfo(info);                           //�G�l�~�[�̏����Z�b�g

        line.Crear();                                     //�e�������Z�b�g

        SetPosAndRot(info.PosAndRot);                     //�ꏊ�Ɗp�x���Z�b�g

        shootInterval.Time = info.ShootInterval;          //���ˊԊu���Z�b�g

        attackType = info.AttackType;                           //�U���^�C�v���Z�b�g

        base.onDeath = () =>                                   //���S�����Ď��s����֐����Z�b�g
        {
            IsActive = false;                             //��A�N�e�B�u

            stageManager.StartCoroutine(onDeath.Invoke());//���S���̏������J�n
        };

        IsActive = true;                                  //�A�N�e�B�u

        while (attackType == AttackType.Random)                                 //�U���^�C�v�������_���Ȍ���J��Ԃ�
        {
            attackType = AttackTypes[Random.Range(default, AttackTypes.Length)];//�����_���ɃZ�b�g
        }
    }

    /// <summary>�^���b�g���^�[�Q�b�g�̕����Ɍ�����</summary>
    private void LookAt()
    {
        Vector3 pos = attackType != AttackType.Deviation ? prediction.TargetPos : prediction.PredictionPos(autoMove.Pos, cannon.BulletSpeed);//�U���^�C�v���΍���������Ȃ���� ? ���݂̃^�[�Q�b�g�̈ʒu : �^�[�Q�b�g�̈ړ��\����

        parts.TargetLookAt(pos - autoMove.Pos);                                                                          //�^���b�g��(�^�[�Q�b�g�̈ʒu - ���g�̌��݈ʒu)�̕����Ɍ�����
    }

    /// <summary>���C���q�b�g ���� �^�[�Q�b�g�ƈ�v��</summary>
    private bool IsSearchRayHit() => search.Ray(prediction.TargetPos, out var r) && r.transform.position == prediction.TargetPos;//���C���q�b�g ���� �^�[�Q�b�g�ƈ�v�Ȃ�

    /// <summary>�������܂Ƃ߂��֐�</summary>
    private void Move()
    {
        var isTargetType = attackType == AttackType.Target;                       //�U���^�C�v���^�[�Q�b�g��

        var isTargetRType = attackType == AttackType.TargetReflection;            //�U���^�C�v���^�[�Q�b�g���˂�

        var isTarget = (isTargetType || isTargetRType) && !IsSearchRayHit();//(�U���^�C�v���^�[�Q�b�g or ����) ���� ���C���q�b�g ���� �^�[�Q�b�g�ƈ�v�Ȃ�

        line.CreateLine();

        if (!isTargetRType) LookAt();                                       //�U���^�C�v���^�[�Q�b�g���˂���Ȃ����/�^���b�g���^�[�Q�b�g�̕����Ɍ�����

        if (isTarget)                                                       //���C�Ƀ^�[�Q�b�g���q�b�g���Ă�����
        {
            if (isTargetRType)         //�U���^�C�v���^�[�Q�b�g���� ���� ���ː�Ń^�[�Q�b�g���q�b�g������
            {
                var ray = search.ReflectionRay();                      //���ː�̌���

                var targetPos = ray ? ray.transform.position : default;//ray���������Ă����� ? ���������I�u�W�F�N�g�̍��W : (0, 0, 0)

                parts.TargetLookAt(search.SearchRot);                  //

                if (targetPos != prediction.TargetPos) return;
            }
            else return;                                                    //����ȊO�Ȃ�/�I��
        }
        else if (isTargetRType) LookAt();                                   //����ȊO�ōU���^�C�v���^�[�Q�b�g���˂Ȃ�/�^���b�g���^�[�Q�b�g�̕����֌�����

        if (shootInterval)                                                  //���ˊԊu���z���Ă�����
        {
            cannon.Shoot();                                                 //�e�𔭎�

            autoMove.VectorChange();                                        //�ړ��x�N�g����ύX
        }

        prediction.UpdatePreviousPos();                                                //�^�[�Q�b�g�ʒu���X�V
    }

    private void FixedUpdate()
    {
        autoMove.MoveSE();                         //�ړ�SE���Đ�

        damage.UpdateHealthGauge();                //�̗�UI���X�V

        parts.CaterpillarLookAt(autoMove.Velocity);//�L���^�s������]

        if (IsNotMove)                             //�ړ��ł��Ȃ���ԂȂ�
        {
            shootInterval.ReSet();                 //�C���^�[�o�������Z�b�g

            autoMove.Stop();                       //�ړ����~

            return;                                //�I��
        }

        Move();                                    //�������܂Ƃ߂��֐����Ăяo��
    }
}

/// <summary>�^���N�̃^�[�Q�b�g��T���N���X</summary>
[System.Serializable]
public class TankSearch
{
    [SerializeField, LightColor] private LineRenderer lineRenderer;

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

    [SerializeField] private float lineWidth;

    /// <summary>���˂��郌�C�̌`�̃g�����X�t�H�[��</summary>
    private Transform RayCollTrafo;

    public Vector2 SearchRot => Trafo.up;

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

        //if (ray) Debug.DrawRay(o, ray.centroid - o, Color.green);                    //���C���q�b�g�����烌�C������

        return ray;                                                                  //���C�̏���Ԃ�
    }

    /// <summary>�J�v�Z���^��Ray�𔭎�</summary>
    /// <param name="target">�^�[�Q�b�g���W�܂��͕���</param>
    /// <param name="hit">�ڐG�����I�u�W�F�N�g�̏��</param>
    /// <returns>�q�b�g������</returns>
    public bool Ray(Vector3 target, out RaycastHit2D hit)
    {
        var pos = Trafo.position;                         //���g�̈ʒu

        hit = CapsuleRayCast(pos, target - pos, RayLayer);//

        return hit;                                             //�q�b�g������
    }

    /// <summary>���˂���J�v�Z���^��Ray�𔭎�</summary>
    /// <returns>���ː�Ƀq�b�g������ ? ��x�ڂ̃��C�̕��� : (0, 0)</returns>
    public RaycastHit2D ReflectionRay()
    {
        Trafo.Rotate(Angle);         //��]

        var pos = Trafo.position;                         //���g�̈ʒu

        var d = Trafo.up;                                      //���C�̕���

        var ray1 = CapsuleRayCast(pos, d, RayLayer);//���C�̏��

        var ray2 = CapsuleRayCast(ray1.centroid, Vector2.Reflect(d, ray1.normal), PlayerLayer);//�q�b�g�����ꏊ���烌�C�𔭎˂��q�b�g������

        //const int LineCount = 3;

        //if (lineRenderer.positionCount >= LineCount)
        //{
        //    lineRenderer.positionCount = LineCount;
        //}

        //int lineCount = default;                                //

        //lineRenderer.SetPosition(lineCount++, pos);             //

        //lineRenderer.SetPosition(lineCount++, ray1.centroid);    //

        //lineRenderer.SetPosition(lineCount++, ray2 ? ray2.centroid : ray1.centroid);    //

        return ray2;                   //�q�b�g������ ? ��x�ڂ̃��C�̕��� : (0, 0)
    }
}
