using System;
using System.Linq;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Tank
{
    /// <summary>ベースのタンク</summary>
    public abstract class TankScript : MonoBehaviour
    {
        /// <summary>マネージャー</summary>
        [SerializeField, LightColor] protected StageManagerScript StageManager;

        /// <summary>自身のObject</summary>
        [SerializeField, LightColor] protected GameObject Obj;

        /// <summary>自身のTransform</summary>
        [SerializeField, LightColor] protected Transform Trafo;

        /// <summary>武器</summary>
        [SerializeField] protected TankCannon Weapon;

        /// <summary>タレット キャタピラー</summary>
        [SerializeField] protected TankMovingParts Parts;

        /// <summary>ダメージ処理</summary>
        [SerializeField] protected TankDamage Damage;

        /// <summary>色</summary>
        [SerializeField] protected TankFillColor FillColor;

        /// <summary>発射Interval</summary>
        [SerializeField] protected Interval ShootInterval;

        /// <summary>死亡演出が終わった際実行</summary>
        protected UnityAction OnDeath;

        /// <summary>移動できない状態か</summary>
        protected bool IsNotMove => StageManager.IsNotMove || Damage.IsDeath;

        /// <summary>アクティブか</summary>
        public bool IsActive
        {
            get => Obj.activeSelf;
            set => Obj.SetActive(value);
        }

        protected virtual void OnEnable() => FillColor.SetColor();//アクティブになった際/色をセット

        protected virtual void Awake()
        {
            FillColor.SetColor();              //色をセット

            Damage.OnHealthGone = OnHealthGone;//体力が0になった際実行する関数をセット
        }

        /// <summary>positionとrotationをセット</summary>
        /// <param name="trafo">セットするpositionとrotation</param>
        public void SetPosAndRot(Transform trafo)
        {
            Trafo.position = trafo.position;   //位置を代入
            Parts.SetRotation = trafo.rotation;//角度を代入
        }

        /// <summary>Healthが0になった際実行</summary>
        protected virtual void OnHealthGone()
        {
            FillColor.SetGray();                        //色をグレーにする

            StartCoroutine(Damage.DeathEffect(OnDeath));//死亡演出を開始
        }

        protected virtual void OnTriggerEnter2D(Collider2D hit)
        {
            if (IsNotMove) return;                                 //移動できない状態なら

            StartCoroutine(Damage.Hit(hit, FillColor.DamageColor));//ダメージエフェクトを開始
        }
    }

    /// <summary>タンクの大砲</summary>
    [System.Serializable]
    public class TankCannon
    {
        /// <summary>弾を撃つSEを再生するAudioSource</summary>
        [SerializeField, LightColor] private AudioSource ShootSource;

        /// <summary>弾の情報</summary>
        [SerializeField] private BulletInfo BulletInfo;

        /// <summary>プールしてある弾を取得する関数</summary>
        public Func<BulletScript> BulletPool { get; set; }

        /// <summary>弾速</summary>
        public float BulletSpeed => BulletInfo.Speed;

        /// <summary>エネミーの情報をセットする関数</summary>
        /// <param name="i">エネミーの情報</param>
        public void SetInfo(TankEnemyInfoScript i)
        {
            BulletInfo.Init(i.BulletSpeed, i.BulletReflectionCount, i.FillColor);//弾の情報をセットする関数を呼び出す
        }

        /// <summary>弾を発射</summary>
        public void Shoot()
        {
            BulletPool.Invoke().Shoot(BulletInfo);                                 //プールされている弾を取得し、発射する関数の引数に弾の情報を入れ呼び出す

            AudioScript.I.TankAudio.Clips[TankClip.Shoot].PlayOneShot(ShootSource);//発射する際のSEを再生する
        }
    }

    /// <summary>タンクの移動部位(タレット : キャタピラー)</summary>
    [System.Serializable]
    public class TankMovingParts
    {
        /// <summary>タレットのTransform</summary>
        [SerializeField, LightColor] private Transform Turret;

        /// <summary>キャタピラのTran</summary>
        [SerializeField, LightColor] private Transform Caterpillar;

        /// <summary>タレット回転時の補間値</summary>
        [SerializeField, Range01] float TurretLerp;

        /// <summary>キャタピラ回転時の補間値</summary>
        [SerializeField, Range01] private float CaterpillarLerp;

        /// <summary>タレットとキャタピラのRotationをセット</summary>
        public Quaternion SetRotation { set => Caterpillar.rotation = Turret.rotation = value; }

        /// <summary>エネミーの情報をセット</summary>
        /// <param name="i">エネミーの情報</param>
        public void SetInfo(TankEnemyInfoScript i) => TurretLerp = i.TurretLerp;

        /// <summary>タレットをターゲットの方向へ向ける</summary>
        /// <param name="target">ターゲットの方向</param>
        public void TargetLookAt(Vector3 target)
        {
            Turret.LerpRotation(target, TurretLerp, Vector3.forward);//タレットをターゲットの方向への補間値を代入
        }

        /// <summary>キャタピラーを移動方向に向ける</summary>
        /// <param name="velocity">移動ベクトル</param>
        public void CaterpillarLookAt(Vector2 velocity)
        {
            if (velocity != Vector2.zero)                                            //移動ベクトルが(0, 0)以外なら
            {
                Caterpillar.LerpRotation(velocity, CaterpillarLerp, Vector3.forward);//キャタピラーを移動方向への補間値を代入
            }
        }
    }

    /// <summary>タンクのダメージ処理</summary>
    [System.Serializable]
    public class TankDamage
    {
        /// <summary>赤の体力ゲージ</summary>
        [SerializeField, LightColor] private Image RedHealthGauge;

        /// <summary>緑の体力ゲージ</summary>
        [SerializeField, LightColor] private Image GreenHealthGauge;

        /// <summary>死亡した際のエフェクトオブジェクト</summary>
        [SerializeField, LightColor] private GameObject DeathEffectObj;

        /// <summary>被弾SEを再生するAudioSource</summary>
        [SerializeField, LightColor] private AudioSource DamageSource;

        /// <summary>体力</summary>
        [SerializeField] private int Health;

        /// <summary>赤体力ゲージの補間値</summary>
        [SerializeField, Range01] private float RedHealthBarLerp;

        /// <summary>ダメージを受けるタグ</summary>
        [SerializeField, Tag] private string[] DamageTags;

        /// <summary>最大体力</summary>
        private int MaxHealth;

        /// <summary>エフェクト表示時間</summary>
        private WaitForSeconds EffectWait;

        /// <summary>ダメージを受けない状態か</summary>
        private bool IsNoDamage;

        /// <summary>体力が0になった際実行</summary>
        public UnityAction OnHealthGone { get; set; }

        /// <summary>死亡しているか</summary>
        public bool IsDeath => Health <= default(int);

        /// <summary>エネミーの情報をセット</summary>
        /// <param name="i">エネミーの情報</param>
        public void SetInfo(TankEnemyInfoScript i)
        {
            MaxHealth = Health = i.Health;  //体力をセット

            DeathEffectObj.SetActive(false);//死亡エフェクトを非アクティブ
        }

        /// <summary>体力をリセット</summary>
        public void HealthReSet()
        {
            if (MaxHealth == default) MaxHealth = Health;//最大体力が0なら/最大体力を更新

            Health = MaxHealth;                          //最大体力を代入
        }

        /// <summary>Healthを1回復</summary>
        public void HealthRecovery()
        {
            if (Health < MaxHealth) Health++;//現在の体力が最大体力より下なら体力を + 1
        }

        /// <summary>死亡演出</summary>
        /// <param name="c">演出終了時実行するコールバック</param>
        public IEnumerator DeathEffect(UnityAction c)
        {
            DeathEffectObj.SetActive(true);                                                                //死亡エフェクトをアクティブ

            yield return EffectWait ??= new(AudioScript.I.TankAudio.Clips[TankClip.Explosion].Clip.length);//待機

            DeathEffectObj.SetActive(false);                                                               //死亡エフェクトを非アクティブ

            c?.Invoke();                                                                                   //コールバックを実行
        }

        /// <summary>ダメージ処理</summary>
        /// <param name="hit">接触したコライダー</param>
        /// <param name="damage">ダメージ演出</param>
        public IEnumerator Hit(Collider2D hit, Func<float, IEnumerator> damage)
        {
            if (!IsNoDamage && DamageTags.Any((v) => hit.CompareTag(v)))      //ノーダメージじゃない かつ ダメージを受けるタグか
            {
                Health--;                                                     //体力 - 1

                var audio = AudioScript.I.TankAudio;                          //タンクAudio

                if (Health == default)                                        //体力が0なら
                {
                    OnHealthGone?.Invoke();                                   //体力が0になった際実行する関数を実行

                    audio.Clips[TankClip.Explosion].PlayOneShot(DamageSource);//爆発SEを再生
                }
                else if (Health > default(int))                               //体力が0より上なら
                {
                    IsNoDamage = true;                                        //ダメージを受けない状態かをtrue

                    var clip = audio.Clips[TankClip.Damage];                  //ダメージクリップ

                    clip.PlayOneShot(DamageSource);                           //ダメージSEを再生

                    yield return damage.Invoke(clip.Clip.length);           　 //ダメージ演出

                    IsNoDamage = false;                                       //ダメージを受けない状態かをfalse
                }
            }
        }

        /// <summary>体力バーを更新</summary>
        public void UpdateHealthGauge()
        {
            if (MaxHealth == default) MaxHealth = Health;                                          //最大体力が0なら/最大体力を更新

            float v = Health / (float)MaxHealth;                                                   //体力ゲージの値

            GreenHealthGauge.fillAmount = v;                                                       //緑ゲージを更新

            RedHealthGauge.fillAmount = Mathf.Lerp(RedHealthGauge.fillAmount, v, RedHealthBarLerp);//赤ゲージを補間値で更新
        }
    }

    /// <summary>タンクの色</summary>
    [System.Serializable]
    public class TankFillColor
    {
        /// <summary>色を変えるスプライト配列</summary>
        [SerializeField, LightColor] private SpriteRenderer[] FillSprites;

        /// <summary>タンクの色</summary>
        [SerializeField] private Color FillColor;

        /// <summary>待機時間</summary>
        private WaitForSeconds Wait;

        /// <summary>色</summary>
        public Color Color => FillColor;

        /// <summary>エネミーの情報をセット</summary>
        /// <param name="i">エネミーの情報</param>
        public void SetInfo(TankEnemyInfoScript i) => SetColor(FillColor = i.FillColor);

        /// <summary>色をグレーにセット</summary>
        public void SetGray() => SetColor(Color.gray);

        /// <summary>色をセット</summary>
        public void SetColor() => SetColor(FillColor);

        /// <summary>色をセット</summary>
        /// <param name="color">セットする色</param>
        public void SetColor(Color color)
        {
            foreach (var s in FillSprites)//FillSpritesの要素数分繰り返す
            {
                s.color = color;          //引数の値を代入
            }
        }

        /// <summary>ダメージ演出</summary>
        /// <param name="waitTime">待機時間</param>
        public IEnumerator DamageColor(float waitTime)
        {
            SetGray();                                         //色をグレーにする

            yield return Wait ??= new WaitForSeconds(waitTime);//待機

            SetColor();                                        //色をセット
        }
    }
}
