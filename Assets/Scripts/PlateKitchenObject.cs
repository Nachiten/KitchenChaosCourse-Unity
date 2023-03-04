using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOs;
    
    private List<KitchenObjectSO> kitchenObjectSOs;

    private void Awake()
    {
        kitchenObjectSOs = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // Check if the ingredient is valid
        if (!validKitchenObjectSOs.Contains(kitchenObjectSO))
            return false;
        
        // Check if the plate already has this ingredient
        if (kitchenObjectSOs.Contains(kitchenObjectSO))
            return false;
        
        // Add ingredient to the plate
        kitchenObjectSOs.Add(kitchenObjectSO);
        return true;
    }
}
