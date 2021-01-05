using UnityEngine;
using System.Collections;

public class OpenCloseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayMouseOverSound();
    }

    public void OnButtonPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        menu.SetActive(!menu.activeInHierarchy);
    }
}
