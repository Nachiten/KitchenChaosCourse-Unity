using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // There is no object on the counter, and player has one
        if (!HasKitchenObject() && player.HasKitchenObject()) 
            player.GetKitchenObject().SetKitchenObjectParent(this);
        
        // There is an object on the counter, and player does not have one
        else if (HasKitchenObject() && !player.HasKitchenObject())
            GetKitchenObject().SetKitchenObjectParent(player);
    }

    public override void InteractAlternate(Player player)
    {
        // Do nothing
    }
}