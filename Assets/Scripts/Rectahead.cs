using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Rectahead : MonoBehaviour
{
    public Vector2Int Location { get; set; }
    public bool IsImmune{ get; private set; } = false;
    public bool IsAlive { get; private set; } = true;
    public bool IsSick { get; private set; } = false;
    
    public SicknessType SicknessType { get; private set; } = SicknessType.none;

    private readonly int moneyCooldown = 90;
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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (hasMoney)
            {
                MedicineManager.Instance.Money += Random.Range(10, 50);
                hasMoney = false;
                moneyBubble.SetActive(false);
                animator.SetBool("HasMoney", false);
                AudioManager.Instance.PlayMoneySound();
            }
            else
            {
                MedicineManager.Instance.RectaheadWasClicked(this, animator);
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
        IsImmune = true;
        immunityDuration += 5;
        spriteRenderer.color = Color.white;
        animator.SetBool("IsSick", false);
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
        float idleCooldown = Time.time + Random.Range(2,10);

        while(true)
        {

            animator.SetBool("IsSick", IsSick);

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
                        SpreadSickness(SicknessType.bacteria, 35, 7, true, true);
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
                animator.SetBool("HasMoney", false);

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
                currentMoneyCooldown = Random.Range(moneyCooldown - 20, moneyCooldown + 20);
                moneyBubble.SetActive(true);
                hasMoney = true;
                animator.SetBool("HasMoney", true);
            }
            else if (!hasMoney)
            {
                currentMoneyCooldown -= 1;
            }

            if(idleCooldown < Time.time)
            {
                animator.SetTrigger("Idle");
                idleCooldown += 5;
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
        GetComponent<BoxCollider2D>().enabled = false;
        IsAlive = false;
        hasMoney = false;
        moneyBubble.SetActive(false);
        immuneBar.gameObject.SetActive(false);
        switch (SicknessType)
        {
            case SicknessType.virus:
                animator.SetTrigger("VirusDeath");
                break;
            case SicknessType.bacteria:
                animator.SetTrigger("BacteriaDeath");
                break;
            case SicknessType.fungi:
                animator.SetTrigger("FungiDeath");
                break;
            default:
                break;
        }
        StopAllCoroutines();
        spriteRenderer.color = Color.white;
        SicknessManager.Instance.RemoveDeadRectahead(Location);
        RectaheadManager.Instance.ReduceRectaheadCount();
        AudioManager.Instance.PlayDeathSound();
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
