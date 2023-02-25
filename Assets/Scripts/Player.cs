using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public event Action<ClearCounter> OnSelectedCounterChanged; 

    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    private void Start()
    {
        gameInput.OnInteract += OnInteract;
    }

    private void OnInteract()
    {
        if (selectedCounter != null)
            selectedCounter.Interact();
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);
        Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
        Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;

        float moveDistance = moveSpeed * Time.deltaTime;
        const float playerRadius = 0.7f;
        const float playerHeight = 2f;

        // Try to move normally, if not, try to move in the x or z direction only
        bool canMove = false;
        List<Vector3> possibleMoveDirs = new() {moveDir, moveDirX, moveDirZ};

        foreach (Vector3 dir in possibleMoveDirs)
        {
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, dir, moveDistance);

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

    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
            lastInteractDir = moveDir;

        const float interactionDistance = 2f;

        // Draw same raycast
        //Debug.DrawRay(transform.position, lastInteractDir * interactionDistance, Color.red, 3f);

        bool raycastDidHit = Physics.Raycast(
            transform.position, 
            lastInteractDir, 
            out RaycastHit raycastHit, 
            interactionDistance, 
            countersLayerMask);

        // if (!raycastDidHit)
        // {
        //     SetSelectedCounter(null);
        //     return;
        // }
        //
        // if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter) && clearCounter != selectedCounter)
        // {
        //     SetSelectedCounter(clearCounter);
        //     return;
        // }
        //
        // SetSelectedCounter(null);
        
        if (raycastDidHit)
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                    SetSelectedCounter(clearCounter);
                else
                {
                    // Nada
                }
            }
            else 
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
        
        // if (raycastDidHit && raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        // {
        //     if (selectedCounter == clearCounter)
        //         return;
        //
        //     selectedCounter = clearCounter;
        // }
        // else
        //     selectedCounter = null;
        //
        // OnSelectedCounterChanged?.Invoke(selectedCounter);
    }
    
    private void SetSelectedCounter(ClearCounter newClearCounter)
    {
        if (newClearCounter == selectedCounter)
            return;
        
        selectedCounter = newClearCounter;
        OnSelectedCounterChanged?.Invoke(selectedCounter);
    }
}