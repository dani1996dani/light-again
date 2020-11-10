using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;

public class MovingDirection : MonoBehaviour
{
    Vector3 direction = Vector3.right;

    private void Awake()
    {
    }

    public Vector3 GetDirection()
    {
        return new Vector3(this.direction.x, this.direction.y, this.direction.z);
    }

    public Vector3 SetDirection(Vector3 newDirection)
    {
        this.direction = new Vector3(newDirection.x, newDirection.y, newDirection.z);
        return this.direction;
    }

    public Vector3 UpdateDirectionBasedOnChase(OwlmanChaseMovement chaseController)
    {
        if (chaseController.isPlayerVisibleOnLeftSide())
        {
            SetDirection(Vector3.left);
        }
        if (chaseController.isPlayerVisibleOnRightSide())
        {
            SetDirection(Vector3.right);
        }
        return this.direction;
    }

    public Vector3 UpdateDirectionBasedOnEdges(OwlmanPatrolMovement patrolController)
    {
        if (patrolController.isAtLeftEdge())
        {
            SetDirection(Vector3.right);
        }
        if (patrolController.isAtRightEdge())
        {
            SetDirection(Vector3.left);
        }
        return this.direction;
    }
}