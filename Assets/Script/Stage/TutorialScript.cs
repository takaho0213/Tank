using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField, LightColor] private GameObject Obj;

    [SerializeField, LightColor] private FaderScript fader;

    [SerializeField, LightColor] private StageEyeCatchUIScript stageEyeCatchUI;

    [SerializeField, LightColor] private StageManagerScript stageManager;

    [SerializeField, LightColor] private StageTankManagerScript tankManager;

    [SerializeField, LightColor] private Button QuitButton;

    [SerializeField, LightColor] private Transform playerInit;

    [SerializeField, LightColor] private StageScript stage;

    [SerializeField] private string stageCountText;

    public bool IsNotMove { get; private set; }

    private UnityAction<UnityAction> onQuit;

    private void Start()
    {
        QuitButton.onClick.AddListener(() =>
        {
            if (fader.IsRun) return;

            onQuit?.Invoke(OnQuit);
        });
    }

    public void Init(UnityAction<UnityAction> onQuit)
    {
        this.onQuit = onQuit;
    }

    public void OnFadeIn()
    {
        Obj.SetActive(true);

        IsNotMove = true;

        stageEyeCatchUI.Display();

        tankManager.Player.ReStart();

        stageEyeCatchUI.SetStageCount(stageCountText);

        stage.Active(tankManager, OnAllEnemysDeath);
    }

    public void OnFadeOut()
    {
        stageEyeCatchUI.Fader.Run(null, () => IsNotMove = false);
    }

    private IEnumerator OnAllEnemysDeath()
    {
        yield break;
    }

    private void OnQuit()
    {
        tankManager.InActive();

        stage.InActive();

        Obj.SetActive(false);
    }
}
