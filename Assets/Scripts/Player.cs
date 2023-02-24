using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = Vector2.zero;
        
        if (Input.GetKey(KeyCode.W))
            inputVector.y += 1;
        
        if (Input.GetKey(KeyCode.S))
            inputVector.y -= 1;

        if (Input.GetKey(KeyCode.A))
            inputVector.x -= 1;
        
        if (Input.GetKey(KeyCode.D))
            inputVector.x += 1;

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);
        
        isWalking = moveDir != Vector3.zero;
        
        // Update position
        transform.position += moveDir * (Time.deltaTime * moveSpeed);
        
        const float rotateSpeed = 10f;
        
        // Update rotation
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
