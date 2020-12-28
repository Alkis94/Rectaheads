using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    public void OnLeftPressed()
    {
        LeanTween.moveX(firstLevels, -600, 0.5f);
        LeanTween.moveX(secondLevels, 0, 0.5f);
        leftArrow.interactable = false;
        rightArrow.interactable = true;
    }

    public void OnRightPressed()
    {
        LeanTween.moveX(firstLevels, 0, 0.5f);
        LeanTween.moveX(secondLevels, 600, 0.5f);
        rightArrow.interactable = false;
        leftArrow.interactable = true;
    }
}
