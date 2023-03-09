using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingsUI : MonoBehaviour
{
    [Header("Key binding texts")]
    [SerializeField] private TMP_Text moveUpText;
    [SerializeField] private TMP_Text moveDownText;
    [SerializeField] private TMP_Text moveLeftText;
    [SerializeField] private TMP_Text moveRightText;
    [SerializeField] private TMP_Text interactText;
    [SerializeField] private TMP_Text interactAlternateText;
    [SerializeField] private TMP_Text pauseText;
    
    [Header("Key binding buttons")]
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private Transform pressToRebindKeyTransform;

    private void Awake()
    {
        moveUpButton.onClick.AddListener(() => OnKeyBindingButtonClicked(GameInput.Binding.MOVE_UP));
        moveDownButton.onClick.AddListener(() => OnKeyBindingButtonClicked(GameInput.Binding.MOVE_DOWN));
        moveLeftButton.onClick.AddListener(() => OnKeyBindingButtonClicked(GameInput.Binding.MOVE_LEFT));
        moveRightButton.onClick.AddListener(() => OnKeyBindingButtonClicked(GameInput.Binding.MOVE_RIGHT));
        interactButton.onClick.AddListener(() => OnKeyBindingButtonClicked(GameInput.Binding.INTERACT));
        interactAlternateButton.onClick.AddListener(() => OnKeyBindingButtonClicked(GameInput.Binding.INTERACT_ALTERNATE));
        pauseButton.onClick.AddListener(() => OnKeyBindingButtonClicked(GameInput.Binding.PAUSE));
    }
    
    private void Start()
    {
        UpdateVisual();
    }
    
    private void OnKeyBindingButtonClicked(GameInput.Binding binding)
    {
        SetShowPressToRebindKey(true);
        GameInput.Instance.RebindBinding(binding, OnRebindingComplete);
    }
    
    private void OnRebindingComplete()
    {
        SetShowPressToRebindKey(false);
        UpdateVisual();
    }
    
    private void UpdateVisual()
    {
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_UP);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_DOWN);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_LEFT);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_RIGHT);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.INTERACT);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.INTERACT_ALTERNATE);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.PAUSE);
    }
    
    private void SetShowPressToRebindKey(bool show)
    {
        pressToRebindKeyTransform.gameObject.SetActive(show);
    }
}
