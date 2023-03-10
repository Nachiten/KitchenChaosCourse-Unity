using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : Singleton<GameInput>
{
    private const string PLAYER_PREFS_BINDINGS_KEY = "InputBindings";
    
    public event Action OnInteract;
    public event Action OnInteractAlternate;
    public event Action OnPause;
    public event Action OnBindingRebind;

    public enum Binding
    {
        MOVE_UP,
        MOVE_DOWN,
        MOVE_LEFT,
        MOVE_RIGHT,
        INTERACT,
        INTERACT_ALTERNATE,
        PAUSE,
        GAMEPAD_INTERACT,
        GAMEPAD_INTERACT_ALTERNATE,
        GAMEPAD_PAUSE
    }
    
    private PlayerInputActions playerInputActions;
    
    protected override void Awake()
    {
        base.Awake();
        
        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS_KEY))
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS_KEY));

        playerInputActions.Player.Enable();
        
        playerInputActions.Player.Interact.performed += InteractPerformed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
        playerInputActions.Player.Pause.performed += PausePerformed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= InteractPerformed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternatePerformed;
        playerInputActions.Player.Pause.performed -= PausePerformed;
        
        playerInputActions.Dispose();
    }

    private void PausePerformed(InputAction.CallbackContext obj)
    {
       OnPause?.Invoke();
    }

    private void InteractAlternatePerformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke();
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.MoveNormalized.ReadValue<Vector2>();
    }

    public string GetBindingText(Binding binding)
    {
        return binding switch
        {
            Binding.MOVE_UP => playerInputActions.Player.MoveNormalized.bindings[1].ToDisplayString(),
            Binding.MOVE_DOWN => playerInputActions.Player.MoveNormalized.bindings[2].ToDisplayString(),
            Binding.MOVE_LEFT => playerInputActions.Player.MoveNormalized.bindings[3].ToDisplayString(),
            Binding.MOVE_RIGHT => playerInputActions.Player.MoveNormalized.bindings[4].ToDisplayString(),
            
            Binding.INTERACT => playerInputActions.Player.Interact.bindings[0].ToDisplayString(),
            Binding.INTERACT_ALTERNATE => playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString(),
            Binding.PAUSE => playerInputActions.Player.Pause.bindings[0].ToDisplayString(),
            
            Binding.GAMEPAD_INTERACT => playerInputActions.Player.Interact.bindings[1].ToDisplayString(),
            Binding.GAMEPAD_INTERACT_ALTERNATE => playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString(),
            Binding.GAMEPAD_PAUSE => playerInputActions.Player.Pause.bindings[1].ToDisplayString(),
            _ => throw new ArgumentOutOfRangeException(nameof(binding), binding, null)
        };
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;
        
        switch (binding)
        {
            case Binding.MOVE_UP:
                inputAction = playerInputActions.Player.MoveNormalized;
                bindingIndex = 1;
                break;
            case Binding.MOVE_DOWN:
                inputAction = playerInputActions.Player.MoveNormalized;
                bindingIndex = 2;
                break;
            case Binding.MOVE_LEFT:
                inputAction = playerInputActions.Player.MoveNormalized;
                bindingIndex = 3;
                break;
            case Binding.MOVE_RIGHT:
                inputAction = playerInputActions.Player.MoveNormalized;
                bindingIndex = 4;
                break;
            case Binding.INTERACT:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.INTERACT_ALTERNATE:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.PAUSE:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.GAMEPAD_INTERACT:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.GAMEPAD_INTERACT_ALTERNATE:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.GAMEPAD_PAUSE:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
        }
        
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(
            callback => { 
                callback.Dispose(); 
                playerInputActions.Player.Enable(); 
                onActionRebound();
                
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS_KEY, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
                
                OnBindingRebind?.Invoke();
            }).Start();
    }
}