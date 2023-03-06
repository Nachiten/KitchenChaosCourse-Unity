using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event Action<BaseCounter> OnAnyObjectPlaced;
    
    [SerializeField] protected Transform counterSpawnPoint;
    private KitchenObject kitchenObject;
    
    public abstract void Interact(Player player);

    public virtual void InteractAlternate(Player player)
    {
        // Optional function to override
    }

    public Transform GetCounterSpawnPoint() => counterSpawnPoint;
    
    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        kitchenObject = _kitchenObject;
        
        if (kitchenObject != null)
            OnAnyObjectPlaced?.Invoke(this);
    }
    
    public KitchenObject GetKitchenObject() => kitchenObject;
    
    public void ClearKitchenObject() => kitchenObject = null;
    
    public bool HasKitchenObject() => kitchenObject != null;
}
