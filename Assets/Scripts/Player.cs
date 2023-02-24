using System.Collections.Generic;
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
        Vector3 moveDirX =new Vector3(moveDir.x, 0f, 0f).normalized;
        Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;

        float moveDistance = moveSpeed * Time.deltaTime;
        const float playerRadius = 0.7f;
        const float playerHeight = 2f;
        
        // Try to move normally, if not, try to move in the x or z direction only
        bool canMove = false;
        List<Vector3> possibleMoveDirs = new(){ moveDir, moveDirX, moveDirZ };

        foreach (Vector3 dir in possibleMoveDirs)
        {
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dir, moveDistance);
            
            if (!canMove) 
                continue;
            
            moveDir = dir;
            break;
        }

        if (canMove)
            transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero;
        
        const float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
