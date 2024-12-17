using System.Linq;
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

    [SerializeField] private Color buttonColor;
    [SerializeField] private Color onUpdateButtonColor;

    /// <summary>アクティブか</summary>
    public bool IsActive
    {
        get => obj.activeSelf;
        set
        {
            enemyInfoUpdateTMP.color = buttonColor;

            obj.SetActive(value);
        }
    }

    private void Start()
    {
        enemyInfoUpdateButton.onClick.AddListener(OnEnemyIndoUpdate);
    }

    private void OnEnemyIndoUpdate()
    {
        enemyInfoUpdateTMP.color = onUpdateButtonColor;

        var info = enemyInfoField.SetInfo();

        foreach (var enemy in stageManager.PoolEnemyList)
        {
            if (enemy.IsActive)
            {
                enemy.UpdateInfo(info);

                return;
            }
        }

        stageManager.GetPoolEnemy.Invoke().UpdateInfo(info);
    }
}
