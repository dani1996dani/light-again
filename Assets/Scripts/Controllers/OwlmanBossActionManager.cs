using System.Collections;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Attacks;
using Assets.Scripts.Movement;

enum BossAttack
{
    SpellHorizontal,
    SpellVertical,
    GroundSmash
}

public class OwlmanBossActionManager : MonoBehaviour
{
    [SerializeField]
    private OwlmanType owlmanType;
    private Health owlmansHealth;
    private Animator owlmanAnimator;
    private GameObject owlmanProjectilePrefab;
    private GameObject owlmanGroundSmashReverbPrefab;
    private GameObject playerGameObject;
    private System.Random random;
    private Camera mainCamera;
    private float topOfScreen;
    private Vector3 playersPositionOnStart;
    private bool canAttack = true;
    private BossAttack[] availableAttackTypes = new BossAttack[3] { BossAttack.SpellHorizontal, BossAttack.SpellVertical, BossAttack.GroundSmash };
    AnimationClip groundSmashClip;

    private void Start()
    {
        random = new System.Random();
        owlmansHealth = gameObject.GetComponent<Health>();
        owlmanAnimator = gameObject.GetComponentInChildren<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag(Settings.TagMainCamera).GetComponent<Camera>();

        playerGameObject = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
        playersPositionOnStart = playerGameObject.transform.position;
        float depth = playerGameObject.transform.position.z - mainCamera.gameObject.transform.position.z;
        topOfScreen = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, depth)).y;

        owlmanProjectilePrefab = (GameObject)Resources.Load("Prefabs/OwlmanSpellProjectile");
        owlmanGroundSmashReverbPrefab = (GameObject)Resources.Load("Prefabs/GroundSmashReverb");

        owlmanType = OwlmanType.Boss;

        groundSmashClip = owlmanAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "GroundSmash");
    }

    private void FixedUpdate()
    {
        if (!owlmansHealth.getIsAlive())
        {
            return;
        }

        if (!Settings.isGameActive)
        {
            owlmanAnimator.SetBool("isCastingSpell", false);
            owlmanAnimator.SetBool("isCastingGroundSmash", false);
            return;
        }

        if (canAttack)
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        int randomAttackIndex = random.Next(0, availableAttackTypes.Length);
        BossAttack selectedType = availableAttackTypes[randomAttackIndex];

        Vector3 positionToSpawnAttackFrom;
        if (selectedType == BossAttack.SpellHorizontal || selectedType == BossAttack.SpellVertical)
        {
            if (selectedType == BossAttack.SpellHorizontal)
            {
                // spawn from X of boss, but from Y of player. So the player will always have to dodge it.
                positionToSpawnAttackFrom = new Vector3(transform.position.x, playersPositionOnStart.y, transform.position.z);
            }
            else
            {
                positionToSpawnAttackFrom = new Vector3(playersPositionOnStart.x, topOfScreen, transform.position.z);
            }
            Vector3 spellDirection = selectedType == BossAttack.SpellVertical ? Vector3.down : Vector3.left;

            IEnumerator castSpellCoroutine = CastSpell(positionToSpawnAttackFrom, spellDirection);
            StartCoroutine(castSpellCoroutine);
        }

        else if (selectedType == BossAttack.GroundSmash)
        {
            GameObject groundSmashOriginPoint = GameObject.FindGameObjectWithTag(Settings.TagGroundSmashOriginPoint);
            IEnumerator castGroundSmashCoroutine = CastGroundSmash(groundSmashOriginPoint.transform.position, Vector3.left);
            StartCoroutine(castGroundSmashCoroutine);
        }

        yield return new WaitForSeconds(Settings.OwlmanBossTimeToWaitBetweenAttacks);
        canAttack = true;
    }

    private IEnumerator CastSpell(Vector3 initialPosition, Vector3 spellDirection)
    {
        owlmanAnimator.SetBool("isCastingSpell", true);
        Vector3[] positionsToSpawnIn;
        if (isHorizontalDirection(spellDirection))
        {
            positionsToSpawnIn = GetHorizontalSpellInitialPositions(initialPosition, 3);
        }
        else
        {
            positionsToSpawnIn = GetVerticalSpellInitialPositions(initialPosition, 10);
        }

        positionsToSpawnIn = positionsToSpawnIn.OrderBy(x => random.Next()).ToArray();

        foreach (Vector3 currPosition in positionsToSpawnIn)
        {
            GameObject projectile = Instantiate(owlmanProjectilePrefab, currPosition, Quaternion.identity);

            int prefabScale = 2;
            float scaleDirection = spellDirection.x != 0 ? -1 : 1;
            projectile.transform.localScale = new Vector3(prefabScale * scaleDirection, prefabScale, 1);
            float angle = spellDirection.x != 0 ? 180 : 90;
            projectile.transform.Rotate(0,0, angle);

            OwlmanProjectileMovement projectileMovement = projectile.GetComponent<OwlmanProjectileMovement>();
            projectileMovement.SetDirection(spellDirection);
            yield return new WaitForSeconds(0.25f);
        }
        owlmanAnimator.SetBool("isCastingSpell", false);
    }

    private IEnumerator CastGroundSmash(Vector3 initialPosition, Vector3 castDirection)
    {
        owlmanAnimator.SetBool("isCastingGroundSmash", true);
        yield return new WaitForSeconds(groundSmashClip.length);

        GameObject projectile = Instantiate(owlmanGroundSmashReverbPrefab, initialPosition, Quaternion.identity);
        OwlmanProjectileMovement projectileMovement = projectile.GetComponent<OwlmanProjectileMovement>();
        projectileMovement.SetDirection(castDirection);
        owlmanAnimator.SetBool("isCastingGroundSmash", false);
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