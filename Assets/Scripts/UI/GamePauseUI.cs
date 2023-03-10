using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    
    private void Start()
    {
        GameManager.Instance.OnTogglePause += OnTogglePause;
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        
        Hide();
    }

    private void OnOptionsButtonClicked()
    {
        Hide();
        OptionsUI.Instance.Show(Show);
    }

    private void OnMainMenuButtonClicked()
    {
        Loader.Load(Loader.SceneName.MainMenuScene);
    }

    private void OnResumeButtonClicked()
    {
        GameManager.Instance.TogglePauseGame();
    }

    private void OnTogglePause(bool isPaused)
    {
        if (isPaused)
        {
            resumeButton.Select();
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
