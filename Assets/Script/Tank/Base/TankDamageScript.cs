using System;
using System.Linq;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TankDamageScript : MonoBehaviour
{
    /// <summary>赤の体力ゲージ</summary>
    [SerializeField, LightColor] private Image redHealthGauge;

    /// <summary>緑の体力ゲージ</summary>
    [SerializeField, LightColor] private Image greenHealthGauge;

    /// <summary>死亡した際のエフェクトオブジェクト</summary>
    [SerializeField, LightColor] private GameObject deathEffectObj;

    /// <summary>被弾SEを再生するAudioSource</summary>
    [SerializeField, LightColor] private AudioSource damageSource;

    /// <summary>体力</summary>
    [SerializeField] private int health;

    /// <summary>赤体力ゲージの補間値</summary>
    [SerializeField, Range01] private float redHealthBarLerp;

    /// <summary>ダメージを受けるタグ</summary>
    [SerializeField, Tag] private string[] damageTags;

    /// <summary>最大体力</summary>
    private int maxHealth;

    /// <summary>エフェクト表示時間</summary>
    private WaitForSeconds effectWait;

    /// <summary>ダメージを受けない状態か</summary>
    private bool isNoDamage;

    /// <summary>体力が0になった際実行</summary>
    public UnityAction OnHealthGone { get; set; }

    /// <summary>死亡しているか</summary>
    public bool IsDeath => health <= default(int);

    /// <summary>エネミーの情報をセット</summary>
    /// <param name="i">エネミーの情報</param>
    public void SetInfo(TankEnemyInfoScript i)
    {
        maxHealth = health = i.Health;  //体力をセット

        deathEffectObj.SetActive(false);//死亡エフェクトを非アクティブ
    }

    /// <summary>体力をリセット</summary>
    public void HealthReSet()
    {
        if (maxHealth == default) maxHealth = health;//最大体力が0なら/最大体力を更新

        health = maxHealth;                          //最大体力を代入
    }

    /// <summary>Healthを1回復</summary>
    public void HealthRecovery()
    {
        if (health < maxHealth) health++;//現在の体力が最大体力より下なら体力を + 1
    }

    /// <summary>死亡演出</summary>
    /// <param name="c">演出終了時実行するコールバック</param>
    public IEnumerator DeathEffect(UnityAction c)
    {
        deathEffectObj.SetActive(true);                                                                //死亡エフェクトをアクティブ

        yield return effectWait ??= new(AudioScript.I.TankAudio.Dictionary[TankClip.Explosion].Clip.length);//待機

        deathEffectObj.SetActive(false);                                                               //死亡エフェクトを非アクティブ

        c?.Invoke();                                                                                   //コールバックを実行
    }

    /// <summary>ダメージ処理</summary>
    /// <param name="hit">接触したコライダー</param>
    /// <param name="damage">ダメージ演出</param>
    public IEnumerator Hit(Collider2D hit, Func<float, IEnumerator> damage)
    {
        if (!isNoDamage && damageTags.Any((v) => hit.CompareTag(v)))      //ノーダメージじゃない かつ ダメージを受けるタグか
        {
            health--;                                                     //体力 - 1

            var audio = AudioScript.I.TankAudio;                          //タンクAudio

            if (health == default)                                        //体力が0なら
            {
                OnHealthGone?.Invoke();                                   //体力が0になった際実行する関数を実行

                audio.Dictionary[TankClip.Explosion].PlayOneShot(damageSource);//爆発SEを再生
            }
            else if (health > default(int))                               //体力が0より上なら
            {
                isNoDamage = true;                                        //ダメージを受けない状態かをtrue

                var clip = audio.Dictionary[TankClip.Damage];                  //ダメージクリップ

                clip.PlayOneShot(damageSource);                           //ダメージSEを再生

                yield return damage.Invoke(clip.Clip.length);             //ダメージ演出

                isNoDamage = false;                                       //ダメージを受けない状態かをfalse
            }
        }
    }

    /// <summary>体力バーを更新</summary>
    public void UpdateHealthGauge()
    {
        if (maxHealth == default) maxHealth = health;                                          //最大体力が0なら/最大体力を更新

        float v = health / (float)maxHealth;                                                   //体力ゲージの値

        greenHealthGauge.fillAmount = v;                                                       //緑ゲージを更新

        redHealthGauge.fillAmount = Mathf.Lerp(redHealthGauge.fillAmount, v, redHealthBarLerp);//赤ゲージを補間値で更新
    }
}
