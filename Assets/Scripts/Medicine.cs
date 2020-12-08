using UnityEngine;
using System.Collections;

public class Medicine : MonoBehaviour
{
    [SerializeField]
    private int cost;
    [SerializeField]
    private MedicineType medicineType;

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
}
