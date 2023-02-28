using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterSpawnPoint;
    
    private KitchenObject kitchenObject;
    
    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            // There is no object, spawn one
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterSpawnPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else 
        {
             // Give the object to the player
             kitchenObject.SetKitchenObjectParent(player);
        }
    }
    
    public Transform GetCounterSpawnPoint() => counterSpawnPoint;
    
    public void SetKitchenObject(KitchenObject _kitchenObject) => kitchenObject = _kitchenObject;
    
    public KitchenObject GetKitchenObject() => kitchenObject;
    
    public void ClearKitchenObject() => kitchenObject = null;
    
    public bool HasKitchenObject() => kitchenObject != null;
}