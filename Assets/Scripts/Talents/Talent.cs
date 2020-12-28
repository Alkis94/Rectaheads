using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Talent : MonoBehaviour
{
    public bool IsMaxed { get; set; } = false;

    [SerializeField]
    private string talentName;
    [SerializeField]
    private string talentDescription;
    [SerializeField]
    private int talentCost;
    [SerializeField]
    private int talentUpgradeCount;
    [SerializeField]
    private int talentUpgradeMax;
    [SerializeField]
    private TextMeshProUGUI talentCostText;
    [SerializeField]
    private TextMeshProUGUI talentUpgradeText;
    private TalentEffect talentEffect;
    private Button button;
    [SerializeField]
    private List<Talent> previousTalents = new List<Talent>();
    [SerializeField]
    private List<Talent> nextTalents = new List<Talent>();
    [SerializeField]
    private bool requiresAllPrevious = false;


    public string TalentName
    {
        get
        {
            return talentName;
        }

        private set
        {
            talentName = value;
        }
    }

    public string TalentDescription
    {
        get
        {
            return talentDescription;
        }

        private set
        {
            talentDescription = value;
        }
    }

    public int TalentCost
    {
        get
        {
            return talentCost;
        }

        private set
        {
            talentCost = value;
        }
    }

    public int TalentUpgradeCount
    {
        get
        {
            return talentUpgradeCount;
        }

        set
        {
            talentUpgradeCount = value;
            talentUpgradeText.text = talentUpgradeCount.ToString() + " / " + talentUpgradeMax.ToString();
        }
    }

    public int TalentUpgradeMax
    {
        get
        {
            return talentUpgradeMax;
        }

        private set
        {
            talentUpgradeMax = value;
        }
    }


    void Start()
    {
        button = GetComponent<Button>();
        talentEffect = GetComponent<TalentEffect>();
        talentCostText.text = talentCost.ToString();
        talentUpgradeText.text = talentUpgradeCount.ToString() + " / " + talentUpgradeMax.ToString();
    }

    public void Effect()
    {
        talentEffect.Effect();
    }

    public void CheckNextForUnlocks()
    {
        if(nextTalents == null)
        {
            return;
        }

        for (int i = 0; i < nextTalents.Count; i++)
        {
            nextTalents[i].UnlockCheck();
        }
        
    }

    public void UnlockCheck()
    {
        if(requiresAllPrevious)
        {
            for (int i = 0; i < previousTalents.Count; i++)
            {
                if (!previousTalents[i].IsMaxed)
                {
                    return;
                }
            }

            button.interactable = true;
        }
        else
        {
            for(int i = 0; i < previousTalents.Count; i++)
            {
                if(previousTalents[i].IsMaxed)
                {
                    button.interactable = true;
                    return;
                }
            }
        }

    }

}
