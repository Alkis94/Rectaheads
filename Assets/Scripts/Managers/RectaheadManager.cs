using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RectaheadManager : MonoBehaviour
{
    public static RectaheadManager Instance { get; private set; } = null;
    public GameObject[,] Rectaheads { get; private set; }
    [SerializeField]
    private GameObject rectahead;
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

        Rectaheads = new GameObject[arraySize.x, arraySize.y];
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
                Rectaheads[i, j] = Instantiate(rectahead, ArrayToLevelCoordinates(i, j), Quaternion.identity, transform);
                Rectaheads[i, j].GetComponent<Rectahead>().Location = new Vector2Int(i, j);
            }
        }

        RectaheadCurrentCount += 15;
        RectaheadTotalCount += 15;
    }

    private Vector2 ArrayToLevelCoordinates(int i, int j)
    {
        float x = -2 + i;
        float y =  1 - j;
        return new Vector2(x, y);
    }

    public void ReduceRectaheadCount()
    {
        RectaheadCurrentCount--;
        rectaheadCount.text = RectaheadCurrentCount + "/" + RectaheadTotalCount;
    }
}
