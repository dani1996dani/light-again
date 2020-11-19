using UnityEngine;
using Assets.Scripts;
using System.Collections;
using UnityEngine.SceneManagement;

class CameraController : MonoBehaviour
{
    GameObject player;
    Camera cameraComponent;

    bool isLevelScene;

    float leftBorder;
    float lowerBorder;
    float rightBorder;
    float upperBorder;

    float screenWidth;
    float screenHeight;
    float depth;

    int horizontalAdjustmentId = 0;
    int verticalAdjustmentId = 0;

    float horizontalDampVelocity = 0f;
    float verticalDampVelocity = 0f;

    float neglegableDampOffset = 0.01f;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitLoad();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void InitLoad()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        isLevelScene = currentScene.name.StartsWith("Level");

        if (!isLevelScene)
        {
            return;
        }

        cameraComponent = gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag(Settings.TagPlayer);

        depth = player.transform.position.z - gameObject.transform.position.z;

        SetBorders();
        screenWidth = rightBorder - leftBorder;
        screenHeight = upperBorder - lowerBorder;
    }

    private void Start()
    {
        InitLoad();
    }

    private void FixedUpdate()
    {
        if (!isLevelScene)
        {
            return;
        }

        Vector3 playerPosition = player.transform.position;
        Vector3 cameraPosition = transform.position;

        CheckHorizontal(playerPosition, cameraPosition);
        CheckVertical(playerPosition, cameraPosition);
    }

    private void CheckHorizontal(Vector3 playerPosition, Vector3 cameraPosition)
    {
        float horizontalOffset = playerPosition.x - cameraPosition.x;
        float maxAllowedHorizontalOffset = screenWidth * Settings.CameraHorizontalMaxOffsetPercents;

        if (horizontalOffset > maxAllowedHorizontalOffset)
        {
            horizontalAdjustmentId++;
            IEnumerator coroutine = HorizontalAdjust(GetTargetPosition(Vector3.right), horizontalAdjustmentId);
            StartCoroutine(coroutine);
        }
        if (horizontalOffset < -1 * maxAllowedHorizontalOffset)
        {
            horizontalAdjustmentId++;
            IEnumerator coroutine = HorizontalAdjust(GetTargetPosition(Vector3.left), horizontalAdjustmentId);
            StartCoroutine(coroutine);
        }
    }

    private void CheckVertical(Vector3 playerPosition, Vector3 cameraPosition)
    {
        float verticalOffset = playerPosition.y - cameraPosition.y;
        float maxAllowedVerticalOffsetUp = screenHeight * Settings.CameraVerticalUpMaxOffsetPercents;

        if (verticalOffset > maxAllowedVerticalOffsetUp)
        {
            verticalAdjustmentId++;
            IEnumerator coroutine = VerticalAdjust(GetTargetPosition(Vector3.up), verticalAdjustmentId);
            StartCoroutine(coroutine);
        }

        float maxAllowedVerticalOffsetDown = screenHeight * Settings.CameraVerticalDownMaxOffsetPercents;
        if (verticalOffset < -1 * maxAllowedVerticalOffsetDown)
        {
            verticalAdjustmentId++;
            IEnumerator coroutine = VerticalAdjust(GetTargetPosition(Vector3.down), verticalAdjustmentId);
            StartCoroutine(coroutine);
        }
    }

    private Vector3 GetTargetPosition(Vector3 direction)
    {
        if (direction.x == 1)
        {
            return transform.position + Vector3.right * screenWidth * Settings.CameraHorizontalMaxOffsetPercents * 2;
        }
        if (direction.x == -1)
        {
            return transform.position + Vector3.left * screenWidth * Settings.CameraHorizontalMaxOffsetPercents * 2;
        }
        if (direction.y == 1)
        {
            return transform.position + Vector3.up * screenHeight * Settings.CameraVerticalUpMaxOffsetPercents;
        }
        return transform.position + Vector3.down * screenHeight * Settings.CameraVerticalDownMaxOffsetPercents;
    }

    IEnumerator HorizontalAdjust(Vector3 targetPosition, int horizontalAdjustmentIdReference)
    {
        yield return new WaitForEndOfFrame();
        float newXPos = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref horizontalDampVelocity, Settings.CameraTimeToAdjust);
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);

        // this ensures that if a newer HorizontalAdjust coroutine was called, do nothing. (otherwise the coroutines cause jittering, cuz they fight over the camera position).
        if (horizontalAdjustmentIdReference == horizontalAdjustmentId)
        {
            if (Mathf.Abs(transform.position.x - targetPosition.x) < neglegableDampOffset)
            {
                transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
            }
            else
            {
                IEnumerator coroutine = HorizontalAdjust(targetPosition, horizontalAdjustmentIdReference);
                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator VerticalAdjust(Vector3 targetPosition, int verticalAdjustmentIdReference)
    {
        yield return new WaitForEndOfFrame();
        float newYPos = Mathf.SmoothDamp(transform.position.y, targetPosition.y, ref verticalDampVelocity, Settings.CameraTimeToAdjust);
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);

        // this ensures that if a newer VerticalAdjust coroutine was called, do nothing. (otherwise the coroutines cause jittering, cuz they fight over the camera position).
        if (verticalAdjustmentIdReference == verticalAdjustmentId)
        {
            if (Mathf.Abs(transform.position.y - targetPosition.y) < neglegableDampOffset)
            {
                transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
            }
            else
            {
                IEnumerator coroutine = VerticalAdjust(targetPosition, verticalAdjustmentIdReference);
                StartCoroutine(coroutine);
            }
        }
    }

    private void SetBorders()
    {
        leftBorder = cameraComponent.ScreenToWorldPoint(new Vector3(0, 0, depth)).x;
        lowerBorder = cameraComponent.ScreenToWorldPoint(new Vector3(0, 0, depth)).y;
        rightBorder = cameraComponent.ScreenToWorldPoint(new Vector3(Screen.width, 0, depth)).x;
        upperBorder = cameraComponent.ScreenToWorldPoint(new Vector3(0, Screen.height, depth)).y;
    }
}
