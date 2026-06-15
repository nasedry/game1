using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("AudioManager");
                _instance = go.AddComponent<AudioManager>();
            }
            return _instance;
        }
    }

    [Header("Music")]
    public AudioClip musicClip;
    [Range(0f, 1f)]
    public float musicVolume = 1f;
    public bool playOnStart = true;

    private AudioSource musicSource;
    private const string PREF_MUSIC = "music_on";
    private const string PREF_MUSIC_VOLUME = "music_volume";

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = GetComponent<AudioSource>();
            if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();

            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.playOnAwake = false;
            musicSource.volume = PlayerPrefs.HasKey(PREF_MUSIC_VOLUME) ? PlayerPrefs.GetFloat(PREF_MUSIC_VOLUME) : musicVolume;

            bool enabled = PlayerPrefs.GetInt(PREF_MUSIC, 1) == 1;
            SetMusicEnabled(enabled, false);

            if (enabled && playOnStart && musicSource.clip != null)
                musicSource.Play();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicEnabled(bool enabled, bool save = true)
    {
        if (musicSource == null) return;
        musicSource.mute = !enabled;
        if (enabled && !musicSource.isPlaying)
            musicSource.Play();
        else if (!enabled && musicSource.isPlaying)
            musicSource.Pause();

        if (save) PlayerPrefs.SetInt(PREF_MUSIC, enabled ? 1 : 0);
    }

    public bool IsMusicEnabled()
    {
        return musicSource != null && !musicSource.mute;
    }

    public void SetVolume(float vol)
    {
        if (musicSource == null) return;
        musicSource.volume = Mathf.Clamp01(vol);
        PlayerPrefs.SetFloat(PREF_MUSIC_VOLUME, musicSource.volume);
    }
}
