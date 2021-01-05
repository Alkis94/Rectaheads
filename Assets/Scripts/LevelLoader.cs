using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private string levelToLoad;


    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayMouseOverSound();
    }

    public void LoadSpecific()
    {
        AudioManager.Instance.PlayButtonClickSound();
        GameManager.Instance.LoadSceneWithFade(levelToLoad);
        Time.timeScale = 1;
    }

    public void ReloadLevel()
    {
        AudioManager.Instance.PlayButtonClickSound();
        GameManager.Instance.LoadSceneWithFade(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void LoadNextLevel()
    {
        AudioManager.Instance.PlayButtonClickSound();
        GameManager.Instance.LoadSceneWithFade(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
}
