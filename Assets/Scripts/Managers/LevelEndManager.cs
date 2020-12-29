using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelEndManager : MonoBehaviour
{

    private int gems;
    [SerializeField]
    private GameObject miniMenu;
    [SerializeField]
    private GameObject[] stars = new GameObject[3];
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private TextMeshProUGUI gemsText;
    private AudioSource audioSource;

    private void OnEnable()
    {
        TimeCountDown.OnCountDownFinished += LevelFinished;
    }

    private void OnDisable()
    {
        TimeCountDown.OnCountDownFinished -= LevelFinished;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void LevelFinished()
    {

        miniMenu.SetActive(true);

        int starCount = 0;
        int rectaheadAlive = RectaheadManager.Instance.RectaheadCurrentCount;
        int rectaheadTotal = RectaheadManager.Instance.RectaheadTotalCount;
        float percentageAlive = (100 * rectaheadAlive) / rectaheadTotal;

        if(percentageAlive >= 25)
        {
            StartCoroutine(SetActiveWithDelay(0.25f, stars[0]));
            gems += 25;
            starCount++;
        }

        if (percentageAlive >= 50)
        {
            StartCoroutine(SetActiveWithDelay(0.75f, stars[1]));
            gems += 50;
            starCount++;
        }

        if (percentageAlive >= 75)
        {
            StartCoroutine(SetActiveWithDelay(1.25f, stars[2]));
            gems += 100;
            starCount++;
        }

        gems += rectaheadAlive * 1;
        StartCoroutine(GemCount());

        ES3.Save("Stars", starCount, "Save/Levels/" + SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            //Debug.Log("Next scene name : " + ExtensionMethods.NameFromBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
            ES3.Save("Unlocked", true, "Save/Levels/" + ExtensionMethods.NameFromBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));

            if(starCount > 0)
            {
                nextButton.SetActive(true);
            }
        }

        GameManager.Instance.Gems += gems;

        Time.timeScale = 0;
    }

    private IEnumerator GemCount()
    {
        audioSource.Play();

        for (int i = 0; i <= gems; i += 10)
        {
            gemsText.text = i.ToString();
            yield return new WaitForSecondsRealtime(0.02f);
        }

        audioSource.Stop();
    }

    private IEnumerator SetActiveWithDelay(float delay, GameObject someObject)
    {
        yield return new WaitForSecondsRealtime(delay);
        someObject.SetActive(true);
    }

}
