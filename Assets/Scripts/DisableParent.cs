using UnityEngine;
using System.Collections;

public class DisableParent : MonoBehaviour
{

    public void OnBackPressed()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
