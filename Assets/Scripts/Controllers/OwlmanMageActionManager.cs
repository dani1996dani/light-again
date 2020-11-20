using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Attacks;

public class OwlmanMageActionManager : MonoBehaviour
{
    [SerializeField]
    private OwlmanType owlmanType;
    private OwlmanPatrolMovement patrolController;
    private OwlmanChaseMovement chaseController;
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

        InitOwlmanType();

        patrolController.QueueUpToggleIsPatrolIdle();
    }

    private void FixedUpdate()
    {
        if (!owlmansHealth.getIsAlive())
        {
            return;
        }

        if (!Settings.isGameActive)
        {
            owlmanAnimator.SetFloat("MovementSpeed", 0);
            owlmanAnimator.SetBool("isAttacking", false);
            return;
        }

        Vector3 lastStoredDirection = directionController.GetDirection();
        mirrorController.MirrorGameObjectBasedOnDirection(gameObject, lastStoredDirection);

        float attackRange = Settings.OwlmanSpellAttackRange;
        if (attackController.isPlayerInAttackRange(lastStoredDirection, attackRange))
        {
            attackController.CastSpellTowardsPlayer(lastStoredDirection, transform.position);

            owlmanAnimator.SetFloat("MovementSpeed", 0);
            owlmanAnimator.SetBool("isAttacking", true);
            return;
        }

        owlmanAnimator.SetBool("isAttacking", false);

        if (chaseController.ShouldChase(attackRange))
        {
            Vector3 chaseDirection = directionController.UpdateDirectionBasedOnChase(chaseController, attackRange);
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

    private void InitOwlmanType()
    {
        if (gameObject.tag == Settings.TagOwlman)
        {
            owlmanType = OwlmanType.Regular;
        }
        if (gameObject.tag == Settings.TagOwlmanStrong)
        {
            owlmanType = OwlmanType.Strong;
        }
    }

    public OwlmanType GetOwlmanType()
    {
        return this.owlmanType;
    }
}