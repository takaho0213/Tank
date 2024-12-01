using UnityEngine;
using System.Collections;

/// <summary>�v���C���[�̃^���N</summary>
public class TankPlayerScript : TankScript
{
    /// <summary>���͈ړ�</summary>
    [SerializeField, LightColor] private TankInputMoveScript inputMove;

    /// <summary>���g���C���邽�߂̃��C�t</summary>
    [SerializeField, LightColor] private TankLifeScript life;

    /// <summary>���C���J����</summary>
    [SerializeField, LightColor] private Camera mainCamera;

    public TankLifeScript Life => life;

    protected override TankMoveScript BaseMove => inputMove;

    /// <summary>�̗͂��ő�܂ŉ�</summary>
    public void HealthRecovery() => damage.HealthRecovery();//�̗͂�1��

    /// <summary>(�v�[���e, ���g���C, �Q�[���I�[�o�[)�֐����Z�b�g</summary>
    /// <param name="pool">�v�[�����Ă���e���擾����֐�</param>
    /// <param name="onDeath">�Q�[���I�[�o�[�ɂȂ����Վ��s����֐�</param>
    public void Init(System.Func<BulletScript> pool, System.Func<bool, IEnumerator> onDeath)
    {
        cannon.BulletPool = pool;                                                           //�v�[�����Ă���e���擾����֐����Z�b�g

        this.onDeath = () => StartCoroutine(onDeath.Invoke(life.LifeCount <= default(int)));//���S�����ێ��s����֐����Z�b�g
    }

    /// <summary>�̗�,�F���Z�b�g����</summary>
    public void OnRetry()
    {
        damage.HealthReSet();//�̗͂����Z�b�g

        fillColor.SetColor();//�F���Z�b�g

        life.ReMoveLife();   //���C�t��0�ȏ�Ȃ�/���C�t�� - 1
    }

    /// <summary>���X�^�[�g</summary>
    public void ReStart()
    {
        damage.HealthReSet();//�̗͂����Z�b�g

        fillColor.SetColor();//�F���Z�b�g

        life.ReSetLife();    //���C�t�����Z�b�g
    }

    /// <summary>�������܂Ƃ߂��֐�</summary>
    protected override void Move()
    {
        line.CreateLine();

        parts.TargetLookAt(Input.mousePosition - mainCamera.WorldToScreenPoint(inputMove.Pos));//�}�E�X�J�[�\���̕����փ^���b�g��������

        inputMove.InputMove();                                                                 //���͈ړ�

        if (Input.GetMouseButton(default) && cannon.IsShoot) cannon.Shoot();                    //�E�N���b�N ���� ���ˊԊu�Ȃ�/���˂���

        base.Move();
    }
}
