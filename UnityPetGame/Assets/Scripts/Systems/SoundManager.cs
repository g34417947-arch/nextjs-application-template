using UnityEngine;

/// <summary>
/// Manages all audio in the game including music and sound effects
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Music Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    
    [Header("Sound Effect Clips")]
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip coinSound;
    
    public static SoundManager Instance { get; private set; }
    
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    
    [Range(0f, 1f)]
    public float sfxVolume = 0.7f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
        
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();
        
        musicSource.loop = true;
        PlayBackgroundMusic();
    }
    
    /// <summary>
    /// Plays background music
    /// </summary>
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }
    
    /// <summary>
    /// Plays a sound effect
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
            sfxSource.PlayOneShot(clip);
        }
    }
    
    /// <summary>
    /// Plays button click sound
    /// </summary>
    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSound);
    }
    
    /// <summary>
    /// Plays level up sound
    /// </summary>
    public void PlayLevelUp()
    {
        PlaySFX(levelUpSound);
    }
    
    /// <summary>
    /// Plays coin collection sound
    /// </summary>
    public void PlayCoinSound()
    {
        PlaySFX(coinSound);
    }
    
    /// <summary>
    /// Sets music volume
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
            musicSource.volume = musicVolume;
    }
    
    /// <summary>
    /// Sets sound effects volume
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}
