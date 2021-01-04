using UnityEngine;
using System.Collections;

public class OpenCloseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void OnButtonPressed()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }
}
