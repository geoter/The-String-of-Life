using UnityEngine;
public class TouchDrag : MonoBehaviour
{
    [SerializeField] private Vector2 initialCardPos;
    [SerializeField] private float attractorSpeed;
    private Camera mainCamera;
    private Vector2 touchPosWorld2D;
    private Vector2? latestPosition = null;
    private bool isMoving = false;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPositionOnScreen = Input.mousePosition;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPositionOnScreen);
            this.touchPosWorld2D = new Vector2(worldPosition.x, worldPosition.y);
            isMoving = true;
        }
        else
        {
            isMoving = false;
            latestPosition = null;
            transform.position = Vector2.MoveTowards(transform.position, initialCardPos, attractorSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            HandleTouch();
        }
    }

    private void HandleTouch()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.touchPosWorld2D, mainCamera.transform.forward * 1000);
        if (hit.transform != null && hit.transform.gameObject == this.gameObject)
        {
            Vector3 currentPosition = transform.position;
            float nexXPosition = hit.point.x;
            float nexYPosition = hit.point.y;
            if (latestPosition == null)
            {
                latestPosition = hit.point;
            }
            float dx = nexXPosition - latestPosition.Value.x;
            float dy = nexYPosition - latestPosition.Value.y;
            transform.position = new Vector3(currentPosition.x + dx, currentPosition.y + dy, transform.position.z);
            latestPosition = hit.point;
        }
    }
}
