using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TutorialScript : MonoBehaviour
{
    [SerializeField, LightColor] private FaderScript fader;

    [SerializeField, LightColor] private StageEyeCatchUIScript stageEyeCatchUI;

    [SerializeField, LightColor] private StageTankManagerScript tankManager;

    [SerializeField, LightColor] private StageScript stage;

    [SerializeField, LightColor] private MenuScript menu;

    [SerializeField, LightColor] private TutorialMenuScript tutorialMenu;

    [SerializeField] private string stageCountText;

    public bool IsNotMove { get; private set; }

    private UnityAction onStart;
    private UnityAction onEnd;

    public void SetAction(UnityAction start, UnityAction end)
    {
        onStart = start;
        onEnd = end;

        onStart += () => IsNotMove = false;
    }

    public void OnFadeIn()
    {
        IsNotMove = true;

        stageEyeCatchUI.Display();

        tankManager.Player.ReStart();

        stageEyeCatchUI.SetStageCount(stageCountText);

        stage.Active(tankManager, OnAllEnemysDeath);

        tutorialMenu.IsActive = true;
    }

    public void OnFadeOut()
    {
        stageEyeCatchUI.Fader.Run(null, onStart);
    }

    private IEnumerator OnAllEnemysDeath()
    {
        yield break;
    }

    public void OnQuit()
    {
        if (!tutorialMenu.IsActive) return;

        tankManager.InActive();

        stage.InActive();

        onEnd?.Invoke();

        tutorialMenu.IsActive = false;
    }
}
