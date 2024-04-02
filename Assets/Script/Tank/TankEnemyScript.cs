using UnityEngine;
using System.Collections;

namespace Tank
{
    /// <summary>エネミーのタンク</summary>
    public class TankEnemyScript : TankScript
    {
        /// <summary>攻撃タイプ</summary>
        public enum AttackType
        {
            /// <summary>ノーマル</summary>
            Normal,
            /// <summary>偏差撃ち</summary>
            Deviation,
            /// <summary>射線にターゲットがいるなら</summary>
            Target,
            /// <summary>射線反射先にターゲットがいるなら</summary>
            TargetReflection,
            /// <summary>ランダム</summary>
            Random,
        }

        /// <summary>自動移動</summary>
        [SerializeField] private TankAutoMove AutoMove;

        /// <summary>ターゲット</summary>
        [SerializeField] private TankChasingTarget Target;

        /// <summary>サーチ</summary>
        [SerializeField] private TankSearch Search;

        /// <summary>攻撃タイプ</summary>
        [SerializeField] private AttackType Type;

        /// <summary>AttackType配列</summary>
        private readonly AttackType[] AttackTypes = (AttackType[])System.Enum.GetValues(typeof(AttackType));

        /// <summary>個体値をセット</summary>
        /// <param name="info">個体情報</param>
        /// <param name="onDeath">死亡時に実行するコールバック</param>
        public void SetInfo(TankEnemyInfoScript info, System.Func<BulletScript> pool, System.Func<IEnumerator> onDeath)
        {
            AutoMove.Stop();                                  //移動を停止

            Weapon.BulletPool = pool;                         //弾プール関数をセット

            Parts.SetInfo(info);                              //エネミーの情報をセット

            Damage.SetInfo(info);                             //エネミーの情報をセット

            FillColor.SetInfo(info);                          //エネミーの情報をセット

            Weapon.SetInfo(info);                             //エネミーの情報をセット

            AutoMove.SetInfo(info);                           //エネミーの情報をセット

            SetPosAndRot(info.PosAndRot);                     //場所と角度をセット

            ShootInterval.Time = info.ShootInterval;          //発射間隔をセット

            Type = info.AttackType;                           //攻撃タイプをセット

            OnDeath = () =>                                   //死亡した再実行する関数をセット
            {
                IsActive = false;                             //非アクティブ

                StageManager.StartCoroutine(onDeath.Invoke());//死亡時の処理を開始
            };

            IsActive = true;                                  //アクティブ

            while (Type == AttackType.Random)                                 //攻撃タイプがランダムな限り繰り返す
            {
                Type = AttackTypes[Random.Range(default, AttackTypes.Length)];//ランダムにセット
            }
        }

        /// <summary>タレットをターゲットの方向に向ける</summary>
        private void LookAt()
        {
            Vector3 pos = Type != AttackType.Deviation ? Target.Pos : Target.PredictionPos(AutoMove.Pos, Weapon.BulletSpeed);//攻撃タイプが偏差撃ちじゃなければ ? 現在のターゲットの位置 : ターゲットの移動予測先

            Parts.TargetLookAt(pos - AutoMove.Pos);                                                                          //タレットを(ターゲットの位置 - 自身の現在位置)の方向に向ける
        }

        /// <summary>レイがヒット かつ ターゲットと一致か</summary>
        private bool IsSearchRayHit() => Search.Ray(Target.Pos, out var r) && r.transform.position == Target.Pos;//レイがヒット かつ ターゲットと一致なら

        /// <summary>処理をまとめた関数</summary>
        private void Move()
        {
            var isTargetType = Type == AttackType.Target;                       //攻撃タイプがターゲットか

            var isTargetRType = Type == AttackType.TargetReflection;            //攻撃タイプがターゲット反射か

            var isTarget = (isTargetType || isTargetRType) && !IsSearchRayHit();//(攻撃タイプがターゲット or 反射) かつ レイがヒット かつ ターゲットと一致なら

            if (!isTargetRType) LookAt();                                       //攻撃タイプがターゲット反射じゃなければ/タレットをターゲットの方向に向ける

            if (isTarget)                                                       //レイにターゲットがヒットしていたら
            {
                if (isTargetRType && Search.ReflectionRay(out var hit))         //攻撃タイプがターゲット反射 かつ 反射先でターゲットがヒットしたら
                {
                    Parts.TargetLookAt(hit);                                    //タレットを反射先にヒットした際の方向に向ける
                }
                else return;                                                    //それ以外なら/終了
            }
            else if (isTargetRType) LookAt();                                   //それ以外で攻撃タイプがターゲット反射なら/タレットをターゲットの方向へ向ける

            if (ShootInterval)                                                  //発射間隔を越えていたら
            {
                Weapon.Shoot();                                                 //弾を発射

                AutoMove.VectorChange();                                        //移動ベクトルを変更
            }

            Target.LastUpdate();                                                //ターゲット位置を更新
        }

