using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
    
    private ClearCounter clearCounter;
    
    public void SetClearCounter(ClearCounter newClearCounter)
    {
        if (clearCounter != null)
            clearCounter.ClearKitchenObject();

        clearCounter = newClearCounter;

        if (clearCounter.HasKitchenObject())
            Debug.LogError("Counter already has a kitchen object!");
        
        clearCounter.SetKitchenObject(this);
        
        transform.parent = newClearCounter.GetCounterSpawnPoint();
        transform.localPosition = Vector3.zero;
    }
    
    public ClearCounter GetClearCounter() => clearCounter;
}
