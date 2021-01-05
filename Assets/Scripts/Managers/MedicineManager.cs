using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MedicineManager : MonoBehaviour
{
    public static MedicineManager Instance { get; private set; } = null;

    private RectTransform[] medicine;
    [SerializeField]
    private RectTransform[] unlockedMedicine = new RectTransform[4];
    [SerializeField]
    private RectTransform[] lockedMedicine = new RectTransform[3];
    private ToggleGroup toggleGroup;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private int money;
    private MedicineType currentMedicine;
    private int currentCost;

    public int Money
    {
        get
        {
            return money;
        }

        set
        {
            if(value > 0)
            {
                money = value;
            }
            else
            {
                money = 0;
            }

            moneyText.text = money.ToString();
        }
    }
    
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

        toggleGroup = GetComponent<ToggleGroup>();        
    }

    private void Start()
    {
        moneyText.text = money.ToString();
        FormMedicineArray();
        currentMedicine = toggleGroup.ActiveToggles().First().gameObject.GetComponent<Medicine>().MedicineType;
        Medicine medicine = toggleGroup.ActiveToggles().First().gameObject.GetComponent<Medicine>();
        currentMedicine = medicine.MedicineType;
        currentCost = medicine.Cost;
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayMouseOverSound();
    }


    private void FormMedicineArray()
    {
        int extraMedicineCounter = 0;

        if (TalentGlobals.MegaAntibioActive)
        {
            extraMedicineCounter++;
        }

        if (TalentGlobals.MegaVitaminActive)
        {
            extraMedicineCounter++;
        }

        if (TalentGlobals.EmergencyActive)
        {
            extraMedicineCounter++;
        }

        medicine = new RectTransform[4 + extraMedicineCounter];

        for (int i = 0; i < 4; i++)
        {
            medicine[i] = unlockedMedicine[i];
        }

        if (TalentGlobals.MegaAntibioActive)
        {
            medicine[4] = lockedMedicine[0];
        }

        if (TalentGlobals.MegaVitaminActive)
        {
            if (extraMedicineCounter == 1)
            {
                medicine[4] = lockedMedicine[1];
            }
            if (extraMedicineCounter == 2)
            {
                if(TalentGlobals.MegaAntibioActive)
                {
                    medicine[5] = lockedMedicine[1];
                }
                else
                {
                    medicine[4] = lockedMedicine[1];
                }
            }
            else if (extraMedicineCounter == 3)
            {
                medicine[5] = lockedMedicine[1];
            }
        }

        if (TalentGlobals.EmergencyActive)
        {
            if (extraMedicineCounter == 1)
            {
                medicine[4] = lockedMedicine[2];
            }
            else if (extraMedicineCounter == 2)
            {
                medicine[5] = lockedMedicine[2];
            }
            else if (extraMedicineCounter == 3)
            {
                medicine[6] = lockedMedicine[2];
            }
        }
    }

    public void RectaheadWasClicked(Rectahead rectahead)
    {
        if (rectahead.IsSick)
        {
            switch (rectahead.SicknessType)
            {
                case SicknessType.virus:
                    break;
                case SicknessType.bacteria:
                    if (currentMedicine == MedicineType.antibiotic && currentCost <= Money)
                    {
                        Money -= currentCost;
                        rectahead.Animator.SetTrigger("SwallowPill");
                        rectahead.Recover();
                        AudioManager.Instance.PlaySwallowSound();
                    }
                    break;
                case SicknessType.fungi:
                    if (currentMedicine == MedicineType.antifungal && currentCost <= Money)
                    {
                        Money -= currentCost;
                        rectahead.Recover();
                        rectahead.Animator.SetTrigger("SwallowSyrop");
                        AudioManager.Instance.PlaySwallowSound();
                    }
                    break;
                default:
                    break;
            }
        }

        if (currentMedicine == MedicineType.vaccine && currentCost <= Money)
        {
            if (!rectahead.IsSick && rectahead.IsAlive)
            {
                Money -= currentCost;
                rectahead.AddImmunity(15 + TalentGlobals.ExtraVaccineEffect);
                rectahead.Animator.SetTrigger("Pain");
                AudioManager.Instance.PlayPainSound();
            }
        }

        if (currentMedicine == MedicineType.vitamin && currentCost <= Money)
        {
            if (rectahead.IsAlive)
            {
                Money -= currentCost;
                rectahead.ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                rectahead.Animator.SetTrigger("SwallowPill");
                AudioManager.Instance.PlaySwallowSound();
            }
        }

        if (currentMedicine == MedicineType.megaAntibiotic && currentCost <= Money)
        {
            if (rectahead.IsAlive)
            {
                Money -= currentCost;
                rectahead.Animator.SetTrigger("SwallowPill");
                AudioManager.Instance.PlaySwallowSound();
                ApplyMegas(rectahead.Location);
            }

            if(rectahead.IsSick)
            {
                rectahead.Recover();
            }
        }

        if (currentMedicine == MedicineType.megaVitamins && currentCost <= Money)
        {
            if (rectahead.IsAlive)
            {
                Money -= currentCost;
                rectahead.ImmuneSystemDefense += 35;
                rectahead.Animator.SetTrigger("SwallowPill");
                AudioManager.Instance.PlaySwallowSound();
                ApplyMegas(rectahead.Location);
            }
        }

        if(currentMedicine == MedicineType.emergency && currentCost <= Money)
        {
            AudioManager.Instance.PlayEmergencySound();
            Money -= currentCost;
            ApplyEmergency();
        }

    }

    public void OnMedicineChanged()
    {
        AudioManager.Instance.PlayMedicineChooseSound();
        currentMedicine = toggleGroup.ActiveToggles().First().gameObject.GetComponent<Medicine>().MedicineType;
        Medicine medicine = toggleGroup.ActiveToggles().First().gameObject.GetComponent<Medicine>();
        currentMedicine = medicine.MedicineType;
        currentCost = medicine.Cost;
    }


    public void OnArrowUpPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        ExtensionMethods.ShiftLeft(ref medicine, 1);
        MakeButtonChanges();
    }

    public void OnArrowDownPressed()
    {
        AudioManager.Instance.PlayButtonClickSound();
        ExtensionMethods.ShiftRight(ref medicine, 1);
        MakeButtonChanges();
    }

    private void MakeButtonChanges()
    {
        medicine[0].localPosition = new Vector3(126, 43, 0);
        medicine[1].localPosition = new Vector3(126, 0, 0);
        medicine[2].localPosition = new Vector3(126, -43, 0);

        for(int i = 3; i < medicine.Length; i++)
        {
            medicine[i].localPosition = new Vector3(126, -120, 0);
        }
    }

    private void ApplyEmergency()
    {
        for(int i = 0; i < RectaheadManager.Instance.AliveRectaheadsLocations.Count; i++)
        {
            Vector2Int location = RectaheadManager.Instance.AliveRectaheadsLocations[i];
            if (RectaheadManager.Instance.Rectaheads[location.x, location.y].IsAlive && !RectaheadManager.Instance.Rectaheads[location.x, location.y].IsSick)
            {
                RectaheadManager.Instance.Rectaheads[location.x, location.y].AddImmunity(15 + TalentGlobals.ExtraVaccineEffect);
            }
        }
    }

    private void ApplyMegas(Vector2Int location)
    {

        if (location.x < RectaheadManager.Instance.Rectaheads.GetLength(0) - 1)
        {
            if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y] != null)
            {
                if(currentMedicine == MedicineType.megaAntibiotic)
                {
                    if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y].SicknessType == SicknessType.bacteria)
                    {
                        RectaheadManager.Instance.Rectaheads[location.x + 1, location.y].Recover();
                    }

                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y].Animator.SetTrigger("SwallowPill");
                }
                else if (currentMedicine == MedicineType.megaVitamins)
                {
                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y].ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y].Animator.SetTrigger("SwallowPill");
                }
            }
        }

        if (location.x > 0)
        {
            if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y] != null)
            {
                if (currentMedicine == MedicineType.megaAntibiotic)
                {
                    if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y].SicknessType == SicknessType.bacteria)
                    {
                        RectaheadManager.Instance.Rectaheads[location.x - 1, location.y].Recover();
                    }

                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y].Animator.SetTrigger("SwallowPill");
                }
                else if(currentMedicine == MedicineType.megaVitamins)
                {
                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y].ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y].Animator.SetTrigger("SwallowPill");
                }
            }
        }

        if (location.y < RectaheadManager.Instance.Rectaheads.GetLength(1) - 1)
        {
            if (RectaheadManager.Instance.Rectaheads[location.x, location.y + 1] != null)
            {
                if (currentMedicine == MedicineType.megaAntibiotic)
                {
                    if (RectaheadManager.Instance.Rectaheads[location.x, location.y + 1].SicknessType == SicknessType.bacteria)
                    {
                        RectaheadManager.Instance.Rectaheads[location.x, location.y + 1].Recover();
                    }

                    RectaheadManager.Instance.Rectaheads[location.x, location.y + 1].Animator.SetTrigger("SwallowPill");
                }
                else if (currentMedicine == MedicineType.megaVitamins)
                {
                    RectaheadManager.Instance.Rectaheads[location.x, location.y + 1].ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                    RectaheadManager.Instance.Rectaheads[location.x, location.y + 1].Animator.SetTrigger("SwallowPill");
                }
            }
        }

        if (location.y > 0)
        {
            if (RectaheadManager.Instance.Rectaheads[location.x, location.y - 1] != null)
            {
                if (currentMedicine == MedicineType.megaAntibiotic)
                {
                    if (RectaheadManager.Instance.Rectaheads[location.x, location.y - 1].SicknessType == SicknessType.bacteria)
                    {
                        RectaheadManager.Instance.Rectaheads[location.x, location.y - 1].Recover();
                    }

                    RectaheadManager.Instance.Rectaheads[location.x, location.y - 1].Animator.SetTrigger("SwallowPill");
                }
                else if (currentMedicine == MedicineType.megaVitamins)
                {
                    RectaheadManager.Instance.Rectaheads[location.x, location.y - 1].ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                    RectaheadManager.Instance.Rectaheads[location.x, location.y - 1].Animator.SetTrigger("SwallowPill");
                }
            }
        }

        if (location.x < RectaheadManager.Instance.Rectaheads.GetLength(0) - 1 && location.y < RectaheadManager.Instance.Rectaheads.GetLength(1) - 1)
        {
            if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y + 1] != null)
            {
                if (currentMedicine == MedicineType.megaAntibiotic)
                {
                    if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y + 1].SicknessType == SicknessType.bacteria)
                    {
                        RectaheadManager.Instance.Rectaheads[location.x + 1, location.y + 1].Recover();
                    }

                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y + 1].Animator.SetTrigger("SwallowPill");
                }
                else if (currentMedicine == MedicineType.megaVitamins)
                {
                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y + 1].ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y + 1].Animator.SetTrigger("SwallowPill");
                }
            }
        }

        if (location.x < RectaheadManager.Instance.Rectaheads.GetLength(0) - 1 && location.y > 0)
        {
            if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y - 1] != null)
            {
                if (currentMedicine == MedicineType.megaAntibiotic)
                {
                    if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y - 1].SicknessType == SicknessType.bacteria)
                    {
                        RectaheadManager.Instance.Rectaheads[location.x + 1, location.y - 1].Recover();
                    }

                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y - 1].Animator.SetTrigger("SwallowPill");
                }
                else if (currentMedicine == MedicineType.megaVitamins)
                {
                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y - 1].ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                    RectaheadManager.Instance.Rectaheads[location.x + 1, location.y - 1].Animator.SetTrigger("SwallowPill");
                }
            }
        }

        if (location.x > 0 && location.y < RectaheadManager.Instance.Rectaheads.GetLength(1) - 1)
        {
            if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y + 1] != null)
            {
                if (currentMedicine == MedicineType.megaAntibiotic)
                {
                    if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y + 1].SicknessType == SicknessType.bacteria)
                    {
                        RectaheadManager.Instance.Rectaheads[location.x - 1, location.y + 1].Recover();
                    }

                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y + 1].Animator.SetTrigger("SwallowPill");
                }
                else if (currentMedicine == MedicineType.megaVitamins)
                {
                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y + 1].ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y + 1].Animator.SetTrigger("SwallowPill");
                }
            }
        }

        if (location.x > 0 && location.y > 0)
        {
            if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y - 1] != null)
            {
                if (currentMedicine == MedicineType.megaAntibiotic)
                {
                    if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y - 1].SicknessType == SicknessType.bacteria)
                    {
                        RectaheadManager.Instance.Rectaheads[location.x - 1, location.y - 1].Recover();
                    }

                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y - 1].Animator.SetTrigger("SwallowPill");
                }
                else if (currentMedicine == MedicineType.megaVitamins)
                {
                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y - 1].ImmuneSystemDefense += 35 + TalentGlobals.ExtraVitaminEffect;
                    RectaheadManager.Instance.Rectaheads[location.x - 1, location.y - 1].Animator.SetTrigger("SwallowPill");
                }
            }
        }
    }


}
