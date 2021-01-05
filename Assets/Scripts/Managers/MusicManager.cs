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

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = mainMenu;
        audioSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.StringStartsWith("Village"))
        {
            if (audioSource.clip != village)
            {
                StartCoroutine(PlayMusicWithDelay(village, 3));
            }
        }
        else if(scene.name.StringStartsWith("Town"))
        {
            if (audioSource.clip != town)
            {
                StartCoroutine(PlayMusicWithDelay(town, 3));
            }
        }
        else if (scene.name.StringStartsWith("City"))
        {
            if (audioSource.clip != city)
            {
                StartCoroutine(PlayMusicWithDelay(city, 3));
            }
        }
        else if (scene.name.StringStartsWith("Capital"))
        {
            if (audioSource.clip != capital)
            {
                StartCoroutine(PlayMusicWithDelay(capital, 3));
            }
        }
        else
        {
            if(audioSource.clip != mainMenu)
            {
                StartCoroutine(PlayMusicWithDelay(mainMenu, 3));
            }
        }
    }

    IEnumerator PlayMusicWithDelay(AudioClip audioClip, float delay)
    {
        if(delay > 1)
        {
            audioSource.FadeOut(delay - 1);
        }
        yield return new WaitForSeconds(delay);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
