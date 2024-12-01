using UnityEngine;
using System.Collections;

public class BulletEffectScript : MonoBehaviour
{
    /// <summary>爆発エフェクトオブジェクト</summary>
    [SerializeField, LightColor] private GameObject explosionObj;

    /// <summary>ジェットエフェクトオブジェクト</summary>
    [SerializeField, LightColor] private GameObject jetObj;

    /// <summary>爆発AudioSource</summary>
    [SerializeField, LightColor] private AudioSource explosionSource;

    /// <summary>ジェットエフェクトを出現させる速度</summary>
    [SerializeField] private float jetEffectActiveSpeed;

    /// <summary>停止時間</summary>
    private WaitForSeconds wait;

    public bool IsActive
    {
        get => explosionObj.activeSelf;
        set => explosionObj.SetActive(value);
    }

    public IEnumerator ExplosionEffect()
    {
        if (IsActive) yield break;

        IsActive = true;                                                      //爆発エフェクトオブジェクトをアクティブ

        var clip = AudioScript.I.BulletAudio[BulletClip.Explosion];//爆発SEクリップ

        clip.PlayOneShot(explosionSource);                                    //爆発SEを再生

        yield return wait ??= new WaitForSeconds(clip.Length);           //SEの秒数分停止
    }

    public void JetSetActive(float speed)
    {
        jetObj.SetActive(speed >= jetEffectActiveSpeed);
    }
}
