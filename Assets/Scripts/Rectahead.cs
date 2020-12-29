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
                float sicknessTimeFactor = CalculateSicknessTimeFactor();

                switch (SicknessType)
                {
                    case SicknessType.virus:
                        ImmuneSystemDefense -= 1f;
                        SicknessManager.Instance.SpreadSickness(Location, SicknessType.virus, 20 * sicknessTimeFactor, 5, SpreadType.cross);
                        CheckForKill(15 * sicknessTimeFactor, 1 * sicknessTimeFactor);
                        break;
                    case SicknessType.bacteria:
                        ImmuneSystemDefense -= 2;
                        SicknessManager.Instance.SpreadSickness(Location, SicknessType.bacteria, 35 * sicknessTimeFactor, 7, SpreadType.square);
                        CheckForKill(30 * sicknessTimeFactor, 3 * sicknessTimeFactor);
                        break;
                    case SicknessType.fungi:
                        ImmuneSystemDefense -= 3;
                        SicknessManager.Instance.SpreadSickness(Location, SicknessType.fungi, 30 * sicknessTimeFactor, 3, SpreadType.diagonal);
                        CheckForKill(5 * sicknessTimeFactor, 0);
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

    private float CalculateSicknessTimeFactor()
    {
        if( timeSick < 3)
        {
            return 0;
        }

        float sicknessStage = timeSick / sicknessDuration;

        if (sicknessStage > 0.25f && sicknessStage < 0.5f )
        {
            return 0.5f;
        }

        if(timeSick / sicknessDuration > 0.5f && sicknessStage < 0.75f)
        {
            return 1;
        }

        return 0.25f;
    }

    private void CheckForKill(float maxKillRoll, float randomKillChance )
    {
        float randomNumber = Random.Range(0, maxKillRoll);
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
        RectaheadManager.Instance.RemoveDeadRectahead(Location);
        RectaheadManager.Instance.ReduceRectaheadCount();
        AudioManager.Instance.PlayDeathSound();
    }
}
