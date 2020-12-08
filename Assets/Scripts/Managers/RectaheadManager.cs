using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectaheadManager : MonoBehaviour
{
    public static RectaheadManager Instance { get; private set; } = null;
    public GameObject[,] Rectaheads { get; private set; } = new GameObject[5, 3];

    [SerializeField]
    private GameObject rectahead;
    

    // Start is called before the first frame update
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
        float x = -2 + i;
        float y = -1 + j;
        return new Vector2(x, y);
    }
}
