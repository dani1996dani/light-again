using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Attacks;

public class OwlmanActionManager : MonoBehaviour
{
    private OwlmanChaseMovement chaseController;
    private OwlmanPatrolMovement patrolController;
    private OwlmanAttack attackController;
    private MovingDirection directionController;

    private void Start()
    {
        chaseController = gameObject.GetComponent<OwlmanChaseMovement>();
        patrolController = gameObject.GetComponent<OwlmanPatrolMovement>();
        attackController = gameObject.GetComponent<OwlmanAttack>();
        directionController = gameObject.GetComponent<MovingDirection>();

        patrolController.QueueUpToggleIsPatrolIdle();
    }

    private void Update()
    {


        if (attackController.isPlayerInAttackRange(directionController.GetDirection()))
        {
            attackController.AttackPlayer();
            return;
        }

        if (chaseController.ShouldChase())
        {
            Vector3 chaseDirection = directionController.UpdateDirectionBasedOnChase(chaseController);
            chaseController.Chase(chaseDirection);
            return;
        }

        Vector3 edgeBasedDirection = directionController.UpdateDirectionBasedOnEdges(patrolController);
        patrolController.Patrol(edgeBasedDirection);
    }
}