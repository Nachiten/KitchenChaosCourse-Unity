using System;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterSpawnPoint;
    
    private KitchenObject kitchenObject;
    
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && testing)
        {
            if (kitchenObject != null)
                kitchenObject.SetClearCounter(secondClearCounter);
        }
    }

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterSpawnPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else 
        {
             Debug.Log(kitchenObject.GetClearCounter());
        }
    }
    
    public Transform GetCounterSpawnPoint() => counterSpawnPoint;
    
    public void SetKitchenObject(KitchenObject _kitchenObject) => kitchenObject = _kitchenObject;
    
    public KitchenObject GetKitchenObject() => kitchenObject;
    
    public void ClearKitchenObject() => kitchenObject = null;
    
    public bool HasKitchenObject() => kitchenObject != null;
}