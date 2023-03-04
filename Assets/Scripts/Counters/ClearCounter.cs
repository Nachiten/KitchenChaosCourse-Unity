using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // There is no object on the counter
        if (!HasKitchenObject())
        {
            // Player doesn't have an object
            if (!player.HasKitchenObject()) 
                return;
            
            player.GetKitchenObject().SetKitchenObjectParent(this);
            return;
        }
        
        // There is an object on the counter

        // Player does not have an object
        if (!player.HasKitchenObject())
        {
            // Give player the object on the counter
            GetKitchenObject().SetKitchenObjectParent(player);
            return;
        }
        
        // Check if player has a plate
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            if (!plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                return;

            // Add counter ingredient to the player plate
            GetKitchenObject().DestroySelf();
            return;
        }

        // Check if counter has plate, and player ingredient can be added
        if (!GetKitchenObject().TryGetPlate(out PlateKitchenObject counterPlateKitchenObject) ||
            !counterPlateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
            return;

        // Add player ingredient to the counter plate
        player.GetKitchenObject().DestroySelf();
    }

    public override void InteractAlternate(Player player)
    {
        // Do nothing
    }
}