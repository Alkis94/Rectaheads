using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class OptionsManager : MonoBehaviour
{

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider soundEffectsSlider;

    private void OnEnable()
    {
        if (ES3.FileExists("Save/General"))
        {
            if (ES3.KeyExists("MusicVolume", "Save/General"))
            {
                musicSlider.value = ES3.Load<float>("MusicVolume", "Save/General");
            }

            if (ES3.KeyExists("SoundEffectsVolume", "Save/General"))
            {
                soundEffectsSlider.value = ES3.Load<float>("SoundEffectsVolume", "Save/General");
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        ES3.Save<float>("MusicVolume", volume, "Save/General");
        volume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        ES3.Save<float>("SoundEffectsVolume", volume, "Save/General");
        volume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("SoundEffects", volume);
    }
}
