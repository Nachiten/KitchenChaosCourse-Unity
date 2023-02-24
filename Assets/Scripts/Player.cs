using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        isWalking = moveDir != Vector3.zero;
        const float rotateSpeed = 10f;
        
        // Update position
        transform.position += moveDir * (Time.deltaTime * moveSpeed);
        // Update rotation
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
