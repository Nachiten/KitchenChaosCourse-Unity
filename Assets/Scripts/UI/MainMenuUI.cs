using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
   [SerializeField] private Button playButton;
   [SerializeField] private Button quitButton;

   private void Awake()
   {
      playButton.onClick.AddListener(OnPlayButtonClicked);
      quitButton.onClick.AddListener(OnQuitButtonClicked);

      Time.timeScale = 1f;
   }

   private void OnPlayButtonClicked()
   {
      Loader.Load(Loader.SceneName.GameScene);
   }
   
   private void OnQuitButtonClicked()
   {
      Application.Quit();
   }
}
