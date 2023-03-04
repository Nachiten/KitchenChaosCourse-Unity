using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;


    private void Start()
    {
        stoveCounter.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(StoveCounter.State newState)
    {
        bool showVisual = newState is StoveCounter.State.Frying or StoveCounter.State.Burning;
        
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
