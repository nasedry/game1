using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Toggle musicToggle;
    public Slider musicVolumeSlider;

    private void Start()
    {
        if (musicToggle != null)
        {
            bool enabled = PlayerPrefs.GetInt("music_on", 1) == 1;
            musicToggle.isOn = enabled;
            musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        }

        if (musicVolumeSlider != null)
        {
            float vol = PlayerPrefs.HasKey("music_volume") ? PlayerPrefs.GetFloat("music_volume") : 1f;
            musicVolumeSlider.value = vol;
            musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    public void OnMusicToggleChanged(bool enabled)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicEnabled(enabled);
    }

    public void OnVolumeChanged(float vol)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetVolume(vol);
    }

    private void OnDestroy()
    {
        if (musicToggle != null)
            musicToggle.onValueChanged.RemoveListener(OnMusicToggleChanged);
        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
    }
}