        private void FixedUpdate()
        {
            AutoMove.MoveSE();                         //移動SEを再生

            Damage.UpdateHealthGauge();                //体力UIを更新

            Parts.CaterpillarLookAt(AutoMove.Velocity);//キャタピラを回転

            if (IsNotMove)                             //移動できない状態なら
            {
                ShootInterval.ReSet();                 //インターバルをリセット

                AutoMove.Stop();                       //移動を停止

                return;                                //終了
            }

            Move();                                    //処理をまとめた関数を呼び出す
        }
    }

    /// <summary>タンクの自動移動</summary>
    [System.Serializable]
    public class TankAutoMove
    {
        /// <summary></summary>
        public enum MoveType
        {
            /// <summary>無し</summary>
            None,
            /// <summary>横</summary>
            Horizontal,
            /// <summary>縦</summary>
            Vertical,
            /// <summary>横 or 縦</summary>
            HorizontalOrVertical,
            /// <summary>横 and 縦</summary>
            HorizontalAndVertical,
            /// <summary>ランダム</summary>
            Random,
        }

        /// <summary>移動用Rigidbody</summary>
        [SerializeField, LightColor] private Rigidbody2D Ribo;

        /// <summary>移動AudioSource</summary>
        [SerializeField, LightColor] private AudioSource Source;

        /// <summary>移動タイプ</summary>
        [SerializeField] private MoveType Type;

        /// <summary>移動速度</summary>
        [SerializeField] private float Speed;

        /// <summary>移動ベクトルを正規化するか</summary>
        [SerializeField] private bool IsNormalized;

        /// <summary>移動SEの再生間隔</summary>
        private Interval SEInterval;

        /// <summary>移動タイプ配列</summary>
        private readonly MoveType[] MoveTypes = (MoveType[])System.Enum.GetValues(typeof(MoveType));

        /// <summary>ランダムに速度を返す</summary>
        private float RandomSpeed => Random.Range(-Speed, Speed);

        /// <summary>ランダムにtrue : falseを返す</summary>
        private bool RandomBool => Random.Range(default, 2) == default;

        /// <summary>自身の位置</summary>
        public Vector3 Pos => Ribo.position;

        /// <summary>移動ベクトル</summary>
        public Vector3 Velocity => Ribo.velocity;

        /// <summary>移動ベクトルの雛形</summary>
        private Vector2 MoveVector
        {
            get
            {
                var type = Type == MoveType.Random ? MoveTypes[Random.Range(default, MoveTypes.Length)] : Type;          //移動タイプがランダムなら

                return type switch                                                                                       //タイプで分岐
                {
                    MoveType.Horizontal            => new(RandomSpeed, default),                                         //(ランダム速度, 0)
                    MoveType.Vertical              => new(default, RandomSpeed),                                         //(0, ランダム速度)
                    MoveType.HorizontalOrVertical  => RandomBool ? new(RandomSpeed, default) : new(default, RandomSpeed),//ランダムbool ? (ランダム速度, 0) : (0, ランダム速度)
                    MoveType.HorizontalAndVertical => new(RandomSpeed, RandomSpeed),                                     //(ランダム速度, ランダム速度)
                    MoveType.Random                => MoveVector,                                                        //移動ベクトルの雛形
                    _                              => default,                                                           //(0, 0)
                };
            }
        }

        /// <summary>エネミーの情報をセット</summary>
        /// <param name="i">エネミーの情報</param>
        public void SetInfo(TankEnemyInfoScript i)
        {
            Type = i.MoveType;                      //移動の仕方をセット
            Speed = i.MoveSpeed;                    //移動速度をセット
            IsNormalized = i.IsMoveVectorNormalized;//移動ベクトルを正規化するかをセット
        }

        /// <summary>停止</summary>
        public void Stop() => Ribo.velocity = Vector2.zero;//移動ベクトルに(0, 0)を代入

        /// <summary>移動ベクトルを変更</summary>
        public void VectorChange()
        {
            Ribo.velocity = IsNormalized ? MoveVector.normalized * Speed : MoveVector;//移動ベクトルに(移動ベクトルを正規化するか ? 正規化した移動ベクトルの雛形 * 移動速度 : 移動ベクトルの雛形)を代入
        }

        /// <summary></summary>
        public void MoveSE()
        {
            if (Ribo.velocity != Vector2.zero)                       //移動ベクトルが(0, 0)以外なら
            {
                var c = AudioScript.I.TankAudio.Clips[TankClip.Move];//移動SEのクリップ

                SEInterval ??= new(c.Clip.length / Speed, true);     //インターバルがnullならインスタンス化

                if (SEInterval) Source.PlayOneShot(c.Clip, c.Volume);//SE再生間隔を越えていたら移動SEを再生
            }
        }
    }

