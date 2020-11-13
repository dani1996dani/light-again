using UnityEngine;
using Assets.Scripts;

//namespace Assets.Scripts.Camera
//{
class CameraController : MonoBehaviour
{
    GameObject player;
    Camera cameraComponent;
    float leftBorder;
    float lowerBorder;
    float rightBorder;
    float upperBorder;
    float screenWidth;
    float screenHeight;
    float depth;

    private void Awake()
    {
        cameraComponent = gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
        depth = player.transform.position.z - gameObject.transform.position.z;
        GetBorders();
        screenWidth = rightBorder - leftBorder;
        screenHeight = upperBorder - lowerBorder;
        Debug.Log("screenWidth " + screenWidth);
        Debug.Log("screenHeight " + screenHeight);
    }

    private void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 cameraPosition = transform.position;
        float horizontalOffset = playerPosition.x - cameraPosition.x;
        float maxAllowedHorizontalOffset = screenWidth * (Settings.CameraHorizontalMaxOffsetPercents / 2) / 100;
        if (horizontalOffset > maxAllowedHorizontalOffset)
        {
            HorizontalAdjust(Vector3.right);
        }
        if (horizontalOffset < -1 * maxAllowedHorizontalOffset)
        {
            HorizontalAdjust(Vector3.left);
        }

        float verticalOffset = playerPosition.y - cameraPosition.y;
        float maxAllowedVerticalOffset = screenHeight * 0.33f;
        Debug.Log("verticalOffset " + verticalOffset);
        Debug.Log("maxAllowedVerticalOffset " + maxAllowedVerticalOffset);
        if (verticalOffset > maxAllowedVerticalOffset)
        {
            VerticalAdjust(Vector3.up);
        }
        if (verticalOffset < -1 * maxAllowedVerticalOffset)
        {
            VerticalAdjust(Vector3.down);
        }


    }

    private void HorizontalAdjust(Vector3 adjustDirection)
    {
        transform.position += adjustDirection * Settings.CameraHorizontalMaxOffsetPercents / 2;
    }

    private void VerticalAdjust(Vector3 adjustDirection)
    {
        transform.position += adjustDirection * screenHeight * 0.33f;
    }

    private void GetBorders()
    {
        leftBorder = cameraComponent.ScreenToWorldPoint(new Vector3(0, 0, depth)).x;
        lowerBorder = cameraComponent.ScreenToWorldPoint(new Vector3(0, 0, depth)).y;
        rightBorder = cameraComponent.ScreenToWorldPoint(new Vector3(Screen.width, 0, depth)).x;
        upperBorder = cameraComponent.ScreenToWorldPoint(new Vector3(0, Screen.height, depth)).y;
        Debug.Log("leftBorder " + leftBorder);
        Debug.Log("lowerBorder " + lowerBorder);
        Debug.Log("rightBorder " + rightBorder);
        Debug.Log("upperBorder " + upperBorder);
    }
}
//}
