using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Attacks;

public class OwlmanActionManager : MonoBehaviour
{
    [SerializeField]
    private OwlmanType owlmanType;
    private OwlmanChaseMovement chaseController;
    private OwlmanPatrolMovement patrolController;
    private OwlmanAttack attackController;
    private OwlmanMovingDirection directionController;
    private readonly MirrorCharacter mirrorController = new MirrorCharacter();
    private Health owlmansHealth;
    private Animator owlmanAnimator;

    private void Start()
    {
        chaseController = gameObject.GetComponent<OwlmanChaseMovement>();
        patrolController = gameObject.GetComponent<OwlmanPatrolMovement>();
        attackController = gameObject.GetComponent<OwlmanAttack>();
        directionController = gameObject.GetComponent<OwlmanMovingDirection>();
        owlmansHealth = gameObject.GetComponent<Health>();
        owlmanAnimator = gameObject.GetComponentInChildren<Animator>();

        if(gameObject.tag == Settings.TagOwlman)
        {
            owlmanType = OwlmanType.Regular;
        }
        if (gameObject.tag == Settings.TagOwlmanStrong)
        {
            owlmanType = OwlmanType.Strong;
        }

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

            owlmanAnimator.SetFloat("MovementSpeed", 0);
            owlmanAnimator.SetBool("isAttacking", true);
            return;
        }

        owlmanAnimator.SetBool("isAttacking", false);
        if (chaseController.ShouldChase())
        {
            Vector3 chaseDirection = directionController.UpdateDirectionBasedOnChase(chaseController);
            chaseController.Chase(chaseDirection);
            owlmanAnimator.SetFloat("MovementSpeed", 1);
            return;
        }

        Vector3 edgeBasedDirection = directionController.UpdateDirectionBasedOnEdges(patrolController);
        bool isCurrentlyMoving = patrolController.Patrol(edgeBasedDirection);
        if (isCurrentlyMoving)
        {
            owlmanAnimator.SetFloat("MovementSpeed", 1);
        } else
        {
            owlmanAnimator.SetFloat("MovementSpeed", 0);
        }
    }

    public OwlmanType GetOwlmanType()
    {
        return this.owlmanType;
    }
}