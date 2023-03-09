using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : Singleton<OptionsUI>
{
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    
    [SerializeField] private TMP_Text soundEffectsText;
    [SerializeField] private TMP_Text musicText;
    
    protected override void Awake()
    {
        base.Awake();
        
        soundEffectsButton.onClick.AddListener(OnSoundEffectsButtonClicked);
        musicButton.onClick.AddListener(OnMusicButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }
    
    private void Start()
    {
        GameManager.Instance.OnTogglePause += OnTogglePause;
        
        UpdateVisual();
        
        SetShow(false);
    }

    private void OnTogglePause(bool isPaused)
    {
        if (!isPaused) SetShow(false);
    }

    private void OnCloseButtonClicked()
    {
        SetShow(false);
    }

    private void OnMusicButtonClicked()
    {
        MusicManager.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void OnSoundEffectsButtonClicked()
    {
        SoundManager.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void SetShow(bool show)
    {
        gameObject.SetActive(show);
    }
}
