using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MedicineManager : MonoBehaviour
{
    public static MedicineManager Instance { get; private set; } = null;
    [SerializeField]
    private RectTransform[] medicine = new RectTransform[5];
    private ToggleGroup toggleGroup;
    public MedicineType CurrentMedicine { get; private set; }

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


    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
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
        medicine[1].localPosition = new Vector3(120, 44, 0);
        medicine[2].localPosition = new Vector3(120, 0, 0);
        medicine[3].localPosition = new Vector3(120, -44, 0);

        medicine[1].gameObject.SetActive(true);
        medicine[2].gameObject.SetActive(true);
        medicine[3].gameObject.SetActive(true);
                   
        medicine[0].gameObject.SetActive(false);
        medicine[4].gameObject.SetActive(false);
    }

    public void OnMedicineChanged ()
    {
        CurrentMedicine = toggleGroup.ActiveToggles().First().gameObject.GetComponent<Medicine>().MedicineType;
        //Debug.Log(CurrentMedicine);
    }
}
