using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    private const string PLAYER_PREFS_MUSIC_VOLUME_KEY = "MusicVolume";
    
    [SerializeField] private AudioSource audioSource;
    
    private float volume;
    private const float defaultVolume = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        
        audioSource = GetComponent<AudioSource>();
        
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME_KEY, defaultVolume);
        audioSource.volume = volume;
    }

    private void Start()
    {
        GameManager.Instance.OnTogglePause += OnTogglePause;
    }

    private void OnTogglePause(bool isPaused)
    {
        SetMusicPause(isPaused);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;

        if (volume > 1f)
            volume = 0f;
        
        audioSource.volume = volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    
    public float GetVolume() => volume;

    private void SetMusicPause(bool isPaused)
    {
        if (isPaused)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }
}