    /// <summary>タンクのターゲットを追いかけるクラス</summary>
    [System.Serializable]
    public class TankChasingTarget
    {
        /// <summary>ターゲットのトランスフォーム</summary>
        [SerializeField, LightColor] private Transform Target;

        /// <summary>ターゲットの前フレームの位置</summary>
        private Vector3 TargetPosPreframe;

        /// <summary>ターゲットの位置</summary>
        public Vector3 Pos => Target.position;

        /// <summary>ターゲットの移動先を予測</summary>
        /// <param name="pos">自身の場所</param>
        /// <param name="speed">弾速</param>
        /// <returns>ターゲットの移動先</returns>
        public Vector3 PredictionPos(Vector3 pos, float speed)
        {
            Vector3 TargetPos = Target.position;                       //ターゲットの位置

            Vector3 TargetMoveDistance = TargetPos - TargetPosPreframe;//ターゲットの移動距離

            return TargetPos + TargetMoveDistance * Vector3.Distance(pos, TargetPos) / speed / Time.fixedDeltaTime;//タゲ位置 + タゲ移動距離 * (自身の位置とタゲ位置の距離) / 弾速 / デルタタイム
        }

        /// <summary>ターゲットの前フレームの位置を更新(PredictionPos()の後に呼び出す)</summary>
        public void LastUpdate() => TargetPosPreframe = Target.position;//ターゲットの位置を更新
    }

    /// <summary>タンクのターゲットを探すクラス</summary>
    [System.Serializable]
    public class TankSearch
    {
        /// <summary>発射するレイの形</summary>
        [SerializeField, LightColor] private CapsuleCollider2D RayColl;

        /// <summary>探す際回転させるトランスフォーム</summary>
        [SerializeField, LightColor] private Transform Trafo;

        /// <summary>回転させる方向</summary>
        [SerializeField] private Vector3 Angle;

        /// <summary>レイが検知するレイヤー</summary>
        [SerializeField] private LayerMask RayLayer;

        /// <summary>プレイヤーのレイヤー</summary>
        [SerializeField] private LayerMask PlayerLayer;

        /// <summary>発射するレイの形のトランスフォーム</summary>
        private Transform RayCollTrafo;

        /// <summary>カプセル型のRayを発射</summary>
        /// <param name="o">発射する座標</param>
        /// <param name="d">発射するベクトル</param>
        /// <param name="l">レイヤー</param>
        /// <returns>レイの情報</returns>
        private RaycastHit2D CapsuleRayCast(Vector2 o, Vector2 d, LayerMask l)
        {
            RayCollTrafo ??= RayColl.transform;                                          //nullなら代入

            var size = RayColl.size * RayCollTrafo.localScale;                           //サイズ

            var type = CapsuleDirection2D.Vertical;                                      //方向

            var angle = RayCollTrafo.eulerAngles.z;                                      //角度

            var ray = Physics2D.CapsuleCast(o, size, type, angle, d, Mathf.Infinity, l); //レイ

            if (ray) Debug.DrawRay(o, ray.centroid - o, Color.green);                    //レイがヒットしたらレイを可視化

            return ray;                                                                  //レイの情報を返す
        }

        /// <summary>カプセル型のRayを発射</summary>
        /// <param name="target">ターゲット座標または方向</param>
        /// <param name="hit">接触したオブジェクトの情報</param>
        /// <returns>ヒットしたか</returns>
        public bool Ray(Vector3 target, out RaycastHit2D hit)
        {
            var pos = Trafo.position;                             //自身の位置

            var direction = target - Trafo.position;              //レイの方向

            Debug.DrawRay(pos, direction, Color.blue);            //レイを可視化

            return hit = CapsuleRayCast(pos, direction, RayLayer);//ヒットしたか
        }

        /// <summary>反射するカプセル型のRayを発射</summary>
        /// <returns>反射先にヒットしたら ? 一度目のレイの方向 : (0, 0)</returns>
        private Vector3 ReflectionCapsuleRay()
        {
            var d = Trafo.up;                                      //レイの方向

            var ray1 = CapsuleRayCast(Trafo.position, d, RayLayer);//レイの情報

            bool isRay2Hit = CapsuleRayCast(ray1.point, Vector2.Reflect(d, ray1.normal), PlayerLayer);//ヒットした場所からレイを発射しヒットしたか

            return isRay2Hit ? d : Vector3.zero;                   //ヒットしたら ? 一度目のレイの方向 : (0, 0)
        }

        /// <summary>角度を変えながら反射するカプセル型のRayを発射</summary>
        /// <param name="hit">ヒットした際の方向</param>
        /// <returns>反射先にオブジェクトがあるか</returns>
        public bool ReflectionRay(out Vector3 hit)
        {
            Trafo.Rotate(Angle);         //回転

            hit = ReflectionCapsuleRay();//反射レイを発射

            return hit != Vector3.zero;  //反射先にオブジェクトがあるかを返す
        }
    }
}
