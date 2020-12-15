using UnityEngine;
using System.Collections;

public class Rectahead : MonoBehaviour
{
    public Vector2Int Location { get; set; }
    public bool IsImmune{ get; private set; } = false;
    public bool IsAlive { get; private set; } = true;
    public bool IsSick { get; private set; } = false;
    
    public SicknessType SicknessType { get; private set; } = SicknessType.none;

    private readonly int moneyCooldown = 60;
    private float sicknessDuration;
    private float timeSick = 0;
    private float immunityDuration = 0;
    private bool hasMoney = false;
    [SerializeField]
    private float immuneSystemDefense;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject moneyBubble;
    [SerializeField]
    private SpriteRenderer immuneBar;
    [SerializeField]
    private Sprite[] immuneBarStates = new Sprite[6];

    public float ImmuneSystemDefense
    {
        get
        {
            return immuneSystemDefense;
        }
        set
        {
            if (value > 100)
            {
                immuneBar.sprite = immuneBarStates[5];
                immuneSystemDefense = 100;
            }
            else if (value > 80)
            {
                immuneBar.sprite = immuneBarStates[5];
                immuneSystemDefense = value;

            }
            else if (value > 60)
            {
                immuneBar.sprite = immuneBarStates[4];
                immuneSystemDefense = value;

            }
            else if (value > 40)
            {
                immuneBar.sprite = immuneBarStates[3];
                immuneSystemDefense = value;

            }
            else if (value > 20)
            {
                immuneBar.sprite = immuneBarStates[2];
                immuneSystemDefense = value;

            }
            else if (value > 0)
            {
                immuneBar.sprite = immuneBarStates[1];
                immuneSystemDefense = value;
            }
            else 
            {
                immuneBar.sprite = immuneBarStates[0];
                immuneSystemDefense = 0;
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

        if(hasMoney)
        {
            MedicineManager.Instance.Money += Random.Range(10,50);
            hasMoney = false;
            moneyBubble.SetActive(false);
        }
        else
        {
            MedicineManager.Instance.RectaheadWasClicked(this);
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
        int currentMoneyCooldown = Random.Range(0, moneyCooldown);

        while(true)
        {
            if(IsSick)
            {
                switch(SicknessType)
                {
                    case SicknessType.virus:
                        ImmuneSystemDefense -= 1f;
                        SpreadSickness(SicknessType.virus, 20, 5, true, false);
                        CheckForKill(15, 1);
                        break;
                    case SicknessType.bacteria:
                        ImmuneSystemDefense -= 2;
                        SpreadSickness(SicknessType.bacteria, 40, 7, true, true);
                        CheckForKill(30, 3);
                        break;
                    case SicknessType.fungi:
                        ImmuneSystemDefense -= 3;
                        SpreadSickness(SicknessType.fungi, 30, 3, false, true);
                        CheckForKill(5, 0);
                        break;
                    default:
                        break;
                }

                currentMoneyCooldown = moneyCooldown;
                moneyBubble.SetActive(false);
                hasMoney = false;

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

            if(currentMoneyCooldown <= 0 && !hasMoney)
            {
                currentMoneyCooldown = moneyCooldown;
                moneyBubble.SetActive(true);
                hasMoney = true;
            }
            else if (!hasMoney)
            {
                currentMoneyCooldown -= 1;
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
        hasMoney = false;
        moneyBubble.SetActive(false);
        immuneBar.gameObject.SetActive(false);
        animator.SetTrigger("Die");
        StopAllCoroutines();
        spriteRenderer.color = Color.white;
        SicknessManager.Instance.RemoveDeadRectahead(Location);
        RectaheadManager.Instance.ReduceRectaheadCount();
    }

    private void SpreadSickness(SicknessType sicknessType, float spreadAttack, float spreadChance, bool spreadSquare, bool spreadCross)
    {

        spreadAttack *= Mathf.Sin(Mathf.PI / sicknessDuration * timeSick);

        if (Location.x < SicknessManager.Instance.rectaheads.GetLength(0) - 1 && spreadSquare)
        {
            if(SicknessManager.Instance.rectaheads[Location.x + 1, Location.y] != null)
            {
                SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x + 1, Location.y], sicknessType, spreadAttack, spreadChance);
            }
        }

        if (Location.x > 0 && spreadSquare)
        {
            if (SicknessManager.Instance.rectaheads[Location.x - 1, Location.y] != null)
            {
                SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x - 1, Location.y], sicknessType, spreadAttack, spreadChance);
            }
        }

        if (Location.y < SicknessManager.Instance.rectaheads.GetLength(1) - 1 && spreadSquare)
        {
            if (SicknessManager.Instance.rectaheads[Location.x, Location.y + 1] != null)
            {
                SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x, Location.y + 1], sicknessType, spreadAttack, spreadChance);
            }
        }

        if (Location.y > 0 && spreadSquare)
        {
            if (SicknessManager.Instance.rectaheads[Location.x, Location.y - 1] != null)
            {
                SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x, Location.y - 1], sicknessType, spreadAttack, spreadChance);
            }
        }

        if (Location.x < SicknessManager.Instance.rectaheads.GetLength(0) - 1 && Location.y < SicknessManager.Instance.rectaheads.GetLength(1) - 1 && spreadCross)
        {
            if (SicknessManager.Instance.rectaheads[Location.x + 1, Location.y + 1] != null)
            {
                SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x + 1, Location.y + 1], sicknessType, spreadAttack, spreadChance);
            }
        }

        if (Location.x < SicknessManager.Instance.rectaheads.GetLength(0) - 1 && Location.y > 0 && spreadCross)
        {
            if (SicknessManager.Instance.rectaheads[Location.x + 1, Location.y - 1] != null)
            {
                SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x + 1, Location.y - 1], sicknessType, spreadAttack, spreadChance);
            }
        }

        if (Location.x > 0 && Location.y < SicknessManager.Instance.rectaheads.GetLength(1) - 1 && spreadCross)
        {
            if (SicknessManager.Instance.rectaheads[Location.x - 1, Location.y + 1] != null)
            {
                SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x - 1, Location.y + 1], sicknessType, spreadAttack, spreadChance);
            }
        }

        if (Location.x > 0 && Location.y > 0 && spreadCross)
        {
            if (SicknessManager.Instance.rectaheads[Location.x - 1, Location.y - 1] != null)
            {
                SicknessManager.Instance.SicknessAttack(SicknessManager.Instance.rectaheads[Location.x - 1, Location.y - 1], sicknessType, spreadAttack, spreadChance);
            }
        }

    }
}
