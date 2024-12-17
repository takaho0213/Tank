using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuScript : MonoBehaviour
{
    [SerializeField, LightColor] private GameObject obj;

    [SerializeField, LightColor] private Button backTitleButton;
    [SerializeField, LightColor] private Button gameQuitButton;

    private UnityAction onBackTitle;
    private UnityAction onGameQuit;

    public void AddOnClickBackTitleButton(UnityAction c) => onBackTitle += c;

    public void AddOnClickGameQuitButton(UnityAction c) => onGameQuit += c;

    private void Start()
    {
        backTitleButton.onClick.AddListener(OnBackTitle);

        gameQuitButton.onClick.AddListener(OnGameQuit);
    }

    private void OnBackTitle()
    {
        OpenOrClose();

        onBackTitle?.Invoke();
    }

    private void OnGameQuit()
    {
        OpenOrClose();

        onGameQuit?.Invoke();
    }

    public void OpenOrClose()
    {
        bool isActive = !obj.activeSelf;

        obj.SetActive(isActive);

        Time.timeScale = isActive ? MathEx.ZeroF : MathEx.OneF;
    }
}
