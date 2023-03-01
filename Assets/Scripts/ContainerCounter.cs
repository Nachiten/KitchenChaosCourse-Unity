using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event Action OnPlayerGrabbedObject;
    
    public override void Interact(Player player)
    {
        // Player already has an object
        if (player.HasKitchenObject())
            return;
        
        // Player doesn't have object, spawn one and give it to them
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        
        OnPlayerGrabbedObject?.Invoke();
    }
}
