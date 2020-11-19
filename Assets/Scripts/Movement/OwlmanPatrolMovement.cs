using Assets.Scripts;
using Assets.Scripts.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using System;

public class OwlmanPatrolMovement : MonoBehaviour
{
    List<GameObject> allChildren;
    GameObject leftEdgeDetectionObject;
    GameObject rightEdgeDetectionObject;
    GameObject rightHandGameObject;
    GameObject leftHandGameObject;
    System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
    private bool isPatrolIdle;
    Vector3 edgeOffset = new Vector3(Settings.PatrolEdgeOffset, 0, 0);

    private void Awake()
    {
        isPatrolIdle = random.NextDouble() > 0.5 ? true : false;
        allChildren = gameObject.GetAllChildren();
        leftEdgeDetectionObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftEdgeDetection);
        rightEdgeDetectionObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightEdgeDetection);
        rightHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightHand);
        leftHandGameObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftHand);
    }

    /**
     * returns if owlman is currently moving or not
     */
    public bool Patrol(Vector3 direction)
    {
        if (!isPatrolIdle)
        {
            Move(direction);
            return true;
        }
        return false;
    }

    public void QueueUpToggleIsPatrolIdle()
    {
        int minRange = 2;
        int maxRange = 7;
        float randomFloat = (float)(random.NextDouble() * (maxRange - minRange) + minRange);
        IEnumerator ToggleIsPatrolIdleCoroutine = ToggleIsPatrolIdle(randomFloat);
        StartCoroutine(ToggleIsPatrolIdleCoroutine);
    }

    private IEnumerator ToggleIsPatrolIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isPatrolIdle = !isPatrolIdle;
        QueueUpToggleIsPatrolIdle();
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * Settings.OwlmanSpeed * Time.deltaTime;
    }

    public bool isAtLeftEdge()
    {
        Vector3 bottomOriginPoint = leftEdgeDetectionObject.transform.position - edgeOffset;
        Vector3 faceHeightOriginPoint = leftHandGameObject.transform.position - edgeOffset;
        bool isAtBottomLeftEdge = !Physics2D.Raycast(bottomOriginPoint, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        bool isAtFaceHegihtLeftEdge = Physics2D.Raycast(faceHeightOriginPoint, Vector2.left, 0.5f, LayerMask.GetMask("Ground"));
        return isAtBottomLeftEdge || isAtFaceHegihtLeftEdge;
    }

    public bool isAtRightEdge()
    {
        Vector3 bottomOriginPoint = leftEdgeDetectionObject.transform.position + edgeOffset;
        Vector3 faceHeightOriginPoint = rightHandGameObject.transform.position + edgeOffset;
        bool isAtBottomRightEdge = !Physics2D.Raycast(bottomOriginPoint, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        bool isAtFaceHegihtRightEdge = Physics2D.Raycast(faceHeightOriginPoint, Vector2.right, 0.5f, LayerMask.GetMask("Ground"));
        return isAtBottomRightEdge || isAtFaceHegihtRightEdge;
    }
}