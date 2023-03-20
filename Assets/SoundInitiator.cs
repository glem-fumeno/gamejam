using UnityEngine;

public class SoundInitiator : MonoBehaviour
{
    public AudioSource mainMusic;

    private void Awake()
    {
        var settings = SaveManager.Load<SettingsManager.Settings>(SettingsManager.SettingsFile);
        if (settings == null)
        {
            return;
        }

        AudioListener.volume = settings.SoundActive ? settings.SoundVolume : 0;
        mainMusic.volume = settings.MusicActive ? settings.MusicVolume : 0;
    }
}
