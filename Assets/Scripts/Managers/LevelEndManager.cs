using UnityEngine;
using System.Collections;

public class LevelEndManager : MonoBehaviour
{

    private int gems;
    [SerializeField]
    private GameObject miniMenu;
    [SerializeField]
    private GameObject[] stars = new GameObject[3];
    [SerializeField]
    private GameObject nextButton;

    private void OnEnable()
    {
        TimeCountDown.OnCountDownFinished += LevelFinished;
    }

    private void OnDisable()
    {
        TimeCountDown.OnCountDownFinished -= LevelFinished;
    }


    void Start()
    {

    }

    private void LevelFinished()
    {
        int rectaheadAlive = RectaheadManager.Instance.RectaheadCurrentCount;
        int rectaheadTotal = RectaheadManager.Instance.RectaheadTotalCount;
        float percentageAlive = rectaheadAlive / rectaheadTotal * 100;

        if(percentageAlive >= 25)
        {
            stars[0].SetActive(true);
            gems += 500;
            nextButton.SetActive(true);
        }

        if (percentageAlive >= 50)
        {
            stars[1].SetActive(true);
            gems += 500;
        }

        if (percentageAlive >= 75)
        {
            stars[2].SetActive(true);
            gems += 500;
        }

        gems += rectaheadAlive * 10;

    }

}
