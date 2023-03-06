using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] TMP_Text recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;
        
        ClearContainer();
        GenerateIcons(recipeSO.ingredients);
    }

    private void ClearContainer()
    {
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) 
                continue;
            
            Destroy(child.gameObject);
        }
    }
    
    private void GenerateIcons(List<KitchenObjectSO> ingredients)
    {
        foreach (KitchenObjectSO ingredient in ingredients)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = ingredient.sprite;
        }
    }
}
