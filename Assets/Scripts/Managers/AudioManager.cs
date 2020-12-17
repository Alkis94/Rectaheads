using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } = null;

    [SerializeField]
    private AudioClip death;
    [SerializeField]
    private AudioClip swallow;
    [SerializeField]
    private AudioClip pain;
    [SerializeField]
    private AudioClip money;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = death;
    }

    public void PlayDeathSound()
    {
        audioSource.Play();
    }

    public void PlayPainSound()
    {
        audioSource.PlayOneShot(pain);
    }

    public void PlaySwallowSound()
    {
        audioSource.PlayOneShot(swallow);
    }

    public void PlayMoneySound()
    {
        audioSource.PlayOneShot(money);
    }
}
