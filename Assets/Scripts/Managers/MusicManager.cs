using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip mainMenu;
    [SerializeField]
    private AudioClip village;
    [SerializeField]
    private AudioClip town;
    [SerializeField]
    private AudioClip city;
    [SerializeField]
    private AudioClip capital;

    private AudioClip currentAudioClip;
    private bool isFading;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        currentAudioClip = mainMenu;
        audioSource.clip = mainMenu;
        audioSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.StringStartsWith("Village"))
        {
            if (currentAudioClip != village)
            {
                currentAudioClip = village;
                if (!isFading)
                {
                    StartCoroutine(FadeOutMusic(2));
                }
            }
        }
        else if(scene.name.StringStartsWith("Town"))
        {
            if (currentAudioClip != town)
            {
                currentAudioClip = town;
                if (!isFading)
                {
                    StartCoroutine(FadeOutMusic(2));
                }
            }
        }
        else if (scene.name.StringStartsWith("City"))
        {
            if (currentAudioClip != city)
            {
                currentAudioClip = city;
                if (!isFading)
                {
                    StartCoroutine(FadeOutMusic(2));
                }
            }
        }
        else if (scene.name.StringStartsWith("Capital"))
        {
            if (currentAudioClip != capital)
            {
                currentAudioClip = capital;
                if (!isFading)
                {
                    StartCoroutine(FadeOutMusic(2));
                }
            }
        }
        else
        {
            if(currentAudioClip != mainMenu)
            {
                currentAudioClip = mainMenu;
                if (!isFading)
                {
                    StartCoroutine(FadeOutMusic(2));
                }
            }
        }
    }

    private IEnumerator FadeOutMusic(float duration)
    {
        isFading = true;
        audioSource.FadeOut(duration);
        yield return new WaitForSeconds(duration + 1);
        isFading = false;
        audioSource.clip = currentAudioClip;
        audioSource.Play();
    }
}
