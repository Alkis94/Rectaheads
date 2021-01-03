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

    public void OnLeftPressed()
    {
        LeanTween.moveX(rectaheads30, -600, 0.75f);
        LeanTween.moveX(rectaheads45, 0, 0.75f);
        leftArrow.interactable = false;
        rightArrow.interactable = true;
    }

    public void OnRightPressed()
    {
        LeanTween.moveX(rectaheads30, 0, 0.75f);
        LeanTween.moveX(rectaheads45, 600, 0.75f);
        rightArrow.interactable = false;
        leftArrow.interactable = true;
    }
}
