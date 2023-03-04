using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjects;

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjects)
        {
            kitchenObjectSoGameObject.gameObject.SetActive(false);
        }
    }

    private void OnIngredientAdded(KitchenObjectSO newIngredient)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjects
                     .Where(kitchenObjectSoGameObject => kitchenObjectSoGameObject.kitchenObjectSO == newIngredient))
        {
            kitchenObjectSoGameObject.gameObject.SetActive(true);
        }
    }
}
