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

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayMouseOverSound();
    }

    public void OnLeftPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        LeanTween.moveX(firstLevels, -600, 0.5f);
        LeanTween.moveX(secondLevels, 0, 0.5f);
        leftArrow.interactable = false;
        rightArrow.interactable = true;
    }

    public void OnRightPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        LeanTween.moveX(firstLevels, 0, 0.5f);
        LeanTween.moveX(secondLevels, 600, 0.5f);
        rightArrow.interactable = false;
        leftArrow.interactable = true;
    }
}
