using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>チュートリアル中のメニュー</summary>
public class TutorialMenuScript : MonoBehaviour
{
    /// <summary>エネミーのプールリスト</summary>
    [SerializeField, LightColor] private StageTankManagerScript stageManager;

    /// <summary>EnemyTankのフィールド</summary>
    [SerializeField, LightColor] private EnemyInfoFieldScript enemyInfoField;

    /// <summary>オブジェクト</summary>
    [SerializeField, LightColor] private GameObject obj;

    /// <summary>敵タンクの情報を更新するボタン</summary>
    [SerializeField, LightColor] private Button enemyInfoUpdateButton;

    /// <summary>敵タンクの情報を更新するボタンテキスト</summary>
    [SerializeField, LightColor] private TextMeshProUGUI enemyInfoUpdateTMP;

    /// <summary>通常のボタンの色</summary>
    [SerializeField] private Color buttonColor;
    /// <summary>クリックされた際のボタンの色</summary>
    [SerializeField] private Color onClickButtonColor;

    /// <summary>アクティブか</summary>
    public bool IsActive
    {
        get => obj.activeSelf;
        set => obj.SetActive(value);
    }

    private void Start()
    {
        enemyInfoUpdateButton.onClick.AddListener(OnEnemyInfoUpdate);//敵タンクの情報を更新するボタンが押された際実行する関数を追加
    }

    /// <summary>敵タンクを情報を更新する際の処理</summary>
    private void OnEnemyInfoUpdate()
    {
        enemyInfoUpdateTMP.color = onClickButtonColor;      //ボタンの色を変更

        var info = enemyInfoField.SetInfo();                //フィールドの情報を敵タンクの情報を

        foreach (var enemy in stageManager.PoolEnemyList)   //プールされている敵タンク数分繰り返す
        {
            if (enemy.IsActive)                             //アクティブなら
            {
                enemy.UpdateInfo(info);                     //情報を更新

                return;                                     //終了
            }
        }

        stageManager.GetPoolEnemy.Invoke().UpdateInfo(info);//プールされている敵タンクをアクティブにし、情報を更新
    }

    /// <summary>オープンまたはクローズされた際の処理</summary>
    public void OpenOrClose()
    {
        enemyInfoUpdateTMP.color = buttonColor;//ボタンの色を変更
    }
}
