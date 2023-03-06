using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : Singleton<DeliveryManager>
{
    [SerializeField] private RecipeListSO recipeListSO;

    public event Action OnRecipeSpawned;
    public event Action OnRecipeCompleted;
    
    private List<RecipeSO> waitingRecipes;

    private float spawnRecipeTimer;
    
    private const float spawnRecipeInterval = 4f;
    private const int waitingRecipesMax = 4;

    protected override void Awake()
    {
        base.Awake();
        waitingRecipes = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;

        if (spawnRecipeTimer < spawnRecipeInterval) 
            return;
        
        spawnRecipeTimer = 0f;
            
        if (waitingRecipes.Count >= waitingRecipesMax)
            return;
            
        RecipeSO waitingRecipeSO = recipeListSO.recipes[Random.Range(0, recipeListSO.recipes.Count)];
        waitingRecipes.Add(waitingRecipeSO);
        
        Debug.Log($"New recipe: {waitingRecipeSO.recipeName}");
        
        OnRecipeSpawned?.Invoke();
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO waitingRecipe in waitingRecipes)
        {
            // The recipe cannot be the same if the number of ingredients is different
            if (waitingRecipe.ingredients.Count != plateKitchenObject.GetKitchenObjectSOs().Count)
                continue;

            bool plateContentsMatchRecipe = true;
            
            // Cycle through all ingredients in the recipe
            foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipe.ingredients)
            {
                bool ingredientFound = false;
                
                // Cycle through all ingredients on the plate
                foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOs())
                {
                    if (plateKitchenObjectSO != recipeKitchenObjectSO) 
                        continue;
                    
                    // Ingredient matches!!
                    ingredientFound = true;
                    break;
                }

                if (!ingredientFound)
                {
                    // This recipe ingredient was not found on the plate
                    plateContentsMatchRecipe = false;
                }
            }

            if (!plateContentsMatchRecipe)
                continue;
            
            // The plate contents match the recipe, player delivered the correct recipe
            Debug.Log($"Delivered CORRECT recipe: {waitingRecipe.recipeName}");
            waitingRecipes.Remove(waitingRecipe);
            OnRecipeCompleted?.Invoke();
            return;
        }
        
        // Player did not deliver a correct recipe
        Debug.Log("Delivered INCORRECT recipe");
    }
    
    public List<RecipeSO> GetWaitingRecipes()
    {
        return waitingRecipes;
    }
}
