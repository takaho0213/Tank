using System.Collections;

using UnityEngine;

/// <summary>プレイヤーのタンク</summary>
public class TankPlayerScript : TankScript
{
    /// <summary>入力移動</summary>
    [SerializeField, LightColor] private TankInputMoveScript inputMove;

    /// <summary>メインカメラ</summary>
    [SerializeField, LightColor] private Camera mainCamera;

    /// <summary>何ステージクリアしたら残機を得るか</summary>
    [SerializeField] private int addLifeCount;

    /// <summary>ライフ(残機)</summary>
    public int Life { get; private set; }

    /// <summary>残機を増やすか判定する</summary>
    /// <param name="count">現在ステージ数</param>
    /// <returns>残機を増やすか</returns>
    public bool IsAddLife(int count) => count % addLifeCount == default;

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
    public void HealthRecovery() => damage.HealthRecovery();//体力を1回復

    /// <summary>(プール弾, リトライ, ゲームオーバー)関数をセット</summary>
    /// <param name="pool">プールしてある弾を取得する関数</param>
    /// <param name="onDeath">ゲームオーバーになった祭実行する関数</param>
    public void Init(System.Func<BulletScript> pool, System.Func<bool, IEnumerator> onDeath)
    {
        cannon.BulletPool = pool;                                            //プールしてある弾を取得する関数をセット

        base.onDeath = () => StartCoroutine(onDeath.Invoke(Life <= default(int)));//死亡した際実行する関数をセット
    }

    /// <summary>処理をまとめた関数</summary>
    private void Move()
    {
        line.CreateLine();

        parts.TargetLookAt(Input.mousePosition - mainCamera.WorldToScreenPoint(inputMove.Pos));//マウスカーソルの方向へタレットを向ける

        inputMove.InputMove();                                                                 //入力移動

        if (Input.GetMouseButton(default) && shootInterval) cannon.Shoot();                    //右クリック かつ 発射間隔なら/発射する
    }

    /// <summary>リトライ</summary>
    public void Retry()
    {
        damage.HealthReSet();                 //体力をリセット

        fillColor.SetColor();                 //色をセット

        if (Life > default(int)) ReMoveLife();//ライフが0以上なら/ライフを - 1
    }

    /// <summary>リスタート</summary>
    public void ReStart()
    {
        damage.HealthReSet();                 //体力をリセット

        fillColor.SetColor();                 //色をセット

        Life = default;                       //ライフをリセット
    }

    private void FixedUpdate()
    {
        damage.UpdateHealthGauge();                 //体力バーを更新

        parts.CaterpillarLookAt(inputMove.Velocity);//キャタピラを回転

        if (IsNotMove)                              //移動できない状態なら
        {
            inputMove.Stop();                       //移動を停止

            return;                                 //終了
        }

        Move();                                     //処理をまとめた関数を呼び出す
    }
}
