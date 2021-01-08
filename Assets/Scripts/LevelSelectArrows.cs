using UnityEngine;
using UnityEngine.UI;

public class LevelSelectArrows : MonoBehaviour
{
    [SerializeField]
    private RectTransform firstLevels;
    [SerializeField]
    private RectTransform secondLevels;
    [SerializeField]
    private Button leftArrow;
    [SerializeField]
    private Button rightArrow;

    private void Start()
    {
        if(GameManager.Instance.LastPlayedLevel == LevelType.city || GameManager.Instance.LastPlayedLevel == LevelType.capital)
        {
            firstLevels.anchoredPosition = new Vector3(-600, firstLevels.anchoredPosition.y, 0);
            secondLevels.anchoredPosition = new Vector3(0, secondLevels.anchoredPosition.y, 0);
            leftArrow.interactable = false;
            rightArrow.interactable = true;
        }
    }

    public void OnLeftPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        LeanTween.moveX(firstLevels, -600, 0.5f).setEase(LeanTweenType.linear);
        LeanTween.moveX(secondLevels, 0, 0.5f).setEase(LeanTweenType.linear); 
        leftArrow.interactable = false;
        rightArrow.interactable = true;
    }

    public void OnRightPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        LeanTween.moveX(firstLevels, 0, 0.5f).setEase(LeanTweenType.linear); 
        LeanTween.moveX(secondLevels, 600, 0.5f).setEase(LeanTweenType.linear); 
        rightArrow.interactable = false;
        leftArrow.interactable = true;
    }
}
