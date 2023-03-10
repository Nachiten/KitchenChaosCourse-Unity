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

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        
        return kitchenObject;
    }
    
    // public static KitchenObject SpawnKitchenObjectWithoutParent(KitchenObjectSO kitchenObjectSO)
    // {
    //     Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, new Vector3(0,-10,0), Quaternion.identity);
    //     KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
    //     
    //     return kitchenObject;
    // }
    
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }

        plateKitchenObject = null;
        return false;
    }
}
