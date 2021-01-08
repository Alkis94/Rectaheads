using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private RectTransform sceneFader;

    public static GameManager Instance { get; private set; } = null;

    public List<int> UnlockedRectaheadIDs { get; set; } = new List<int>();
    public bool[] IsRectaheadUnlocked { get; set; } = new bool[30];
    public LevelType LastPlayedLevel { get; private set; }

    private int gems = 0;
    private TextMeshProUGUI gemsText;
    

    public int Gems
    {
        get
        {
            return gems;
        }

        set
        {
            if (value > 0)
            {
                gems = value;
            }
            else
            {
                gems = 0;
            }

            if (gemsText != null)
            {
                gemsText.text = Gems.ToString();
            }

            ES3.Save("Gems", gems, "Save/General");
        }
    }

    private void Awake()
    {
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }
        else
        {
            // Here we save our singleton instance
            Instance = this;
        }

        TalentGlobals.LoadTalentGlobals();
        LoadUnlockedRectaheads();
    }

    private void Start()
    {
        LoadOptions();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Time.timeScale = 1;
        GameObject tempGameObject = GameObject.FindGameObjectWithTag("Gems");
        if(tempGameObject != null)
        {
            gemsText = tempGameObject.GetComponent<TextMeshProUGUI>();
        }
        
        if(gemsText != null)
        {
            gemsText.text = Gems.ToString();
        }

        if (scene.name.StringStartsWith("Village"))
        {
            LastPlayedLevel = LevelType.village;
            ES3.Save<int>("LastPlayedLevel", (int)LastPlayedLevel, "Save/General");
        }
        else if (scene.name.StringStartsWith("Town"))
        {
            LastPlayedLevel = LevelType.town;
            ES3.Save<int>("LastPlayedLevel", (int)LastPlayedLevel, "Save/General");
        }
        else if (scene.name.StringStartsWith("City"))
        {
            LastPlayedLevel = LevelType.city;
            ES3.Save<int>("LastPlayedLevel", (int)LastPlayedLevel, "Save/General");
        }
        else if (scene.name.StringStartsWith("Capital"))
        {
            LastPlayedLevel = LevelType.capital;
            ES3.Save<int>("LastPlayedLevel", (int)LastPlayedLevel, "Save/General");
        }


    }

    public void LoadSceneWithFade(string scene)
    {
        StartCoroutine(StartLoadSceneWithFade(scene));
    }

    public void LoadSceneWithFade(int scene)
    {
        StartCoroutine(StartLoadSceneWithFade(scene));
    }

    IEnumerator StartLoadSceneWithFade(string scene)
    {
        LeanTween.alpha(sceneFader, 1f, 0.5f).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(scene);
        LeanTween.alpha(sceneFader, 0f, 0.5f).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
    }

    IEnumerator StartLoadSceneWithFade(int scene)
    {
        LeanTween.alpha(sceneFader, 1f, 0.5f).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(scene);
        LeanTween.alpha(sceneFader, 0f, 0.5f).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
    }

    private void LoadOptions()
    {
        if (ES3.FileExists("Save/General"))
        {
            if (ES3.KeyExists("Gems", "Save/General"))
            {
                Gems = ES3.Load<int>("Gems", "Save/General");
            }

            if (ES3.KeyExists("MusicVolume", "Save/General"))
            {
                float musicVolume = ES3.Load<float>("MusicVolume", "Save/General");
                audioMixer.SetFloat("Music", musicVolume);
            }

            if (ES3.KeyExists("SoundEffectsVolume", "Save/General"))
            {
                float soundEffectsVolume = ES3.Load<float>("SoundEffectsVolume", "Save/General");
                audioMixer.SetFloat("SoundEffects", soundEffectsVolume);
            }
            if (ES3.KeyExists("LastPlayedLevel", "Save/General"))
            {
                LastPlayedLevel = (LevelType)ES3.Load<int>("LastPlayedLevel", "Save/General");
            }
        }
    }

    private void LoadUnlockedRectaheads()
    {
        if (ES3.FileExists("Save/Shop"))
        {
            string[] keys = ES3.GetKeys("Save/Shop");

            for (int i = 0; i < keys.Length; i++)
            {
                int rectaheadID = ES3.Load<int>(keys[i], "Save/Shop");
                IsRectaheadUnlocked[rectaheadID - 16] = true;
                UnlockedRectaheadIDs.Add(rectaheadID);
            }
        }
    }

}
