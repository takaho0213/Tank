using System;
using System.Linq;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TankDamageScript : MonoBehaviour
{
    /// <summary>�Ԃ̗̑̓Q�[�W</summary>
    [SerializeField, LightColor] private Image redHealthGauge;

    /// <summary>�΂̗̑̓Q�[�W</summary>
    [SerializeField, LightColor] private Image greenHealthGauge;

    /// <summary>���S�����ۂ̃G�t�F�N�g�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject deathEffectObj;

    /// <summary>��eSE���Đ�����AudioSource</summary>
    [SerializeField, LightColor] private AudioSource damageSource;

    /// <summary>�̗�</summary>
    [SerializeField] private int health;

    /// <summary>�ԑ̗̓Q�[�W�̕�Ԓl</summary>
    [SerializeField, Range01] private float redHealthBarLerp;

    /// <summary>�_���[�W���󂯂�^�O</summary>
    [SerializeField, Tag] private string[] damageTags;

    /// <summary>�ő�̗�</summary>
    private int maxHealth;

    /// <summary>�G�t�F�N�g�\������</summary>
    private WaitForSeconds effectWait;

    /// <summary>�_���[�W���󂯂Ȃ���Ԃ�</summary>
    private bool isNoDamage;

    /// <summary>�̗͂�0�ɂȂ����ێ��s</summary>
    public UnityAction OnHealthGone { get; set; }

    /// <summary>���S���Ă��邩</summary>
    public bool IsDeath => health <= default(int);

    /// <summary>�G�l�~�[�̏����Z�b�g</summary>
    /// <param name="i">�G�l�~�[�̏��</param>
    public void SetInfo(TankEnemyInfoScript i)
    {
        maxHealth = health = i.Health;  //�̗͂��Z�b�g

        deathEffectObj.SetActive(false);//���S�G�t�F�N�g���A�N�e�B�u
    }

    /// <summary>�̗͂����Z�b�g</summary>
    public void HealthReSet()
    {
        if (maxHealth == default) maxHealth = health;//�ő�̗͂�0�Ȃ�/�ő�̗͂��X�V

        health = maxHealth;                          //�ő�̗͂���
    }

    /// <summary>Health��1��</summary>
    public void HealthRecovery()
    {
        if (health < maxHealth) health++;//���݂̗̑͂��ő�̗͂�艺�Ȃ�̗͂� + 1
    }

    /// <summary>���S���o</summary>
    /// <param name="c">���o�I�������s����R�[���o�b�N</param>
    public IEnumerator DeathEffect(UnityAction c)
    {
        deathEffectObj.SetActive(true);                                                                //���S�G�t�F�N�g���A�N�e�B�u

        yield return effectWait ??= new(AudioScript.I.TankAudio.Dictionary[TankClip.Explosion].Clip.length);//�ҋ@

        deathEffectObj.SetActive(false);                                                               //���S�G�t�F�N�g���A�N�e�B�u

        c?.Invoke();                                                                                   //�R�[���o�b�N�����s
    }

    /// <summary>�_���[�W����</summary>
    /// <param name="hit">�ڐG�����R���C�_�[</param>
    /// <param name="damage">�_���[�W���o</param>
    public IEnumerator Hit(Collider2D hit, Func<float, IEnumerator> damage)
    {
        if (!isNoDamage && damageTags.Any((v) => hit.CompareTag(v)))      //�m�[�_���[�W����Ȃ� ���� �_���[�W���󂯂�^�O��
        {
            health--;                                                     //�̗� - 1

            var audio = AudioScript.I.TankAudio;                          //�^���NAudio

            if (health == default)                                        //�̗͂�0�Ȃ�
            {
                OnHealthGone?.Invoke();                                   //�̗͂�0�ɂȂ����ێ��s����֐������s

                audio.Dictionary[TankClip.Explosion].PlayOneShot(damageSource);//����SE���Đ�
            }
            else if (health > default(int))                               //�̗͂�0����Ȃ�
            {
                isNoDamage = true;                                        //�_���[�W���󂯂Ȃ���Ԃ���true

                var clip = audio.Dictionary[TankClip.Damage];                  //�_���[�W�N���b�v

                clip.PlayOneShot(damageSource);                           //�_���[�WSE���Đ�

                yield return damage.Invoke(clip.Clip.length);             //�_���[�W���o

                isNoDamage = false;                                       //�_���[�W���󂯂Ȃ���Ԃ���false
            }
        }
    }

    /// <summary>�̗̓o�[���X�V</summary>
    public void UpdateHealthGauge()
    {
        if (maxHealth == default) maxHealth = health;                                          //�ő�̗͂�0�Ȃ�/�ő�̗͂��X�V

        float v = health / (float)maxHealth;                                                   //�̗̓Q�[�W�̒l

        greenHealthGauge.fillAmount = v;                                                       //�΃Q�[�W���X�V

        redHealthGauge.fillAmount = Mathf.Lerp(redHealthGauge.fillAmount, v, redHealthBarLerp);//�ԃQ�[�W���Ԓl�ōX�V
    }
}
