using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public bool SoundActive = true;
    public bool MusicActive = true;
    public bool Fullscreen = true;

    public Image SoundButton;
    public Sprite SoundOn;
    public Sprite SoundOff;
    public Scrollbar SoundScrollbar;
    public TMPro.TextMeshProUGUI SoundValue;
    public Image MusicButton;
    public Sprite MusicOn;
    public Sprite MusicOff;
    public Scrollbar MusicScrollbar;
    public TMPro.TextMeshProUGUI MusicValue;
    public Image FullscreenButton;
    public Sprite Checkmark;
    public MenuManager Menu;

    public void onChange(){
        MusicButton.sprite = MusicActive ? MusicOn : MusicOff;
        MusicButton.SetNativeSize();
        MusicValue.text = Mathf.CeilToInt(MusicScrollbar.value * 100).ToString();
        SoundButton.sprite = SoundActive ? SoundOn : SoundOff;
        SoundButton.SetNativeSize();
        SoundValue.text = Mathf.CeilToInt(SoundScrollbar.value * 100).ToString();
        FullscreenButton.color = Fullscreen ? Color.white : Color.clear;
    }

    public void ToggleSoundActive()
    {
        SoundActive = !SoundActive;
        onChange();
    }
    public void ToggleMusicActive()
    {
        MusicActive = !MusicActive;
        onChange();
    }
    public void ToggleFullscreen()
    {
        Fullscreen = !Fullscreen;
        onChange();
    }

    private class Settings
    {
        public bool SoundActive;
        public bool MusicActive;
        public bool Fullscreen;
        public float SoundVolume;
        public float MusicVolume;
    }

    private const string SettingsFile = "settings.json";

    public void Persist()
    {
        var settings = new Settings
        {
            SoundActive = SoundActive,
            MusicActive = MusicActive,
            Fullscreen = Fullscreen,
            SoundVolume = SoundScrollbar.value,
            MusicVolume = MusicScrollbar.value
        };
        SaveManager.Save(SettingsFile, settings);
    }

    private void Awake()
    {
        var settings = SaveManager.Load<Settings>(SettingsFile);
        if (settings == null)
        {
            return;
        }
        SoundActive = settings.SoundActive;
        MusicActive = settings.MusicActive;
        Fullscreen = settings.Fullscreen;
        SoundScrollbar.value = settings.SoundVolume;
        MusicScrollbar.value = settings.MusicVolume;
        onChange();
    }
}
