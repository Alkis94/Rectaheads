using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private string levelToLoad;

    public void OnButtonPressed()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
