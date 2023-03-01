using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;
    
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(BaseCounter newBaseCounter)
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(newBaseCounter == baseCounter);
        }
    }
}