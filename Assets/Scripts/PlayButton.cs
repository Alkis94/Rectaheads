using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayMouseOverSound();
    }

    public void OnPlayPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        GameManager.Instance.LoadSceneWithFade("LevelSelect");
    }

}
