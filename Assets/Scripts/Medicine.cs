using UnityEngine;
using System.Collections;
using TMPro;

public class Medicine : MonoBehaviour
{
    [SerializeField]
    private int cost;
    [SerializeField]
    private MedicineType medicineType;
    private TextMeshProUGUI costText;

    public MedicineType MedicineType
    {
        get
        {
            return medicineType;
        }

        private set
        {
            medicineType = value;
        }
    }

    public int Cost
    {
        get
        {
            return cost;
        }

        private set
        {
            cost = value;
        }
    }

    private void Awake()
    {
       

    }

    private void Start()
    {

        switch (MedicineType)
        {
            case MedicineType.antibiotic:
                Cost -= TalentGlobals.AntibioDiscount;
                break;
            case MedicineType.megaAntibiotic:
                Cost -= TalentGlobals.AntibioDiscount;
                break;
            case MedicineType.vitamin:
                Cost -= TalentGlobals.VitaminDiscount;
                break;
            case MedicineType.megaVitamins:
                Cost -= TalentGlobals.VitaminDiscount;
                break;
            case MedicineType.vaccine:
                Cost -= TalentGlobals.VaccineDiscount;
                break;
            case MedicineType.antifungal:
                Cost -= TalentGlobals.AntifungalDiscount;
                break;
        }

        costText = GetComponentInChildren<TextMeshProUGUI>();
        costText.text = cost.ToString() + "$";
    }
}
