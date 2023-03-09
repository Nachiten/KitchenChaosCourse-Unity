using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    
    private void Start()
    {
        GameManager.Instance.OnTogglePause += OnTogglePause;
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        
        gameObject.SetActive(false);
    }

    private void OnMainMenuButtonClicked()
    {
        //GameManager.Instance.TogglePauseGame();
        Loader.Load(Loader.SceneName.MainMenuScene);
    }

    private void OnResumeButtonClicked()
    {
        GameManager.Instance.TogglePauseGame();
    }

    private void OnTogglePause(bool isPaused)
    {
        gameObject.SetActive(isPaused);
    }
}