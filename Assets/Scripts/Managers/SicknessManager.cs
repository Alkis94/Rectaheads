using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SicknessManager : MonoBehaviour
{
    public static SicknessManager Instance { get; private set; } = null;
    public Rectahead[,] rectaheads { get; private set; } 
    private List <Vector2Int> aliveRectaheadsLocations = new List<Vector2Int>();

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
        rectaheads = new Rectahead[RectaheadManager.Instance.Rectaheads.GetLength(0), RectaheadManager.Instance.Rectaheads.GetLength(1)];

        for (int i = 0; i < RectaheadManager.Instance.Rectaheads.GetLength(0); i++)
        {
            for (int j = 0; j < RectaheadManager.Instance.Rectaheads.GetLength(1); j++)
            {
                if(RectaheadManager.Instance.Rectaheads[i, j] != null)
                {
                    rectaheads[i, j] = RectaheadManager.Instance.Rectaheads[i, j].GetComponent<Rectahead>();
                    aliveRectaheadsLocations.Add(new Vector2Int(i, j));
                }   
            }
        }

        StartCoroutine(UpdateEverySecond());
    }

    public void RemoveDeadRectahead(Vector2Int location)
    {
        aliveRectaheadsLocations.Remove(location);
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
                SicknessAttack(RandomRectahead(), SicknessType.virus, 40, 3);
                virusCooldown = Time.time + 30;
            }

            if (Time.time > bacteriaCooldown)
            {
                SicknessAttack(RandomRectahead(), SicknessType.bacteria, 70, 7);
                bacteriaCooldown = Time.time + 15;
            }


            if (Time.time > fungiCooldown)
            {
                SicknessAttack(RandomRectahead(), SicknessType.fungi, 60, 5);
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

    private Rectahead RandomRectahead()
    {
        if(aliveRectaheadsLocations.Count <= 0)
        {
            return null;
        }

        Vector2Int randomAliveRectahead = aliveRectaheadsLocations[Random.Range(0, aliveRectaheadsLocations.Count - 1)];
        return rectaheads[randomAliveRectahead.x, randomAliveRectahead.y];
    }

}
