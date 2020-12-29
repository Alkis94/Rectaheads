using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } = null;

    
    private int gems = 95000;
    private TextMeshProUGUI gemsText;

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
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public int Gems
    {
        get
        {
            return gems;
        }

        set
        {
            if(value > 0)
            {
                gems = value;
            }
            else
            {
                gems = 0;
            }

            if(gemsText != null)
            {
                gemsText.text = Gems.ToString();
            }

            ES3.Save("Gems", gems, "Save/General");
        }
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
