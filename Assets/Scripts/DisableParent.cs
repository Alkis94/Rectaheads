using UnityEngine;
using System.Collections;

public class DisableParent : MonoBehaviour
{

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayMouseOverSound();
    }

    public void OnBackPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        transform.parent.gameObject.SetActive(false);
    }
}
