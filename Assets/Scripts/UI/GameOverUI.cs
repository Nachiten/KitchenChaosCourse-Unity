using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TMP_Text recipesDeliveredText;
    
    private void Start()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
        
        gameObject.SetActive(false);
    }

    private void OnStateChanged()
    {
        if (GameManager.Instance.IsGameOver())
        {
            gameObject.SetActive(true);
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesDelivered().ToString();
        }
        else
            gameObject.SetActive(false);
    }
}
