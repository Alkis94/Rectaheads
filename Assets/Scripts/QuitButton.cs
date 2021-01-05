using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour
{

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayMouseOverSound();
    }

    public void OnQuitPressed()
    {
       AudioManager.Instance.PlayButtonClickSound();
       Application.Quit();
    }
}
