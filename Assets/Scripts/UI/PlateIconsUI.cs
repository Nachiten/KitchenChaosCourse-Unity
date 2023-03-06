using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += OnIngredientAdded;
    }

    private void OnIngredientAdded(KitchenObjectSO obj)
    {
        DeleteVisuals();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOs())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
    
    private void DeleteVisuals()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate)
                continue;
            
            Destroy(child.gameObject);
        }
    }
}
