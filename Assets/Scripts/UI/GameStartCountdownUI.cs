using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;

    private Animator animator;
    private int previousCountdownNumber;
    private static readonly int CountdownTrigger = Animator.StringToHash("NumberPopup");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
        
        gameObject.SetActive(false);
    }

    private void OnStateChanged()
    {
        gameObject.SetActive(GameManager.Instance.IsCountdownToStart());
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        // Check if the countdown number has changed
        if (previousCountdownNumber == countdownNumber) 
            return;
        
        previousCountdownNumber = countdownNumber;
        animator.SetTrigger(CountdownTrigger);
        SoundManager.Instance.PlayCountdownSound();
    }
}
