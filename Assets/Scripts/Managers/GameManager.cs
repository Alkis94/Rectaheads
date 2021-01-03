using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } = null;

    public List<int> UnlockedRectaheadIDs { get; set; } = new List<int>();
    public bool[] IsRectaheadUnlocked { get; set; } = new bool[30];

    private int gems = 95000;
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

    void Awake()
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

        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }

        TalentGlobals.LoadTalentGlobals();

        if(ES3.FileExists("Save/General"))
        {
            if(ES3.KeyExists("Gems", "Save/General"))
            {
                Gems = ES3.Load<int>("Gems", "Save/General");
            }
        }

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

        GameObject tempGameObject = GameObject.FindGameObjectWithTag("Gems");
        if(tempGameObject != null)
        {
            gemsText = tempGameObject.GetComponent<TextMeshProUGUI>();
        }
        
        if(gemsText != null)
        {
            gemsText.text = Gems.ToString();
        }
    }

}
