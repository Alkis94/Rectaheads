using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private Vector2Int currentMapLocation = Vector2Int.zero;
    private GameObject virtualCamera;
    private int[,] map;

    [SerializeField]
    private Button arrowUp;
    [SerializeField]
    private Button arrowDown;
    [SerializeField]
    private Button arrowLeft;
    [SerializeField]
    private Button arrowRight;

    private void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
        map = MapManager.Instance.Map;
        Vector2Int startPosition = MapManager.Instance.GetStartingRoom();
        currentMapLocation = startPosition;
        virtualCamera.transform.position = new Vector3(0.5f + startPosition.x * 5 / 2, startPosition.y * (-3) / 2, -1);
        EnableCorrectArrows();
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlayMouseOverSound();
    }

    public void OnArrowDown()
    {
        AudioManager.Instance.PlayButtonClickSound();
        MoveCameraY(1);
        EnableCorrectArrows();
    }

    public void OnArrowUp()
    {
        AudioManager.Instance.PlayButtonClickSound();
        MoveCameraY(-1);
        EnableCorrectArrows();
    }

    public void OnArrowRight()
    {
        AudioManager.Instance.PlayButtonClickSound();
        MoveCameraX(1);
        EnableCorrectArrows();
    }

    public void OnArrowLeft()
    {
        AudioManager.Instance.PlayButtonClickSound();
        MoveCameraX(-1);
        EnableCorrectArrows();
    }

    private void MoveCameraX(int x)
    {
        currentMapLocation = new Vector2Int(currentMapLocation.x + x * 2, currentMapLocation.y);
        LeanTween.moveX(virtualCamera, 0.5f + currentMapLocation.x * 5 / 2, 0.5f);
        MapManager.Instance.MoveCurrentLocation(currentMapLocation);
    }

    private void MoveCameraY(int y)
    {
        currentMapLocation = new Vector2Int(currentMapLocation.x, currentMapLocation.y + y * 2);
        LeanTween.moveY(virtualCamera, 0 - (currentMapLocation.y * 3 / 2), 0.5f);
        MapManager.Instance.MoveCurrentLocation(currentMapLocation);
    }

    private void EnableCorrectArrows()
    {
        if (currentMapLocation.y + 1 < map.GetLength(1))
        {
            if (map[currentMapLocation.x, currentMapLocation.y + 1] == 1)
            {
                arrowDown.interactable = true;
            }
            else
            {
                arrowDown.interactable = false;
            }
        }
        else
        {
            arrowDown.interactable = false;
        }

        if (currentMapLocation.y - 1 > 0)
        {
            if (map[currentMapLocation.x, currentMapLocation.y - 1] == 1)
            {
                arrowUp.interactable = true;
            }
            else
            {
                arrowUp.interactable = false;
            }
        }
        else
        {
            arrowUp.interactable = false;
        }

        if (currentMapLocation.x + 1 < map.GetLength(0))
        {
            if (map[currentMapLocation.x + 1, currentMapLocation.y] == 1)
            {
                arrowRight.interactable = true;
            }
            else
            {
                arrowRight.interactable = false;
            }
        }
        else
        {
            arrowRight.interactable = false;
        }

        if (currentMapLocation.x - 1 > 0)
        {
            if (map[currentMapLocation.x - 1, currentMapLocation.y] == 1)
            {
                arrowLeft.interactable = true;
            }
            else
            {
                arrowLeft.interactable = false;
            }
        }
        else
        {
            arrowLeft.interactable = false;
        }
    }
}
