using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
    
    private IKitchenObjectParent kitchenObjectParent;
    
    public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
    {
        // Clear the old parent
        kitchenObjectParent?.ClearKitchenObject();
        
        kitchenObjectParent = newKitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
            Debug.LogError("Kitchen parent already has a kitchen object!");
        
        kitchenObjectParent.SetKitchenObject(this);
        
        transform.parent = newKitchenObjectParent.GetCounterSpawnPoint();
        transform.localPosition = Vector3.zero;
    }
    
    public IKitchenObjectParent GetKitchenObjectParent() => kitchenObjectParent;
}
