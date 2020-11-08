using Assets.Scripts;
using Assets.Scripts.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OwlmanPatrolMovement : MonoBehaviour
{
    List<GameObject> allChildren;
    GameObject leftEdgeDetectionObject;
    GameObject rightEdgeDetectionObject;
    Vector3 patrolDirection = Vector3.right;
    private void Awake()
    {
        allChildren = gameObject.GetAllChildren();
        leftEdgeDetectionObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanLeftEdgeDetection);
        rightEdgeDetectionObject = allChildren.FirstOrDefault(child => child.tag == Settings.TagOwlmanRightEdgeDetection);
    }

    private void Update()
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
        return !Physics2D.Raycast(leftEdgeDetectionObject.transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }

    private bool isAtRightEdge()
    {
        return !Physics2D.Raycast(rightEdgeDetectionObject.transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }
}