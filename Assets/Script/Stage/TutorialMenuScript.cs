using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�`���[�g���A�����̃��j���[</summary>
public class TutorialMenuScript : MonoBehaviour
{
    /// <summary>�G�l�~�[�̃v�[�����X�g</summary>
    [SerializeField, LightColor] private StageTankManagerScript stageManager;

    /// <summary>EnemyTank�̃t�B�[���h</summary>
    [SerializeField, LightColor] private EnemyInfoFieldScript enemyInfoField;

    /// <summary>�I�u�W�F�N�g</summary>
    [SerializeField, LightColor] private GameObject obj;

    /// <summary>�G�^���N�̏����X�V����{�^��</summary>
    [SerializeField, LightColor] private Button enemyInfoUpdateButton;

    /// <summary>�G�^���N�̏����X�V����{�^���e�L�X�g</summary>
    [SerializeField, LightColor] private TextMeshProUGUI enemyInfoUpdateTMP;

    [SerializeField] private Color buttonColor;
    [SerializeField] private Color onUpdateButtonColor;

    /// <summary>�A�N�e�B�u��</summary>
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
