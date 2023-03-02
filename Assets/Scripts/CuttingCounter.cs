using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    
    public override void Interact(Player player)
    {
        // There is no object on the counter, and player has one
        if (!HasKitchenObject() && player.HasKitchenObject()) 
            // Place the player item on the counter
            player.GetKitchenObject().SetKitchenObjectParent(this);
        
        // There is an object on the counter, and player does not have one
        else if (HasKitchenObject() && !player.HasKitchenObject())
            // Pick up the counter item
            GetKitchenObject().SetKitchenObjectParent(player);
    }

    public override void InteractAlternate(Player player)
    {
        // There must be an object on the counter
        if (!HasKitchenObject())
            return;
        
        // Cut the object
        GetKitchenObject().DestroySelf();
            
        KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
    }
}
