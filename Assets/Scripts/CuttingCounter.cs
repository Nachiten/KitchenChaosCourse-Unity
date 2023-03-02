using System;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;
    
    public override void Interact(Player player)
    {
        // There is no object on the counter, and player has one
        if (!HasKitchenObject() && player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            // Place the player item on the counter
            player.GetKitchenObject().SetKitchenObjectParent(this);
        
        // There is an object on the counter, and player does not have one
        else if (HasKitchenObject() && !player.HasKitchenObject())
            // Pick up the counter item
            GetKitchenObject().SetKitchenObjectParent(player);
    }

    public override void InteractAlternate(Player player)
    {
        // There must be an object on the counter, and a valid recipe for cutting it
        if (!HasKitchenObject() || !HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            return;
        
        KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
        
        // Destroy the uncut object
        GetKitchenObject().DestroySelf();
            
        // Spawn the cut object
        KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        return GetOutputForInput(input) != null;
    }
    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        return (from cuttingRecipe in cuttingRecipes where cuttingRecipe.input == input select cuttingRecipe.output).FirstOrDefault();
    }
}
