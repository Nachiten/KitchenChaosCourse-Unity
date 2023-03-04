using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public event Action OnPlayerGrabbedObject;
    
    public override void Interact(Player player)
    {
        // Player already has an object
        if (player.HasKitchenObject())
            return;
        
        // Player doesn't have object, spawn one and give it to them
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

        OnPlayerGrabbedObject?.Invoke();
    }

    public override void InteractAlternate(Player player)
    {
        // Do nothing
    }

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
}
