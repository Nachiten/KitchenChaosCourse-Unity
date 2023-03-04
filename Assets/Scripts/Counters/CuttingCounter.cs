using System;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;
    
    public event Action<float> OnProgressChanged;

    private int cuttingProgress;
    
    public override void Interact(Player player)
    {
        // There is no object on the counter, and player has one
        if (!HasKitchenObject() && player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            // Place the player item on the counter
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            cuttingProgress = 0;
            
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnProgressChanged?.Invoke((float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax);
            return;
        }
        
        // There is no object on the counter
        if (!HasKitchenObject()) 
            return;
        
        // Player picks up the object on the counter
        if (!player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
            return;
        }
           
        // Check if player has a plate
        if (!player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            return;

        // Check if the plate can add the ingredient
        if (!plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
            return;

        // Player adds the object on the counter to their plate
        GetKitchenObject().DestroySelf();
    }

    public override void InteractAlternate(Player player)
    {
        KitchenObjectSO currentKitchenObjectSO = GetKitchenObject()?.GetKitchenObjectSO();
        
        // There must be an object on the counter, and a valid recipe for cutting it
        if (!HasKitchenObject() || !HasRecipeWithInput(currentKitchenObjectSO))
            return;
        
        cuttingProgress++;
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(currentKitchenObjectSO);

        OnProgressChanged?.Invoke((float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax);
        
        // If the cutting progress is not yet at the max, return
        if (cuttingProgress < cuttingRecipeSO.cuttingProgressMax)
            return;
        
        KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(currentKitchenObjectSO);
        
        // Destroy the uncut object
        GetKitchenObject().DestroySelf();
            
        // Spawn the cut object
        KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        return GetCuttingRecipeSOFromInput(input) != null;
    }
    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        return GetCuttingRecipeSOFromInput(input)?.output;
    }
    
    private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO input)
    {
        return (from cuttingRecipe in cuttingRecipes where cuttingRecipe.input == input select cuttingRecipe).FirstOrDefault();
    }
}
