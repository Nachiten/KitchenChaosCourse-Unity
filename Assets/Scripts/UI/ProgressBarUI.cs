using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
   [SerializeField] private GameObject hasProgressGameObject;
   [SerializeField] private Image barImage;

   private IHasProgress hasProgress;
   
   private void Start()
   {
      hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

      if (hasProgress is null)
         throw new MissingComponentException(
            $"The GameObject {hasProgressGameObject.name} does not have a component that implements IHasProgress.");
      
      hasProgress.OnProgressChanged += OnProgressChanged;

      barImage.fillAmount = 0f;
      gameObject.SetActive(false);
   }

   private void OnProgressChanged(float progressNormalized)
   {
      barImage.fillAmount = progressNormalized;

      gameObject.SetActive(progressNormalized is > 0f and < 1f);
   }
}
