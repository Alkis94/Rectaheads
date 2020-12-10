using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Vector2Int border = Vector2Int.zero;
    private Vector2Int currentLocation = Vector2Int.zero;
    private Transform virtualCamera;

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
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>().transform;
    }

    public void OnArrowUp()
    {
        if(currentLocation.y < border.y)
        {
            MoveCamera(new Vector2Int(0, 1));
            arrowDown.interactable = true;

            if (currentLocation.y == border.y)
            {
                arrowUp.interactable = false;
            }
        }
    }

    public void OnArrowDown()
    {
        if (currentLocation.y > 0)
        {
            MoveCamera(new Vector2Int(0, -1));
            arrowUp.interactable = true;

            if (currentLocation.y == 0)
            {
                arrowDown.interactable = false;
            }
        }
    }

    public void OnArrowRight()
    {
        if (currentLocation.x < border.x)
        {
            MoveCamera(new Vector2Int(1, 0));
            arrowLeft.interactable = true;

            if (currentLocation.x == border.x)
            {
                arrowRight.interactable = false;
            }
        }
    }

    public void OnArrowLeft()
    {
        if (currentLocation.x > 0)
        {
            MoveCamera(new Vector2Int(-1, 0));
            arrowRight.interactable = true;

            if (currentLocation.x == 0)
            {
                arrowLeft.interactable = false;
            }
        }
    }

    private void MoveCamera(Vector2Int moveBy)
    {
        virtualCamera.position = new Vector3(virtualCamera.position.x + moveBy.x * 5, virtualCamera.position.y + moveBy.y * 3, -1);
        currentLocation = new Vector2Int(currentLocation.x + moveBy.x , currentLocation.y + moveBy.y);
    }
}
