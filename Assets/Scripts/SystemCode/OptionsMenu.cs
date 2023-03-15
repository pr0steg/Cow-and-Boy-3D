using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    

    void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
    }

    public void updateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        AudioPlayerManager.instance.musicVolumeChanged();
    }

    public void updateEffectsVolume()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
        AudioPlayerManager.instance.effectVolumeChanged();
    }
}