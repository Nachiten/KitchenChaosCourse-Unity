using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClipsSO audioClipsSO;
    
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
        CuttingCounter.OnAnyCut += OnAnyCut;
        Player.Instance.OnPickedSomething += OnPickedSomething;
        BaseCounter.OnAnyObjectPlaced += OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += OnAnyObjectTrashed;
    }

    private void OnAnyObjectTrashed(TrashCounter baseCounter)
    {
        PlaySound(audioClipsSO.trash, baseCounter.transform.position);
    }

    private void OnAnyObjectPlaced(BaseCounter baseCounter)
    {
        PlaySound(audioClipsSO.objectDrop, baseCounter.transform.position);
    }

    private void OnPickedSomething()
    {
        PlaySound(audioClipsSO.objectPickup, Player.Instance.transform.position);
    }

    private void OnAnyCut(CuttingCounter cuttingCounter)
    {
        PlaySound(audioClipsSO.chop, cuttingCounter.transform.position);
    }

    private void OnRecipeSuccess()
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsSO.deliverySuccess, deliveryCounter.transform.position);
    }
    
    private void OnRecipeFailed()
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsSO.deliveryFail, deliveryCounter.transform.position);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }
    
    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipsSO.footstep, position, volume);
    }
}
