using Assets.Scripts;
using Assets.Scripts.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class OwlmanPatrolMovement : MonoBehaviour
{
    List<GameObject> allChildren;
    GameObject leftEdgeDetectionObject;
    GameObject rightEdgeDetectionObject;
    GameObject rightHandGameObject;
    GameObject leftHandGameObject;
    Vector3 patrolDirection = Vector3.right;
    System.Random random = new System.Random();
    private bool isPatrolIdle = false;
    private void Awake()
    {
        allChildren = gameObject.GetAllChildren();
        leftEdgeDetectionObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftEdgeDetection);
        rightEdgeDetectionObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightEdgeDetection);
        rightHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightHand);
        leftHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftHand);
    }

    private void Start()
    {
QueueUpToggleIsPatrolIdle();
    }

    private void Update()
    {
        if (!isPatrolIdle)
        {
            Move();
        }
    }

    private void QueueUpToggleIsPatrolIdle()
    {
        int minRange = 2;
        int maxRange = 5;
        float randomFloat = (float)(random.NextDouble() * maxRange + minRange);
        IEnumerator ToggleIsPatrolIdleCoroutine = ToggleIsPatrolIdle(randomFloat);
        StartCoroutine(ToggleIsPatrolIdleCoroutine);
    }

    private IEnumerator ToggleIsPatrolIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isPatrolIdle = !isPatrolIdle;
        QueueUpToggleIsPatrolIdle();
    }

    private void Move()
    {
        if (isAtLeftEdge())
        {
            patrolDirection = Vector3.right;
        }
        if (isAtRightEdge())
        {
            patrolDirection = Vector3.left;
        }

        transform.position += patrolDirection * Settings.OwlmanSpeed * Time.deltaTime;
    }

    private bool isAtLeftEdge()
    {
        bool isAtBottomLeftEdge = !Physics2D.Raycast(leftEdgeDetectionObject.transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        bool isAtFaceHegihtLeftEdge = Physics2D.Raycast(leftHandGameObject.transform.position, Vector2.left, 0.1f, LayerMask.GetMask("Ground"));
        return isAtBottomLeftEdge || isAtFaceHegihtLeftEdge;
    }

    private bool isAtRightEdge()
    {
        bool isAtBottomRightEdge = !Physics2D.Raycast(rightEdgeDetectionObject.transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        bool isAtFaceHegihtRightEdge = Physics2D.Raycast(rightHandGameObject.transform.position, Vector2.right, 0.1f, LayerMask.GetMask("Ground"));
        return isAtBottomRightEdge || isAtFaceHegihtRightEdge;
    }
}