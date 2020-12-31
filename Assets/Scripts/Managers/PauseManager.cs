using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeInHierarchy)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            
        }
    }

    public void OnContinuePressed()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void OnOptionsPressed()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
