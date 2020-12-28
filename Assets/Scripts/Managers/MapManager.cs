using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-99)]
public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; } = null;

    [SerializeField]
    private Vector2Int startingRoom = Vector2Int.zero;
    [SerializeField]
    private GameObject road;
    [SerializeField]
    private GameObject room;
    [SerializeField]
    private RectTransform currentLocationIcon;
    [SerializeField]
    private Transform mapParent;
    [SerializeField]
    private GameObject background;
    public int [,] Map { get; private set; }

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

        var fileContent = File.ReadAllText(Application.streamingAssetsPath + "/MapData/" + SceneManager.GetActiveScene().name + ".json");
        MapData mapData = JsonConvert.DeserializeObject<MapData>(fileContent);
        Map = mapData.map;
        InvertMap();
    }

    private void Start()
    {
        currentLocationIcon.localPosition = new Vector3(-15 + startingRoom.x * 3, 9 +  startingRoom.y * - 3, 0);
        Transform backgroundParent = GameObject.FindGameObjectWithTag("Background").transform;

        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                switch(Map[i,j])
                {
                    case 0:
                        break;
                    case 1:
                        ExtensionMethods.InstantiateAtLocalPosition(road, mapParent, ArrayToMapCoordinates(i, j));
                        break;
                    case 2:
                        ExtensionMethods.InstantiateAtLocalPosition(room, mapParent, ArrayToMapCoordinates(i, j));
                        Instantiate(background, new Vector3(5 * i / 2, -3 * j / 2, 0),Quaternion.identity, backgroundParent);
                        break;
                }
            }
        }
    }

    private Vector2Int ArrayToMapCoordinates(int x, int y)
    {
        Vector2Int coordinates = new Vector2Int(-15 + 3 * x, 9 - 3 * y);
        return coordinates;
    }

    public void MoveCurrentLocation(Vector2Int currentLocation)
    {
        currentLocationIcon.localPosition = new Vector3(-15 + currentLocation.x * 3, 9 - currentLocation.y * 3, 0);
    }

    private void InvertMap()
    {
        int[,] tempMap = new int[Map.GetLength(1), Map.GetLength(0)];

        for(int i = 0; i < Map.GetLength(0); i++)
        {
            for(int j = 0; j < Map.GetLength(1); j++)
            {
                tempMap[j, i] = Map[i, j];
            }
        }

        Map = tempMap;
    }

    private void SpawnBackgrounds()
    {

    }

    public Vector2Int GetStartingRoom()
    {
        return startingRoom;
    }
}

public struct MapData
{
    public int[,] map;
}