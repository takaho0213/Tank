using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Tank
{
    /// <summary>プレイヤーのタンク</summary>
    public class TankPlayerScript : TankScript
    {
        /// <summary>入力移動</summary>
        [SerializeField] private TankInputMove InputMove;

        /// <summary>メインカメラ</summary>
        [SerializeField, LightColor] private Camera MainCamera;

        /// <summary>何ステージクリアしたら残機を得るか</summary>
        [SerializeField] private int AddLifeCount;

        /// <summary>ライフ(残機)</summary>
        public int Life { get; private set; }

        /// <summary>残機を増やすか判定する</summary>
        /// <param name="count">現在ステージ数</param>
        /// <returns>残機を増やすか</returns>
        public bool IsAddLife(int count) => count % AddLifeCount == default;

        /// <summary>残機を1増やす</summary>
        public void AddLife()
        {
            Life++;                                          //ライフを + 1

            AudioScript.I.StageAudio.Play(StageClip.LifeAdd);//ライフ追加SEを再生
        }

        /// <summary>残機を1減らす</summary>
        private void ReMoveLife()
        {
            if (Life > default(int))                                //ライフが0より上なら
            {
                Life--;                                             //ライフを - 1

                AudioScript.I.StageAudio.Play(StageClip.LifeReMove);//ライフ減少SEを再生
            }
        }

        /// <summary>体力を1回復</summary>
        public void HealthRecovery() => Damage.HealthRecovery();//体力を1回復

        /// <summary>(プール弾, リトライ, ゲームオーバー)関数をセット</summary>
        /// <param name="pool">プールしてある弾を取得する関数</param>
        /// <param name="onRetry">リトライした際実行する関数</param>
        /// <param name="onGameOver">ゲームオーバーになった祭実行する関数</param>
        public void Init(System.Func<BulletScript> pool, System.Func<bool, IEnumerator> onDeath)
        {
            Weapon.BulletPool = pool;                                            //プールしてある弾を取得する関数をセット

            OnDeath = () => StartCoroutine(onDeath.Invoke(Life <= default(int)));//死亡した際実行する関数をセット
        }

        /// <summary>処理をまとめた関数</summary>
        private void Move()
        {
            Parts.TargetLookAt(Input.mousePosition - MainCamera.WorldToScreenPoint(InputMove.Pos));//マウスカーソルの方向へタレットを向ける

            InputMove.InputMove();                                                                 //入力移動

            if (Input.GetMouseButton(default) && ShootInterval) Weapon.Shoot();                    //右クリック かつ 発射間隔なら/発射する
        }

        /// <summary>リトライ</summary>
        public void Retry()
        {
            Damage.HealthReSet();                 //体力をリセット

            FillColor.SetColor();                 //色をセット

            if (Life > default(int)) ReMoveLife();//ライフが0以上なら/ライフを - 1
        }

        /// <summary>リスタート</summary>
        public void ReStart()
        {
            Damage.HealthReSet();                 //体力をリセット

            FillColor.SetColor();                 //色をセット

            Life = default;                       //ライフをリセット
        }

        private void FixedUpdate()
        {
            Damage.UpdateHealthGauge();                 //体力バーを更新

            Parts.CaterpillarLookAt(InputMove.Velocity);//キャタピラを回転

            if (IsNotMove)                              //移動できない状態なら
            {
                InputMove.Stop();                       //移動を停止

                return;                                 //終了
            }

            Move();                                     //処理をまとめた関数を呼び出す
        }
    }

    /// <summary>タンクの入力移動</summary>
    [System.Serializable]
    public class TankInputMove
    {
        /// <summary>InputModule</summary>
        [SerializeField, LightColor] private StandaloneInputModule InputModule;

        /// <summary>移動に使うRigidbody</summary>
        [SerializeField, LightColor] private Rigidbody2D Ribo;

        /// <summary>移動SEを再生するAudioSource</summary>
        [SerializeField, LightColor] private AudioSource MoveSource;

        /// <summary>移動速度</summary>
        [SerializeField] private float MoveSpeed;

        /// <summary>SEを再生するインターバル</summary>
        private Interval SEInterval;

        /// <summary>入力ベクトル</summary>
        private Vector2 InputVector;

        /// <summary>場所</summary>
        public Vector2 Pos => Ribo.position;

        /// <summary>移動ベクトル</summary>
        public Vector3 Velocity => Ribo.velocity;

        /// <summary>移動停止</summary>
        public void Stop() => Ribo.velocity = Vector2.zero;

        /// <summary>入力移動</summary>
        public void InputMove()
        {
            InputVector.x = Input.GetAxisRaw(InputModule.horizontalAxis);//入力ベクトルXを代入
            InputVector.y = Input.GetAxisRaw(InputModule.verticalAxis);  //入力ベクトルYを代入

            Ribo.velocity = InputVector * MoveSpeed;                     //(入力ベクトル * 移動速度)を移動ベクトルに代入

            if (Ribo.velocity != Vector2.zero)                           //移動ベクトルが(0, 0)以外なら
            {
                var c = AudioScript.I.TankAudio.Clips[TankClip.Move];    //移動SEクリップ

                SEInterval ??= new(c.Clip.length / MoveSpeed, true);     //nullなら代入

                if (SEInterval) MoveSource.PlayOneShot(c.Clip, c.Volume);//SE再生間隔を越えていたら/SEを再生
            }
        }
    }
}
