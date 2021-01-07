using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SicknessManager : MonoBehaviour
{
    public static SicknessManager Instance { get; private set; } = null;

    [SerializeField]
    private float virusMaxAttack = 40;
    [SerializeField]
    private float virusInfectionChance = 1;
    [SerializeField]
    private readonly float virusCooldown = 30;

    [SerializeField]
    private float bacteriaMaxAttack = 70;
    [SerializeField]
    private float bacteriaInfectionChance = 7;
    [SerializeField]
    private readonly float bacteriaCooldown = 15;

    [SerializeField]
    private float fungiMaxAttack = 60;
    [SerializeField]
    private float fungiInfectionChance = 5;
    [SerializeField]
    private readonly float fungiCooldown = 20;

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
        StartCoroutine(UpdateEverySecond());
    }

    private IEnumerator UpdateEverySecond()
    {
        float virusCurrentCooldown = Time.time + virusCooldown;
        float bacteriaCurrentCooldown = Time.time + bacteriaCooldown;
        float fungiCurrentCooldown = Time.time + fungiCooldown;

        while (true)
        {
            if(Time.time > virusCurrentCooldown)
            {
                SicknessAttack(RectaheadManager.Instance.RandomAliveRectahead(), SicknessType.virus, virusMaxAttack, virusInfectionChance);
                virusCurrentCooldown = Time.time + virusCooldown;
            }

            if (Time.time > bacteriaCurrentCooldown)
            {
                SicknessAttack(RectaheadManager.Instance.RandomAliveRectahead(), SicknessType.bacteria, bacteriaMaxAttack, bacteriaInfectionChance);
                bacteriaCurrentCooldown = Time.time + bacteriaCooldown;
            }


            if (Time.time > fungiCurrentCooldown)
            {
                SicknessAttack(RectaheadManager.Instance.RandomAliveRectahead(), SicknessType.fungi, fungiMaxAttack, fungiInfectionChance);
                fungiCurrentCooldown = Time.time + fungiCooldown;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void SicknessAttack(Rectahead rectahead,SicknessType sicknessType, float maxInfectionAttack, float randomInfectionChance)
    {
        if (rectahead == null)
        {
            return;
        }

        if (rectahead.IsSick || !rectahead.IsAlive || rectahead.IsImmune)
        {
            return;
        }

        float randomNumber = Random.Range(0, maxInfectionAttack);
        if(randomNumber > rectahead.ImmuneSystemDefense)
        {
            rectahead.GetSick(sicknessType);
        }

        randomNumber = Random.Range(0, 100);

        if(randomNumber < randomInfectionChance)
        {
            rectahead.GetSick(sicknessType);
        }
    }

    public void SpreadSickness(Vector2Int location, SicknessType sicknessType, float spreadAttack, float spreadChance, SpreadType spreadType)
    {
        if (spreadType == SpreadType.cross || spreadType == SpreadType.square)
        {
            if (location.x < RectaheadManager.Instance.Rectaheads.GetLength(0) - 1)
            {
                if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y] != null)
                {
                    SicknessAttack(RectaheadManager.Instance.Rectaheads[location.x + 1, location.y], sicknessType, spreadAttack, spreadChance);
                }
            }

            if (location.x > 0)
            {
                if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y] != null)
                {
                    SicknessAttack(RectaheadManager.Instance.Rectaheads[location.x - 1, location.y], sicknessType, spreadAttack, spreadChance);
                }
            }

            if (location.y < RectaheadManager.Instance.Rectaheads.GetLength(1) - 1)
            {
                if (RectaheadManager.Instance.Rectaheads[location.x, location.y + 1] != null)
                {
                    SicknessAttack(RectaheadManager.Instance.Rectaheads[location.x, location.y + 1], sicknessType, spreadAttack, spreadChance);
                }
            }

            if (location.y > 0)
            {
                if (RectaheadManager.Instance.Rectaheads[location.x, location.y - 1] != null)
                {
                    SicknessAttack(RectaheadManager.Instance.Rectaheads[location.x, location.y - 1], sicknessType, spreadAttack, spreadChance);
                }
            }
        }

        if (spreadType == SpreadType.diagonal || spreadType == SpreadType.square)
        {

            if (location.x < RectaheadManager.Instance.Rectaheads.GetLength(0) - 1 && location.y < RectaheadManager.Instance.Rectaheads.GetLength(1) - 1)
            {
                if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y + 1] != null)
                {
                    SicknessAttack(RectaheadManager.Instance.Rectaheads[location.x + 1, location.y + 1], sicknessType, spreadAttack, spreadChance);
                }
            }

            if (location.x < RectaheadManager.Instance.Rectaheads.GetLength(0) - 1 && location.y > 0)
            {
                if (RectaheadManager.Instance.Rectaheads[location.x + 1, location.y - 1] != null)
                {
                    SicknessAttack(RectaheadManager.Instance.Rectaheads[location.x + 1, location.y - 1], sicknessType, spreadAttack, spreadChance);
                }
            }

            if (location.x > 0 && location.y < RectaheadManager.Instance.Rectaheads.GetLength(1) - 1)
            {
                if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y + 1] != null)
                {
                    SicknessAttack(RectaheadManager.Instance.Rectaheads[location.x - 1, location.y + 1], sicknessType, spreadAttack, spreadChance);
                }
            }

            if (location.x > 0 && location.y > 0)
            {
                if (RectaheadManager.Instance.Rectaheads[location.x - 1, location.y - 1] != null)
                {
                    SicknessAttack(RectaheadManager.Instance.Rectaheads[location.x - 1, location.y - 1], sicknessType, spreadAttack, spreadChance);
                }
            }
        }
    }

}
