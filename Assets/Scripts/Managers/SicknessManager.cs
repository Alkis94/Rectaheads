using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SicknessManager : MonoBehaviour
{
    public static SicknessManager Instance { get; private set; } = null;

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
        float virusCooldown = Time.time + 30;
        float bacteriaCooldown = Time.time + 15;
        float fungiCooldown = Time.time + 20;

        while (true)
        {
            if(Time.time > virusCooldown)
            {
                SicknessAttack(RectaheadManager.Instance.RandomAliveRectahead(), SicknessType.virus, 40, 3);
                virusCooldown = Time.time + 30;
            }

            if (Time.time > bacteriaCooldown)
            {
                SicknessAttack(RectaheadManager.Instance.RandomAliveRectahead(), SicknessType.bacteria, 70, 7);
                bacteriaCooldown = Time.time + 15;
            }


            if (Time.time > fungiCooldown)
            {
                SicknessAttack(RectaheadManager.Instance.RandomAliveRectahead(), SicknessType.fungi, 60, 5);
                fungiCooldown = Time.time + 20;
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
