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

    /// <summary>�ʏ�̃{�^���̐F</summary>
    [SerializeField] private Color buttonColor;
    /// <summary>�N���b�N���ꂽ�ۂ̃{�^���̐F</summary>
    [SerializeField] private Color onClickButtonColor;

    /// <summary>�A�N�e�B�u��</summary>
    public bool IsActive
    {
        get => obj.activeSelf;
        set => obj.SetActive(value);
    }

    private void Start()
    {
        enemyInfoUpdateButton.onClick.AddListener(OnEnemyInfoUpdate);//�G�^���N�̏����X�V����{�^���������ꂽ�ێ��s����֐���ǉ�
    }

    /// <summary>�G�^���N�������X�V����ۂ̏���</summary>
    private void OnEnemyInfoUpdate()
    {
        enemyInfoUpdateTMP.color = onClickButtonColor;      //�{�^���̐F��ύX

        var info = enemyInfoField.SetInfo();                //�t�B�[���h�̏���G�^���N�̏���

        foreach (var enemy in stageManager.PoolEnemyList)   //�v�[������Ă���G�^���N�����J��Ԃ�
        {
            if (enemy.IsActive)                             //�A�N�e�B�u�Ȃ�
            {
                enemy.UpdateInfo(info);                     //�����X�V

                return;                                     //�I��
            }
        }

        stageManager.GetPoolEnemy.Invoke().UpdateInfo(info);//�v�[������Ă���G�^���N���A�N�e�B�u�ɂ��A�����X�V
    }

    /// <summary>�I�[�v���܂��̓N���[�Y���ꂽ�ۂ̏���</summary>
    public void OpenOrClose()
    {
        enemyInfoUpdateTMP.color = buttonColor;//�{�^���̐F��ύX
    }
}
