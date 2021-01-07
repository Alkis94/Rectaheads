using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RectaheadManager : MonoBehaviour
{
    public static RectaheadManager Instance { get; private set; } = null;
    public Rectahead[,] Rectaheads { get; private set; }
    public List<Vector2Int> AliveRectaheadsLocations { get; private set; } =  new List<Vector2Int>();
    public List<Vector2Int> RectaheadsLocations { get; private set; } = new List<Vector2Int>();
    [SerializeField]
    private List<GameObject> rectaheadVariants;
    [SerializeField]
    private GameObject[] lockedRectaheadVariants = new GameObject [30];
    [SerializeField]
    private Vector2Int arraySize;
    private int[,] map;
    private TextMeshProUGUI rectaheadCount;
    public int RectaheadCurrentCount { get; private set; } = 0;
    public int RectaheadTotalCount { get; private set; } = 0 ;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        for(int i = 0; i < GameManager.Instance.UnlockedRectaheadIDs.Count; i++)
        {
            rectaheadVariants.Add(lockedRectaheadVariants[GameManager.Instance.UnlockedRectaheadIDs[i] - 16]);
        }

        Rectaheads = new Rectahead[arraySize.x, arraySize.y];
        map = MapManager.Instance.Map;

        for(int i = 0; i < map.GetLength(0); i += 2)
        {
            for (int j = 0; j < map.GetLength(1); j += 2)
            {
                if (map[i, j] == 2) SpawnRectaheadsInRoom(i, j);
            }
        }

    }

    private void Start()
    {
        rectaheadCount = GameObject.FindGameObjectWithTag("RectaheadCount").GetComponent<TextMeshProUGUI>();
        rectaheadCount.text = RectaheadCurrentCount + "/" + RectaheadTotalCount;
    }

    private void SpawnRectaheadsInRoom(int x, int y)
    {

        x = x / 2;
        y = y / 2;

        for (int i = x * 5; i < x * 5  + 5; i++)
        {
            for (int j = y * 3 ; j < y * 3 + 3; j++)
            {
                Rectaheads[i, j] = Instantiate(RandomRectaheadVariant(), ArrayToLevelCoordinates(i, j), Quaternion.identity, transform).GetComponent<Rectahead>();
                Rectaheads[i, j].Location = new Vector2Int(i, j);
                AliveRectaheadsLocations.Add(new Vector2Int(i, j));
                RectaheadsLocations.Add(new Vector2Int(i, j));
            }
        }

        RectaheadCurrentCount += 15;
        RectaheadTotalCount += 15;
    }

    private Vector3 ArrayToLevelCoordinates(int i, int j)
    {
        float x = -2 + i;
        float y =  1 - j;
        return new Vector3(x, y, 1);
    }

    public void ReduceRectaheadCount()
    {
        RectaheadCurrentCount--;
        rectaheadCount.text = RectaheadCurrentCount + "/" + RectaheadTotalCount;

        if( RectaheadCurrentCount  * 100 / RectaheadTotalCount < 25)
        {
            LevelEndManager.Instance.LevelFinished();
        }
    }

    public void RemoveDeadRectahead(Vector2Int location)
    {
        AliveRectaheadsLocations.Remove(location);
    }

    public Rectahead RandomAliveRectahead()
    {
        if (AliveRectaheadsLocations.Count <= 0)
        {
            return null;
        }

        Vector2Int randomAliveRectahead = AliveRectaheadsLocations[Random.Range(0, AliveRectaheadsLocations.Count - 1)];
        return Rectaheads[randomAliveRectahead.x, randomAliveRectahead.y];
    }

    public Vector3 RandomRectaheadLocation()
    {
        Vector2Int randomRectahead = AliveRectaheadsLocations[Random.Range(0, AliveRectaheadsLocations.Count - 1)];
        return Rectaheads[randomRectahead.x, randomRectahead.y].transform.position;
    }


    private GameObject RandomRectaheadVariant ()
    {
        int randomNumber = Random.Range(0, rectaheadVariants.Count);
        return rectaheadVariants[randomNumber];
    }
}
