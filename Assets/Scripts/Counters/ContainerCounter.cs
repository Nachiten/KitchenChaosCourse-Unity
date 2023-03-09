using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public event Action OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        // Player doesn't have object, spawn one and give it to them
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke();

            return;
        }
    
        // Player has an object
        
        // Check if player has a plate
        if (!player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            return;

        // Try to add the ingredient to the plate
        if (plateKitchenObject.TryAddIngredient(kitchenObjectSO))
            OnPlayerGrabbedObject?.Invoke();
        
    }

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
}
