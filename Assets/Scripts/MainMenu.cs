using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{


    

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    
}
