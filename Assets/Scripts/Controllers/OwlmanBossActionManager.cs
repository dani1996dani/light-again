using System.Collections;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Attacks;
using Assets.Scripts.Movement;

public class OwlmanBossActionManager : MonoBehaviour
{
    [SerializeField]
    private OwlmanType owlmanType;
    private OwlmanAttack attackController;
    private OwlmanMovingDirection directionController;
    private readonly MirrorCharacter mirrorController = new MirrorCharacter();
    private Health owlmansHealth;
    private Animator owlmanAnimator;
    private GameObject owlmanProjectilePrefab;
    private GameObject playerGameObject;
    private System.Random random;
    private Camera mainCamera;
    private float topOfScreen;

    private void Start()
    {
        random = new System.Random();
        attackController = gameObject.GetComponent<OwlmanAttack>();
        directionController = gameObject.GetComponent<OwlmanMovingDirection>();
        owlmansHealth = gameObject.GetComponent<Health>();
        owlmanAnimator = gameObject.GetComponentInChildren<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag(Settings.TagMainCamera).GetComponent<Camera>();



        playerGameObject = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
        float depth = playerGameObject.transform.position.z - mainCamera.gameObject.transform.position.z;
        topOfScreen = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, depth)).y;

        owlmanProjectilePrefab = (GameObject)Resources.Load("Prefabs/OwlmanSpellProjectile");

        owlmanType = OwlmanType.Boss;

        StartCoroutine("Attack");
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



        //Vector3 lastStoredDirection = directionController.GetDirection();
        //mirrorController.MirrorGameObjectBasedOnDirection(gameObject, lastStoredDirection);

        //float attackRange = Settings.OwlmanMeleeAttackRange;
        //if (attackController.isPlayerInAttackRange(lastStoredDirection, attackRange))
        //{
        //    attackController.AttackPlayer(lastStoredDirection, attackRange);

        //    owlmanAnimator.SetFloat("MovementSpeed", 0);
        //    owlmanAnimator.SetBool("isAttacking", true);
        //    return;
        //}

        //owlmanAnimator.SetBool("isAttacking", false);
        //float visionRange = Settings.OwlmanStartChasingVisionRange;
        //if (chaseController.ShouldChase(visionRange))
        //{
        //    Vector3 chaseDirection = directionController.UpdateDirectionBasedOnChase(chaseController, visionRange);

        //    chaseController.Chase(chaseDirection, chaseSpeed);
        //    owlmanAnimator.SetFloat("MovementSpeed", 1);
        //    return;
        //}

        //Vector3 edgeBasedDirection = directionController.UpdateDirectionBasedOnEdges(patrolController);
        //bool isCurrentlyMoving = patrolController.Patrol(edgeBasedDirection);
        //if (isCurrentlyMoving)
        //{
        //    owlmanAnimator.SetFloat("MovementSpeed", 1);
        //} else
        //{
        //    owlmanAnimator.SetFloat("MovementSpeed", 0);
        //}
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);

        bool isHorizontalAttack = random.NextDouble() > 0.5;
        Vector3 playersPosition = playerGameObject.transform.position;
        Vector3 positionToSpawnAttackFrom;
        if (isHorizontalAttack)
        {
            // spawn from X of boss, but from Y of player. So the player will always have to dodge it.
            positionToSpawnAttackFrom = new Vector3(transform.position.x, playersPosition.y, transform.position.z);
        }
        else
        {
            positionToSpawnAttackFrom = new Vector3(playersPosition.x, topOfScreen, transform.position.z);
        }

        Vector3 spellDirection = isHorizontalAttack ? Vector3.left : Vector3.down;

        CastSpell(positionToSpawnAttackFrom, spellDirection);
        StartCoroutine("Attack");
    }

    private void CastSpell(Vector3 initialPosition, Vector3 spellDirection)
    {
        GameObject projectile = Instantiate(owlmanProjectilePrefab, initialPosition, Quaternion.identity);

        int prefabScale = 10;
        projectile.transform.localScale = new Vector3(prefabScale, prefabScale, 1);

        OwlmanProjectileMovement projectileMovement = projectile.GetComponent<OwlmanProjectileMovement>();
        projectileMovement.SetDirection(spellDirection);
    }

    public OwlmanType GetOwlmanType()
    {
        return this.owlmanType;
    }
}