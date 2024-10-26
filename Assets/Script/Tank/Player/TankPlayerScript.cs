using System.Collections;

using UnityEngine;

/// <summary>�v���C���[�̃^���N</summary>
public class TankPlayerScript : TankScript
{
    /// <summary>���͈ړ�</summary>
    [SerializeField, LightColor] private TankInputMoveScript inputMove;

    /// <summary>���C���J����</summary>
    [SerializeField, LightColor] private Camera mainCamera;

    /// <summary>���X�e�[�W�N���A������c�@�𓾂邩</summary>
    [SerializeField] private int addLifeCount;

    /// <summary>���C�t(�c�@)</summary>
    public int Life { get; private set; }

    /// <summary>�c�@�𑝂₷�����肷��</summary>
    /// <param name="count">���݃X�e�[�W��</param>
    /// <returns>�c�@�𑝂₷��</returns>
    public bool IsAddLife(int count) => count % addLifeCount == default;

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
    public void HealthRecovery() => damage.HealthRecovery();//�̗͂�1��

    /// <summary>(�v�[���e, ���g���C, �Q�[���I�[�o�[)�֐����Z�b�g</summary>
    /// <param name="pool">�v�[�����Ă���e���擾����֐�</param>
    /// <param name="onDeath">�Q�[���I�[�o�[�ɂȂ����Վ��s����֐�</param>
    public void Init(System.Func<BulletScript> pool, System.Func<bool, IEnumerator> onDeath)
    {
        cannon.BulletPool = pool;                                            //�v�[�����Ă���e���擾����֐����Z�b�g

        base.onDeath = () => StartCoroutine(onDeath.Invoke(Life <= default(int)));//���S�����ێ��s����֐����Z�b�g
    }

    /// <summary>�������܂Ƃ߂��֐�</summary>
    private void Move()
    {
        line.CreateLine();

        parts.TargetLookAt(Input.mousePosition - mainCamera.WorldToScreenPoint(inputMove.Pos));//�}�E�X�J�[�\���̕����փ^���b�g��������

        inputMove.InputMove();                                                                 //���͈ړ�

        if (Input.GetMouseButton(default) && shootInterval) cannon.Shoot();                    //�E�N���b�N ���� ���ˊԊu�Ȃ�/���˂���
    }

    /// <summary>���g���C</summary>
    public void Retry()
    {
        damage.HealthReSet();                 //�̗͂����Z�b�g

        fillColor.SetColor();                 //�F���Z�b�g

        if (Life > default(int)) ReMoveLife();//���C�t��0�ȏ�Ȃ�/���C�t�� - 1
    }

    /// <summary>���X�^�[�g</summary>
    public void ReStart()
    {
        damage.HealthReSet();                 //�̗͂����Z�b�g

        fillColor.SetColor();                 //�F���Z�b�g

        Life = default;                       //���C�t�����Z�b�g
    }

    private void FixedUpdate()
    {
        damage.UpdateHealthGauge();                 //�̗̓o�[���X�V

        parts.CaterpillarLookAt(inputMove.Velocity);//�L���^�s������]

        if (IsNotMove)                              //�ړ��ł��Ȃ���ԂȂ�
        {
            inputMove.Stop();                       //�ړ����~

            return;                                 //�I��
        }

        Move();                                     //�������܂Ƃ߂��֐����Ăяo��
    }
}
