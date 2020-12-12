using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MedicineManager : MonoBehaviour
{
    public static MedicineManager Instance { get; private set; } = null;

    [SerializeField]
    private RectTransform[] medicine = new RectTransform[5];
    private float medicineCoordX;
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
        medicineCoordX = medicine[1].localPosition.x;
        
    }

    private void Start()
    {
        OnMedicineChanged();
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
                        rectahead.Recover();
                    }
                    break;
                case SicknessType.fungi:
                    if (currentMedicine == MedicineType.antifungal && currentCost <= Money)
                    {
                        Money -= currentCost;
                        rectahead.Recover();
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
                rectahead.AddImmunity(15);
            }
        }

        if (currentMedicine == MedicineType.vitamin && currentCost <= Money)
        {
            if (rectahead.IsAlive)
            {
                Money -= currentCost;
                rectahead.ImmuneSystemDefense += 35;
            }
        }
    }

    public void OnMedicineChanged()
    {
        currentMedicine = toggleGroup.ActiveToggles().First().gameObject.GetComponent<Medicine>().MedicineType;
        Medicine medicine = toggleGroup.ActiveToggles().First().gameObject.GetComponent<Medicine>();
        currentMedicine = medicine.MedicineType;
        currentCost = medicine.Cost;
    }


    public void OnArrowUpPressed()
    {
        ExtensionMethods.ShiftLeft(ref medicine, 1);
        MakeButtonChanges();
    }

    public void OnArrowDownPressed()
    {
        ExtensionMethods.ShiftRight(ref medicine, 1);
        MakeButtonChanges();
    }

    private void MakeButtonChanges()
    {
        medicine[1].localPosition = new Vector3(medicineCoordX, 44, 0);
        medicine[2].localPosition = new Vector3(medicineCoordX, 0, 0);
        medicine[3].localPosition = new Vector3(medicineCoordX, -44, 0);

        medicine[1].gameObject.SetActive(true);
        medicine[2].gameObject.SetActive(true);
        medicine[3].gameObject.SetActive(true);
                   
        medicine[0].gameObject.SetActive(false);
        medicine[4].gameObject.SetActive(false);
    }

    
}
