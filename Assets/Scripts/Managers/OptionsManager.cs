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
            if (ES3.KeyExists("MusicSliderValue", "Save/General"))
            {
                musicSlider.value = ES3.Load<float>("MusicSliderValue", "Save/General");
            }

            if (ES3.KeyExists("SoundEffectsSliderValue", "Save/General"))
            {
                soundEffectsSlider.value = ES3.Load<float>("SoundEffectsSliderValue", "Save/General");
            }
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        ES3.Save<float>("MusicSliderValue", sliderValue, "Save/General");
        float volume = Mathf.Log10(sliderValue) * 20;
        if (sliderValue == 0f)
        {
            volume = -80f;
        }
        audioMixer.SetFloat("Music", volume);
        ES3.Save<float>("MusicVolume", volume, "Save/General");
    }

    public void SetSoundEffectsVolume(float sliderValue)
    {
        ES3.Save<float>("SoundEffectsSliderValue", sliderValue, "Save/General");
        float volume = Mathf.Log10(sliderValue) * 20;
        if(sliderValue == 0f)
        {
            volume = -80f;
        }
        audioMixer.SetFloat("SoundEffects", volume);
        ES3.Save<float>("SoundEffectsVolume", volume, "Save/General");
    }
}
