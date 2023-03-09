using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    
    public event Action OnPlateSpawned;
    public event Action OnPlateRemoved;
    
    private float spawnPlateTimer;
    private const float spawnPlateInterval = 4f;

    private int platesSpawned;
    private const int platesSpawnedMax = 4;
    
    public override void Interact(Player player)
    {
        // Player needs to not have anything in their hands
        if (player.HasKitchenObject())
        {
            // I want to try to add the ingredient that the user has to the plate

            // // Spawn tentative plate
            // KitchenObject kitchenObject = KitchenObject.SpawnKitchenObjectWithoutParent(plateKitchenObjectSO);
            // kitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject);
            //
            // if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
            // {
            //     player.GetKitchenObject().DestroySelf();
            //     kitchenObject.SetKitchenObjectParent(player);
            // }
            // else
            //     Destroy(kitchenObject);
            
            return;
        }
        
        // There must be at least one plate spawned
        if (platesSpawned == 0)
            return;
        
        // Spawn plate
        platesSpawned--;
        KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
        
        OnPlateRemoved?.Invoke();
    }

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (!(spawnPlateTimer >= spawnPlateInterval)) 
            return;
        
        spawnPlateTimer = 0f;

        if (platesSpawned >= platesSpawnedMax) 
            return;
        platesSpawned++;
                
        OnPlateSpawned?.Invoke();
    }
}
