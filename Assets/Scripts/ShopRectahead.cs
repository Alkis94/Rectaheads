using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ShopRectahead : MonoBehaviour
{
    [SerializeField]
    private int ID;
    [SerializeField]
    private int cost;
    [SerializeField]
    private TextMeshProUGUI gems;
    [SerializeField]
    private Image gemIcon;
    [SerializeField]
    private GameObject sold;
    private Button button;
    private AudioSource audioSource;

    private void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        gems.text = cost.ToString();

        if(GameManager.Instance.IsRectaheadUnlocked[ID - 16])
        {
            RectaheadBought();
        }
    }

    public void OnButtonPressed()
    {
        if(cost <= GameManager.Instance.Gems)
        {
            GameManager.Instance.Gems -= cost;
            audioSource.Play();
            ES3.Save(ID.ToString() ,ID, "Save/Shop");
            GameManager.Instance.UnlockedRectaheadIDs.Add(ID);
            GameManager.Instance.IsRectaheadUnlocked[ID - 16] = true;
            RectaheadBought();
        }
    }

    private void RectaheadBought()
    {
        button.interactable = false;
        gems.enabled = false;
        gemIcon.enabled = false;
        sold.SetActive(true);
    }
}
