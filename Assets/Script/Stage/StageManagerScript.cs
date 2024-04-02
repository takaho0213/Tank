using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Tank
{
    /// <summary>ステージの管理</summary>
    public class StageManagerScript : MonoBehaviour
    {
        /// <summary>ステージ終了UI</summary>
        [SerializeField, LightColor] private StageEndUIScript StageEndUI;

        /// <summary>スコアUI</summary>
        [SerializeField, LightColor] private ScoreUIScript ScoreUI;

        /// <summary>アイキャッチ</summary>
        [SerializeField, LightColor] private StageEyeCatchScript EyeCatch;

        /// <summary>プレイヤータンク</summary>
        [SerializeField, LightColor] private TankPlayerScript Player;

        /// <summary>エネミーのプールリスト</summary>
        [SerializeField] private PoolList<TankEnemyScript> EnemyList;

        /// <summary>弾のプールリスト</summary>
        [SerializeField] private PoolList<BulletScript> BulletList;

        /// <summary>全ステージ</summary>
        [SerializeField] private StageScript[] Stages;

        /// <summary>ステージ開始時の待機時間</summary>
        [SerializeField] private float StartWaitTime;

        /// <summary>フレームレート</summary>
        [SerializeField, ReadOnly] private int FrameRate;

        /// <summary>現在のステージ</summary>
        private StageScript CurrentStage;

        /// <summary>ステージ開始時の待機時間</summary>
        private Interval StageStartWaitInterval;

        /// <summary>前ステージクリアタイム計測用</summary>
        private Interval AllStageClearTime;

        /// <summary>終了条件</summary>
        private System.Func<bool> IsBreak;

        /// <summary>現在のステージ数</summary>
        private int StageNumber;

        /// <summary>タンクが動けない状態か</summary>
        private bool isNotMove;

        /// <summary>ステージのAudio</summary>
        private AudioInfo<StageClip> Audio => AudioScript.I.StageAudio;

        /// <summary>タンクが動けない状態か</summary>
        public bool IsNotMove => EyeCatch.IsRun || !StageStartWaitInterval.IsOver || isNotMove;

        /// <summary>ゲームクリア時実行</summary>
        public UnityAction OnGameClear { get; set; }

        private void Start()
        {
            Application.targetFrameRate = FrameRate;                        //フレームレートを固定

            IsBreak = () => !Audio.Audios[StageClip.Clear].Source.isPlaying;//終了条件を代入

            AllStageClearTime = new Interval();                             //Intervalをインスタンス化

            StageStartWaitInterval = new(Time : StartWaitTime);             //Intervalをインスタンス化

            Player.Init(BulletList.Pool, PlayerDeath);                      //プレイヤーの関数をセット

            CurrentStage = Stages[default];                                 //現在のステージを代入
        }

        /// <summary>リスタートのフェードイン時実行</summary>
        public void OnReStartFadeIn()
        {
            AllStageClearTime.ReSet();//スコアをリセット

            EyeCatch.Display();       //アイキャッチを表示

            Player.ReStart();         //リスタート

            StageReSet();             //ステージリセット

            StageNumber = default;    //現在のステージ数を0にする
        }

        /// <summary>リスタートのフェードアウト時実行</summary>
        public void OnReStartFadeOut() => EyeCatchFade(Generate);//アイキャッチをフェードし、フェードイン時Generate関数を実行

        /// <summary>プレイヤー死亡時に実行</summary>
        /// <param name="isNoLife">ライフが残っていないか</param>
        private IEnumerator PlayerDeath(bool isNoLife)
        {
            if (isNotMove) yield break;                                          //動かせない状態なら/終了

            var audio = AudioScript.I.StageAudio.Audios[StageClip.PlayerDeath];  //プレイヤー死亡Audio

            audio.Play();                                                        //SEを再生

            isNotMove = true;                                                    //動けない状態かをtrue

            var type = isNoLife ? UI.GameOver : UI.Retry;                        //表示UI

            yield return StageEndUI.Display(type, () => !audio.Source.isPlaying);//UIを表示

            EyeCatchFade(isNoLife ? PlayerGameOver : PlayerRetry);               //アイキャッチをフェードし、フェードイン時(ライフが無ければゲームオーバー : あればリトライ)関数を実行

            isNotMove = false;                                                   //動けない状態かをfalse
        }

        /// <summary>Playerがリトライした際実行</summary>
        private void PlayerRetry()
        {
            Player.Retry();//プレイヤーをリトライ

            StageReSet();  //現在のステージをリセット

            Generate();    //現在のステージを生成
        }

        /// <summary>Playerがゲームオーバーした際実行</summary>
        private void PlayerGameOver()
        {
            StageNumber = default;    //現在のステージ数を0にする

            PlayerRetry();            //プレイヤーをリトライ
        }

        /// <summary>アイキャッチをフェードする関数</summary>
        /// <param name="c">フェードイン時実行する関数</param>
        private void EyeCatchFade(UnityAction c)
        {
            Audio.Audios[StageClip.BGM].Source.Stop();         //BGMを停止

            c += () => Audio.Audios[StageClip.EyeCatch].Play();//アイキャッチSEを再生する関数を加算代入

            EyeCatch.Run(c, StageStart);                       //フェード開始 フェードインした際引数の関数を実行 フェードアウトした際StageStart関数を実行
        }

        /// <summary>ステージを開始した際実行</summary>
        private void StageStart()
        {
            StageStartWaitInterval.ReSet();    //インターバルをリセット

            Audio.Audios[StageClip.BGM].Play();//BGMを再生

            isNotMove = false;                 //動けない状態かをfalse
        }

        /// <summary>次のステージへ移行</summary>
        private void NextStage()
        {
            if (Player.IsAddLife(++StageNumber))//ライフを増やすステージ数なら
            {
                Player.AddLife();               //プレイヤーのライフを増やす
            }

            Player.HealthRecovery();            //体力を回復

            StageReSet();                       //現在のステージをリセット

            Generate();                         //現在のステージを生成
        }

        /// <summary>Enemyをすべて倒した際実行</summary>
        private IEnumerator OnAllEnemysDeath()
        {
            if (isNotMove) yield break;                                     //動けない状態なら/終了

            isNotMove = true;                                               //動けない状態かをtrue

            Audio.Audios[StageClip.BGM].Source.Stop();                      //BGMを停止

            int number = StageNumber + 1;                                   //表示ステージ数

            bool isAllClear = number >= Stages.Length;                      //現在のステージ数 + 1が全ステージ数以上なら

            if (isAllClear) Audio.Play(StageClip.AllClear);                 //オールクリアSEを再生
            else Audio.Play(StageClip.Clear);                               //クリアSEを再生

            var type = isAllClear ? UI.AllClear : UI.Clear;                 //表示UI

            yield return StageEndUI.Display(type, IsBreak);                 //UIを表示

            if (isAllClear)                                                 //全クリなら
            {
                yield return ScoreUI.Display(AllStageClearTime.ElapsedTime);//スコアUIを表示

                OnGameClear.Invoke();                                       //ゲームクリア時実行する関数を実行
            }
            else EyeCatchFade(NextStage);                                   //アイキャッチをフェードし、フェードイン時NextStage関数を実行
        }

        /// <summary>現在のステージを生成</summary>
        private void Generate()
        {
            CurrentStage = Stages[StageNumber];                                              //現在のステージを代入

            CurrentStage.Generate(EnemyList.Pool, BulletList.Pool, Player, OnAllEnemysDeath);//現在のステージを生成

            EyeCatch.StageText = (StageNumber + 1).ToString();                               //ステージテキストを代入

            EyeCatch.PlayerLifeText = Player.Life.ToString();                                //プレイヤーのライフテキストを代入
        }

        /// <summary>現在のステージをリセット</summary>
        private void StageReSet()
        {
            Player.IsActive = false;                        //プレイヤーを非アクティブ

            CurrentStage.Clear(EnemyList.List);             //現在のステージをクリア

            foreach (var b in BulletList.List) b.Inactive();//弾のプールリスト分繰り返す/弾を非アクティブ
        }
    }
}
