using System;
using System.Linq;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    [SerializeField] private FryingRecipeSO[] fryingRecipes;
    
    public event Action<float> OnProgressChanged;
    public event Action<State> OnStateChanged;
    
    private float timer;
    private FryingRecipeSO fryingRecipeSO;
    private State state;
    
    public enum State
    {
        Idle,
        Frying,
        Burning,
        Burned
    }

    private void ChangeState(State newState)
    {
        state = newState;
        OnStateChanged?.Invoke(state);
    }
    
    private void Start()
    {
        ChangeState(State.Idle);
    }

    private void Update()
    {
        if (!HasKitchenObject())
            return;
        
        ExecuteStateMachine();
    }

    private void ExecuteStateMachine()
    {
        switch (state)
        {
            case State.Idle:
                // Nothing
                break;
            case State.Frying:
                if (ExecuteTimerFinished())
                {
                    // Set the burning recipe
                    fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

                    // If there is no next recipe, it means the object was inserted already cooked (now its burned)
                    ChangeState(fryingRecipeSO == null ? State.Burned : State.Burning);
                }
                
                break;
            case State.Burning:
                if (ExecuteTimerFinished())
                    ChangeState(State.Burned);

                break;
            case State.Burned:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool ExecuteTimerFinished()
    {
        timer += Time.deltaTime;
        OnProgressChanged?.Invoke(timer / fryingRecipeSO.fryingTimerMax);

        // Timer is not finished
        if (timer < fryingRecipeSO.fryingTimerMax)
            return false;
                
        ResetTimer();
                
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

        return true;
    }

    private void ResetTimer()
    {
        timer = 0f;
        OnProgressChanged?.Invoke(0f);
    }

    public override void Interact(Player player)
    {
        // There is no object on the counter, and player has one
        if (!HasKitchenObject() && player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            // Place the player item on the counter
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            
            fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
            ChangeState(State.Frying);
            ResetTimer();
        }
        
        // There is an object on the counter, and player does not have one
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            // Pick up the counter item
            GetKitchenObject().SetKitchenObjectParent(player);
            
            // Reset the state
            ChangeState(State.Idle);
            
            OnProgressChanged?.Invoke(0f);
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        return GetFryingRecipeSOFromInput(input) != null;
    }
    
    // private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    // {
    //     return GetFryingRecipeSOFromInput(input)?.output;
    // }
    
    private FryingRecipeSO GetFryingRecipeSOFromInput(KitchenObjectSO input)
    {
        return (from fryingRecipes in fryingRecipes where fryingRecipes.input == input select fryingRecipes).FirstOrDefault();
    }
}
