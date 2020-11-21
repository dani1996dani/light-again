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
    private Vector3 playersPositionOnStart;

    private void Start()
    {
        random = new System.Random();
        attackController = gameObject.GetComponent<OwlmanAttack>();
        directionController = gameObject.GetComponent<OwlmanMovingDirection>();
        owlmansHealth = gameObject.GetComponent<Health>();
        owlmanAnimator = gameObject.GetComponentInChildren<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag(Settings.TagMainCamera).GetComponent<Camera>();



        playerGameObject = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
        playersPositionOnStart = playerGameObject.transform.position;
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
        yield return new WaitForSeconds(3f);

        bool isHorizontalAttack = random.NextDouble() > 0.5;
        //bool isHorizontalAttack = true;
        //Vector3 playersPosition = playerGameObject.transform.position;
        Vector3 positionToSpawnAttackFrom;
        if (isHorizontalAttack)
        {
            // spawn from X of boss, but from Y of player. So the player will always have to dodge it.
            positionToSpawnAttackFrom = new Vector3(transform.position.x, playersPositionOnStart.y, transform.position.z);
        }
        else
        {
            positionToSpawnAttackFrom = new Vector3(playersPositionOnStart.x, topOfScreen, transform.position.z);
        }

        Vector3 spellDirection = isHorizontalAttack ? Vector3.left : Vector3.down;

        IEnumerator castSpellCoroutine = CastSpell(positionToSpawnAttackFrom, spellDirection);
        StartCoroutine(castSpellCoroutine);
        StartCoroutine("Attack");
    }

    private IEnumerator CastSpell(Vector3 initialPosition, Vector3 spellDirection)
    {
        Vector3[] positionsToSpawnIn;
        if (isHorizontalDirection(spellDirection))
        {
            positionsToSpawnIn = GetHorizontalSpellInitialPositions(initialPosition, 3);
        } else
        {
            positionsToSpawnIn = GetVerticalSpellInitialPositions(initialPosition, 10);
        }

        positionsToSpawnIn = positionsToSpawnIn.OrderBy(x => random.Next()).ToArray();

        foreach (Vector3 currPosition in positionsToSpawnIn)
        {
            GameObject projectile = Instantiate(owlmanProjectilePrefab, currPosition, Quaternion.identity);

            int prefabScale = 10;
            projectile.transform.localScale = new Vector3(prefabScale, prefabScale, 1);

            OwlmanProjectileMovement projectileMovement = projectile.GetComponent<OwlmanProjectileMovement>();
            projectileMovement.SetDirection(spellDirection);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private Vector3[] GetHorizontalSpellInitialPositions(Vector3 realInitialPosition, int amountOfPositions)
    {
        Vector3[] positions = new Vector3[amountOfPositions];
        float verticalOffset = 7f;
        for (int i = 0; i < positions.Length; i++)
        {

            positions[i] = new Vector3(realInitialPosition.x, realInitialPosition.y + (i * verticalOffset), realInitialPosition.z);
        }
        return positions;
    }

    private Vector3[] GetVerticalSpellInitialPositions(Vector3 realInitialPosition, int amountOfPositions)
    {
        Vector3[] positions = new Vector3[amountOfPositions];
        float horizontalOffset = 7f;
        for (int i = 0; i < positions.Length; i++)
        {

            positions[i] = new Vector3(realInitialPosition.x + (i * horizontalOffset), realInitialPosition.y, realInitialPosition.z);
        }
        return positions;
    }

    bool isHorizontalDirection(Vector3 direction)
    {
        return direction.x != 0;
    }

    public OwlmanType GetOwlmanType()
    {
        return this.owlmanType;
    }
}