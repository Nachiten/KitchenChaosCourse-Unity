using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Animator animator;
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += OnPlayerGrabbedObject;
        spriteRenderer.sprite = containerCounter.GetKitchenObjectSO().sprite;
    }

    private void OnPlayerGrabbedObject()
    {
        animator.SetTrigger(OpenClose);
    }
}
