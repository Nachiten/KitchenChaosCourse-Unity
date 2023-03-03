using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
   [SerializeField] private CuttingCounter cuttingCounter;
   [SerializeField] private Image barImage;

   private void Start()
   {
      cuttingCounter.OnProgressChanged += OnProgressChanged;

      barImage.fillAmount = 0f;
      gameObject.SetActive(false);
   }

   private void OnProgressChanged(float progressNormalized)
   {
      barImage.fillAmount = progressNormalized;

      gameObject.SetActive(progressNormalized is > 0f and < 1f);
   }
}
