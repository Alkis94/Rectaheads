using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectaheadManager : MonoBehaviour
{

    public static RectaheadManager Instance { get; private set; } = null;
    public GameObject[,] Rectaheads { get; private set; } 

    [SerializeField]
    private GameObject rectahead;
    [SerializeField]
    private Vector2Int arraySize;
    [SerializeField]
    private Vector2Int startingPoint;
    
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


        for (int i = 0; i < Rectaheads.GetLength(0); i++)
        {
            for(int j = 0; j < Rectaheads.GetLength(1); j++)
            {
                Rectaheads[i, j] = Instantiate(rectahead, ArrayToLevelCoordinates(i, j), Quaternion.identity, transform);
                Rectaheads[i, j].GetComponent<Rectahead>().Location = new Vector2Int(i, j);
            }
        }
    }

    private Vector2 ArrayToLevelCoordinates(int i, int j)
    {
        float x = startingPoint.x + i;
        float y = startingPoint.y - j;
        return new Vector2(x, y);
    }
}
