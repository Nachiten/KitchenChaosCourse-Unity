using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>, IKitchenObjectParent
{
    public event Action<BaseCounter> OnSelectedCounterChanged; 
    public event Action OnPickedSomething;
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    
    private KitchenObject kitchenObject;

    private void Start()
    {
        gameInput.OnInteract += OnInteract;
        gameInput.OnInteractAlternate += OnInteractAlternate;
    }

    private void OnInteract()
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;
        
        if (selectedCounter != null)
            selectedCounter.Interact(this);
    }
    
    private void OnInteractAlternate()
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;
        
        if (selectedCounter != null)
            selectedCounter.InteractAlternate(this);
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
        
        for (int index = 0; index < possibleMoveDirs.Count; index++)
        {
            // Player can rotate if they are moving in the x or z direction
            bool extraCondition = index switch
            {
                1 => moveDir.x is < -0.5f or > 0.5f,
                2 => moveDir.z is < -0.5f or > 0.5f,
                _ => true
            };

            Vector3 dir = possibleMoveDirs[index];
            canMove = extraCondition && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
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

        if (!raycastDidHit || !raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
        {
            SetSelectedCounter(null);
            return;
        }

        if (baseCounter != selectedCounter)
            SetSelectedCounter(baseCounter);
    }
    
    private void SetSelectedCounter(BaseCounter newBaseCounter)
    {
        if (newBaseCounter == selectedCounter)
            return;
        
        selectedCounter = newBaseCounter;
        OnSelectedCounterChanged?.Invoke(selectedCounter);
    }

    public Transform GetCounterSpawnPoint() => kitchenObjectHoldPoint;
    
    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        kitchenObject = _kitchenObject;
        
        if (kitchenObject != null)
            OnPickedSomething?.Invoke();
    }
    
    public KitchenObject GetKitchenObject() => kitchenObject;
    
    public void ClearKitchenObject() => kitchenObject = null;
    
    public bool HasKitchenObject() => kitchenObject != null;
}