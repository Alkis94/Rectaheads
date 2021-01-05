using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    [SerializeField]
    private AudioClip mouseOver;
    [SerializeField]
    private AudioClip buttonClick;
    [SerializeField]
    private AudioClip emergency;
    [SerializeField]
    private AudioClip medicineChoose;

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

    public void PlayMouseOverSound()
    {
        audioSource.PlayOneShot(mouseOver);
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    public void PlayEmergencySound()
    {
        audioSource.PlayOneShot(emergency);
    }

    public void PlayMedicineChooseSound()
    {
        audioSource.PlayOneShot(medicineChoose);
    }

}