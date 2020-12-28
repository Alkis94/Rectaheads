using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarLockLoader : MonoBehaviour
{
    [SerializeField]
    private string level;
    [SerializeField]
    private GameObject[] stars = new GameObject[3];
    [SerializeField]
    private GameObject locked;

    void Start()
    {
        if(ES3.FileExists("Save/Levels/" + level))
        {
            int starCount = 0;

            if (ES3.KeyExists("Stars", "Save/Levels/" + level))
            {
                starCount = ES3.Load<int>("Stars", "Save/Levels/" + level);
            }
            

            if(ES3.KeyExists("Unlocked", "Save/Levels/" + level))
            {
                bool unlocked = ES3.Load<bool>("Unlocked", "Save/Levels/" + level);
                if (unlocked)
                {
                    locked.SetActive(false);
                    GetComponent<Button>().interactable = true;
                }
            }
            
            if (starCount > 0)
            {
                stars[0].SetActive(true);
            }

            if (starCount > 1)
            {
                stars[1].SetActive(true);
            }

            if (starCount > 2)
            {
                stars[2].SetActive(true);
            }
        }
    }
}
