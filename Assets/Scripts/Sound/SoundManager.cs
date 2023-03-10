using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private const string PLAYER_PREFS_SOUND_VOLUME_KEY = "SoundEffectsVolume";
    
    [SerializeField] private AudioClipsSO audioClipsSO;

    private float volume;
    private const float defaultVolume = 1f;

    protected override void Awake()
    {
        base.Awake();
        
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME_KEY, defaultVolume);
    }

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
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
    
    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volumeMultiplier);
    }
    
    public void PlayFootstepsSound(Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipsSO.footstep, position, volumeMultiplier);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipsSO.warning, Vector3.zero);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;

        if (volume > 1f)
            volume = 0f;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    
    public float GetVolume() => volume;
}
