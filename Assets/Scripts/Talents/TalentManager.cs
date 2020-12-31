using UnityEngine;
using System.Collections;
using TMPro;

public class TalentManager : MonoBehaviour
{

    public static TalentManager Instance { get; private set; } = null;

    [SerializeField]
    private GameObject talentMenu;
    [SerializeField]
    private TextMeshProUGUI talentName;
    [SerializeField]
    private TextMeshProUGUI talentDescription;
    [SerializeField]
    private TextMeshProUGUI talentCostText;
    [SerializeField]
    private TextMeshProUGUI talentUpgradeText;
    private Talent currentTalent;

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

    void Start()
    {

    }

    public void TalentPressed(Talent talent)
    {
        currentTalent = talent;
        talentMenu.SetActive(true);
        talentName.text = currentTalent.TalentName;
        talentDescription.text = currentTalent.TalentDescription;
        talentUpgradeText.text = currentTalent.TalentUpgradeCount.ToString() + " / " + currentTalent.TalentUpgradeMax.ToString();
        talentCostText.text = currentTalent.TalentCost.ToString();
    }

    public void UpgradePressed()
    {
        if(currentTalent.TalentCost <= GameManager.Instance.Gems)
        {
            GameManager.Instance.Gems -= currentTalent.TalentCost;

            if (currentTalent.TalentUpgradeCount < currentTalent.TalentUpgradeMax)
            {
                currentTalent.TalentUpgradeCount++;
                currentTalent.Effect();
                talentUpgradeText.text = currentTalent.TalentUpgradeCount.ToString() + " / " + currentTalent.TalentUpgradeMax.ToString();
            }

            if (currentTalent.TalentUpgradeCount == currentTalent.TalentUpgradeMax)
            {
                currentTalent.IsMaxed = true;
                currentTalent.CheckNextForUnlocks();
            }

            ES3.Save("Talent" +currentTalent.TalentID, currentTalent.TalentUpgradeCount, "Save/Talents");
        }
    }
}
