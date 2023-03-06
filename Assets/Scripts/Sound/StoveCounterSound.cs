using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(StoveCounter.State state)
    {
        bool isOn = state is StoveCounter.State.Frying or StoveCounter.State.Burning;

        if (isOn)
            audioSource.Play();
        else
            audioSource.Pause();
        
    }
}
