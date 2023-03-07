using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] TMP_Text countdownText;

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
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
    }
}
