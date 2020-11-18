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
    private OwlmanMovingDirection directionController;
    private readonly MirrorCharacter mirrorController = new MirrorCharacter();
    private Health owlmansHealth;

    private void Start()
    {
        chaseController = gameObject.GetComponent<OwlmanChaseMovement>();
        patrolController = gameObject.GetComponent<OwlmanPatrolMovement>();
        attackController = gameObject.GetComponent<OwlmanAttack>();
        directionController = gameObject.GetComponent<OwlmanMovingDirection>();
        owlmansHealth = gameObject.GetComponent<Health>();

        patrolController.QueueUpToggleIsPatrolIdle();
    }

    private void FixedUpdate()
    {
        if (!owlmansHealth.getIsAlive())
        {
            return;
        }

        Vector3 lastStoredDirection = directionController.GetDirection();
        mirrorController.MirrorGameObjectBasedOnDirection(gameObject, lastStoredDirection);

        if (attackController.isPlayerInAttackRange(lastStoredDirection))
        {
            attackController.AttackPlayer(lastStoredDirection);
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