using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelEndManager : MonoBehaviour
{
    public static LevelEndManager Instance { get; private set; } = null;

    private int gems;
    [SerializeField]
    private GameObject miniMenu;
    [SerializeField]
    private GameObject[] stars = new GameObject[3];
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private TextMeshProUGUI gemsText;
    [SerializeField]
    private TextMeshProUGUI gradeText;
    private AudioSource audioSource;
    private int oldStarCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (ES3.KeyExists("Stars", "Save/Levels/" + SceneManager.GetActiveScene().name))
        {
            oldStarCount = ES3.Load<int>("Stars", "Save/Levels/" + SceneManager.GetActiveScene().name);
        }
    }

    public void LevelFinished()
    {
        miniMenu.SetActive(true);
        int starCount = 0;
        int rectaheadAlive = RectaheadManager.Instance.RectaheadCurrentCount;
        int rectaheadTotal = RectaheadManager.Instance.RectaheadTotalCount;
        float percentageAlive = (100 * rectaheadAlive) / rectaheadTotal;

        if(percentageAlive >= 25)
        {
            StartCoroutine(SetActiveWithDelay(0.25f, stars[0]));
            starCount++;
        }

        if (percentageAlive >= 50)
        {
            StartCoroutine(SetActiveWithDelay(0.75f, stars[1]));
            starCount++;
        }

        if (percentageAlive >= 75)
        {
            StartCoroutine(SetActiveWithDelay(1.25f, stars[2]));
            starCount++;
        }

        GradePerformance(starCount);
        gems += starCount - oldStarCount > 0 ? 200 * (starCount - oldStarCount) : 0;
        gems += rectaheadAlive * 5;
        StartCoroutine(GemCount());

        if(ES3.KeyExists("Stars", "Save/Levels/" + SceneManager.GetActiveScene().name))
        {
            if(starCount > oldStarCount)
            {
                ES3.Save("Stars", starCount, "Save/Levels/" + SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            ES3.Save("Stars", starCount, "Save/Levels/" + SceneManager.GetActiveScene().name);
        }
        

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            if(starCount > 0)
            {
                nextButton.SetActive(true);
                ES3.Save("Unlocked", true, "Save/Levels/" + ExtensionMethods.NameFromBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
            }
        }

        GameManager.Instance.Gems += gems;
        Time.timeScale = 0;
    }

    private IEnumerator GemCount()
    {
        audioSource.Play();

        float countMultiplier;

        if (gems < 100)
        {
            countMultiplier = 1;
        }
        else
        {
            countMultiplier = 10;
        }

        for (int i = 0; i <= gems / countMultiplier; i ++)
        {
            gemsText.text = (i * countMultiplier).ToString() ;
            yield return new WaitForSecondsRealtime(0.02f);
        }

        if(gems%10 != 0 && countMultiplier == 10)
        {
            gemsText.text = gems.ToString();
        }


        audioSource.Stop();
    }

    private IEnumerator SetActiveWithDelay(float delay, GameObject someObject)
    {
        yield return new WaitForSecondsRealtime(delay);
        someObject.SetActive(true);
    }

    private void GradePerformance(int stars)
    {
        switch(stars)
        {
            case 0:
                gradeText.text = "Try Again";
                break;
            case 1:
                gradeText.text = "Good";
                break;
            case 2:
                gradeText.text = "Excellent";
                break;
            case 3:
                gradeText.text = "Amazing";
                break;
            default:
                gradeText.text = "Error 404";
                break;
        }
    }

}
