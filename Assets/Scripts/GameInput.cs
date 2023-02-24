using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.MoveNormalized.ReadValue<Vector2>();
    }
}
