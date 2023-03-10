using System;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [Header("Keyboard keys")]
    [SerializeField] private TMP_Text keyMoveUpText;
    [SerializeField] private TMP_Text keyMoveDownText;
    [SerializeField] private TMP_Text keyMoveLeftText;
    [SerializeField] private TMP_Text keyMoveRightText;
    [SerializeField] private TMP_Text keyInteractText;
    [SerializeField] private TMP_Text keyInteractAltText;
    [SerializeField] private TMP_Text keyPauseText;

    [Header("Gamepad keys")]
    [SerializeField] private TMP_Text keyGamepadInteractText;
    [SerializeField] private TMP_Text keyGamepadInteractAltText;
    [SerializeField] private TMP_Text keyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += OnBindingRebind;
        GameManager.Instance.OnStateChanged += OnStateChanged;
        
        
        UpdateVisual();
        SetShow(true);
    }

    private void OnStateChanged()
    {
        if (GameManager.Instance.IsCountdownToStart())
            SetShow(false);
    }

    private void OnBindingRebind()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_UP);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_DOWN);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_LEFT);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_RIGHT);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.INTERACT);
        keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.INTERACT_ALTERNATE);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.PAUSE);
        keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GAMEPAD_INTERACT);
        keyGamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GAMEPAD_INTERACT_ALTERNATE);
        keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GAMEPAD_PAUSE);
    }

    private void SetShow(bool show)
    {
        gameObject.SetActive(show);
    }
}
