using UnityEngine;
using System.Collections;

/// <summary>プレイヤーのタンク</summary>
public class TankPlayerScript : TankScript
{
    /// <summary>入力移動</summary>
    [SerializeField, LightColor] private TankInputMoveScript inputMove;

    /// <summary>リトライするためのライフ</summary>
    [SerializeField, LightColor] private TankLifeScript life;

    /// <summary>メインカメラ</summary>
    [SerializeField, LightColor] private Camera mainCamera;

    /// <summary>ライフ</summary>
    public TankLifeScript Life => life;

    protected override TankMoveScript BaseMove => inputMove;

    /// <summary>体力を最大まで回復</summary>
    public void HealthRecovery() => damage.HealthReSet();

    /// <summary>(プール弾, リトライ, ゲームオーバー)関数をセット</summary>
    /// <param name="pool">プールしてある弾を取得する関数</param>
    /// <param name="onDeath">ゲームオーバーになった祭実行する関数</param>
    public void Init(System.Func<BulletScript> pool, System.Func<bool, IEnumerator> onDeath)
    {
        cannon.BulletPool = pool;                                                           //プールしてある弾を取得する関数をセット

        this.onDeath = () => StartCoroutine(onDeath.Invoke(life.LifeCount <= default(int)));//死亡した際実行する関数をセット
    }

    /// <summary>体力,色をセットする</summary>
    public void OnRetry()
    {
        damage.HealthReSet();//体力をリセット

        fillColor.SetColor();//色をセット

        life.ReMoveLife();   //ライフが0以上なら/ライフを - 1
    }

    public void ReSetTank()
    {
        damage.HealthReSet();//体力をリセット

        fillColor.SetColor();//色をセット

        life.ReSetLife();
    }

    /// <summary>リスタート</summary>
    public void ReStart()
    {
        fillColor.SetColor();//色をセット
    }

    protected override void Move()
    {
        line.CreateLine();                                                                     //ラインを生成

        parts.TargetLookAt(Input.mousePosition - mainCamera.WorldToScreenPoint(inputMove.Pos));//マウスカーソルの方向へタレットを向ける

        inputMove.InputMove();                                                                 //入力移動

        if (Input.GetMouseButton(default) && cannon.IsShoot) cannon.Shoot();                   //右クリック かつ 発射間隔なら/発射する

        base.Move();                                                                           //ベースのMove関数を呼び出す
    }
}
