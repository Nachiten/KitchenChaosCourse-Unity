using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event Action OnInteract;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += InteractPerformed;
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.MoveNormalized.ReadValue<Vector2>();
    }
}