using UnityEngine;
using UnityEngine.UI;

public class ShopArrows : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectaheads30;
    [SerializeField]
    private RectTransform rectaheads45;
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
        LeanTween.moveX(rectaheads30, -200, 0.5f);
        LeanTween.moveX(rectaheads45, 0, 0.5f);
        leftArrow.interactable = false;
        rightArrow.interactable = true;
    }

    public void OnRightPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        LeanTween.moveX(rectaheads30, 0, 0.5f);
        LeanTween.moveX(rectaheads45, 200, 0.5f);
        rightArrow.interactable = false;
        leftArrow.interactable = true;
    }
}
