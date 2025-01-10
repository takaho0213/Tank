using UnityEngine;
using System.Collections;

/// <summary>�e�̃G�t�F�N�g</summary>
public class BulletEffectScript : MonoBehaviour
{
    /// <summary>�����G�t�F�N�g�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject explosionObj;

    /// <summary>�W�F�b�g�G�t�F�N�g�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject jetObj;

    /// <summary>����AudioSource</summary>
    [SerializeField, LightColor] private AudioSource explosionSource;

    /// <summary>�W�F�b�g�G�t�F�N�g���o�������鑬�x</summary>
    [SerializeField] private float jetEffectActiveSpeed;

    /// <summary>��~����</summary>
    private WaitForSeconds wait;

    /// <summary>�A�N�e�B�u��</summary>
    public bool IsActive
    {
        get => explosionObj.activeSelf;
        set => explosionObj.SetActive(value);
    }

    /// <summary>�����G�t�F�N�g��\��</summary>
    public IEnumerator ExplosionEffect()
    {
        if (IsActive) yield break;                                 //�A�N�e�B�u�Ȃ�/�I��

        IsActive = true;                                           //�����G�t�F�N�g�I�u�W�F�N�g���A�N�e�B�u

        var clip = AudioScript.I.BulletAudio[BulletClip.Explosion];//����SE�N���b�v

        clip.PlayOneShot(explosionSource);                         //����SE���Đ�

        yield return wait ??= new WaitForSeconds(clip.Length);     //SE�̕b������~
    }

    /// <summary>�W�F�b�g�G�t�F�N�g�̃A�N�e�B�u��Ԃ��Z�b�g</summary>
    /// <param name="speed">�e��</param>
    public void JetSetActive(float speed)
    {
        jetObj.SetActive(speed >= jetEffectActiveSpeed);//�e���ɂ���ăA�N�e�B�u��Ԃ�ύX
    }
}
