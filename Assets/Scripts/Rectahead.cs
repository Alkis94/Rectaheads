using UnityEngine;
using System.Collections;

public class Rectahead : MonoBehaviour
{
    public Vector2Int Location { get; set; }
    public bool IsImmune{ get; private set; } = false;
    public bool IsAlive { get; private set; } = true;
    public bool IsSick { get; private set; } = false;
    public SicknessType SicknessType { get; private set; } = SicknessType.none;

    private float sicknessDuration;
    private float timeSick = 0;
    private float immunityDuration = 0;
    [SerializeField]
    private float immuneSystemDefense;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float ImmuneSystemDefense
    {
        get
        {
            return immuneSystemDefense;
        }
        set
        {
            if(value > 100)
            {
                immuneSystemDefense = 100;
            }
            else if ( value < 0 )
            {
                immuneSystemDefense = 0;
            }
            else
            {
                immuneSystemDefense = value;
            }
        }
    }

    private void Awake()
    {
        ImmuneSystemDefense = Random.Range(ImmuneSystemDefense - 20, ImmuneSystemDefense + 20);
        ImmuneSystemDefense = Mathf.Round(ImmuneSystemDefense);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {   
        StartCoroutine(UpdateEverySecond());
    }

    private void OnMouseDown()
    {
        if(IsSick)
        {
            switch (SicknessType)
            {
                case SicknessType.virus:
                    break;
                case SicknessType.bacteria:
                    if (MedicineManager.Instance.CurrentMedicine == MedicineType.antibiotic)
                    {
                        Recover();
                    }
                    break;
                case SicknessType.fungi:
                    if (MedicineManager.Instance.CurrentMedicine == MedicineType.antifungal)
                    {
                        Recover();
                    }
                    break;
                default:
                    break;
            }
        }

        if(MedicineManager.Instance.CurrentMedicine == MedicineType.vaccine)
        {
            if(!IsSick && IsAlive)
            {
                AddImmunity(15);
            }
        }

        if (MedicineManager.Instance.CurrentMedicine == MedicineType.vitamin)
        {
            if (IsAlive)
            {
                ImmuneSystemDefense += 35;
            }
        }

    }

    public void GetSick(SicknessType sicknessType)
    {
        IsSick = true;
        SicknessType = sicknessType;

        switch (SicknessType)
        {
            case SicknessType.virus:
                sicknessDuration = 15;
                spriteRenderer.color = Color.red;
                break;
            case SicknessType.bacteria:
                sicknessDuration = 10;
                spriteRenderer.color = Color.green;
                break;
            case SicknessType.fungi:
                sicknessDuration = 20;
                spriteRenderer.color = Color.magenta;
                break;
            default:
                break;
        }
    }

    public void Recover()
    {
        IsSick = false;
        SicknessType = SicknessType.none;
        sicknessDuration = 0;
        timeSick = 0;
        spriteRenderer.color = Color.white;
    }

    public void AddImmunity(float duration)
    {
        spriteRenderer.color = Color.cyan;
        IsImmune = true;
        immunityDuration += duration;
    }

    private void RemoveImmunity()
    {
        spriteRenderer.color = Color.white;
        IsImmune = false;
        immunityDuration = 0;
    }



    private IEnumerator UpdateEverySecond()
    {
        while(true)
        {
            if(IsSick)
            {
                switch(SicknessType)
                {
                    case SicknessType.virus:
                        ImmuneSystemDefense -= 1f;
                        SpreadSickness(SicknessType.virus, 30, 1, true, false);
                        CheckForKill(15, 1);
                        break;
                    case SicknessType.bacteria:
                        ImmuneSystemDefense -= 2;
                        SpreadSickness(SicknessType.bacteria, 50, 3, true, true);
                        CheckForKill(30, 3);
                        break;
                    case SicknessType.fungi:
                        ImmuneSystemDefense -= 3;
                        SpreadSickness(SicknessType.fungi, 30, 1, false, true);
                        CheckForKill(5, 0);
                        break;
                    default:
                        break;
                }

                timeSick += 1;
                if(sicknessDuration <= timeSick)
                {
                    Recover();
                }
            }
            else
            {
                ImmuneSystemDefense -= 0.25f;
            }

            if(IsImmune)
            {
                immunityDuration--;

                if(immunityDuration <= 0)
                {
                    RemoveImmunity();
                }

            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void CheckForKill(float maxKillRoll, float randomKillChance )
    {
        float randomNumber = Random.Range(0,  Mathf.Sin( Mathf.PI / sicknessDuration * timeSick) * maxKillRoll);
        randomNumber = Mathf.Round(randomNumber);

        if (randomNumber > immuneSystemDefense)
        {
            Kill();
            return;
        }

        randomNumber = Random.Range(0, 100);

        if (randomNumber < randomKillChance)
        {
            Kill();
            return;
        }
    }

    private void Kill()
    {
        IsAlive = false;
        animator.SetTrigger("Die");
        StopAllCoroutines();
        spriteRenderer.color = Color.white;
        SicknessManager.Instance.RemoveDeadRectahead(Location);
    }

    private void SpreadSickness(SicknessType sicknessType, float spreadAttack, float spreadChance, bool spreadSquare, bool spreadCross)
    {

        spreadAttack *= Mathf.Sin(Mathf.PI / sicknessDuration * timeSick);

        if (Location.x < SicknessManager.Instance.rectaheads.GetLength(0) - 1 && spreadSquare)
        {
            SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x + 1, Location.y], sicknessType, spreadAttack, spreadChance);
        }

        if (Location.x > 0 && spreadSquare)
        {
            SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x - 1, Location.y], sicknessType, spreadAttack, spreadChance);
        }

        if (Location.y < SicknessManager.Instance.rectaheads.GetLength(1) - 1 && spreadSquare)
        {
            SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x, Location.y + 1], sicknessType, spreadAttack, spreadChance);
        }

        if (Location.y > 0 && spreadSquare)
        {
            SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x, Location.y - 1], sicknessType, spreadAttack, spreadChance);
        }

        if (Location.x < SicknessManager.Instance.rectaheads.GetLength(0) - 1 && Location.y < SicknessManager.Instance.rectaheads.GetLength(1) - 1 && spreadCross)
        {
            SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x + 1, Location.y + 1], sicknessType, spreadAttack, spreadChance);
        }

        if (Location.x < SicknessManager.Instance.rectaheads.GetLength(0) - 1 && Location.y > 0 && spreadCross)
        {
            SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x + 1, Location.y - 1], sicknessType, spreadAttack, spreadChance);
        }

        if (Location.x > 0 && Location.y < SicknessManager.Instance.rectaheads.GetLength(1) - 1 && spreadCross)
        {
            SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x - 1, Location.y + 1], sicknessType, spreadAttack, spreadChance);
        }

        if (Location.x > 0 && Location.y > 0 && spreadCross)
        {
            SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x - 1, Location.y - 1], sicknessType, spreadAttack, spreadChance);
        }

    }
}
