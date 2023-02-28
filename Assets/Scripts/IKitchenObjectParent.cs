using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetCounterSpawnPoint();
    
    public void SetKitchenObject(KitchenObject _kitchenObject);
    
    public KitchenObject GetKitchenObject();
    
    public void ClearKitchenObject();
    
    public bool HasKitchenObject();
}
