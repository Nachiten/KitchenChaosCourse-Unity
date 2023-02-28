using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterSpawnPoint;
    
    public void Interact()
    {
        Debug.Log("Interacted with " + name);
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterSpawnPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}