using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOs;

    public event Action<KitchenObjectSO> OnIngredientAdded;
    
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
        OnIngredientAdded?.Invoke(kitchenObjectSO);
        return true;
    }
}
